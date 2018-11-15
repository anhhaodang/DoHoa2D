using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;

namespace Project_DoHoa2D
{
    class MyLine : MyShape
    {
        private int numPoint;

        public MyLine()
        {
            point = new List<Point>(2);
            point.Add(new Point(0, 0));
            point.Add(new Point(1, 1));
            this.numPoint = 2;
        }
        public MyLine(Point a, Point b)
        {
            point = new List<Point>(2);
            point.Add(a);
            point.Add(b);
            this.numPoint = 2;
        }
        public MyLine(int x1, int y1, int x2, int y2)
        {
            point = new List<Point>(2);
            point.Add(new Point(x1, y1));
            point.Add(new Point(x2, y2));
            this.numPoint = 2;
        }

        public override void Draw(Graphics graphics)
        {
            Pen myPen = new Pen(borderColor);
            if (width > 1)
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            else
                myPen.DashStyle = dashStyle;
            myPen.Width = width;
          //  if (dashStyle >= 0 && dashStyle <= 5)
                graphics.DrawLine(myPen, point[0], point[1]);
        }

        public override void Draw(Graphics graphics, Color borderColor)
        {
            Pen myPen = new Pen(borderColor);
            this.borderColor = borderColor;
            graphics.DrawLine(myPen, point[0], point[1]);
        }

        public override void Draw(Graphics graphics, Color borderColor, DashStyle dashStyle, float width = 1)
        {

            Pen myPen = new Pen(borderColor);
            this.borderColor = borderColor;
            this.dashStyle = dashStyle;
            this.width = width;
            if (width > 1)
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            else
            {
                myPen.DashStyle = dashStyle;
            }
            myPen.Width = width;
            // if (penStyle >= 0 && penStyle <= 5)
            graphics.DrawLine(myPen, point[0], point[1]);

        }


        public override void Set(Point point, int index)
        {
            this.point[index] = point;
        }
        public override Point Get(int index)
        {
            return this.point[index];
        }

        public override void Save(string filePath)
        {
            Point a = this.Get(0);
            Point b = this.Get(1);
            string data = "Line " +  a.X.ToString() + " " + a.Y.ToString() + " " + b.X.ToString() + " " + b.Y.ToString()
                 + " " + dashStyle.ToString() + " " + width.ToString() + " " + borderColor.ToArgb().ToString() + "\n";

            StreamWriter sw = File.AppendText(filePath);
            sw.WriteLine(data);
            sw.Close();
        }

        public override void Open(string data)
        {
            char delimiters = ' ';
            string[] dt = data.Split(delimiters);
            Point a = new Point(Int32.Parse(dt[1]), Int32.Parse(dt[2]));
            Point b = new Point(Int32.Parse(dt[3]), Int32.Parse(dt[4]));

            point = new List<Point>(2);
            point.Add(a);
            point.Add(b);

            switch (dt[5])
            {
                case "Dash": this.dashStyle = DashStyle.Dash; break;
                case "DashDot": this.dashStyle = DashStyle.DashDot; break;
                case "DashDotDot": this.dashStyle = DashStyle.DashDotDot; break;
                case "Dot": this.dashStyle = DashStyle.Dot; break;
                case "Solid": this.dashStyle = DashStyle.Solid; break;
                case "Custom": this.dashStyle = DashStyle.Custom; break;
            }
            this.width = Int32.Parse(dt[6]);
            this.borderColor = Color.FromArgb(Convert.ToInt32(dt[7]));

        }

        public override void Translation(Point Src, Point Des)
        {
            Point a = this.Get(0);
            Point b = this.Get(1);
            a.X += Des.X - Src.X; a.Y += Des.Y - Src.Y;
            b.X += Des.X - Src.X; b.Y += Des.Y - Src.Y;
            this.Set(a, 0);
            this.Set(b, 1);

        }


        public override void Scaling(Point pivotPoint, float Sx, float Sy)
        {
            //Point a = this.Get(0);
            //Point b = this.Get(1);
            //float da = (pivotPoint.X - a.X) * (pivotPoint.X - a.X) + (pivotPoint.Y - a.Y) * (pivotPoint.Y - a.Y);
            //float db = (pivotPoint.X - b.X) * (pivotPoint.X - b.X) + (pivotPoint.Y - b.Y) * (pivotPoint.Y - b.Y);
            //if (da < db)
            //{
            //    b.X = (int)(Sx * b.X);
            //    b.Y = (int)(Sy * b.Y);
            //    this.Set(b, 1);
            //}
            //else
            //{
            //    a.X = (int)(Sx * a.X);
            //    a.Y = (int)(Sy * a.Y);
            //    this.Set(a, 0);
            //}

            Point a = this.Get(0);
            Point b = this.Get(1);
            Point mid = new Point((a.X + b.X) / 2, (a.Y + b.Y) / 2);
            Translation(pivotPoint, new Point(0, 0));
            a = this.Get(0);
            b = this.Get(1);

            a.X = (int)(Sx * a.X); a.Y = (int)(Sy * a.Y);
            b.X = (int)(Sx * b.X); b.Y = (int)(Sy * b.Y);
            this.Set(a, 0);
            this.Set(b, 1);
            Translation(new Point(0, 0), pivotPoint);
        }

        public override void Rotation(double alpha)
        {
            Point a = this.Get(0);
            Point b = this.Get(1);
            Point mid = new Point((a.X + b.X) / 2, (a.Y + b.Y) / 2);
            Translation(mid,new Point(0,0));
            a = this.Get(0);
            b = this.Get(1);
            int x, y;
            alpha = -alpha;
            x = Convert.ToInt32(Math.Cos(alpha) * a.X - Math.Sin(alpha) * a.Y);
            y = Convert.ToInt32(Math.Sin(alpha) * a.X + Math.Cos(alpha) * a.Y);
            a.X = x; a.Y = y;

            x = Convert.ToInt32(Math.Cos(alpha) * b.X - Math.Sin(alpha) * b.Y);
            y = Convert.ToInt32(Math.Sin(alpha) * b.X + Math.Cos(alpha) * b.Y);
            b.X = x; b.Y = y;

            this.Set(a, 0);
            this.Set(b, 1);
            Translation(new Point(0,0) , mid);

        }

        public override void Fill(Graphics graphics, Color backgroundColor, int fillStyle)
        {
            throw new NotImplementedException();
        }
    }
}
