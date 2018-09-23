namespace putio
{
    partial class FormPutioAuthWeb
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelInfo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.webBrowserUserAuth = new System.Windows.Forms.WebBrowser();
            this.labelAddress = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 406);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(858, 74);
            this.panel1.TabIndex = 0;
            // 
            // labelInfo
            // 
            this.labelInfo.BackColor = System.Drawing.Color.Transparent;
            this.labelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfo.Location = new System.Drawing.Point(0, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(856, 72);
            this.labelInfo.TabIndex = 1;
            this.labelInfo.Text = "Sign in to get your authentication token!\r\nClick Here This Banner To Close";
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelInfo.Click += new System.EventHandler(this.labelInfo_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Controls.Add(this.labelAddress);
            this.panel2.Controls.Add(this.webBrowserUserAuth);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(8);
            this.panel2.Size = new System.Drawing.Size(858, 406);
            this.panel2.TabIndex = 1;
            // 
            // webBrowserUserAuth
            // 
            this.webBrowserUserAuth.AllowWebBrowserDrop = false;
            this.webBrowserUserAuth.Dock = System.Windows.Forms.DockStyle.Top;
            this.webBrowserUserAuth.Location = new System.Drawing.Point(8, 8);
            this.webBrowserUserAuth.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserUserAuth.Name = "webBrowserUserAuth";
            this.webBrowserUserAuth.ScriptErrorsSuppressed = true;
            this.webBrowserUserAuth.Size = new System.Drawing.Size(842, 363);
            this.webBrowserUserAuth.TabIndex = 2;
            // 
            // labelAddress
            // 
            this.labelAddress.BackColor = System.Drawing.Color.White;
            this.labelAddress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelAddress.Location = new System.Drawing.Point(8, 375);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(842, 23);
            this.labelAddress.TabIndex = 4;
            this.labelAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormPutioAuthWeb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 480);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPutioAuthWeb";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PutioAuthWeb";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.WebBrowser webBrowserUserAuth;
        private System.Windows.Forms.Label labelInfo;
        public System.Windows.Forms.Label labelAddress;
    }
}