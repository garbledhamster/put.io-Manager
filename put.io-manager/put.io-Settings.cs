using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.htt
namespace put.io_manager
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {

        }

        private void buttonGetToken_Click(object sender, EventArgs e)
        {
           string requestURL = Authentication.OAuthRequestURL("3355", "oob", "na");

        }
    }
}
