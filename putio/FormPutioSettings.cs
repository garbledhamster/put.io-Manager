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
    public partial class FormPutioSettings : Form
    {
        public FormPutioSettings()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            textBoxToken.Text = Properties.Settings.Default.OAuthToken;
            textBoxDownloadPath.Text = Properties.Settings.Default.DownloadDirectory;
            checkBoxShowToolTips.Checked = Properties.Settings.Default.ShowToolTips;
            numericUpDown1.Value = Properties.Settings.Default.ParallelDownloads;
        }

        private void PutioSettings_Load(object sender, EventArgs e)
        {
           
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.OAuthToken = textBoxToken.Text;
            Properties.Settings.Default.DownloadDirectory = textBoxDownloadPath.Text;
            Properties.Settings.Default.ShowToolTips = checkBoxShowToolTips.Checked;
            Properties.Settings.Default.ParallelDownloads = Convert.ToInt32(numericUpDown1.Value);
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void buttonGetToken_Click(object sender, EventArgs e)
        {
            string url = "https://api.put.io/v2/oauth2/authenticate?client_id=3510&response_type=oob&redirect_uri=";
            FormPutioAuthWeb authweb = new FormPutioAuthWeb();
            Uri uri = new Uri(url);
            authweb.labelAddress.Text = url;
            authweb.webBrowserUserAuth.Url = uri;
            authweb.StartPosition = FormStartPosition.CenterParent;
            
            authweb.ShowDialog();
        }

        private void buttonDownloadPathChange_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.ShowDialog();
                if (fbd.SelectedPath.Length > 0)
                {
                    Properties.Settings.Default.DownloadDirectory = fbd.SelectedPath;
                    Properties.Settings.Default.Save();
                    InitializeControls();
                }
            }
        }
    }
}
