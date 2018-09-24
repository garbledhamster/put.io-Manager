using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Diagnostics;

namespace putio
{
    public partial class FormPutioManager : Form
    {
        public FormPutioManager()
        {
            InitializeComponent();
            InitializeControls();
            InitializeAsync();
        }

        private void InitializeControls()
        {
            OAuthToken = Properties.Settings.Default.OAuthToken;
            filemgr = new PutioManager(OAuthToken);
            treeViewPutioFiles.Nodes.Clear();
            rootnode = treeViewPutioFiles.Nodes.Add("put.io files");
            rootnode.Tag = "root";
            treeViewPutioFiles.ShowNodeToolTips = Properties.Settings.Default.ShowToolTips;
        }

        private void InitializeAsync()
        {
            for (int i = 0; i < ParallelDownloads; i++)
            {
                WebClient client = new WebClient();
                client.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                client.DownloadFileCompleted += WebClient_DownloadDataCompleted;
                WebClients.Add(client);
            }
            treeViewPutioFiles.NodeMouseClick += (sender, args) => treeViewPutioFiles.SelectedNode = args.Node;
        }

        public string OAuthToken;
        public string OAuthParamater;
        public string DownloadPath;
        public TreeNode rootnode;
        PutioManager filemgr;
        int ParallelDownloads = Properties.Settings.Default.ParallelDownloads;

        private const string urlPutioApi = "https://api.put.io/v2/";
        List<WebClient> WebClients = new List<WebClient>();
        Queue<FileDownload> FileDownloads = new Queue<FileDownload>();

        // Initalizers

        private void CstripDownload_Click(object sender, EventArgs e, FileDownload file)
        {
            if (file.PrimaryWebClient.IsBusy)
            {
                Console.WriteLine("download canceled");
                file.PrimaryWebClient.CancelAsync();
            }
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var FileDownload = e.UserState as FileDownload;
            DataGridViewRow FileQueRow = FileDownload.RowInQue;
            
            string bytesrecieved = ((e.BytesReceived / 1024d) / 1024d).ToString("0.0");
            string totalbytes = ((e.TotalBytesToReceive / 1024d) / 1024d).ToString("0.0");
            UpdateCellValue(FileQueRow, "ColumnStatus", string.Format("{0} Mb / {1} Mb", bytesrecieved, totalbytes));
        }

        private void WebClient_DownloadDataCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var FileDownload = e.UserState as FileDownload;
            DataGridViewRow FileQueRow = FileDownload.RowInQue;

            if (e.Cancelled == true)
                UpdateCellValue(FileQueRow, "ColumnStatus", "Canceled");
            else if (e.Error != null)
                UpdateCellValue(FileQueRow, "ColumnStatus", "Error");
            else
                UpdateCellValue(FileQueRow, "ColumnStatus", "Complete");

            UpdateCellValue(FileQueRow, "ColumnCompleted", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            DownloadFile();
        }

        // Form Controls

        private async void FormPutioManager_Load(object sender, EventArgs e)
        {
            try
            {
                UpdateTreeView(await filemgr.List("0"));
            }
            catch 
            {
                MessageBox.Show("the token provided did not work");
            }
        }

        private void FormPutioManager_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private async void treeViewPutioFiles_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            treeViewPutioFiles.SelectedNode = e.Node;
            if (treeViewPutioFiles.SelectedNode != rootnode)
            {
                JArray files = await filemgr.List(treeViewPutioFiles.SelectedNode.Tag.ToString());
                treeViewPutioFiles.SelectedNode.Nodes.Clear();
                UpdateTreeView(files);
            }
        }

        private async void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectednode = treeViewPutioFiles.SelectedNode;
            string id = selectednode.Tag.ToString();
            var deletemessage = await filemgr.Delete(id);

            string status = JObject.Parse(deletemessage)["status"].ToString();

