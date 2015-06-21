using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace skripsi
{
    public partial class Value : Form
    {
        List<Point> x = new List<Point>();
        List<Point> y = new List<Point>();
        List<Point> xya = new List<Point>();
        List<Point> xyb = new List<Point>();
        List<Point> black = new List<Point>();

        public Value()
        {
            InitializeComponent();
        }

        public void sumbu(List<Point> x, List<Point> y, List<Point> xya, List<Point> xyb)
        {
            this.x = x;
            this.y = y;
            this.xya = xyb;
            this.xya = xyb;

            listBox3.DataSource = x;
            listBox4.DataSource = y;
            listBox1.DataSource = xya;
            listBox2.DataSource = xyb;
        }

        public void itsBlack(List<Point> black)
        {
            this.black = black;
            listBox5.DataSource = black;
        }
    }
}
