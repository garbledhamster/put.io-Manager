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
    public partial class FormPutioRename : Form
    {
        public FormPutioRename()
        {
            InitializeComponent();
        }

        public string NewName;

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            NewName = textBoxNewName.Text;
            Close();
        }

        private void FormPutioRename_Load(object sender, EventArgs e)
        {
            textBoxNewName.Focus();
        }
    }
}
