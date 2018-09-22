using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace putio
{
    public partial class FormPutioManager : Form
    {
        public FormPutioManager()
        {
            InitializeComponent();
        }

        PutioManager putioManager = new PutioManager(Properties.Settings.Default.OAuthToken);

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPutioSettings putioSettings = new FormPutioSettings();
            putioSettings.Location = this.Location;
            putioSettings.Show();
        }

        private void FormPutioManager_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.DownloadDirectory.Length < 1)
            {
                Properties.Settings.Default.DownloadDirectory = Application.StartupPath;
                Properties.Settings.Default.Save();
            }

            foreach (PutioManager.PutioFile folder in putioManager.listOfFiles.Where(file => file.parent_id == "0"))
            {
                TreeNode node = treeViewPutioFiles.Nodes.Add(folder.name);
            }
            SelectNodesFromSelectedNodes();
        }

        private void UpdateStatusText(string inStrStatusText)
        {
            toolStripStatusLabel1.Text = inStrStatusText;
            toolStripStatusLabel1.ForeColor = Color.Black;
        }

        private void UpdateStatusText(string inStrStatusText, Color fontColor)
        {
            toolStripStatusLabel1.Text = inStrStatusText;
            toolStripStatusLabel1.ForeColor = fontColor;
        }

        private bool TokenFound()
        {
            if (Properties.Settings.Default.OAuthToken.Length > 0)
                return true;
            else
                return false;
        }

        private void treeViewPutioFiles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count < 1)
                ExpandTreeNodes(e.Node);
        }

        private void ExpandTreeNodes(TreeNode node)
        {
            if (node.Nodes.Count < 1)
            {
                string id = putioManager.listOfFiles.Where(file => file.name == node.Text).First().id;
                putioManager.GetFiles(id);
                foreach (PutioManager.PutioFile file in putioManager.listOfFiles.Where(file => file.parent_id == id))
                {
                    node.Tag = file;
                    if (node.Parent == null)
                    {
                        //Console.WriteLine(file.name + "=" + file.id + ";" + file.file_type);
                        treeViewPutioFiles.Nodes[node.Index].Nodes.Add(file.name).Tag = file;
                    }
                    else
                    {
                        //Console.WriteLine(file.name + "=" + file.id+";"+file.file_type);
                         node.Nodes.Add(file.name).Tag = file;
                    }
                    SetNodeIcon(node);
                }
            }
        }

        private void SetNodeIcon(TreeNode node)
        {
            PutioManager.PutioFile file = node.Tag as PutioManager.PutioFile;
            if (file.file_type == "FOLDER" | 
                file.file_type == "ARCHIVE" | 
                file.file_type == "FILE")
            {
                node.ImageIndex = 0;
                node.SelectedImageIndex = 1;
            }
            else
            {
                node.SelectedImageIndex = 2;
                node.ImageIndex = 2;
            }
        }

        private void SelectNodesFromSelectedNodes()
        {
            TreeNode selectednode = treeViewPutioFiles.SelectedNode;
            foreach (TreeNode node in treeViewPutioFiles.Nodes)
            {
                ExpandTreeNodes(node);
            }

            treeViewPutioFiles.SelectedNode = selectednode;
            treeViewPutioFiles.EndUpdate();
        }

        private void treeViewPutioFiles_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            foreach (TreeNode node in e.Node.Nodes)
            {
                ExpandTreeNodes(node);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var file = treeViewPutioFiles.SelectedNode.Tag as PutioManager.PutioFile;
            Console.WriteLine(file.name + "=" + file.id);
            putioManager.DeleteFile(file.id);
        }
    }
}
