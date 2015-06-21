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
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace skripsi
{

    public partial class Main : Form
    {

        //variable Image Processing
        Mean smooting = null;
         Threshold threshold = null;
  
        //lebar radius titik
        private const int object_radius = 3;
        private List<Point> tma = new List<Point>();
        private List<Point> cekx = new List<Point>();
        private List<Point> ceky = new List<Point>();
        
        //wilayah hit cursor pada object
        private const int wilayah_object = object_radius * object_radius;
        private List<Point> Scan = null;

        //-------------------------variabel sumbu----------------------------------//
        //4 titik penanda wilayah sumbu kurva
        private List<Point> WilayahSumbu = new List<Point>();
        private List<Point> NewSumbu = null;
        private List<Point> SumbuX = new List<Point>();
        private List<Point> SumbuY = new List<Point>();
        private List<Point> Atas = new List<Point>();
        private List<Point> Kanan = new List<Point>();
        private List<Point> xya = null;
        private List<Point> xyb = null;


        //--------------------------variabel kurva----------------------------------//
        private List<Point> Kurva = new List<Point>();
        private List<Point> KurvaScan = new List<Point>();

        //temp untuk newpoint pada pembuatan sebuah titik
        private Point NewPoint;

        //mendeskripsikan tombol mana yng dipilih
        private string tombol;

        private List<thistma> test = new List<thistma>();

        private List<Point> MovingPolyline = null;
        private int MovingPoint = -1;
        private int OffsetX, OffsetY;
        private double y4 = -1, x4 = -1,y5 = -1 , x5 = -1;

        private int id_petugas;
        private formGen f = new formGen();



        Point find = new Point();
        bool damn = false;
        public Main()
        {
            InitializeComponent();
            openFileImage.Title = "Load Grafik";
            openFileImage.Filter = "Image Files|*.jpg;*.png";
            style();
            dtPasang.Value = DateTime.Today;

        }

        public void id_pet(int id_petugas)
        {
            this.id_petugas = id_petugas;
        }

        private void style()
        {
            btnLoad.ForeColor = Color.White;
            btnLoad.FlatStyle = FlatStyle.Flat;
            btnLoad.BackColor = Color.FromArgb(33, 150, 243);
            btnLoad.FlatAppearance.BorderColor = Color.FromArgb(33, 150, 243);
            btnLoad.FlatAppearance.BorderSize = 1;

            btnSumbu.ForeColor = Color.White;
            btnSumbu.FlatStyle = FlatStyle.Flat;
            btnSumbu.BackColor = Color.FromArgb(33, 150, 243);
            btnSumbu.FlatAppearance.BorderColor = Color.FromArgb(33, 150, 243);
            btnSumbu.FlatAppearance.BorderSize = 1;


            btnGen.ForeColor = Color.White;
            btnGen.FlatStyle = FlatStyle.Flat;
            btnGen.BackColor = Color.FromArgb(33, 150, 243);
            btnGen.FlatAppearance.BorderColor = Color.FromArgb(33, 150, 243);
            btnGen.FlatAppearance.BorderSize = 1;
        }
        
        //Aksi dari tombol generate
        private void btnGen_Click(object sender, EventArgs e)
        {
           
            test.Clear();
            int x0 = WilayahSumbu[1].X;
            int x1 = WilayahSumbu[0].X;
            int x2 = WilayahSumbu[2].X;
            int x3;
            int y0 = WilayahSumbu[1].Y;
            int y1 = WilayahSumbu[0].Y;
            int y2 = WilayahSumbu[2].Y;
            int y3;
            int jam, menit, dt;
            double a, a1, b, b1, x, y,belakangkoma;
            DateTime tanggal;
            Point point = new Point();
            thistma testpoint;

            double m1 = ((double)y0 - (double)y1) / ((double)x0 - (double)x1);
            double m2 = ((double)y2 - (double)y0) / ((double)x2 - (double)x0);

            for (int i = 0; i < KurvaScan.Count; i++)
            {
                x3 = KurvaScan[i].X;
                y3 = KurvaScan[i].Y;

                x4 = ((m2 * x0) - (m1 * x3) + y3 - y0) / (m2 - m1);
                y4 = (m2 * x4) + y0 - (m2 * x0);
                point.X = (int)x4;
                point.Y = (int)y4;
                cekx.Add(point);
                x5 = ((m1 * x0) - (m2 * x3) + y3 - y0) / (m1 - m2);
                y5 =(m1 * x5) + y0 - (m1 * x0);
                point.X = (int)x5;
                point.Y = (int)y5;
                ceky.Add(point);

                a = Math.Sqrt(Math.Pow(x4 - x0, 2) + Math.Pow(y4 - y0, 2));
                a1 = Math.Sqrt(Math.Pow(x2 - x0, 2) + Math.Pow(y2 - y0, 2));
                x = Math.Round(186 * (double)a / (double)a1, 2);
                belakangkoma = Math.Round(x % (int)x, 2);
                menit = (int)(belakangkoma * 60);
                jam = (int)x;
                
                dt = jam / 24;
                jam = jam % 24;
                
                tanggal = dtPasang.Value;
                tanggal = tanggal.AddDays(dt);
                

                b = Math.Sqrt(Math.Pow(x5 - x0, 2) + Math.Pow(y5 - y0, 2));
                b1 = Math.Sqrt(Math.Pow(x1 - x0, 2) + Math.Pow(y1 - y0, 2));
                y = Math.Round(250 * (double)b / (double)b1, 3);
                
                point.X = 186 * (int)a / (int)a1;
                point.Y = 250 *  (int)b / (int)b1;
                testpoint = new thistma(tanggal.ToString(), jam, menit, y);
                test.Add(testpoint);
                tma.Add(point);
                //richTextBox2.AppendText("x5 : " + x5 + " ");
                //richTextBox2.AppendText("y5 : " + y5 + " ");
                //richTextBox2.AppendText("x4 : " + x4 + " ");
                //richTextBox2.AppendText("y4 : " + y4 + " ");
                //richTextBox2.AppendText("x : " + point.X + ", " + "y : " + point.Y + "             ");
            }

            //picCanvas.Invalidate();
            
            f.passList(this.test);
            f.id_pet(id_petugas);
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
                    btnSumbu.BackColor = Color.FromArgb(46, 204, 113);
                    btnSumbu.FlatStyle = FlatStyle.Flat;
                    btnSumbu.FlatAppearance.BorderColor = Color.FromArgb(46, 204, 113);
                    btnSumbu.FlatAppearance.BorderSize = 1;
                    btnSumbu.ForeColor = Color.White;
                    tombol = "sumbu";
                }
            }
        }