            if (status == "OK")
            {
                treeViewPutioFiles.Nodes.Remove(selectednode);
                UpdateStatusText(string.Format("'{0}' was successfully deleted from put.io", selectednode.Text));
            }
        }

        private async void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectednode = treeViewPutioFiles.SelectedNode;
            string oldname = selectednode.Text;
            using (var rename = new FormPutioRename())
            {
                rename.textBoxNewName.Text = selectednode.Text;
                rename.ShowDialog();
                try
                {
                    if (rename.NewName.Length > 0 & rename.NewName != oldname)
                    {
                        string id = selectednode.Tag.ToString();
                        var rename_response = await filemgr.Rename(id, rename.NewName);

                        string status = JObject.Parse(rename_response)["status"].ToString();

                        if (status == "OK")
                        {
                            selectednode.Text = rename.NewName;
                            UpdateStatusText(string.Format("'{0}' was successfully renamed to '{1}'", oldname, selectednode.Text));
                        }
                    }
                }
                catch
                {
                    UpdateStatusText(string.Format("'{0}' name was not modified", oldname));
                }
               
                
            }
        }

        private async void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string id = treeViewPutioFiles.SelectedNode.Tag.ToString();
            JObject fileProperties = await filemgr.Get(id);
            string filename = fileProperties["name"].ToString();
            string filetype = fileProperties["file_type"].ToString();
            string started = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            string status = "Not Started";
            Console.WriteLine(filename + " started downloading at " + started);
            string path = Properties.Settings.Default.DownloadDirectory + @"\" + filename;
            Uri uriDownloadFile = new Uri(urlPutioApi + "files/" + id + "/download?oauth_token=" + OAuthToken);
            dataGridView1.Rows.Add(filename, filetype, started, "", status);
            DataGridViewRow newDownloadRow = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
            newDownloadRow.Tag = uriDownloadFile;

            FileDownload dlfile = new FileDownload(uriDownloadFile, path, newDownloadRow);
            dlfile.FileDownloaded = false;
            dlfile.RowInQue.Tag = dlfile;

            FileDownloads.Enqueue(dlfile);
            Console.WriteLine("Queue Lengths = " + FileDownloads.Count);
            DownloadFile();

        }

        private async void zipDownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectednode = treeViewPutioFiles.SelectedNode;
            string id = selectednode.Tag.ToString();
            var createzip_response = await filemgr.CreateZip(id);
            string zipid = JObject.Parse(createzip_response)["zip_id"].ToString();
            string status = "Not Started";
            string zipstatus = "";
            //Console.WriteLine("ZipId=" + zipid);

            Uri uriZipFile = null;

            do
            {
                var zip_properties = await filemgr.GetZip(zipid);
                //Console.WriteLine(zip_properties);
                zipstatus = zip_properties["zip_status"].ToString();
                //Console.WriteLine(zipstatus);
                if (zipstatus == "DONE")
                {
                    uriZipFile= new Uri(zip_properties["url"].ToString());
                }

            } while (zipstatus != "DONE");

            string filename = selectednode.Text + ".zip";
            string filetype = "ZIP";
            string started = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            string path = Properties.Settings.Default.DownloadDirectory + @"\" + filename;
            dataGridView1.Rows.Add(filename, filetype, started, "", status);
            DataGridViewRow newDownloadRow = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
            
            FileDownload dlfile = new FileDownload(uriZipFile, path, newDownloadRow);
            dlfile.FileDownloaded = false;
            dlfile.RowInQue.Tag = dlfile;

            FileDownloads.Enqueue(dlfile);
            Console.WriteLine("Queue Lengths = " + FileDownloads.Count);
            DownloadFile();
        }

        private async void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            TreeNode selectednode = treeViewPutioFiles.SelectedNode;
            string id = selectednode.Tag.ToString();

            string message = null;

            var response = await filemgr.Get(id);

            foreach (var property in response)
            {
                message += property.Key + ": " + property.Value + Environment.NewLine;
            }

            selectednode.ToolTipText = message;

            var form_properties = new FormPutioProperties();
            form_properties.StartPosition = FormStartPosition.CenterParent;
            form_properties.labelProperties.Text = message;
            form_properties.ShowDialog();

        }

        private async void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Settings = new FormPutioSettings();
            Settings.StartPosition = FormStartPosition.CenterParent;
            Settings.ShowDialog();

            InitializeControls();
            treeViewPutioFiles.SelectedNode = treeViewPutioFiles.Nodes[0];
            UpdateTreeView(await filemgr.List("0"));
            filemgr = new PutioManager(OAuthToken);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Methods and Modules

        private void UpdateTreeView(JArray inJArrFiles)
        {
            TreeNode selectedNode = treeViewPutioFiles.SelectedNode;
            foreach (JObject file in inJArrFiles)
            {
                string message = null;
                TreeNode newnode = selectedNode.Nodes.Add(file["name"].ToString());
                newnode.Tag = file["id"].ToString();

                // set tooltip node text to the files properties
                foreach (var property in file)
                {
                    message += property.Key.ToString().ToLower() + ": " + property.Value.ToString().ToLower() + Environment.NewLine;
                }

                newnode.ToolTipText = message;
                if (file["file_type"].ToString() != "FOLDER")
                {
                    newnode.SelectedImageIndex = 2;
                    newnode.ImageIndex = 2;
                }
                else
                {
                    newnode.Nodes.Add("Loading...");
                }

            }
        }

        private void UpdateCellValue(DataGridViewRow inDgvrParentOfCell, string inStrColumnName, string inStrValue)
        {
            inDgvrParentOfCell.Cells[inStrColumnName].Value = inStrValue;
        }

        private void UpdateStatusText(string inStrStatusText)
        {
            toolStripStatusLabel1.Text = inStrStatusText;
        }

        private void DownloadFile()
        {
            if (FileDownloads.Any())
            {
                foreach (WebClient wc in WebClients)
                {
                    if (!wc.IsBusy)
                    {
                        var nextItem = FileDownloads.Dequeue();
                        nextItem.RowInQue.Cells["ColumnStatus"].Value = "Queued";
                        Console.WriteLine("started looking at file downloads");
                        Console.WriteLine("looking for a open webclient");
                        Console.WriteLine("open webclient found");
                        nextItem.PrimaryWebClient = wc;

                        ContextMenuStrip cstrip = new ContextMenuStrip();
                        cstrip.Items.Add("Cancel").Click += (s, e1) => CstripDownload_Click(s, e1, nextItem);
                        nextItem.RowInQue.ContextMenuStrip = cstrip;

                        wc.DownloadFileAsync(nextItem.DownloadLink, nextItem.FilePath, nextItem);
                        return;
                    }
                }
            }
            return;
        }

        // test button!

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // internal classes

        internal class FileDownload
        {
            public readonly Uri DownloadLink;
            public readonly string FilePath;
            public readonly DataGridViewRow RowInQue;
            public bool FileDownloaded;
            public WebClient PrimaryWebClient;
            public FileDownload(Uri inUriDownloadLink, string inStrFilePath, DataGridViewRow inDgvrRowInQue)
            {
                DownloadLink = inUriDownloadLink;
                FilePath = inStrFilePath;
                RowInQue = inDgvrRowInQue;
            }
        }

    }
}
