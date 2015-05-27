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
            // formGen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 262);
            this.Controls.Add(this.dgTMA);
            this.Name = "formGen";
            this.Text = "Generate Data";
            this.Load += new System.EventHandler(this.formGen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgTMA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgTMA;


    }
}