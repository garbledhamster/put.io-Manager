namespace put.io_manager
{
    partial class FormSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStripSettings = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControlSettingsMain = new System.Windows.Forms.TabControl();
            this.tabPageSetings1 = new System.Windows.Forms.TabPage();
            this.tabPageSetings2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBoxGetOAuthToken = new System.Windows.Forms.GroupBox();
            this.buttonGetToken = new System.Windows.Forms.Button();
            this.textBoxOAuthToken = new System.Windows.Forms.TextBox();
            this.statusStripSettings.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControlSettingsMain.SuspendLayout();
            this.tabPageSetings1.SuspendLayout();
            this.groupBoxGetOAuthToken.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStripSettings
            // 
            this.statusStripSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStripSettings.Location = new System.Drawing.Point(0, 329);
            this.statusStripSettings.Name = "statusStripSettings";
            this.statusStripSettings.Size = new System.Drawing.Size(482, 22);
            this.statusStripSettings.TabIndex = 0;
            this.statusStripSettings.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabControlSettingsMain, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(482, 329);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tabControlSettingsMain
            // 
            this.tabControlSettingsMain.Controls.Add(this.tabPageSetings1);
            this.tabControlSettingsMain.Controls.Add(this.tabPageSetings2);
            this.tabControlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSettingsMain.Location = new System.Drawing.Point(3, 3);
            this.tabControlSettingsMain.Name = "tabControlSettingsMain";
            this.tabControlSettingsMain.SelectedIndex = 0;
            this.tabControlSettingsMain.Size = new System.Drawing.Size(476, 295);
            this.tabControlSettingsMain.TabIndex = 2;
            // 
            // tabPageSetings1
            // 
            this.tabPageSetings1.Controls.Add(this.groupBoxGetOAuthToken);
            this.tabPageSetings1.Location = new System.Drawing.Point(4, 22);
            this.tabPageSetings1.Name = "tabPageSetings1";
            this.tabPageSetings1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSetings1.Size = new System.Drawing.Size(468, 269);
            this.tabPageSetings1.TabIndex = 0;
            this.tabPageSetings1.Text = "Connection";
            this.tabPageSetings1.UseVisualStyleBackColor = true;
            // 
            // tabPageSetings2
            // 
            this.tabPageSetings2.Location = new System.Drawing.Point(4, 22);
            this.tabPageSetings2.Name = "tabPageSetings2";
            this.tabPageSetings2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSetings2.Size = new System.Drawing.Size(468, 269);
            this.tabPageSetings2.TabIndex = 1;
            this.tabPageSetings2.Text = "Schedule";
            this.tabPageSetings2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(384, 304);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 22);
            this.button1.TabIndex = 3;
            this.button1.Text = "Save Settings...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBoxGetOAuthToken
            // 
            this.groupBoxGetOAuthToken.Controls.Add(this.textBoxOAuthToken);
            this.groupBoxGetOAuthToken.Controls.Add(this.buttonGetToken);
            this.groupBoxGetOAuthToken.Location = new System.Drawing.Point(8, 8);
            this.groupBoxGetOAuthToken.Name = "groupBoxGetOAuthToken";
            this.groupBoxGetOAuthToken.Size = new System.Drawing.Size(232, 48);
            this.groupBoxGetOAuthToken.TabIndex = 0;
            this.groupBoxGetOAuthToken.TabStop = false;
            this.groupBoxGetOAuthToken.Text = "Authentication Token";
            // 
            // buttonGetToken
            // 
            this.buttonGetToken.Location = new System.Drawing.Point(152, 16);
            this.buttonGetToken.Name = "buttonGetToken";
            this.buttonGetToken.Size = new System.Drawing.Size(75, 23);
            this.buttonGetToken.TabIndex = 0;
            this.buttonGetToken.Text = "Get Token";
            this.buttonGetToken.UseVisualStyleBackColor = true;
            this.buttonGetToken.Click += new System.EventHandler(this.buttonGetToken_Click);
            // 
            // textBoxOAuthToken
            // 
            this.textBoxOAuthToken.Location = new System.Drawing.Point(8, 16);
            this.textBoxOAuthToken.Name = "textBoxOAuthToken";
            this.textBoxOAuthToken.Size = new System.Drawing.Size(136, 20);
            this.textBoxOAuthToken.TabIndex = 1;
            this.textBoxOAuthToken.Tag = "";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 351);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStripSettings);
            this.Name = "FormSettings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.statusStripSettings.ResumeLayout(false);
            this.statusStripSettings.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControlSettingsMain.ResumeLayout(false);
            this.tabPageSetings1.ResumeLayout(false);
            this.groupBoxGetOAuthToken.ResumeLayout(false);
            this.groupBoxGetOAuthToken.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStripSettings;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControlSettingsMain;
        private System.Windows.Forms.TabPage tabPageSetings1;
        private System.Windows.Forms.TabPage tabPageSetings2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBoxGetOAuthToken;
        private System.Windows.Forms.TextBox textBoxOAuthToken;
        private System.Windows.Forms.Button buttonGetToken;
    }
}