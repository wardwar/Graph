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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formGen));
            this.dgTMA = new System.Windows.Forms.DataGridView();
            this.btnKirim = new System.Windows.Forms.Button();
            this.logo = new System.Windows.Forms.PictureBox();
            this.pbInsert = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgTMA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // dgTMA
            // 
            this.dgTMA.AllowUserToDeleteRows = false;
            this.dgTMA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTMA.Location = new System.Drawing.Point(-2, 0);
            this.dgTMA.Name = "dgTMA";
            this.dgTMA.Size = new System.Drawing.Size(368, 502);
            this.dgTMA.TabIndex = 0;
            // 
            // btnKirim
            // 
            this.btnKirim.Font = new System.Drawing.Font("Roboto Medium", 10F, System.Drawing.FontStyle.Bold);
            this.btnKirim.Location = new System.Drawing.Point(372, 121);
            this.btnKirim.Name = "btnKirim";
            this.btnKirim.Size = new System.Drawing.Size(177, 48);
            this.btnKirim.TabIndex = 1;
            this.btnKirim.Text = "KIRIM";
            this.btnKirim.UseVisualStyleBackColor = true;
            this.btnKirim.Click += new System.EventHandler(this.btnKirim_Click);
            // 
            // logo
            // 
            this.logo.Image = ((System.Drawing.Image)(resources.GetObject("logo.Image")));
            this.logo.Location = new System.Drawing.Point(412, 12);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(95, 95);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logo.TabIndex = 6;
            this.logo.TabStop = false;
            // 
            // pbInsert
            // 
            this.pbInsert.Location = new System.Drawing.Point(372, 175);
            this.pbInsert.Name = "pbInsert";
            this.pbInsert.Size = new System.Drawing.Size(177, 23);
            this.pbInsert.TabIndex = 7;
            this.pbInsert.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(373, 205);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // formGen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(555, 500);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbInsert);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.btnKirim);
            this.Controls.Add(this.dgTMA);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formGen";
            this.Text = "Generate Data";
            this.Load += new System.EventHandler(this.formGen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgTMA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgTMA;
        private System.Windows.Forms.Button btnKirim;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.ProgressBar pbInsert;
        private System.Windows.Forms.Label label1;


    }
}