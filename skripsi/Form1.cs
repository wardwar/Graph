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
    public partial class Form1 : Form
    {
        //lebar radius titik
        private const int object_radius = 3;

        //wilayah hit cursor pada object
        private const int wilayah_object = object_radius * object_radius;

        //4 titik penanda wilayah sumbu kurva
        private List<Point> WilayahSumbu = new List<Point>();

        private List<Point> Scan = null;
        //new sumbu
        private List<Point> NewSumbu = null;

        private List<Point> SumbuX = new List<Point>();
        private List<Point> SumbuY = new List<Point>();

        private List<Point> xya = null;
        private List<Point> xyb = null;
        
        //temp untuk newpoint pada pembuatan sebuah titik
        private Point NewPoint;

        //mendeskripsikan tombol mana yng dipilih
        private string tombol;

        private List<Point> MovingPolyline = null;
        private int MovingPoint = -1;
        private int OffsetX, OffsetY;

        public Form1()
        {
            InitializeComponent();
            openFileImage.Title = "Load Grafik";
            openFileImage.Filter = "JPEG Image (.jpg) | *jpg";
        }
        
        //Aksi dari tombol generate
        private void btnGen_Click(object sender, EventArgs e)
        {
            formGen f = new formGen();
            f.passList(this.Scan);
            f.Show();
        }


        //aksi pada saat load aplikasi
        private void Form1_Load(object sender, EventArgs e)
        {
            btnGen.Enabled = false;
            btnSumbu.Enabled = false;
        }

        //tombol untuk load gambar
        private void btnLoad_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileImage.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = openFileImage.FileName;
                picCanvas.Image = new Bitmap(filePath);

                //clean area dari Point
                WilayahSumbu.Clear();

                //nonaktif tombol generate 
                btnGen.Enabled = false;

                //aktifkan tombol
                btnSumbu.Enabled = true;
               

                //status tombol
                tombol ="kosong";
            }
        }

  //--------------------------Tombol----------------------------------//
        //aksi tombol untuk 4 titik wilayah sumbu
        private void btnSumbu_Click(object sender, EventArgs e)
        {
            if (picCanvas.Image != null)
            {
                if(WilayahSumbu != null)
                {
                    tombol = "sumbu";
                }
            }
        }

        private void btnKurva_Click(object sender, EventArgs e)
        {
            if (picCanvas.Image != null)
            {
                if (WilayahSumbu != null)
                {
                    tombol = "Kurva";
                }
            }
        }

