﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace put.io_manager
{
    public partial class FormManager : Form
    {
        public FormManager()
        {
            InitializeComponent();
        }

        FormSettings FormSettings = new FormSettings();


        private void FormManager_Load(object sender, EventArgs e)
        {

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings.Show();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
