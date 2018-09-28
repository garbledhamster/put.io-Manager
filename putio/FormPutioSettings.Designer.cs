namespace putio
{
    partial class FormPutioSettings
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
            this.groupBoxAuthentication = new System.Windows.Forms.GroupBox();
            this.buttonGetToken = new System.Windows.Forms.Button();
            this.textBoxToken = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBoxDownloads = new System.Windows.Forms.GroupBox();
            this.labelAutoDownloadInterval = new System.Windows.Forms.Label();
            this.numericUpDownAutoDownloadInterval = new System.Windows.Forms.NumericUpDown();
            this.checkBoxDeleteAfterDownload = new System.Windows.Forms.CheckBox();
            this.labelParallelDownloads = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.buttonDownloadPathChange = new System.Windows.Forms.Button();
            this.textBoxDownloadPath = new System.Windows.Forms.TextBox();
            this.checkBoxShowToolTips = new System.Windows.Forms.CheckBox();
            this.groupBoxGeneral = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBoxAuthentication.SuspendLayout();
            this.groupBoxDownloads.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoDownloadInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBoxGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAuthentication
            // 
            this.groupBoxAuthentication.Controls.Add(this.buttonGetToken);
            this.groupBoxAuthentication.Controls.Add(this.textBoxToken);
            this.groupBoxAuthentication.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAuthentication.Location = new System.Drawing.Point(12, 12);
            this.groupBoxAuthentication.Name = "groupBoxAuthentication";
            this.groupBoxAuthentication.Size = new System.Drawing.Size(262, 82);
            this.groupBoxAuthentication.TabIndex = 0;
            this.groupBoxAuthentication.TabStop = false;
            this.groupBoxAuthentication.Text = "Authentication";
            // 
            // buttonGetToken
            // 
            this.buttonGetToken.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.buttonGetToken.Location = new System.Drawing.Point(161, 27);
            this.buttonGetToken.Name = "buttonGetToken";
            this.buttonGetToken.Size = new System.Drawing.Size(90, 23);
            this.buttonGetToken.TabIndex = 1;
            this.buttonGetToken.Text = "Get Token";
            this.buttonGetToken.UseVisualStyleBackColor = true;
            this.buttonGetToken.Click += new System.EventHandler(this.buttonGetToken_Click);
            // 
            // textBoxToken
            // 
            this.textBoxToken.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.textBoxToken.Location = new System.Drawing.Point(8, 29);
            this.textBoxToken.Name = "textBoxToken";
            this.textBoxToken.PasswordChar = '•';
            this.textBoxToken.Size = new System.Drawing.Size(149, 20);
            this.textBoxToken.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.buttonSave.Location = new System.Drawing.Point(381, 288);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(90, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // groupBoxDownloads
            // 
            this.groupBoxDownloads.Controls.Add(this.labelAutoDownloadInterval);
            this.groupBoxDownloads.Controls.Add(this.numericUpDownAutoDownloadInterval);
            this.groupBoxDownloads.Controls.Add(this.checkBoxDeleteAfterDownload);
            this.groupBoxDownloads.Controls.Add(this.labelParallelDownloads);
            this.groupBoxDownloads.Controls.Add(this.numericUpDown1);
            this.groupBoxDownloads.Controls.Add(this.buttonDownloadPathChange);
            this.groupBoxDownloads.Controls.Add(this.textBoxDownloadPath);
            this.groupBoxDownloads.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.groupBoxDownloads.Location = new System.Drawing.Point(12, 100);
            this.groupBoxDownloads.Name = "groupBoxDownloads";
            this.groupBoxDownloads.Size = new System.Drawing.Size(459, 115);
            this.groupBoxDownloads.TabIndex = 3;
            this.groupBoxDownloads.TabStop = false;
            this.groupBoxDownloads.Text = "Downloads";
            // 
            // labelAutoDownloadInterval
            // 
            this.labelAutoDownloadInterval.AutoSize = true;
            this.labelAutoDownloadInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelAutoDownloadInterval.Location = new System.Drawing.Point(5, 87);
            this.labelAutoDownloadInterval.Name = "labelAutoDownloadInterval";
            this.labelAutoDownloadInterval.Size = new System.Drawing.Size(144, 13);
            this.labelAutoDownloadInterval.TabIndex = 8;
            this.labelAutoDownloadInterval.Text = "Auto Download Interval (sec)";
            // 
            // numericUpDownAutoDownloadInterval
            // 
            this.numericUpDownAutoDownloadInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.numericUpDownAutoDownloadInterval.Location = new System.Drawing.Point(154, 85);
            this.numericUpDownAutoDownloadInterval.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numericUpDownAutoDownloadInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAutoDownloadInterval.Name = "numericUpDownAutoDownloadInterval";
            this.numericUpDownAutoDownloadInterval.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownAutoDownloadInterval.TabIndex = 7;
            this.numericUpDownAutoDownloadInterval.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // checkBoxDeleteAfterDownload
            // 
            this.checkBoxDeleteAfterDownload.AutoSize = true;
            this.checkBoxDeleteAfterDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.checkBoxDeleteAfterDownload.Location = new System.Drawing.Point(176, 56);
            this.checkBoxDeleteAfterDownload.Name = "checkBoxDeleteAfterDownload";
            this.checkBoxDeleteAfterDownload.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxDeleteAfterDownload.Size = new System.Drawing.Size(133, 17);
            this.checkBoxDeleteAfterDownload.TabIndex = 6;
            this.checkBoxDeleteAfterDownload.Text = "Delete After Download";
            this.checkBoxDeleteAfterDownload.UseVisualStyleBackColor = true;
            // 
            // labelParallelDownloads
            // 
            this.labelParallelDownloads.AutoSize = true;
            this.labelParallelDownloads.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelParallelDownloads.Location = new System.Drawing.Point(6, 55);
            this.labelParallelDownloads.Name = "labelParallelDownloads";
            this.labelParallelDownloads.Size = new System.Drawing.Size(97, 13);
            this.labelParallelDownloads.TabIndex = 5;
            this.labelParallelDownloads.Text = "Parallel Downloads";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.numericUpDown1.Location = new System.Drawing.Point(109, 53);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(48, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonDownloadPathChange
            // 
            this.buttonDownloadPathChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDownloadPathChange.Location = new System.Drawing.Point(378, 23);
            this.buttonDownloadPathChange.Name = "buttonDownloadPathChange";
            this.buttonDownloadPathChange.Size = new System.Drawing.Size(75, 23);
            this.buttonDownloadPathChange.TabIndex = 3;
            this.buttonDownloadPathChange.Text = "Change...";
            this.buttonDownloadPathChange.UseVisualStyleBackColor = true;
            this.buttonDownloadPathChange.Click += new System.EventHandler(this.buttonDownloadPathChange_Click);
            // 
            // textBoxDownloadPath
            // 
            this.textBoxDownloadPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDownloadPath.Location = new System.Drawing.Point(8, 25);
            this.textBoxDownloadPath.Name = "textBoxDownloadPath";
            this.textBoxDownloadPath.ReadOnly = true;
            this.textBoxDownloadPath.Size = new System.Drawing.Size(364, 20);
            this.textBoxDownloadPath.TabIndex = 2;
            // 
            // checkBoxShowToolTips
            // 
            this.checkBoxShowToolTips.AutoSize = true;
            this.checkBoxShowToolTips.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.checkBoxShowToolTips.Location = new System.Drawing.Point(17, 27);
            this.checkBoxShowToolTips.Name = "checkBoxShowToolTips";
            this.checkBoxShowToolTips.Size = new System.Drawing.Size(100, 17);
            this.checkBoxShowToolTips.TabIndex = 4;
            this.checkBoxShowToolTips.Text = "Show Tool Tips";
            this.checkBoxShowToolTips.UseVisualStyleBackColor = true;
            // 
            // groupBoxGeneral
            // 
            this.groupBoxGeneral.Controls.Add(this.checkBox1);
            this.groupBoxGeneral.Controls.Add(this.checkBoxShowToolTips);
            this.groupBoxGeneral.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.groupBoxGeneral.Location = new System.Drawing.Point(280, 12);
            this.groupBoxGeneral.Name = "groupBoxGeneral";
            this.groupBoxGeneral.Size = new System.Drawing.Size(191, 82);
            this.groupBoxGeneral.TabIndex = 5;
            this.groupBoxGeneral.TabStop = false;
            this.groupBoxGeneral.Text = "General";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.checkBox1.Location = new System.Drawing.Point(17, 50);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(128, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Auto Start Application";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // FormPutioSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(483, 321);
            this.Controls.Add(this.groupBoxGeneral);
            this.Controls.Add(this.groupBoxDownloads);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBoxAuthentication);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(499, 360);
            this.MinimumSize = new System.Drawing.Size(499, 360);
            this.Name = "FormPutioSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Put.io Manager - Settings";
            this.Load += new System.EventHandler(this.PutioSettings_Load);
            this.groupBoxAuthentication.ResumeLayout(false);
            this.groupBoxAuthentication.PerformLayout();
            this.groupBoxDownloads.ResumeLayout(false);
            this.groupBoxDownloads.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoDownloadInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBoxGeneral.ResumeLayout(false);
            this.groupBoxGeneral.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxAuthentication;
        private System.Windows.Forms.Button buttonGetToken;
        private System.Windows.Forms.TextBox textBoxToken;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.GroupBox groupBoxDownloads;
        private System.Windows.Forms.Button buttonDownloadPathChange;
        private System.Windows.Forms.TextBox textBoxDownloadPath;
        private System.Windows.Forms.CheckBox checkBoxShowToolTips;
        private System.Windows.Forms.GroupBox groupBoxGeneral;
        private System.Windows.Forms.Label labelParallelDownloads;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.CheckBox checkBoxDeleteAfterDownload;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label labelAutoDownloadInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownAutoDownloadInterval;
    }
}