//--------------------------Mouse Action----------------------------------//
        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            int hit_point;
            List<Point> hit_polyline;
            if (tombol == "sumbu")
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (NewSumbu != null)
                    {
                        if (NewSumbu.Count == 4)
                        {
                            WilayahSumbu = NewSumbu;
                            NewSumbu = null;
                            Plotline(WilayahSumbu, "sumbu");

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
                        richTextBox1.Clear();
                        Plotline(WilayahSumbu, "sumbu");
                        listBox1.DataSource = SumbuX;
                        listBox2.DataSource = SumbuY;
                    }
                    else if (NewSumbu == null && WilayahSumbu.Count > 0)
                    {
                        picCanvas.MouseMove += picCanvas_MouseMove_NotDrawing;
                        picCanvas.MouseMove -= picCanvas_MouseMove_Drawing;
                    }
                    else if (NewSumbu == null)
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
                        if (NewSumbu.Count != 0)
                            NewSumbu.RemoveAt(NewSumbu.Count - 1);
                        picCanvas.Invalidate();
                    }
                }
            }
            else if (tombol == "kurva")
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (NewSumbu != null)
                    {
                        if (e.Clicks == 2)
                        {
                            if (NewSumbu.Count > 2)
                            {
                                Kurva = NewSumbu;
                                NewSumbu = null;
                                Plotline(Kurva, "kurva");
                                picCanvas.MouseMove += picCanvas_MouseMove_NotDrawing;
                                picCanvas.MouseMove -= picCanvas_MouseMove_Drawing;
                            }
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
                    else if (MouseTitik(e.Location, out hit_point, out hit_polyline, Kurva))
                    {
                        KurvaScan.Clear();
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
                        richTextBox1.Clear();
                        Plotline(Kurva, "kurva");
                        listBox1.DataSource = KurvaScan;
                        

                    }
                    else if (NewSumbu == null && Kurva.Count > 0)
                    {
                        picCanvas.MouseMove += picCanvas_MouseMove_NotDrawing;
                        picCanvas.MouseMove -= picCanvas_MouseMove_Drawing;
                    }
                    else if (NewSumbu == null)
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
                        if (NewSumbu.Count != 0)
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

            if(WilayahSumbu.Count>0 && Kurva.Count >0)
            {
                if(dtPasang.Value.Date != DateTime.Today && dtPasang.Value.Date < DateTime.Today)
                {
                    btnGen.Enabled = true;
                }
                
                //btnBug.Visible = true;
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
            if (MouseTitik(e.Location, out hit_point, out hit_polyline, WilayahSumbu) || MouseTitik(e.Location, out hit_point, out hit_polyline, Kurva))
            {
                cursor = Cursors.Arrow;
            }

            if (picCanvas.Cursor != cursor)
            {
                picCanvas.Cursor = cursor;
            }
            
            label2.Text = picCanvas.PointToClient(Cursor.Position).ToString();
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
                    label1.Text = Garis[i].ToString();
                    label2.Text = hit_pt.ToString();
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
            //if (x4 != -1)
            //{
                
            //    e.Graphics.DrawLine(Pens.DarkViolet, (float)x4, (float)y4, Kurva[1].X, Kurva[1].Y);
            //    e.Graphics.DrawLine(Pens.DarkViolet, (float)x5, (float)y5, Kurva[1].X, Kurva[1].Y);
            //}
            //if(cekx.Count != 0)
            //{
            //    e.Graphics.DrawLine(Pens.DarkViolet, cekx[1], KurvaScan[1]);
            //    e.Graphics.DrawLine(Pens.DarkViolet, ceky[1], KurvaScan[1]);
            //}
            //if (damn == true)
            //{
            //    e.Graphics.DrawLine(Pens.DarkViolet, find, Kurva[1]);

            //}
            if (Kurva.Count > 0)
            {
                e.Graphics.DrawLines(Pens.DarkBlue, Kurva.ToArray());
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
            foreach (Point titik in Kurva)
            {

                Rectangle rect = new Rectangle(
                    titik.X - object_radius, titik.Y - object_radius,
                    2 * object_radius + 1, 2 * object_radius + 1);
                e.Graphics.FillEllipse(Brushes.Black, rect);
                e.Graphics.DrawEllipse(Pens.Black, rect);

            }

            if (NewSumbu != null)
            {
                if (NewSumbu.Count > 1)
                {
                    e.Graphics.DrawLines(Pens.Green, NewSumbu.ToArray());
                }

                if (NewSumbu.Count > 0)
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

       private List<Point> Bresenham(Point ptA, Point ptB)
       {
           Scan = new List<Point>();
           Point plot = new Point();
           int x, y;
           int dx, dy;
           int incx, incy;
           int balance;
           int x1 = ptA.X;
           int y1 = ptA.Y;
           int x2 = ptB.X;
           int y2 = ptB.Y;

           if (x2 >= x1)
           {
               dx = x2 - x1;
               incx = 1;
           }
           else
           {
               dx = x1 - x2;
               incx = -1;
           }

           if (y2 >= y1)
           {
               dy = y1 - y2;
               incy = 1;
           }
           else
           {
               dy = y1 - y2;
               incy = -1;
           }

           x = x1;
           y = y1;

           if (dx >= dy)
           {
               dy <<= 1;
               balance = dy - dx;
               dx <<= 1;

               while (x != x2)
               {
                   plot.X = x;
                   plot.Y = y;
                   Scan.Add(plot);
                   if (balance >= 0)
                   {
                       y += incy;
                       balance -= dx;
                   }
                   balance += dy;
                   x += incx;
               }
               plot.X = x;
               plot.Y = y;
               Scan.Add(plot);
           }
           else
           {
               dx <<= 1;
               balance = dx - dy;
               dy <<= 1;

               while (y != y2)
               {
                   plot.X = x;
                   plot.Y = y;
                   Scan.Add(plot);

                   if (balance >= 0)
                   {
                       x += incx;
                       balance -= dy;
                   }
                   balance += dx;
                   y += incy;
               }
               plot.X = x;
               plot.Y = y;
               Scan.Add(plot);
           }
           //int deltaX;
           //int deltaY;
           //int curDist = -1;
           //int distance = -1;
           //curDist = 0;
           //deltaX = ptB.X - ptA.X;
           //deltaY = ptB.Y - ptA.Y;
           //distance = (int)Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
           //plot = ptA;
           //Scan.Add(plot);
           //while (curDist < distance)
           //{
           //    curDist++;
           //    if (curDist > 0)
           //    {
           //        int offsetX = (int)((double)curDist / (double)distance * (double)deltaX);
           //        int offsetY = (int)((double)curDist / (double)distance * (double)deltaY);
           //        plot.X = ptA.X + offsetX;
           //        plot.Y = ptA.Y + offsetY;
           //        Scan.Add(plot);
           //    }
           //}
          return Scan;
       }


       private void Plotline(List<Point> Garis, string type)
       {
           xya = new List<Point>();
           xyb = new List<Point>();
           if (type == "sumbu")
           {
               for (int i = 0; i < Garis.Count - 1; i++)
               {
                   if (i < 2)
                   {
                       xya.Add(Garis[i]);
                   }
                   if (i > 0)
                   {
                       xyb.Add(Garis[i]);
                   }
               }
           }
           else if (type == "kurva")
           {
               for (int i = 0; i < Garis.Count; i++)
               {
                   if (i < Garis.Count - 1)
                   {
                       xya.Add(Garis[i]);
                   }
                   if (i > 0)
                   {
                       xyb.Add(Garis[i]);
                   }
               }
           }
           else if(type == "test")
           {
               xya.Add(Garis[0]);
               xya.Add(Garis[3]);
               xyb.Add(Garis[3]);
               xyb.Add(Garis[2]);
           }

           for (int i = 0; i < xya.Count; i++)
           {
               richTextBox1.AppendText(xyb[i].ToString());

               if (type == "sumbu" && i == 0)
               {
                   SumbuY = Bresenham(xya[i], xyb[i]);

               }
               else if (type == "sumbu" && i == 1)
               {
                   SumbuX = Bresenham(xya[i], xyb[i]); ;
               }

               if (type == "kurva")
               {
                   KurvaScan.AddRange(Bresenham(xya[i], xyb[i]));
               }
               if (type == "test")
               {
                   if (i == 0)
                   {
                       Atas = Bresenham(xya[i], xyb[i]);

                   }
                   else if (i == 1)
                   {
                       Kanan = Bresenham(xya[i], xyb[i]); ;
                   }
               }
           }
               if (type == "kurva")
               {
                   KurvaScan = KurvaScan.Distinct().ToList();
               }

           
               listBox1.DataSource = KurvaScan;
               listBox2.DataSource = Atas;
          
       }

       

        private void label1_Click(object sender, EventArgs e)
        {
            listBox1.DataSource = SumbuX;
            listBox2.DataSource = SumbuY;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Plotline(WilayahSumbu,"sumbu");
            Plotline(WilayahSumbu, "test");
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnBug_Click(object sender, EventArgs e)
        {
            double m1 = ((double)WilayahSumbu[0].Y - (double)WilayahSumbu[1].Y) / ((double)WilayahSumbu[0].X - (double)WilayahSumbu[1].X);
            double m2 = ((double)WilayahSumbu[2].Y - (double)WilayahSumbu[1].Y) / ((double)WilayahSumbu[2].X - (double)WilayahSumbu[1].X);
            x4 = (((m2 * WilayahSumbu[1].X) - (m1 * Kurva[1].X)) + Kurva[1].Y - WilayahSumbu[1].Y) / (m2 - m1);
            y4 = m2 * x4 + WilayahSumbu[1].Y - m2 * WilayahSumbu[1].X;
            double a = Math.Sqrt(Math.Pow(x4 - WilayahSumbu[1].X, 2) + Math.Pow(y4 - WilayahSumbu[1].Y, 2));
            double a1 = Math.Sqrt(Math.Pow(WilayahSumbu[2].X - WilayahSumbu[1].X, 2) + Math.Pow(WilayahSumbu[2].Y - WilayahSumbu[1].Y, 2));
            double x = Math.Round((186 * a) / a1 , 2);
            double asli = Math.Round((double)x % (int)x, 2);
            double menit = asli* 60;
            double jamx = Math.Floor(x);
            string jammenit = jamx%24 + ":" + (int)menit; 



            x5 = (m1 * WilayahSumbu[1].X - m2 * Kurva[1].X + Kurva[1].Y - WilayahSumbu[1].Y) / (m1 - m2);
            y5 = m1 * x5 + WilayahSumbu[1].Y - m1 * WilayahSumbu[1].X;
            double b = Math.Sqrt(Math.Pow(x5 - WilayahSumbu[1].X, 2) + Math.Pow(y5 - WilayahSumbu[1].Y, 2));
            double b1 = Math.Sqrt(Math.Pow(WilayahSumbu[0].X - WilayahSumbu[1].X, 2) + Math.Pow(WilayahSumbu[0].Y - WilayahSumbu[1].Y, 2));
            int y = 250 * (int)b / (int)b1;
            label1.Text = "x : " + x4 + ", y: " + y4;
            double asu = Math.Pow(WilayahSumbu[1].X, 2);

            double gradien = y5 - Kurva[1].Y / Kurva[1].X - x5;
            //richTextBox2.AppendText();
           //richTextBox2.AppendText("m1 : "+m1 + " ");
            //richTextBox2.AppendText("m2 : " + m2 + " ");
            //richTextBox2.AppendText("x4 : " + x4 + " y4 : " + y4 + " ");
            //richTextBox2.AppendText("x5 : " + x5 + " y5 : " + y5 + " ");
           //richTextBox2.AppendText("a : " + a + " a1 : " + a1 + " ");
            //richTextBox2.AppendText("b : " + b + " b1 : " + b1 + " ");
            //richTextBox2.AppendText("m2 : " + m2);

            label1.Text = jammenit.ToString();
            label2.Text = m1.ToString();
            label3.Text = m2.ToString();
            //richTextBox2.AppendText("asli : " + jammenit+ " ");
            //richTextBox2.AppendText("menit : " + menit+ " ");
            //richTextBox2.AppendText("mod : " + asli);
            //richTextBox2.AppendText("x : " + x + ", " + "y : " + y);
            //richTextBox2.AppendText(" gradien : " + gradien + " m2 : " + m2);
            //richTextBox2.AppendText(WilayahSumbu[2].Y +" - " +WilayahSumbu[1].Y +" / "+ WilayahSumbu[2].X+ " - " +WilayahSumbu[1].X);
            picCanvas.Invalidate();

            //formGen f = new formGen();
            //f.passList(this.Scan);
            //f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Point> scaning = new List<Point>();
            find = new Point();
            Point atas = new Point();
            Point bawah = new Point();
            Plotline(WilayahSumbu, "test");
            listBox1.DataSource = Atas;
            listBox2.DataSource = SumbuX;
            for (int i = 0; damn == false;i++ )
            {
                atas = Kanan[i];
                bawah = SumbuY[i];
                scaning = Bresenham(bawah, atas);
                if (scaning.Contains(Kurva[1]))
                {
                    
                    for(int n=0;n<scaning.Count -1;n++)
                    {
                        if (scaning[n] == Kurva[1])
                        {
                            find = scaning[0];
                            label1.Text = "true";
                            label3.Text = find.ToString();
                            picCanvas.Invalidate();
                            damn = true;
                            break;
                            
                        }
                    }
                    
                }
            }
          
                
        }

        private void dtPasang_ValueChanged(object sender, EventArgs e)
        {
            if (dtPasang.Value.Date != DateTime.Now.Date && dtPasang.Value.Date < DateTime.Now.Date)
            {
                btnGen.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double deltax = WilayahSumbu[2].X - WilayahSumbu[1].X;
            double deltay = WilayahSumbu[2].Y - WilayahSumbu[1].Y;

        }

        private void btnDeteksi_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = new Bitmap(picCanvas.Image);
                smooting = new Mean();
                threshold = new Threshold();
                bmp = smooting.Apply(bmp);
                threshold.ThresholdValue = 162;
                bmp = threshold.Apply(bmp);
                picCanvas.Image = bmp;
                picCanvas.Refresh();
            }
            catch( Exception )
            {
                MessageBox.Show("Gambar Belum di Load");
            }

        }

    }
    public class thistma
    {
        public DateTime date;
        
        public string dt
        {
            get
            {
                return date.ToString();
            }
            set
            {
                DateTime.TryParse(value, out date);
            }
        }
        public int jam { get; set; }
        public int menit { get; set; }
        public double tinggi { get; set; }

        public thistma(string dt, int jam, int menit, double tinggi)
        {
            this.jam = jam;
            this.menit = menit;
            this.tinggi = tinggi;
            this.dt = dt;
        }
    }
}


