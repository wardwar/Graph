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

        private List<Point> Tma = new List<Point>();

        public void passList(List<Point> tma)
        {
            this.Tma = tma;
        }

        private void formGen_Load(object sender, EventArgs e)
        {
            dgTMA.DataSource = Tma;
        }


    }
}
