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

        private List<List<Point>> Polylines = new List<List<Point>>();

        public void passList(List<List<Point>> Polylines)
        {
            this.Polylines = Polylines;
        }

        private void formGen_Load(object sender, EventArgs e)
        {
            foreach (List<Point> polyline in Polylines)
            {
                // Draw the polygon.


                listPolyline.DataSource = polyline;

                // Draw the corners.
               
            }
        }


    }
}
