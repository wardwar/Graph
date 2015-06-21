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
        //lebar radius titik
        private const int object_radius = 3;

        //wilayah hit cursor pada objek
        private const int wilayah_object = object_radius * object_radius;

        //variabel sumbu
        private List<Point> Sumbu = new List<Point>();
        private List<Point> SumbuDraw = new List<Point>();
        private List<Point> newSumbu = null;
        private List<Point> newSumbuDraw = null;
        private List<Point> SumbuX = new List<Point>();
        private List<Point> SumbuY = new List<Point>();
        private List<Point> xya = null;
        private List<Point> xyb = null;
        private List<Point> Scan = null;

        //variabel black
        private List<Point> black = new List<Point>();

        //variabel menu
        private string menu;

        //variable moving
        private List<Point> MovingPolyline = null;
        private int MovingPoint = -1;
        private int OffsetX, OffsetY;

        //variabel dimensi
        private float stretch1X;
        private float stretch1Y;
        private Point dimensi;
        private int pixelX;
        private int pixelY;

        //variabel point
        private Point NewPoint;
        private Point NewPointDraw;

        //variabel aforge
        Mean mean = null;
        Threshold threshold = null;
        Bitmap filter = null;

        //formGen value
        Value v;
        formGen f;

        //generate variabel
        private double y4 = -1, x4 = -1, y5 = -1, x5 = -1;
        private List<Point> cekx = new List<Point>();
        private List<Point> ceky = new List<Point>();
        private List<tma> HasilTMA = new List<tma>();

        //variabel header
        private int id_petugas;
        private string petugas;
        private string pos;

        public Main()
        {
            InitializeComponent();
            openGambar.Title = "Load Grafik";
            openGambar.Filter = "Image Files|*.jpg;*.png";

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            lbPetugas.Text = petugas;
            lbPos.Text = pos;
            lbPetugas.Visible = true;
            lbPos.Visible = true;
            dtPasang.Value = DateTime.Now.Date;
        }

        //MenuStrip event-------------------------------------------------
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openGambar.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = openGambar.FileName;
                picCanvas.Image = new Bitmap(filePath);

                stretch1X = 1f * picCanvas.Image.Width / picCanvas.ClientSize.Width;
                stretch1Y = 1f * picCanvas.Image.Height / picCanvas.ClientSize.Height;

               label3.Text = picCanvas.Image.Width + " = " + picCanvas.ClientSize.Width + " : " + picCanvas.ClientSize.Width*stretch1X;
               label4.Text = picCanvas.Image.Height + " = " + picCanvas.ClientSize.Height + " : " + picCanvas.ClientSize.Height*stretch1Y;
                

                tandaToolStripMenuItem.Enabled = true;

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void sumbuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menu = "sumbu";
        }

        private void deteksiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point b = new Point();
            Color color = new Color();
            color = Color.FromArgb(0, 0, 0);
            filter = new Bitmap(picCanvas.Image);
            mean = new Mean();
            threshold = new Threshold();
            threshold.ThresholdValue = 162;
            filter = mean.Apply(filter);
            filter = threshold.Apply(filter);
            pbDeteksi.Value = 0;
            pbDeteksi.Maximum = filter.Width;


                for (int x = 0; x < filter.Width; x++)
                {
                    for (int y = 0; y < filter.Height; y++)
                    {
                        // 20 is an arbitrary value and subject to your opinion and need.
                        if (filter.GetPixel(x, y).R == color.R)
                        {
                            b.X = x;
                            b.Y = y;
                            black.Add(b);
                            break;
                        }

                    }
                    pbDeteksi.Value++;
                }
            picCanvas.Invalidate();
            MessageBox.Show("Deteksi Selesai");
            pbDeteksi.Value = 0;
        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x0 = Sumbu[1].X;
            int x1 = Sumbu[0].X;
            int x2 = Sumbu[2].X;
            int x3;
            int y0 = Sumbu[1].Y;
            int y1 = Sumbu[0].Y;
            int y2 = Sumbu[2].Y;
            int y3;
            int jam, menit, dt;
            double a, a1, b, b1, x, y, belakangkoma;
            DateTime tanggal;
            Point point = new Point();
            tma testpoint;

            double m1 = ((double)y0 - (double)y1) / ((double)x0 - (double)x1);
            double m2 = ((double)y2 - (double)y0) / ((double)x2 - (double)x0);

            for (int i = 0; i < black.Count; i++)
            {
                x3 = black[i].X;
                y3 = black[i].Y;

                x4 = ((m2 * x0) - (m1 * x3) + y3 - y0) / (m2 - m1);
                y4 = (m2 * x4) + y0 - (m2 * x0);
                point.X = (int)x4;
                point.Y = (int)y4;
                cekx.Add(point);
                x5 = ((m1 * x0) - (m2 * x3) + y3 - y0) / (m1 - m2);
                y5 = (m1 * x5) + y0 - (m1 * x0);
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

                point.X = (186 * (int)a) / (int)a1;
                point.Y = (250 * (int)b) / (int)b1;
                testpoint = new tma(tanggal.ToString(), jam, menit, y);

                bool existDate = HasilTMA.Exists(element => element.date == tanggal);
                bool existJam = HasilTMA.Exists(element => element.jam == jam);
                bool existMenit = HasilTMA.Exists(element => element.menit == menit);
                
                HasilTMA.Add(testpoint);
      
            }
            f = new formGen();
            f.passList(HasilTMA);
            f.id_pet(id_petugas);
            f.Show();
        }

        private void dtPasang_ValueChanged(object sender, EventArgs e)
        {
            if (dtPasang.Value.Date != DateTime.Now.Date && dtPasang.Value.Date < DateTime.Now.Date && dtPasang.Value.Date.AddDays(8) <= DateTime.Now.Date)
            {
                generateToolStripMenuItem.Enabled = true;
            }
        }

        //End MenuStrip event-------------------------------------------------

        //piccanvas event---------------------------------------------------------
        private void picCanvas_MouseMove_NotDrawing(object sender, MouseEventArgs e)
        {
            int x = (int)Math.Floor(e.Location.X * stretch1X);
            int y = (int)Math.Floor(e.Location.Y * stretch1Y);
            label5.Text = Math.Floor(e.Location.X * stretch1X) + ":" + Math.Floor(e.Location.Y * stretch1Y);
            label6.Text = (x / stretch1X) + ":"+(y / stretch1Y) ;

            Cursor cursor = Cursors.Cross;
            int hit_point;
            List<Point> list_hitpt;
            pixelX = (int)Math.Floor(e.Location.X * stretch1X);
            pixelY = (int)Math.Floor(e.Location.Y * stretch1Y);
            dimensi.X = pixelX;
            dimensi.Y = pixelY;
            
            if(MousePoint(dimensi, out hit_point, out list_hitpt, Sumbu))
            {
               cursor = Cursors.Arrow;
            }
            if (picCanvas.Cursor != cursor)
            {
                picCanvas.Cursor = cursor;
            }

        }

        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            pixelX = (int)Math.Floor(e.Location.X * stretch1X);
            pixelY = (int)Math.Floor(e.Location.Y * stretch1Y);
            dimensi.X = pixelX;
            dimensi.Y = pixelY;

            int hit_point;
            List<Point> hit_polyline;

            if (menu == "sumbu")
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (newSumbu != null)
                    {
                        if (newSumbu.Count == 4)
                        {
                            Sumbu = newSumbu;
                            SumbuDraw = newSumbuDraw;
                            newSumbu = null;
                            newSumbuDraw = null;
                            DeteksiGaris(Sumbu, "sumbu");
                            prosesToolStripMenuItem.Enabled = true;

                            picCanvas.MouseMove += picCanvas_MouseMove_NotDrawing;
                            picCanvas.MouseMove -= picCanvas_MouseMove_Drawing;
                        }
                        else
                        {
                            if (newSumbu.Count > 0)
                            {
                                if (newSumbu[newSumbu.Count - 1] != dimensi)
                                {
                                    newSumbu.Add(dimensi);
                                    newSumbuDraw.Add(e.Location);
                                }
                            }
                            else
                            {
                                newSumbu.Add(dimensi);
                                newSumbuDraw.Add(e.Location);

                            }
                        }
                    }
                    else if (MousePoint(dimensi, out hit_point, out hit_polyline, Sumbu))
                    {
                        picCanvas.MouseMove -= picCanvas_MouseMove_NotDrawing;
                        picCanvas.MouseMove += picCanvas_MouseMove_MovingCorner;
                        picCanvas.MouseUp += picCanvas_MouseUp_MovingCorner;

                        // Remember the polygon and point number.
                        MovingPolyline = hit_polyline;
                        MovingPoint = hit_point;

                        // Remember the offset from the mouse to the point.
                        OffsetX = hit_polyline[hit_point].X - e.X;
                        OffsetY = hit_polyline[hit_point].Y - e.Y;
                    }
                    else if (newSumbu == null && Sumbu.Count > 0)
                    {
                        picCanvas.MouseMove += picCanvas_MouseMove_NotDrawing;
                        picCanvas.MouseMove -= picCanvas_MouseMove_Drawing;
                    }
                    else if (newSumbu == null)
                    {
                        newSumbu = new List<Point>();
                        newSumbu.Add(dimensi);
                        newSumbuDraw = new List<Point>();
                        newSumbuDraw.Add(e.Location);
                        NewPoint = dimensi;
                        NewPointDraw = e.Location;

                        picCanvas.MouseMove -= picCanvas_MouseMove_NotDrawing;
                        picCanvas.MouseMove += picCanvas_MouseMove_Drawing;
                    }


                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (newSumbu != null)
                    {
                        if (newSumbu.Count != 0) { 
                            newSumbu.RemoveAt(newSumbu.Count - 1);
                            newSumbuDraw.RemoveAt(newSumbuDraw.Count - 1);}
                        picCanvas.Invalidate();
                    }
                }
            }

            else if (picCanvas.Image == null)
            {
                string message = "Load Grafik Terlebih Dahulu!";
                string tittle = "Error";

                MessageBox.Show(message, tittle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (Sumbu.Count > 0 )
            {
                if (dtPasang.Value.Date != DateTime.Today && dtPasang.Value.Date < DateTime.Today)
                {
                    generateToolStripMenuItem.Enabled = true;
                }

            }
            
            picCanvas.Invalidate();
        }

        private void picCanvas_MouseMove_Drawing(object sender, MouseEventArgs e)
        {
            pixelX = (int)Math.Floor(e.Location.X * stretch1X);
            pixelY = (int)Math.Floor(e.Location.Y * stretch1Y);
            dimensi.X = pixelX;
            dimensi.Y = pixelY;

            NewPoint = dimensi;
            NewPointDraw = e.Location;
            picCanvas.Invalidate();
        }

        private void picCanvas_MouseUp_MovingCorner(object sender, MouseEventArgs e)
        {
            picCanvas.MouseMove += picCanvas_MouseMove_NotDrawing;
            picCanvas.MouseMove -= picCanvas_MouseMove_MovingCorner;
            picCanvas.MouseUp -= picCanvas_MouseUp_MovingCorner;
        }

        private void picCanvas_MouseMove_MovingCorner(object sender, MouseEventArgs e)
        {
            // Move the point.
            MovingPolyline[MovingPoint] = new Point(e.X + OffsetX, e.Y + OffsetY);

            // Redraw.
            picCanvas.Invalidate();
        }

        //end piccanvas event---------------------------------------------------------

        private bool MousePoint(Point mouse_pt, out int hit_pt, out List<Point> hit_point, List<Point> Garis)
        {
            for (int i = 0; i < Garis.Count; i++)
            {
                if (AreaTitik(Garis[i], mouse_pt) < wilayah_object)
                {
                    hit_pt = i;
                    hit_point = Garis;
                    return true;
                }
            }
            hit_pt = -1;
            hit_point = null;
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

            if (Sumbu.Count > 0)
            {
                e.Graphics.DrawPolygon(Pens.DarkMagenta, SumbuDraw.ToArray());
            }
            if (black.Count != null)
            {

                foreach (Point b in black)
                {

                    Rectangle rect = new Rectangle(
                    (int)Math.Floor(b.X /stretch1X), (int)Math.Floor(b.Y/stretch1Y),1,1);
                    e.Graphics.FillEllipse(Brushes.White, rect);
                    e.Graphics.DrawEllipse(Pens.Red, rect);
                }
            }
           
            
            //gambar titik
            foreach (Point titik in SumbuDraw)
            {

                Rectangle rect = new Rectangle(
                    titik.X - object_radius, titik.Y - object_radius,
                    2 * object_radius + 1, 2 * object_radius + 1);
                e.Graphics.FillEllipse(Brushes.White, rect);
                e.Graphics.DrawEllipse(Pens.Red, rect);
            }

            if (newSumbuDraw != null)
            {
                if (newSumbuDraw.Count > 1)
                {
                    e.Graphics.DrawLines(Pens.Green, newSumbuDraw.ToArray());
                }

                if (newSumbuDraw.Count > 0)
                {
                    using (Pen dashed_pen = new Pen(Color.Green))
                    {
                        dashed_pen.DashPattern = new float[] { 3, 3 };
                        e.Graphics.DrawLine(dashed_pen,
                            newSumbuDraw[newSumbuDraw.Count - 1],
                            NewPointDraw);
                    }
                }
            }
            
        }

        private void DeteksiGaris(List<Point> Garis, string type)
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

            

            for (int i = 0; i < xya.Count; i++)
            {

                if (type == "sumbu" && i == 0)
                {
                    SumbuY = Bresenham(xya[i], xyb[i]);

                }
                else if (type == "sumbu" && i == 1)
                {
                    SumbuX = Bresenham(xya[i], xyb[i]); ;
                }

                //v = new Value();
                //v.sumbu(SumbuX,SumbuY,xya, xyb);
                //v.Show();

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
                dy = y2 - y1;
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
            return Scan;
        }

        public void datapetugas(int id_petugas,string petugas,string pos)
        {
            this.id_petugas = id_petugas;
            this.petugas = petugas;
            this.pos = pos;
        }

        

    }


    public class tma
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

        public tma(string dt, int jam, int menit, double tinggi)
        {
            this.jam = jam;
            this.menit = menit;
            this.tinggi = tinggi;
            this.dt = dt;
        }
    }
        
        
    }
