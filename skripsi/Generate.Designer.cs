namespace skripsi
{
    partial class formGen
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
            this.listPolyline = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listPolyline
            // 
            this.listPolyline.FormattingEnabled = true;
            this.listPolyline.Location = new System.Drawing.Point(12, 12);
            this.listPolyline.Name = "listPolyline";
            this.listPolyline.Size = new System.Drawing.Size(260, 95);
            this.listPolyline.TabIndex = 1;
            // 
            // formGen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.listPolyline);
            this.Name = "formGen";
            this.Text = "Generate Data";
            this.Load += new System.EventHandler(this.formGen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listPolyline;

    }
}