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
            treeViewPutioFiles.SelectedNode = treeViewPutioFiles.Nodes[0];
            filemgr = new PutioManager(OAuthToken);
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
        Queue<PutioFile> FileDownloads = new Queue<PutioFile>();

        // Initalizers

        private void CstripDownload_Click(object sender, EventArgs e, PutioFile file)
        {
            if (file.PrimaryWebClient.IsBusy)
            {
                Console.WriteLine("download canceled");
                file.PrimaryWebClient.CancelAsync();
            }
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var FileDownload = e.UserState as PutioFile;
            DataGridViewRow FileQueRow = FileDownload.RowInQue;
            
            string bytesrecieved = ((e.BytesReceived / 1024d) / 1024d).ToString("0.0");
            string totalbytes = ((e.TotalBytesToReceive / 1024d) / 1024d).ToString("0.0");
            UpdateCellValue(FileQueRow, "ColumnStatus", string.Format("{0} Mb / {1} Mb", bytesrecieved, totalbytes));
        }

        private void WebClient_DownloadDataCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var FileDownload = e.UserState as PutioFile;
            DataGridViewRow FileQueRow = FileDownload.RowInQue;

            if (e.Cancelled == true)
                UpdateCellValue(FileQueRow, "ColumnStatus", "Canceled");
            else if (e.Error != null)
                UpdateCellValue(FileQueRow, "ColumnStatus", "Error");
            else
            {
                UpdateCellValue(FileQueRow, "ColumnStatus", "Complete");
                if (Properties.Settings.Default.DeleteAfterDownload)
                    DeleteFile(FileDownload);
            }
            

            UpdateCellValue(FileQueRow, "ColumnCompleted", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            DownloadFile();
        }

        // Form Controls

        private async void FormPutioManager_Load(object sender, EventArgs e)
        {
            try
            {
                UpdateTreeView(await filemgr.List("0"));
                var response = await filemgr.AccountInfo();
                UpdateStatusText(string.Format("Connected Account: " + response["username"].ToString()));
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
                JArray files = await filemgr.List((treeViewPutioFiles.SelectedNode.Tag as PutioFile).FileID);
                treeViewPutioFiles.SelectedNode.Nodes.Clear();
                UpdateTreeView(files);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPutioFiles.SelectedNode;
            DeleteFile(selectedNode.Tag as PutioFile);
        }

        private async void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPutioFiles.SelectedNode;
            var putiofile = selectedNode.Tag as PutioFile;

            string id = putiofile.FileID;
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

            putiofile.DownloadLink = uriDownloadFile;
            putiofile.FilePath = path;
            putiofile.RowInQue = newDownloadRow;
            putiofile.FileDownloaded = false;

            FileDownloads.Enqueue(putiofile);
            Console.WriteLine("Queue Lengths = " + FileDownloads.Count);
            DownloadFile();

        }

        private async void zipDownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPutioFiles.SelectedNode;
            var putiofile = selectedNode.Tag as PutioFile;

            string id = putiofile.FileID;
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

            string filename = selectedNode.Text + ".zip";
            string filetype = "ZIP";
            string started = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            string path = Properties.Settings.Default.DownloadDirectory + @"\" + filename;
            dataGridView1.Rows.Add(filename, filetype, started, "", status);
            DataGridViewRow newDownloadRow = dataGridView1.Rows[dataGridView1.Rows.Count - 1];

            putiofile.DownloadLink = uriZipFile;
            putiofile.FilePath = path;
            putiofile.RowInQue = newDownloadRow;
            putiofile.FileDownloaded = false;

            FileDownloads.Enqueue(putiofile);

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
            UpdateTreeView(await filemgr.List("0"));
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openDownloadsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dirDownload = Properties.Settings.Default.DownloadDirectory;
            if (Directory.Exists(dirDownload))
                Process.Start(dirDownload);
        }

        private async void treeViewPutioFiles_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            TreeNode selectednode = treeViewPutioFiles.SelectedNode;
            string id = selectednode.Tag.ToString();
            var rename_response = await filemgr.Rename(id, e.Label);
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewPutioFiles.SelectedNode.BeginEdit();
        }

        private void autoDownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewPutioFiles.SelectedNode != treeViewPutioFiles.Nodes[0])
            {
                TreeNode selectedNode = treeViewPutioFiles.SelectedNode;
                var putioFile = selectedNode.Tag as PutioFile;
                treeViewAutoDownloads.Nodes.Add(putioFile.FileName).Tag = putioFile;
            }
        }

        // Methods and Modules

        private void UpdateTreeView(JArray inJArrFiles)
        {
            TreeNode selectedNode = treeViewPutioFiles.SelectedNode;
            foreach (JObject file in inJArrFiles)
            {
                string message = null;
                var putiofile = new PutioFile(file["name"].ToString(), file["id"].ToString());
                TreeNode newnode = selectedNode.Nodes.Add(file["name"].ToString());
                newnode.Tag = putiofile;
                putiofile.FileNode = newnode;
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

        private async void DeleteFile(PutioFile inPutioFile)
        {
            var deletemessage = await filemgr.Delete(inPutioFile.FileID);
            string status = JObject.Parse(deletemessage)["status"].ToString();
            if (status == "OK")
            {
                treeViewPutioFiles.Nodes.Remove(inPutioFile.FileNode);
            }
        }

        // test button!

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectednode = treeViewPutioFiles.SelectedNode;
            var putiofile = selectednode.Tag as PutioFile;
            Console.WriteLine(putiofile.DownloadLink);
        }

        // internal classes

        internal class PutioFile
        {

            public readonly string FileName;
            public readonly string FileID;
            public string FilePath;
            public bool FileDownloaded;
            public Uri DownloadLink;
            public DataGridViewRow RowInQue;
            public TreeNode FileNode;
            public WebClient PrimaryWebClient;

            public PutioFile(string inStrFileName, string inStrFileID)
            {
                FileName = inStrFileName;
                FileID = inStrFileID;
            }

        }

    }
}
