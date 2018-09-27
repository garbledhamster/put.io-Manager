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
using PutioApi;

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

            filemgr = new Files(OAuthToken);
            zipmgr = new Zips(OAuthToken);
            trfrmgr = new Transfers(OAuthToken);

            var root = new PutioFile("0", "putio files");

            treeViewPutioFiles.Nodes.Clear();
            rootnode = treeViewPutioFiles.Nodes.Add("putio files");
            rootnode.Tag = root;

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
        Files filemgr;
        Zips zipmgr;
        Transfers trfrmgr;
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
            UpdateCellValue(FileQueRow, "ColumnStatus", string.Format("{0} Mb / {1} Mb", bytesrecieved, totalbytes));
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
            treeViewPutioFiles.SelectedNode = rootnode;
            try
            {
                UpdateTreeView(await filemgr.List("0"), rootnode);
                var response = await filemgr.AccountInfo();
                UpdateStatusText(string.Format("Connected Account: " + response["username"].ToString()));
                GetTransfers();
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
            var createzip_response = await zipmgr.CreateZip(selectedfile.id);
            string zipid = JObject.Parse(createzip_response)["zip_id"].ToString();
            PutioFile zipfile = new PutioFile(zipid, selectedfile.name + ".zip");
            zipfile.file_type = "ZIP";

            do
            {
                var zip_properties = await zipmgr.GetZip(zipid);
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

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewAutoDownloads.SelectedNode != null)
                treeViewAutoDownloads.SelectedNode.Remove();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetText().ToString().StartsWith("magnet:"))
            {
                string url = Clipboard.GetText().ToString();
                var file = treeViewPutioFiles.SelectedNode.Tag as PutioFile;
                var option = MessageBox.Show(string.Format("Download {0} to {1}", magnet(url), file.name), "test", MessageBoxButtons.OKCancel);
        
                if (option == DialogResult.OK)
                {
                    trfrmgr.Add(url, file.id);
                    GetTransfers();
                }
            }
            else
                MessageBox.Show("Copy a magnet link and try again", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTransfers();
        }

        private void cleanToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private async void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewPutioFiles.SelectedNode;

            var file = treeViewPutioFiles.SelectedNode.Tag as PutioFile;
            string id = file.id;
            UpdateTreeView(await filemgr.List(id), treeViewPutioFiles.SelectedNode);

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
            if (e.Label != null)
            {                
                var rename_response = await filemgr.Rename((e.Node.Tag as PutioFile).id, e.Label);
                (e.Node.Tag as PutioFile).name = e.Label;
            }
        }

        private async void treeViewPutioFiles_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            treeViewPutioFiles.SelectedNode = e.Node;
            PutioFile file = e.Node.Tag as PutioFile;

            if (e.Node != rootnode)
            {
                UpdateTreeView(await filemgr.List(file.id), e.Node);
            }
            
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

            dataGridViewDownloads.Rows.Add(putiofile.name, putiofile.file_type, started, "", "Not Started");
            DataGridViewRow newDownloadRow = dataGridViewDownloads.Rows[dataGridViewDownloads.Rows.Count - 1];

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

                //PrintPutioProperties(putiofile);

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
            dt = (DataTable)dataGridViewDownloads.DataSource;
            dt.TableName = "Download Queue";
            dt.WriteXml(Application.StartupPath + @"\DownloadQueue.xml", true);
        }

        private async void GetTransfers()
        {
            dataGridViewTransfers.Rows.Clear();
            var transfers = await trfrmgr.List();
            foreach (JObject transfer in transfers)
            {

                string name = transfer["name"].ToString();
                string id = transfer["id"].ToString();
                string peers = transfer["peers_connected"].ToString();
                string uploaded = ((Convert.ToInt32(transfer["uploaded"]) / 1024) / 1024).ToString() + " Mb";
                string status = transfer["status"].ToString();
                string parentid = transfer["save_parent_id"].ToString();
                string source = transfer["source"].ToString();
                string started = transfer["created_at"].ToString();
                string size = transfer["size"].ToString();

                var putiotransfer = new PutioTransfer(name, id);
                putiotransfer.save_parent_id = parentid;
                putiotransfer.source = source;
                putiotransfer.status = status;
                putiotransfer.started = started;
                putiotransfer.size = size;

                size = ((Convert.ToInt64(size) / 1024) / 1024).ToString() + " Mb";

                int rowindex = dataGridViewTransfers.Rows.Add(name, size, peers, uploaded, started, status);
                var row = dataGridViewTransfers.Rows[rowindex];
                row.Tag = putiotransfer;

            }

            dataGridViewTransfers.Sort(dataGridViewTransfers.Columns[4], ListSortDirection.Descending);

        }


        private string magnet(string inStrUrl)
        {
            return inStrUrl.Split('=')[2].Replace("&tr", "").Replace("+", " ");
        }


        // test button!

        private async void testToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var response = await trfrmgr.List();
            foreach (JObject jobject in response){
                Console.WriteLine(jobject);
            }
        }

    }
}
