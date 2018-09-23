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
        }

        private void InitializeControls()
        {
            rootnode = treeViewPutioFiles.Nodes.Add("put.io files");
            rootnode.Tag = "root";
            OAuthToken = Properties.Settings.Default.OAuthToken;
            filemgr = new PutioManager(OAuthToken);

        }

        public string OAuthToken;
        public string OAuthParamater;
        public TreeNode rootnode;
        PutioManager filemgr;
        
        private const string urlPutioApi = "https://api.put.io/v2/";

        // Form Controls

        private async void FormPutioManager_Load(object sender, EventArgs e)
        {
            treeViewPutioFiles.ShowNodeToolTips = Properties.Settings.Default.ShowToolTips;
            UpdateTreeView(await filemgr.List("0"));
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

        private async void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string id = treeViewPutioFiles.SelectedNode.Tag.ToString();
            JObject fileProperties = await filemgr.Get(id);
            string filename = fileProperties["name"].ToString();
            string filetype = fileProperties["file_type"].ToString();
            string size = fileProperties["size"].ToString();
            string started = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            string path = Properties.Settings.Default.DownloadDirectory + @"\" + filename;
            Uri uriDownloadFile = new Uri(urlPutioApi + "files/" + id + "/download?oauth_token=" + OAuthToken);
            dataGridView1.Rows.Add(filename, filetype, size, started, "");

            DataGridViewRow newDownloadRow = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
            newDownloadRow.Tag = id;
            DownloadFile(uriDownloadFile, path, newDownloadRow);

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
        }

        private async void zipDownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectednode = treeViewPutioFiles.SelectedNode;
            string id = selectednode.Tag.ToString();
            var createzip_response = await filemgr.CreateZip(id);
            string zipid = JObject.Parse(createzip_response)["zip_id"].ToString();

            Console.WriteLine("ZipId=" + zipid);

            string status = null;
            string size = null;
            Uri uriZipFile = null;

            do
            {
                var zip_properties = await filemgr.GetZip(zipid);
                Console.WriteLine(zip_properties);
                string zipstatus = zip_properties["zip_status"].ToString();
                Console.WriteLine(zipstatus);
                if (zipstatus == "DONE")
                {
                    status = zipstatus;
                    uriZipFile= new Uri(zip_properties["url"].ToString());
                    size = zip_properties["size"].ToString();
                }

            } while (status != "DONE");

            string filename = selectednode.Text + ".zip";
            string filetype = "ZIP";
            string started = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            string path = Properties.Settings.Default.DownloadDirectory + @"\" + filename;
            dataGridView1.Rows.Add(filename, filetype, size, started, "0%");
            DataGridViewRow newDownloadRow = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
            DownloadFile(uriZipFile, path, newDownloadRow);

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

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Settings = new FormPutioSettings();
            Settings.StartPosition = FormStartPosition.CenterParent;
            Settings.ShowDialog();

            treeViewPutioFiles.ShowNodeToolTips = Properties.Settings.Default.ShowToolTips;

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

        private void DownloadFile(Uri inUriDownloadFile, string inStrFilePath, DataGridViewRow inDgvrNewDownload)
        {

            using (WebClient wc = new WebClient())
            {
                ContextMenuStrip cstrip = new ContextMenuStrip();
                cstrip.Items.Add("Cancel").Click += (sender, e) => CstripDownload_Click(sender, e, wc, inDgvrNewDownload);
                inDgvrNewDownload.ContextMenuStrip = cstrip;
                wc.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                wc.DownloadFileCompleted += WebClient_DownloadDataCompleted;
                wc.DownloadFileAsync(inUriDownloadFile, inStrFilePath, inDgvrNewDownload);
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

        // Initalizers

        private void CstripDownload_Click(object sender, EventArgs e, WebClient wc, DataGridViewRow dgvr)
        {
            if (wc.IsBusy)
            {
                wc.CancelAsync();
            }
        }
      
        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DataGridViewRow newDownloadRow = e.UserState as DataGridViewRow;
            string bytesrecieved = ((e.BytesReceived / 1024d) / 1024d).ToString("0.00");
            string totalbytes = ((e.TotalBytesToReceive / 1024d) / 1024d).ToString("0.00");
            UpdateCellValue(newDownloadRow, "ColumnStatus", string.Format("{0} mb / {1} mb", bytesrecieved, totalbytes ));
        }

        private void WebClient_DownloadDataCompleted(object sender, AsyncCompletedEventArgs e)
        {
            DataGridViewRow newDownloadRow = e.UserState as DataGridViewRow;
            if (e.Cancelled == true)
            {
                UpdateCellValue(newDownloadRow, "ColumnStatus", "Canceled");
                return;
            }
            if (e.Error != null)
            {
                UpdateCellValue(newDownloadRow, "ColumnStatus", "Error");
                return;
            }

            UpdateCellValue(newDownloadRow, "ColumnStatus", "Complete");
        }


        // test button!

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServicePointManager.DefaultConnectionLimit = Properties.Settings.Default.ConcurrentDownloads;
        }
    }
}
