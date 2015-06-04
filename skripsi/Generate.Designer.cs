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
            this.dgTMA = new System.Windows.Forms.DataGridView();
            this.listBox1 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgTMA)).BeginInit();
            this.SuspendLayout();
            // 
            // dgTMA
            // 
            this.dgTMA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTMA.Location = new System.Drawing.Point(-2, 0);
            this.dgTMA.Name = "dgTMA";
            this.dgTMA.Size = new System.Drawing.Size(725, 150);
            this.dgTMA.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 156);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(582, 95);
            this.listBox1.TabIndex = 1;
            // 
            // formGen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 262);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.dgTMA);
            this.Name = "formGen";
            this.Text = "Generate Data";
            this.Load += new System.EventHandler(this.formGen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgTMA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgTMA;
        private System.Windows.Forms.ListBox listBox1;


    }
}