//--------------------------Mouse Action----------------------------------//
        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            int hit_point;
            List<Point> hit_polyline;
            if(WilayahSumbu != null)
            {
                btnGen.Enabled = true;
            }
            if (tombol == "sumbu")
            {
                if (e.Button == MouseButtons.Left)
                {
                    if(NewSumbu != null)
                    {
                        if (NewSumbu.Count == 4)
                        {
                            WilayahSumbu = NewSumbu;
                            NewSumbu = null;

                            picCanvas.MouseMove += picCanvas_MouseMove_NotDrawing;
                            picCanvas.MouseMove -= picCanvas_MouseMove_Drawing;
                        }
                        else
                        {
                            if (NewSumbu.Count > 0)
                            {
                                if (NewSumbu[NewSumbu.Count - 1] != e.Location)
                                {
                                    NewSumbu.Add(e.Location);
                                }
                            }
                            else
                            {
                                NewSumbu.Add(e.Location);
                                
                            }
                        }
                    }
                    else if (MouseTitik(e.Location, out hit_point, out hit_polyline, WilayahSumbu))
                    {
                        picCanvas.MouseMove -= picCanvas_MouseMove_NotDrawing;
                        picCanvas.MouseMove += picCanvas_MouseMove_MovingCorner;
                        picCanvas.MouseUp += picCanvas_MouseUp_MovingCorner;

                        // Remember the polygon and point number.
                        MovingPolyline = hit_polyline;
                        MovingPoint = hit_point;
                        label1.Text = MovingPoint.ToString();

                        // Remember the offset from the mouse to the point.
                        OffsetX = hit_polyline[hit_point].X - e.X;
                        OffsetY = hit_polyline[hit_point].Y - e.Y;
                    }
                    else if (NewSumbu == null && WilayahSumbu.Count > 0)
                    {
                        picCanvas.MouseMove += picCanvas_MouseMove_NotDrawing;
                        picCanvas.MouseMove -= picCanvas_MouseMove_Drawing;
                    }
                    else if(NewSumbu == null)
                    {
                        NewSumbu = new List<Point>();
                        NewSumbu.Add(e.Location);
                        NewPoint = e.Location;

                        picCanvas.MouseMove -= picCanvas_MouseMove_NotDrawing;
                        picCanvas.MouseMove += picCanvas_MouseMove_Drawing;
                    }
                    
                    
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (NewSumbu != null)
                    {
                        if(NewSumbu.Count != 0)
                        NewSumbu.RemoveAt(NewSumbu.Count - 1);
                        picCanvas.Invalidate();
                    }
                }
            }
            else if(picCanvas.Image == null)
            {
                string message = "Load Grafik Terlebih Dahulu!";
                string tittle = "Error";

                MessageBox.Show(message, tittle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (WilayahSumbu.Count > 0)
            {
                btnGen.Enabled = true;
            }

            picCanvas.Invalidate();
         
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            listBox1.DataSource = WilayahSumbu;
            
        }

        private void picCanvas_MouseMove_NotDrawing(object sender, MouseEventArgs e)
        {
            Cursor cursor = Cursors.Cross;
            int hit_point;
            List<Point> hit_polyline;
            if(MouseTitik(e.Location,out hit_point,out hit_polyline,WilayahSumbu))
            {
                cursor = Cursors.Arrow;
            }

            if (picCanvas.Cursor != cursor)
            {
                picCanvas.Cursor = cursor;
            }
        }

        private void picCanvas_MouseMove_MovingCorner(object sender, MouseEventArgs e)
        {
            // Move the point.
            MovingPolyline[MovingPoint] = new Point(e.X + OffsetX, e.Y + OffsetY);

            // Redraw.
            picCanvas.Invalidate();
        }

        private void picCanvas_MouseUp_MovingCorner(object sender, MouseEventArgs e)
        {
            picCanvas.MouseMove += picCanvas_MouseMove_NotDrawing;
            picCanvas.MouseMove -= picCanvas_MouseMove_MovingCorner;
            picCanvas.MouseUp -= picCanvas_MouseUp_MovingCorner;
        }

        private void picCanvas_MouseMove_Drawing(object sender, MouseEventArgs e)
        {
            NewPoint = e.Location; 
            picCanvas.Invalidate();
        }

        private bool MouseTitik(Point mouse_pt, out int hit_pt, out List<Point> hit_polyline, List<Point> Garis)
        {
            for (int i = 0; i < Garis.Count; i++)
            {
                if (AreaTitik(Garis[i], mouse_pt) < wilayah_object)
                {
                    hit_pt = i;
                    hit_polyline = Garis;
                    
                    return true;
                }
            }
            hit_pt = -1;
            hit_polyline = null;
            return false;
        }

        private int AreaTitik(Point pt1, Point pt2)
        {
            int dx = Math.Abs(pt1.X - pt2.X);
            int dy = Math.Abs(pt1.Y - pt2.Y);
            int insideCorner = dx + dy;
            Math.Abs(insideCorner);
            if (insideCorner < wilayah_object)
            {
                return insideCorner;
            }
            return wilayah_object + 1;

        }

        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if(WilayahSumbu.Count > 0)
            {
                e.Graphics.DrawPolygon(Pens.DarkMagenta, WilayahSumbu.ToArray());
            }

            //gambar titik
            foreach (Point titik in WilayahSumbu)
            {
                
                Rectangle rect = new Rectangle(
                    titik.X - object_radius, titik.Y - object_radius,
                    2 * object_radius + 1, 2 * object_radius + 1);
                e.Graphics.FillEllipse(Brushes.White, rect);
                e.Graphics.DrawEllipse(Pens.Red, rect);

            }

            if (NewSumbu != null)
            {
                if (NewSumbu.Count > 1)
                {
                    e.Graphics.DrawLines(Pens.Green, NewSumbu.ToArray());
                }

                if (NewSumbu.Count > 0 && NewSumbu.Count < 4)
                {
                    using (Pen dashed_pen = new Pen(Color.Green))
                    {
                        dashed_pen.DashPattern = new float[] { 3, 3 };
                        e.Graphics.DrawLine(dashed_pen,
                            NewSumbu[NewSumbu.Count - 1],
                            NewPoint);
                    }
                }
            }
        }

       private List<Point> plotLine(Point ptA, Point ptB)
       {
           Scan = new List<Point>();
           Point plot = new Point();
           int deltaX;
           int deltaY;
           int curDist = -1;
           int distance = -1;
           curDist = 0;
           deltaX = ptB.X - ptA.X;
           deltaY = ptB.Y - ptA.Y;
           distance = (int)Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
           plot = ptA;
           Scan.Add(plot);
           while (curDist < distance)
           {
               curDist++;
               if (curDist > 0)
               {
                   int offsetX = (int)((double)curDist / (double)distance * (double)deltaX);
                   int offsetY = (int)((double)curDist / (double)distance * (double)deltaY);
                   plot.X = ptA.X + offsetX;
                   plot.Y = ptA.Y + offsetY;
                   Scan.Add(plot);
               }
           }
           return Scan;
       }


       private void Bresenham(List<Point> Sumbu, string type)
       {
           xya = new List<Point>();
           xyb = new List<Point>();
           if (type == "sumbu")
           {
               for (int i = 0; i < Sumbu.Count - 1; i++)
               {
                   if (i < 2)
                   {
                       xya.Add(Sumbu[i]);
                   }
                   if (i > 0)
                   {
                       xyb.Add(Sumbu[i]);
                   }
               }
           }

           for (int i = 0; i < xya.Count; i++)
           {
               plotLine(xya[i], xyb[i]);
               richTextBox1.AppendText(xya[i].ToString());
               richTextBox1.AppendText(xyb[i].ToString());

               if (type == "sumbu" && i == 0)
               {
                   SumbuX = plotLine(xya[i], xyb[i]);

               }
               else if (type == "sumbu" && i == 1)
               {
                   SumbuY = plotLine(xya[i], xyb[i]); ;
               }
           }
           listBox1.DataSource = SumbuX;
           listBox2.DataSource = SumbuY;
       }

       

        private void label1_Click(object sender, EventArgs e)
        {
            listBox1.DataSource = SumbuX;
            listBox2.DataSource = SumbuY;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Bresenham(WilayahSumbu,"sumbu");
        }


    }
}
