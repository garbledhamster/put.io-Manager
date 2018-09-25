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
        List<BackgroundWorker> TimeKeepers = new List<BackgroundWorker>();

        // Initalizers

        private void CstripDownload_Click(object sender, EventArgs e, PutioFile file)
        {
            if (file.webcilent.IsBusy)
            {
                Console.WriteLine("download canceled");
                file.webcilent.CancelAsync();
            }
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var FileDownload = e.UserState as PutioFile;
            DataGridViewRow FileQueRow = FileDownload.rowinque;
            string bytesrecieved = ((e.BytesReceived / 1024d) / 1024d).ToString("0.0");
            string totalbytes = ((e.TotalBytesToReceive / 1024d) / 1024d).ToString("0.0");
            UpdateCellValue(FileQueRow, "ColumnStatus", string.Format("{0} Mb / {1} Mb ({2} Mb/s)", bytesrecieved, totalbytes, bytesrecieved));
        }

        private void WebClient_DownloadDataCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var FileDownload = e.UserState as PutioFile;
            DataGridViewRow FileQueRow = FileDownload.rowinque;

            if (e.Cancelled == true)
            {
                UpdateCellValue(FileQueRow, "ColumnStatus", "Canceled");
                File.Delete(FileDownload.download_path);
            }
            else if (e.Error != null)
            {
                UpdateCellValue(FileQueRow, "ColumnStatus", "Error");
                File.Delete(FileDownload.download_path);
            }
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
                UpdateTreeView(await filemgr.List("0"), rootnode);
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

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPutioFiles.SelectedNode;
            DeleteFile(selectedNode.Tag as PutioFile);
        }

        // Menu Strip

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var putiofile = treeViewPutioFiles.SelectedNode.Tag as PutioFile;
            putiofile.downloadlink = new Uri(urlPutioApi + "files/" + putiofile.id + "/download?oauth_token=" + OAuthToken);
            QueueDownload(putiofile);
        }

        private async void zipDownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedfile = treeViewPutioFiles.SelectedNode.Tag as PutioFile;

            string zipstatus = "";
            var createzip_response = await filemgr.CreateZip(selectedfile.id);
            string zipid = JObject.Parse(createzip_response)["zip_id"].ToString();
            PutioFile zipfile = new PutioFile(zipid, selectedfile.name + ".zip");
            zipfile.file_type = "ZIP";

            do
            {
                var zip_properties = await filemgr.GetZip(zipid);
                zipstatus = zip_properties["zip_status"].ToString();
                if (zipstatus == "DONE")
                    zipfile.downloadlink = new Uri(zip_properties["url"].ToString());
            } while (zipstatus != "DONE");

            QueueDownload(zipfile);
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
            UpdateTreeView(await filemgr.List("0"), treeViewPutioFiles.SelectedNode);
        }

        // Tool Strip

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
                treeViewAutoDownloads.Nodes.Add(putioFile.name).Tag = putioFile;
            }
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

        // Tree View Putio Files

        private async void treeViewPutioFiles_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            var rename_response = await filemgr.Rename((e.Node.Tag as PutioFile).id, e.Label);
            (e.Node.Tag as PutioFile).name = e.Label;
        }

        private async void treeViewPutioFiles_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            treeViewPutioFiles.SelectedNode = e.Node;
            PutioFile file = e.Node.Tag as PutioFile;

            if (e.Node != rootnode)
                UpdateTreeView(await filemgr.List(file.id), e.Node);
            else
                UpdateTreeView(await filemgr.List("0"), e.Node);
        }

        // Methods and Modules

        private void DownloadFile()
        {
            if (FileDownloads.Any())
            {
                foreach (WebClient wc in WebClients)
                {
                    if (!wc.IsBusy)
                    {
                        var putiofile = FileDownloads.Dequeue();
                        putiofile.rowinque.Cells["ColumnStatus"].Value = "Queued";
                        putiofile.webcilent = wc;

                        ContextMenuStrip cstrip = new ContextMenuStrip();
                        cstrip.Items.Add("Cancel").Click += (s, e1) => CstripDownload_Click(s, e1, putiofile);
                        putiofile.rowinque.ContextMenuStrip = cstrip;
                        wc.DownloadFileAsync(putiofile.downloadlink, putiofile.download_path, putiofile);
                        return;
                    }
                }
            }
            return;
        }

        private void QueueDownload(PutioFile putiofile)
        {
            string started = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            string path = Properties.Settings.Default.DownloadDirectory + @"\" + putiofile.name;

            dataGridView1.Rows.Add(putiofile.name, putiofile.file_type, started, "", "Not Started");
            DataGridViewRow newDownloadRow = dataGridView1.Rows[dataGridView1.Rows.Count - 1];

            putiofile.rowinque = newDownloadRow;
            putiofile.download_path = path;

            FileDownloads.Enqueue(putiofile);
            DownloadFile();
        }

        private void UpdateTreeView(JArray inJArrFiles, TreeNode node)
        {
            node.Nodes.Clear();
            foreach (JObject file in inJArrFiles)
            {
                string message = null;

                var putiofile = new PutioFile(file["id"].ToString(), file["name"].ToString());
                putiofile.parent_id = file["parent_id"].ToString();
                putiofile.content_type = file["content_type"].ToString();
                putiofile.file_type = file["file_type"].ToString();

                PrintPutioProperties(putiofile);

                TreeNode newnode = node.Nodes.Add(putiofile.name);
                newnode.Tag = putiofile;
                putiofile.file = newnode;

                // set tooltip node text to the files properties
                foreach (var property in file)
                    message += property.Key.ToString().ToLower() + ": " + property.Value.ToString().ToLower() + Environment.NewLine;


                newnode.ToolTipText = message;
                if (file["file_type"].ToString() != "FOLDER")
                {
                    newnode.SelectedImageIndex = 2;
                    newnode.ImageIndex = 2;
                }
                else
                    newnode.Nodes.Add("Loading...");

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

        private async void DeleteFile(PutioFile inPutioFile)
        {
            var deletemessage = await filemgr.Delete(inPutioFile.id);
            string status = JObject.Parse(deletemessage)["status"].ToString();
            if (status == "OK")
            {
                treeViewPutioFiles.Nodes.Remove(inPutioFile.file);
            }
        }

        private void PrintPutioProperties(PutioFile putiofile)
        {
            Console.WriteLine("===================");
            Console.WriteLine("filename: " + putiofile.name);
            Console.WriteLine("fileid: " + putiofile.id);
            Console.WriteLine("parent_id: " + putiofile.parent_id);
            Console.WriteLine("filetype: " + putiofile.file_type);
        }

        private void ExportDataGridView()
        {
            var dt = new DataTable();
            dt = (DataTable)dataGridView1.DataSource;
            dt.TableName = "Download Queue";
            dt.WriteXml(Application.StartupPath + @"\DownloadQueue.xml", true);
        }

        // test button!

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectednode = treeViewPutioFiles.SelectedNode;
            var putiofile = selectednode.Tag as PutioFile;
            Console.WriteLine(putiofile.downloadlink);
        }

    }
}
