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
        struct realValue
        {
            public int Tinggi;
            public int jam;
        }
        private List<realValue> Tma = new List<realValue>();
        private List<Point> data = new List<Point>();
        public formGen()
        {
            InitializeComponent();
        }
        
        

        public void passList(List<Point> tma)
        {
            this.data = tma;
        }

        public void convert()
        {
            realValue real;
            for(int i=0; i<data.Count -1;i++)
            {
                real.Tinggi = data[i].X;
                real.jam = data[i].Y;
                Tma.Add(real);
            }
            
        }

        private void formGen_Load(object sender, EventArgs e)
        {
            convert();
            dgTMA.DataSource = data;
            listBox1.DataSource = Tma;
        }


    }
}
