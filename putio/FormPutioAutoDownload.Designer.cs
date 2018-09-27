namespace putio
{
    partial class FormPutioAutoDownload
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
            this.textBoxMinFileSize = new System.Windows.Forms.TextBox();
            this.labelMinFileSize = new System.Windows.Forms.Label();
            this.labelExtensions = new System.Windows.Forms.Label();
            this.textBoxExtensions = new System.Windows.Forms.TextBox();
            this.checkBoxKeepFolderStructure = new System.Windows.Forms.CheckBox();
            this.buttonAddAutoDownload = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxMinFileSize
            // 
            this.textBoxMinFileSize.Location = new System.Drawing.Point(12, 25);
            this.textBoxMinFileSize.Name = "textBoxMinFileSize";
            this.textBoxMinFileSize.Size = new System.Drawing.Size(100, 20);
            this.textBoxMinFileSize.TabIndex = 0;
            this.textBoxMinFileSize.Text = "300";
            // 
            // labelMinFileSize
            // 
            this.labelMinFileSize.AutoSize = true;
            this.labelMinFileSize.Location = new System.Drawing.Point(12, 9);
            this.labelMinFileSize.Name = "labelMinFileSize";
            this.labelMinFileSize.Size = new System.Drawing.Size(89, 13);
            this.labelMinFileSize.TabIndex = 1;
            this.labelMinFileSize.Text = "Min File Size (mb)";
            // 
            // labelExtensions
            // 
            this.labelExtensions.AutoSize = true;
            this.labelExtensions.Location = new System.Drawing.Point(12, 49);
            this.labelExtensions.Name = "labelExtensions";
            this.labelExtensions.Size = new System.Drawing.Size(98, 13);
            this.labelExtensions.TabIndex = 3;
            this.labelExtensions.Text = "Allowed Extensions";
            // 
            // textBoxExtensions
            // 
            this.textBoxExtensions.Location = new System.Drawing.Point(12, 65);
            this.textBoxExtensions.Name = "textBoxExtensions";
            this.textBoxExtensions.Size = new System.Drawing.Size(261, 20);
            this.textBoxExtensions.TabIndex = 2;
            this.textBoxExtensions.Text = ".txt,.avi,.mov";
            // 
            // checkBoxKeepFolderStructure
            // 
            this.checkBoxKeepFolderStructure.AutoSize = true;
            this.checkBoxKeepFolderStructure.Location = new System.Drawing.Point(157, 25);
            this.checkBoxKeepFolderStructure.Name = "checkBoxKeepFolderStructure";
            this.checkBoxKeepFolderStructure.Size = new System.Drawing.Size(116, 17);
            this.checkBoxKeepFolderStructure.TabIndex = 4;
            this.checkBoxKeepFolderStructure.Text = "Keep File Structure";
            this.checkBoxKeepFolderStructure.UseVisualStyleBackColor = true;
            // 
            // buttonAddAutoDownload
            // 
            this.buttonAddAutoDownload.Location = new System.Drawing.Point(157, 91);
            this.buttonAddAutoDownload.Name = "buttonAddAutoDownload";
            this.buttonAddAutoDownload.Size = new System.Drawing.Size(116, 23);
            this.buttonAddAutoDownload.TabIndex = 5;
            this.buttonAddAutoDownload.Text = "Add Auto Download";
            this.buttonAddAutoDownload.UseVisualStyleBackColor = true;
            this.buttonAddAutoDownload.Click += new System.EventHandler(this.buttonAddAutoDownload_Click);
            // 
            // FormPutioAutoDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 121);
            this.Controls.Add(this.buttonAddAutoDownload);
            this.Controls.Add(this.checkBoxKeepFolderStructure);
            this.Controls.Add(this.labelExtensions);
            this.Controls.Add(this.textBoxExtensions);
            this.Controls.Add(this.labelMinFileSize);
            this.Controls.Add(this.textBoxMinFileSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPutioAutoDownload";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Auto Download";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelMinFileSize;
        private System.Windows.Forms.Label labelExtensions;
        private System.Windows.Forms.Button buttonAddAutoDownload;
        private System.Windows.Forms.TextBox textBoxMinFileSize;
        private System.Windows.Forms.TextBox textBoxExtensions;
        private System.Windows.Forms.CheckBox checkBoxKeepFolderStructure;
    }
}