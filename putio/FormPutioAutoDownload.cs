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
    public partial class FormPutioAutoDownload : Form
    {
        public FormPutioAutoDownload()
        {
            InitializeComponent();
        }

        public Int64 minsize;
        public string extensions;
        public bool keepFolderStructure;

        private void buttonAddAutoDownload_Click(object sender, EventArgs e)
        {
            minsize = Convert.ToInt64(textBoxMinFileSize.Text);
            extensions = textBoxExtensions.Text;
            keepFolderStructure = checkBoxKeepFolderStructure.Checked;
            this.Close();
        }
    }
}
