using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Drawing2D;

namespace skripsi
{
    public partial class formGen : Form
    {
        public formGen()
        {
            InitializeComponent();
        }

        private List<Point> Scans = new List<Point>();

        public void passList(List<Point> scan)
        {
            this.Scans = scan;
        }

        private void formGen_Load(object sender, EventArgs e)
        {
            listPolyline.DataSource = Scans;
        }


    }
}
