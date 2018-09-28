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

           
            autoDownloadsToolStripMenuItem.Checked = !Properties.Settings.Default.ShowAutoDownloads;
            transfersToolStripMenuItem.Checked = !Properties.Settings.Default.ShowTransfers;
            managersToolStripMenuItem.Checked = !Properties.Settings.Default.ShowManager;
            splitContainerManager.Panel2Collapsed = Properties.Settings.Default.ShowTransfers;
            splitContainerFiles.Panel2Collapsed = Properties.Settings.Default.ShowAutoDownloads;
            splitContainer1.Panel2Collapsed = Properties.Settings.Default.ShowManager;

            TimeWorker.DoWork += TimeWorker_DoWork;
            TimeWorker.ProgressChanged += TimeWorker_ReportProgress;
            TimeWorker.WorkerReportsProgress = true;
            TimeWorker.WorkerSupportsCancellation = true;
            TimeWorker.RunWorkerCompleted += TimeWorker_Complete;
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

        BackgroundWorker TimeWorker = new BackgroundWorker();

        bool closeform = false;



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

        private void TimeWorker_DoWork(object sender , DoWorkEventArgs e)
        {
            do
            {
                if (TimeWorker.CancellationPending)
                    break;
                int sleeptime = Properties.Settings.Default.AutoDownloadInterval * 1000;
                System.Threading.Thread.Sleep(sleeptime);
                TimeWorker.ReportProgress(0);
            } while (true);
        }

        private void TimeWorker_ReportProgress(object sender , ProgressChangedEventArgs e)
        {
            CheckAutoDownloads();
        }

        private void TimeWorker_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("TimeWorker Stopped");
        }

        // Form Controls

        private async void FormPutioManager_Load(object sender, EventArgs e)
        {
            treeViewPutioFiles.SelectedNode = rootnode;
            try
            {
                notifyIcon1.Visible = true;
                UpdateTreeView(await filemgr.List("0"), rootnode);
                var response = await filemgr.AccountInfo();
                UpdateStatusText(string.Format("Connected Account: " + response["username"].ToString()));
                TimeWorker.RunWorkerAsync();
                GetTransfers();
            }
            catch 
            {
                MessageBox.Show("the token provided did not work");
            }
        }

        private void FormPutioManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closeform)
            {
                Properties.Settings.Default.Save();
                if (DownloadInProgress())
                {
                    DialogResult result = MessageBox.Show("Something is still downloading, continue closing?", "Download in progress", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                        e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPutioFiles.SelectedNode;
            DeleteFile(selectedNode.Tag as PutioFile);
        }

        private void FormPutioManager_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void FormPutioManager_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }

        // Files Context Menu

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

        private async void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewPutioFiles.SelectedNode;

            var file = treeViewPutioFiles.SelectedNode.Tag as PutioFile;
            string id = file.id;
            UpdateTreeView(await filemgr.List(id), treeViewPutioFiles.SelectedNode);

        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewPutioFiles.SelectedNode.BeginEdit();
        }

        private void autoDownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewPutioFiles.SelectedNode;
            var putiofile = selectedNode.Tag as PutioFile;
            if (putiofile.file_type == "FOLDER")
            {
                var frmAutoDownload = new FormPutioAutoDownload();
                frmAutoDownload.ShowDialog();
                TreeNode auto = treeViewAutoDownloads.Nodes.Add(putiofile.name);
                putiofile.autodownload_extensions = frmAutoDownload.extensions;
                putiofile.autodownload_minsize = (frmAutoDownload.minsize * 1024) * 1024;
                auto.Tag = putiofile;
                frmAutoDownload.Dispose();
            }
        }

        // AutoDownloads Context Menu

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (treeViewAutoDownloads.SelectedNode != null)
                treeViewAutoDownloads.SelectedNode.Remove();
        }

        // Transfers Context Menu

        private async void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetText().ToString().StartsWith("magnet:"))
            {
                string url = Clipboard.GetText().ToString();
                var file = treeViewPutioFiles.SelectedNode.Tag as PutioFile;
                var option = MessageBox.Show(string.Format("Download {0} to {1}", magnet(url), file.name), "test", MessageBoxButtons.OKCancel);

                if (option == DialogResult.OK)
                {
                    await trfrmgr.Add(url, file.id);
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

        // View - Menu Strip

        private void autoDownloadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var toolStripMenuItem = (sender as ToolStripMenuItem);
            Properties.Settings.Default.ShowAutoDownloads = toolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
            toolStripMenuItem.Checked = !toolStripMenuItem.Checked;
            splitContainerFiles.Panel2Collapsed = !toolStripMenuItem.Checked;
        }

        private void transfersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var toolStripMenuItem = (sender as ToolStripMenuItem);
            Properties.Settings.Default.ShowTransfers = toolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
            toolStripMenuItem.Checked = !toolStripMenuItem.Checked;
            splitContainerManager.Panel2Collapsed = !toolStripMenuItem.Checked;
        }

        private void managersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var toolStripMenuItem = (sender as ToolStripMenuItem);
            Properties.Settings.Default.ShowManager = toolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
            toolStripMenuItem.Checked = !toolStripMenuItem.Checked;
            splitContainer1.Panel2Collapsed = !toolStripMenuItem.Checked;
        }

        // File - Menu strip

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
            if (!AlreadyDownloading(putiofile))
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
                if (inPutioFile.file != null)   
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

        private string GetFileExtension(string inStrFileName)
        {
            return Path.GetExtension(inStrFileName.ToLower());
        }

        private void CheckAutoDownloads()
        {
            foreach (TreeNode rootfolder in treeViewAutoDownloads.Nodes)
            {
                AutoDownloadFiles(rootfolder.Tag as PutioFile);
            }
        }

        private async void AutoDownloadFiles(PutioFile putiofile)
        {
            var response = await filemgr.List(putiofile.id);
            List<string> allowed_extensions = new List<string>();
            allowed_extensions.AddRange(putiofile.autodownload_extensions.Split(',').ToArray());

            foreach (JObject jobject in response)
            {
                var file = SetPutioFile(jobject);
                file.autodownload_extensions = putiofile.autodownload_extensions;
                file.autodownload_minsize = putiofile.autodownload_minsize;
                if (file.file_type == "FOLDER")
                {
                    AutoDownloadFiles(file);
                }
                else
                {
                    var match = allowed_extensions.FirstOrDefault(filext => filext.Contains(GetFileExtension(file.name)));
                    if (match != null & file.autodownload_minsize <= Convert.ToInt64(file.size))
                        QueueDownload(file);
                }
            }
        }

        private bool AlreadyDownloading(PutioFile putiofile)
        {
            foreach (DataGridViewRow row in dataGridViewDownloads.Rows)
            {
                if (row.Cells["ColumnFile"].Value.ToString() == putiofile.name)
                {
                    //Console.WriteLine("file " + putiofile.name + " is already downloading");
                    return true;
                }
            }
            return false;
        }

        private bool DownloadInProgress()
        {
            foreach (DataGridViewRow row in dataGridViewDownloads.Rows)
            {
                if (row.Cells["ColumnStatus"].Value.ToString() != "COMPLETE")
                {
                    return true;
                }
            }
            return false;
        }

        private PutioFile SetPutioFile(JObject file)
        {
            var putiofile = new PutioFile(file["id"].ToString(), file["name"].ToString());
            putiofile.parent_id = file["parent_id"].ToString();
            putiofile.content_type = file["content_type"].ToString();
            putiofile.file_type = file["file_type"].ToString();
            putiofile.download_path = Properties.Settings.Default.DownloadDirectory + @"\" + putiofile.name;
            putiofile.size = file["size"].ToString();
            if (putiofile.content_type != "FOLDER")
                putiofile.downloadlink = new Uri(urlPutioApi + "files/" + putiofile.id + "/download?oauth_token=" + OAuthToken);

            return putiofile;
        }

        // test button!

        private async void testToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var response = await trfrmgr.List();
            foreach (JObject jobject in response){
                Console.WriteLine(jobject);
            }
        }

        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckAutoDownloads();
        }

        private void dataGridViewDownloads_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            closeform = true;
            Application.Exit();
        }

    }
}
