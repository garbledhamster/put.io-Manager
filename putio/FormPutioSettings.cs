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
        }

        private void buttonGetToken_Click(object sender, EventArgs e)
        {
            FormPutioAuthWeb authweb = new FormPutioAuthWeb();
            Uri uri = new Uri(@"https://api.put.io/v2/oauth2/authenticate?client_id=3504&response_type=oob&redirect_uri=");
            authweb.webBrowserUserAuth.Url = uri;
            authweb.webBrowserUserAuth.ScriptErrorsSuppressed = true;
            authweb.Location = this.Location;
            authweb.Show();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.OAuthToken = textBoxToken.Text;
            Properties.Settings.Default.DownloadDirectory = textBoxDownloadPath.Text;  
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void PutioSettings_Load(object sender, EventArgs e)
        {
            SetTextBoxes();
        }

        private void SetTextBoxes()
        {
            textBoxToken.Text = Properties.Settings.Default.OAuthToken;
            textBoxDownloadPath.Text = Properties.Settings.Default.DownloadDirectory;
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
                    SetTextBoxes();
                }
            }
        }
    }
}
