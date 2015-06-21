namespace skripsi
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuDigitasi = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tandaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sumbuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prosesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deteksiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lb1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbDeteksi = new System.Windows.Forms.ProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.openGambar = new System.Windows.Forms.OpenFileDialog();
            this.dtPasang = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbPos = new System.Windows.Forms.Label();
            this.lbPetugas = new System.Windows.Forms.Label();
            this.menuDigitasi.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // menuDigitasi
            // 
            this.menuDigitasi.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.tandaToolStripMenuItem,
            this.prosesToolStripMenuItem});
            this.menuDigitasi.Location = new System.Drawing.Point(0, 0);
            this.menuDigitasi.Name = "menuDigitasi";
            this.menuDigitasi.Size = new System.Drawing.Size(851, 24);
            this.menuDigitasi.TabIndex = 0;
            this.menuDigitasi.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tandaToolStripMenuItem
            // 
            this.tandaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sumbuToolStripMenuItem});
            this.tandaToolStripMenuItem.Enabled = false;
            this.tandaToolStripMenuItem.Name = "tandaToolStripMenuItem";
            this.tandaToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.tandaToolStripMenuItem.Text = "Tanda";
            // 
            // sumbuToolStripMenuItem
            // 
            this.sumbuToolStripMenuItem.Name = "sumbuToolStripMenuItem";
            this.sumbuToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.sumbuToolStripMenuItem.Text = "Sumbu";
            this.sumbuToolStripMenuItem.Click += new System.EventHandler(this.sumbuToolStripMenuItem_Click);
            // 
            // prosesToolStripMenuItem
            // 
            this.prosesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deteksiToolStripMenuItem,
            this.generateToolStripMenuItem});
            this.prosesToolStripMenuItem.Enabled = false;
            this.prosesToolStripMenuItem.Name = "prosesToolStripMenuItem";
            this.prosesToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.prosesToolStripMenuItem.Text = "Proses";
            // 
            // deteksiToolStripMenuItem
            // 
            this.deteksiToolStripMenuItem.Name = "deteksiToolStripMenuItem";
            this.deteksiToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.deteksiToolStripMenuItem.Text = "Deteksi";
            this.deteksiToolStripMenuItem.Click += new System.EventHandler(this.deteksiToolStripMenuItem_Click);
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.Enabled = false;
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.generateToolStripMenuItem.Text = "Generate";
            this.generateToolStripMenuItem.Click += new System.EventHandler(this.generateToolStripMenuItem_Click);
            // 
            // lb1
            // 
            this.lb1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb1.AutoSize = true;
            this.lb1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lb1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb1.Location = new System.Drawing.Point(514, 6);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(52, 13);
            this.lb1.TabIndex = 1;
            this.lb1.Text = "Petugas :";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.Controls.Add(this.pbDeteksi);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.picCanvas);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(851, 472);
            this.panel1.TabIndex = 2;
            // 
            // pbDeteksi
            // 
            this.pbDeteksi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDeteksi.Location = new System.Drawing.Point(0, 0);
            this.pbDeteksi.Name = "pbDeteksi";
            this.pbDeteksi.Size = new System.Drawing.Size(851, 2);
            this.pbDeteksi.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(450, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(447, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(444, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(441, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "label3";
            // 
            // picCanvas
            // 
            this.picCanvas.BackColor = System.Drawing.SystemColors.Highlight;
            this.picCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCanvas.Location = new System.Drawing.Point(0, 0);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(851, 472);
            this.picCanvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCanvas.TabIndex = 0;
            this.picCanvas.TabStop = false;
            this.picCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.picCanvas_Paint);
            this.picCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseDown);
            this.picCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseMove_NotDrawing);
            // 
            // openGambar
            // 
            this.openGambar.FileName = "openFileDialog1";
            // 
            // dtPasang
            // 
            this.dtPasang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtPasang.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPasang.Location = new System.Drawing.Point(402, 2);
            this.dtPasang.Name = "dtPasang";
            this.dtPasang.Size = new System.Drawing.Size(95, 20);
            this.dtPasang.TabIndex = 6;
            this.dtPasang.Value = new System.DateTime(2015, 6, 21, 0, 0, 0, 0);
            this.dtPasang.ValueChanged += new System.EventHandler(this.dtPasang_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(305, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Tanggal Pasang :";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(650, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Pos :";
            // 
            // lbPos
            // 
            this.lbPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPos.AutoSize = true;
            this.lbPos.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lbPos.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbPos.Location = new System.Drawing.Point(687, 6);
            this.lbPos.Name = "lbPos";
            this.lbPos.Size = new System.Drawing.Size(24, 13);
            this.lbPos.TabIndex = 9;
            this.lbPos.Text = "pos";
            this.lbPos.Visible = false;
            // 
            // lbPetugas
            // 
            this.lbPetugas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPetugas.AutoSize = true;
            this.lbPetugas.BackColor = System.Drawing.Color.Transparent;
            this.lbPetugas.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbPetugas.Location = new System.Drawing.Point(571, 7);
            this.lbPetugas.Name = "lbPetugas";
            this.lbPetugas.Size = new System.Drawing.Size(27, 13);
            this.lbPetugas.TabIndex = 10;
            this.lbPetugas.Text = "ptgs";
            this.lbPetugas.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 496);
            this.Controls.Add(this.lbPetugas);
            this.Controls.Add(this.lbPos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtPasang);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lb1);
            this.Controls.Add(this.menuDigitasi);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuDigitasi;
            this.Name = "Main";
            this.Text = "Digitasi Grafik";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuDigitasi.ResumeLayout(false);
            this.menuDigitasi.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuDigitasi;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tandaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sumbuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prosesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deteksiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.OpenFileDialog openGambar;
        private System.Windows.Forms.DateTimePicker dtPasang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbPos;
        private System.Windows.Forms.Label lbPetugas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar pbDeteksi;
    }
}