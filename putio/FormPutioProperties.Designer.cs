namespace putio
{
    partial class FormPutioProperties
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
            this.labelProperties = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelProperties
            // 
            this.labelProperties.BackColor = System.Drawing.Color.White;
            this.labelProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProperties.Location = new System.Drawing.Point(8, 8);
            this.labelProperties.Name = "labelProperties";
            this.labelProperties.Size = new System.Drawing.Size(312, 319);
            this.labelProperties.TabIndex = 0;
            this.labelProperties.Click += new System.EventHandler(this.FormPutioProperties_Click);
            // 
            // FormPutioProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(328, 335);
            this.Controls.Add(this.labelProperties);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPutioProperties";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormPutioProperties";
            this.Click += new System.EventHandler(this.FormPutioProperties_Click);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label labelProperties;
    }
}