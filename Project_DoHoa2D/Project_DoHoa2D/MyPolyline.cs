using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DoHoa2D
{
    class MyPolyline : MyShape
    {
        private int numPoint;
        public MyPolyline()
        {
            point = new List<Point>(4);
            point.Add(new Point(100, 100));
            point.Add(new Point(110, 100));
            point.Add(new Point(110, 110));
            point.Add(new Point(100, 110));
            this.numPoint = 4;
        }

        public MyPolyline(params Point[] p)
        {
            int n = p.Length;
            point = new List<Point>(n);
            foreach (Point pi in p)
                point.Add(pi);
            this.numPoint = n;
        }

        public Point[] ConvertPoint(List<Point> point, Point pivot)
        {
            Point[] myPoint = new Point[point.Count];
            for (int i = 0; i < point.Count; i++)
            {
                myPoint[i].X = point[i].X - pivot.X;
                myPoint[i].Y = point[i].Y - pivot.Y;
            }
            return myPoint;
        }

        public override void Draw(Graphics graphics)
        {
            int x = 0, y = 0;

            for (int i = 0; i < numPoint; i++)
            {
                x += point[i].X; y += point[i].Y;
            }
            x /= numPoint; y /= numPoint;

            Point pivot = new Point(x, y);
            Point[] polyline = ConvertPoint(point, pivot);

            graphics.TranslateTransform(x, y);
            graphics.RotateTransform(angle);

            Pen myPen = new Pen(borderColor);
            myPen.DashStyle = dashStyle;
            myPen.Width = width;

            graphics.DrawLines(myPen, polyline);
            graphics.ResetTransform();
        }


        public override void Set(Point point, int index)
        {
            if (index >= 0 && index <= numPoint - 1)
                this.point[index] = point;
        }

        public override Point Get(int index)
        {
            if (index >= 0 && index <= numPoint - 1)
                return this.point[index];
            return this.point[0];
        }

        public override void Save(string filePath)
        {
            Point[] p = new Point[numPoint];
            for (int i = 0; i < numPoint; i++)
                p[i] = this.Get(i);

            string data = "Polyline ";
            for (int i = 0; i < numPoint; i++)
            {
                data += p[i].X.ToString() + " ";
                data += p[i].Y.ToString() + " ";
            }
            data += dashStyle.ToString() + " " + width.ToString() + " " + borderColor.ToArgb().ToString()
              + "\n";
            StreamWriter sw = File.AppendText(filePath);
            sw.WriteLine(data);
            sw.Close();
        }

        public override void Open(string data)
        {
            char delimiters = ' ';
            string[] dt = data.Split(delimiters);
            int numberString = dt.Length;
            this.numPoint = (numberString - 6) / 2;

            Point[] p = new Point[numPoint];
            for (int i = 0, j = 0; i < numPoint; i++, j += 2)
                p[i] = new Point(Int32.Parse(dt[(j + 1)]), Int32.Parse(dt[(j + 1) + 1]));

            point = new List<Point>(numPoint);
            for (int i = 0; i < numPoint; i++)
                point.Add(p[i]);

            switch (dt[numPoint * 2 + 1])
            {
                case "Dash": this.dashStyle = DashStyle.Dash; break;
                case "DashDot": this.dashStyle = DashStyle.DashDot; break;
                case "DashDotDot": this.dashStyle = DashStyle.DashDotDot; break;
                case "Dot": this.dashStyle = DashStyle.Dot; break;
                case "Solid": this.dashStyle = DashStyle.Solid; break;
                case "Custom": this.dashStyle = DashStyle.Custom; break;
            }
            this.width = Int32.Parse(dt[numPoint * 2 + 2]);
            this.borderColor = Color.FromArgb(Convert.ToInt32(numPoint * 2 + 3));
        }

       

        public override bool Inside(Point p)
        {
            bool res = false;

            Point[] polyline = new Point[numPoint];
            for (int i = 0; i < numPoint; i++)
                polyline[i] = point[i];

            GraphicsPath path = new GraphicsPath();
            path.AddLines(polyline);

            Pen pen = new Pen(borderColor, width + 2);
            res = path.IsOutlineVisible(p, pen);

            return res;
        }

        public override void Move(Point d)
        {
            for (int i = 0; i < numPoint; i++)
                Set(new Point(point[i].X + d.X, point[i].Y + d.Y), i);
        }

        public override bool AtScalePosition(Point p)
        {
            throw new NotImplementedException();
        }
    }
}
