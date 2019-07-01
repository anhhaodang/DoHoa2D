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
        public MyPolyline()
        {
            points = new List<Point>(4);
            points.Add(new Point(100, 100));
            points.Add(new Point(110, 100));
            points.Add(new Point(110, 110));
            points.Add(new Point(100, 110));
            this.numPoint = 4;
        }

        public MyPolyline(params Point[] p)
        {
            int n = p.Length;
            points = new List<Point>(n);
            foreach (Point pi in p)
                points.Add(pi);
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
            int x_min, y_min, x_max, y_max;
            x_min = x_max = points[0].X;
            y_min = y_max = points[0].Y;
            int x = 0, y = 0;
            for (int i = 0; i < numPoint; i++)
            {
                x += points[i].X; y += points[i].Y;
                if (x_min > points[i].X) x_min = points[i].X;
                if (y_min > points[i].Y) y_min = points[i].Y;
                if (x_max < points[i].X) x_max = points[i].X;
                if (y_max < points[i].Y) y_max = points[i].Y;
            }

            Point p0 = new Point(x_min, y_min);
            Point p1 = new Point(x_max, y_max);

            if (isSelected)
            {
                //vẽ bao

                Pen penBound = new Pen(Color.Blue);
                penBound.DashStyle = DashStyle.Dash;

                graphics.DrawRectangle(penBound, new Rectangle(p0, new Size(p1.X - p0.X, p1.Y - p0.Y)));
                int size = 3;
                graphics.FillEllipse(new SolidBrush(Color.Blue), new Rectangle(p0.X - size, p0.Y - size, size * 2, size * 2));
            }

            x /= numPoint; y /= numPoint;


            Point pivot = new Point(x, y);
            Point[] polyline = ConvertPoint(points, pivot);

            graphics.TranslateTransform(x, y);
            graphics.RotateTransform(angle);

            Pen myPen = new Pen(borderColor);
            myPen.DashStyle = dashStyle;
            myPen.Width = width;

            graphics.DrawLines(myPen, polyline);
            graphics.ResetTransform();
        }


        public override void Set(Point p, int index)
        {
            this.points[index] = base.Rotate(base.Center(), p, -angle);

        }

        public override Point Get(int index)
        {
            if (index >= 0 && index <= numPoint - 1)
                return this.points[index];
            return this.points[0];
        }

        public override void Save(string filePath)
        {
            Point[] p = new Point[numPoint];
            for (int i = 0; i < numPoint; i++)
                p[i] = this.Get(i);

            string data = "Polyline ";
            for (int i = 0; i < numPoint - 2; i++)
            {
                data += p[i].X.ToString() + " ";
                data += p[i].Y.ToString() + " ";
            }
            data += dashStyle.ToString() + " " + width.ToString() + " " + borderColor.ToArgb().ToString()
              + " " + angle.ToString() + "\n";
            StreamWriter sw = File.AppendText(filePath);
            sw.WriteLine(data);
            sw.Close();
        }

        public override void Open(string data)
        {
            char delimiters = ' ';
            string[] dt = data.Split(delimiters);
            int numberString = dt.Length;
            this.numPoint = (numberString - 5) / 2;

            Point[] p = new Point[numPoint];
            for (int i = 0, j = 0; i < numPoint; i++, j += 2)
                p[i] = new Point(Int32.Parse(dt[(j + 1)]), Int32.Parse(dt[(j + 1) + 1]));

            points = new List<Point>(numPoint);
            for (int i = 0; i < numPoint; i++)
                points.Add(p[i]);

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
            this.angle = float.Parse(dt[numPoint * 2 + 4]);

        }

        public override void Move(Point d)
        {
            for (int i = 0; i < numPoint; i++)
            {
                Point p = new Point(points[i].X + d.X, points[i].Y + d.Y);
                points[i] = p;
            }
        }

        //public override bool AtScalePosition(Point p)
        //{
        //    if (angle != 0)
        //    {
        //        p = base.Rotate(base.Center(), p, angle);
        //    }

        //    int x_min, y_min;
        //    x_min = points[0].X; y_min = points[0].Y;
        //    for (int i = 0; i < numPoint; i++)
        //    {
        //        if (x_min > points[i].X) x_min = points[i].X;
        //        if (y_min > points[i].Y) y_min = points[i].Y;
        //    }

        //    Point p0 = new Point(x_min, y_min);

        //    if (Math.Abs(p.X - p0.X) < 5 && Math.Abs(p.Y - p0.Y) < 5)
        //        return true;
        //    return false;
        //}


        public override void Extend_ExtendableShape(Point p)
        {
            points.Add(p);
            this.numPoint += 1;
        }

        protected override GraphicsPath GetGraphicsPath(Rectangle boundingBox)
        {
            Point[] polyline = new Point[numPoint];
            for (int i = 0; i < numPoint; i++)
                polyline[i] = points[i];

            GraphicsPath path = new GraphicsPath();
            path.AddLines(polyline);
            return path;
        }
    }
}
