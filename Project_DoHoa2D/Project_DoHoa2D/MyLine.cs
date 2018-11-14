using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Project_DoHoa2D
{
    class MyLine
    {
        private Point a;
        private Point b;
        private Color color = Color.Black;
        private int dashStyle = 0;
        private float width = 1;

        public MyLine()
        {
            this.a = new Point(0, 0);
            this.b = new Point(0, 0);
        }
        public MyLine(Point a, Point b)
        {
            this.a = a; this.b = b;
        }
        public MyLine(int x1, int y1, int x2, int y2)
        {
            this.a = new Point(x1, y1);
            this.b = new Point(x2, y2);
        }

        public void Draw(Graphics graphics)
        {
            Pen myPen = new Pen(color);
            if (width > 1)
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            else
                switch (dashStyle)
                {
                    case 0: myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid; break;
                    case 1: myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash; break;
                    case 2: myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot; break;
                    case 3: myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot; break;
                    case 4: myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot; break;
                    case 5: myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom; break;
                }
            myPen.Width = width;
            if (dashStyle >= 0 && dashStyle <= 5)
                graphics.DrawLine(myPen, a, b);
        }

        public void Draw(Graphics graphics, Color color)
        {
            Pen myPen = new Pen(color);
            this.color = color;
            graphics.DrawLine(myPen, a, b);
        }

        public void Draw(Graphics graphics, Color color, int penStyle = 0, float width = 1)
        {

            Pen myPen = new Pen(color);
            this.color = color;
            this.dashStyle = penStyle;
            this.width = width;
            if (width > 1)
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            else
            {
                switch (penStyle)
                {
                    case 0: myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid; break;
                    case 1: myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash; break;
                    case 2: myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot; break;
                    case 3: myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot; break;
                    case 4: myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot; break;
                    case 5: myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom; break;
                }
                myPen.Width = width;
            }
            if (penStyle >= 0 && penStyle <= 5)
                graphics.DrawLine(myPen, a, b);
        }

        public void Save(string filePath)
        {
            string data = a.X.ToString() + " " + a.Y.ToString() + " " + b.X.ToString() + " " + b.Y.ToString()
                 + " " + dashStyle.ToString() + " " + width.ToString() + " " + color.ToArgb().ToString();
            File.WriteAllText(filePath, data);
        }

        public void Open(string filePath)
        {
            if (File.Exists(filePath))
            {
                string data;
                data = File.ReadAllText(filePath);
                char delimiters = ' ';
                string[] dt = data.Split(delimiters);
                a = new Point(Int32.Parse(dt[0]), Int32.Parse(dt[1]));
                b = new Point(Int32.Parse(dt[2]), Int32.Parse(dt[3]));
                this.dashStyle = Int32.Parse(dt[4]);
                this.width = Int32.Parse(dt[5]);
                color = Color.FromArgb(Convert.ToInt32(dt[6]));

            }
        }

        public void Translation(int tr_x, int tr_y)
        {
            this.a.X += tr_x; this.a.Y += tr_y;
            this.b.X += tr_x; this.b.Y += tr_y;
        }

        public void Scaling(Point scalingPoint, Point newPoint)
        {
            float da = (scalingPoint.X - a.X) * (scalingPoint.X - a.X) + (scalingPoint.Y - a.Y) * (scalingPoint.Y - a.Y);
            float db = (scalingPoint.X - b.X) * (scalingPoint.X - b.X) + (scalingPoint.Y - b.Y) * (scalingPoint.Y - b.Y);
            if (da > db)
                this.b = newPoint;
            else
                this.a = newPoint;
        }

        public void Rotation(double alpha)
        {
            Point mid = new Point((this.a.X + this.b.X) / 2, (this.a.Y + this.b.Y) / 2);
            Translation(-mid.X, -mid.Y);

            int x, y;
            x = Convert.ToInt32(Math.Cos(alpha) * this.a.X - Math.Sin(alpha) * this.a.Y);
            y = Convert.ToInt32(Math.Sin(alpha) * this.a.X + Math.Cos(alpha) * this.a.Y);
            this.a.X = x; this.a.Y = y;

            x = Convert.ToInt32(Math.Cos(alpha) * this.b.X - Math.Sin(alpha) * this.b.Y);
            y = Convert.ToInt32(Math.Sin(alpha) * this.b.X + Math.Cos(alpha) * this.b.Y);
            this.b.X = x; this.b.Y = y;

            Translation(mid.X, mid.Y);

        }
    }
}
