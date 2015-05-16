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

        public Form1()
        {
            InitializeComponent();
            openFileImage.Title = "Load Grafik";
            openFileImage.Filter = "JPEG Image (.jpg) | *jpg";
        }


        private const int object_radius = 3;

        private const int over_dist_squared = object_radius * object_radius;

        private List<List<Point>> Polylines = new List<List<Point>>();

        private List<Point> NewPolyline = null;

        private List<Point> Kurva = new List<Point>();

        private Point NewPoint;

        private List<Point> MovingPolyline = null;
        private int MovingPoint = -1;
        private int OffsetX, OffsetY;




        private void btnLoad_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileImage.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = openFileImage.FileName;

                picCanvas.Image = new Bitmap(filePath);

                Polylines.Clear();
                btnGen.Enabled = false;
            }


        }

        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {

                if (picCanvas.Image != null)
                {

                    List<Point> hit_polyline;
                    int hit_point;

                    if (NewPolyline != null)
                    {
                        if (e.Button == MouseButtons.Right)
                        {
                            if (NewPolyline.Count > 2) Polylines.Add(NewPolyline);
                            NewPolyline = null;

                            picCanvas.MouseMove += picCanvas_MouseMove_NotDrawing;
                            picCanvas.MouseMove -= picCanvas_MouseMove_Drawing;

                        }
                        else
                        {
                            if (NewPolyline[NewPolyline.Count - 1] != e.Location)
                            {
                                NewPolyline.Add(e.Location);
                            }
                        }
                    }
                    else if (MouseIsOverCornerPoint(e.Location, out hit_polyline, out hit_point))
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
                    else if (MouseIsOverPolyline(e.Location, out hit_polyline))
                    {
                        // Start moving this polygon.
                        picCanvas.MouseMove -= picCanvas_MouseMove_NotDrawing;
                        picCanvas.MouseMove += picCanvas_MouseMove_MovingPolyline;
                        picCanvas.MouseUp += picCanvas_MouseUp_MovingPolyline;

                        // Remember the polygon.
                        MovingPolyline = hit_polyline;

                        // Remember the offset from the mouse to the segment's first point.
                        OffsetX = hit_polyline[0].X - e.X;
                        OffsetY = hit_polyline[0].Y - e.Y;
                    }
                    else
                    {
                        // Start a new polygon.
                        NewPolyline = new List<Point>();
                        NewPoint = e.Location;
                        NewPolyline.Add(e.Location);

                        // Get ready to work on the new polygon.
                        picCanvas.MouseMove -= picCanvas_MouseMove_NotDrawing;
                        picCanvas.MouseMove += picCanvas_MouseMove_Drawing;


                    }

                    // Redraw.
                    picCanvas.Invalidate();
                }
            



            else
            {
                string message = "Load Grafik Terlebih Dahulu!";
                string tittle = "Error";

                MessageBox.Show(message, tittle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (Polylines.Count > 0)
            {
                btnGen.Enabled = true;
            }


        }



        private void picCanvas_MouseUp_MovingPolyline(object sender, MouseEventArgs e)
        {

            picCanvas.MouseMove += picCanvas_MouseMove_NotDrawing;
            picCanvas.MouseMove -= picCanvas_MouseMove_MovingPolyline;
            picCanvas.MouseUp -= picCanvas_MouseUp_MovingPolyline;
        }

        private void picCanvas_MouseMove_MovingPolyline(object sender, MouseEventArgs e)
        {
            // See how far the first point will move.
            int new_x1 = e.X + OffsetX;
            int new_y1 = e.Y + OffsetY;

            int dx = new_x1 - MovingPolyline[0].X;
            int dy = new_y1 - MovingPolyline[0].Y;
            
            if (dx == 0 && dy == 0) return;

            // Move the polygon.
            for (int i = 0; i < MovingPolyline.Count; i++)
            {
                MovingPolyline[i] = new Point(
                    MovingPolyline[i].X + dx,
                    MovingPolyline[i].Y + dy);
            }

            // Redraw.
            picCanvas.Invalidate();
        }

        private void picCanvas_MouseMove_Drawing(object sender, MouseEventArgs e)
        {
            NewPoint = e.Location;
            picCanvas.Invalidate();
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

        private void picCanvas_MouseMove_NotDrawing(object sender, MouseEventArgs e)
        {
            Cursor new_cursor = Cursors.Cross;

            // See what we're over.
            List<Point> hit_polyline;
            int hit_point;

            if (MouseIsOverCornerPoint(e.Location, out hit_polyline, out hit_point))
            {
                new_cursor = Cursors.Arrow;
            }
            else if (MouseIsOverPolyline(e.Location, out hit_polyline))
            {
                new_cursor = Cursors.Hand;
            }

            // Set the new cursor.
            if (picCanvas.Cursor != new_cursor)
            {
                picCanvas.Cursor = new_cursor;
            }
        }

        private bool MouseIsOverPolyline(Point mouse_pt, out List<Point> hit_polyline)
        {
            // Examine each polygon.
            // Examine them in reverse order to check the ones on top first.
            foreach (List<Point> polyline in Polylines)
            {

                for (int i = 0; i >= Polylines.Count; i++)
                {
                    // Make a GraphicsPath representing the polygon.
                    GraphicsPath path = new GraphicsPath();
                    path.AddLine(polyline[i],polyline[i+1]);
                    // See if the point is inside the GraphicsPath.
                    if (path.IsVisible(mouse_pt))
                    {
                        hit_polyline = Polylines[i];
                        return true;
                    }
                }

            }

            hit_polyline = null;
            return false;
        }



        private bool MouseIsOverCornerPoint(Point mouse_pt, out List<Point> hit_polyline, out int hit_pt)
        {
            foreach (List<Point> Polyline in Polylines)
            {
                for (int i = 0; i < Polyline.Count; i++)
                {
                    if (FindDistanceToPointSquared(Polyline[i], mouse_pt) < over_dist_squared)
                    {
                        hit_polyline = Polyline;
                        hit_pt = i;
                        return true;
                    }
                }
            }

            hit_polyline = null;
            hit_pt = -1;
            return false;

        }

        private int FindDistanceToPointSquared(Point pt1, Point pt2)
        {
            int dx = Math.Abs(pt1.X - pt2.X);
            int dy = Math.Abs(pt1.Y - pt2.Y);
            int insideCorner = dx + dy;
            if (insideCorner < over_dist_squared)
            {
                return insideCorner;
            }
            return over_dist_squared + 1;
            
        }


        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw the old polygons.
            foreach (List<Point> polyline in Polylines)
            {
                // Draw the polygon.
                //e.Graphics.FillPolygon(Brushes.White, polyline.ToArray());

                e.Graphics.DrawLines(Pens.Blue, polyline.ToArray());

                // Draw the corners.
                foreach (Point corner in polyline)
                {
                    Rectangle rect = new Rectangle(
                        corner.X - object_radius, corner.Y - object_radius,
                        2 * object_radius + 1, 2 * object_radius + 1);
                    e.Graphics.FillEllipse(Brushes.White, rect);
                    e.Graphics.DrawEllipse(Pens.Black, rect);
                }
            }

            // Draw the new polygon.
            if (NewPolyline != null)
            {
                // Draw the new polygon.
                if (NewPolyline.Count > 1)
                {
                    e.Graphics.DrawLines(Pens.Green, NewPolyline.ToArray());
                }

                // Draw the newest edge.
                if (NewPolyline.Count > 0)
                {
                    using (Pen dashed_pen = new Pen(Color.Green))
                    {
                        dashed_pen.DashPattern = new float[] { 3, 3 };
                        e.Graphics.DrawLine(dashed_pen,
                            NewPolyline[NewPolyline.Count - 1],
                            NewPoint);
                    }
                }
            }
        }

        private void SwapPoint(ref int x, ref int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }

        
        private void btnGen_Click(object sender, EventArgs e)
        {
            formGen f = new formGen();
            f.passList(this.Polylines);
            f.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnGen.Enabled = false;
        }


    }
}
