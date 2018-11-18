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
    class MyPolygon: MyShape
    {
        public MyPolygon()
        {
            point = new List<Point>(4);
            point.Add(new Point(100, 100));
            point.Add(new Point(110, 100));
            point.Add(new Point(110, 110));
            point.Add(new Point(100, 110));

            this.numPoint = 4;
        }

        public MyPolygon(params Point[] p)
        {
            int n = p.Length;
            point = new List<Point>(n);
            foreach (Point pi in p)
                point.Add(pi);
            this.numPoint = n;
        }

        public override void Extend_ExtendableShape(Point p)
        {
            point.Add(p);
            this.numPoint += 1;
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
            x_min = x_max = point[0].X;
            y_min = y_max = point[0].Y;
            int x = 0, y = 0;
            for (int i = 0; i < numPoint; i++)
            {
                x += point[i].X; y += point[i].Y;
                if (x_min > point[i].X) x_min = point[i].X;
                if (y_min > point[i].Y) y_min = point[i].Y;
                if (x_max < point[i].X) x_max = point[i].X;
                if (y_max < point[i].Y) y_max = point[i].Y;
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
            Point[] polygon = ConvertPoint(point, pivot);

            graphics.TranslateTransform(x,y);
            graphics.RotateTransform(angle);

            Pen myPen = new Pen(borderColor);
            myPen.DashStyle = dashStyle;
            myPen.Width = width;


            if (isFill)
            {
                if (fillStyle == 0)
                    graphics.FillPolygon(new SolidBrush(backgroundColor), polygon);
                else
                {
                    HatchBrush hatchBrush = new HatchBrush(hatchStyle, backgroundColor);
                    graphics.FillPolygon(hatchBrush, polygon);

                }
            }

            graphics.DrawPolygon(myPen, polygon);
            graphics.ResetTransform();
        }

        public override void Set(Point p, int index)
        {
            this.point[index] = base.Rotate(base.Center(), p, -angle);
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

            string data = "Polygon ";
            for (int i = 0; i < numPoint - 2; i++)
            {
                data += p[i].X.ToString() + " ";
                data += p[i].Y.ToString() + " ";
            }
            data += dashStyle.ToString() + " " + width.ToString() + " " + borderColor.ToArgb().ToString() 
                + " " + backgroundColor.ToArgb().ToString() + " " + fillStyle.ToString()
                + " " + isFill.ToString() + " " + hatchStyle.GetHashCode() + " " + angle.ToString() + "\n";
            StreamWriter sw = File.AppendText(filePath);
            sw.WriteLine(data);
            sw.Close();
        }

        public override void Open(string data)
        {
            char delimiters = ' ';
            string[] dt = data.Split(delimiters);
            int numberString = dt.Length;
            this.numPoint = (numberString - 9) / 2;

            Point[] p = new Point[numPoint];
            for (int i = 0, j = 0; i < numPoint; i++, j+=2)
                p[i] = new Point(Int32.Parse(dt[(j+1)]), Int32.Parse(dt[(j+1)+1]));

            point = new List<Point>(numPoint);
            for (int i = 0; i < numPoint; i++)
                point.Add(p[i]);

            int k = numPoint;
            switch (dt[k * 2 +1])
            {
                case "Dash": this.dashStyle = DashStyle.Dash; break;
                case "DashDot": this.dashStyle = DashStyle.DashDot; break;
                case "DashDotDot": this.dashStyle = DashStyle.DashDotDot; break;
                case "Dot": this.dashStyle = DashStyle.Dot; break;
                case "Solid": this.dashStyle = DashStyle.Solid; break;
                case "Custom": this.dashStyle = DashStyle.Custom; break;
            }
            this.width = Int32.Parse(dt[k * 2 + 2]);
            this.borderColor = Color.FromArgb(Convert.ToInt32(dt[k * 2 + 3]));
            this.backgroundColor = Color.FromArgb(Convert.ToInt32(dt[k * 2 + 4]));
            this.fillStyle = Int32.Parse(dt[k * 2 + 5]);
            this.isFill = bool.Parse(dt[k * 2 + 6]);
            this.hatchStyle = (HatchStyle)(Int32.Parse(dt[k * 2 + 7]));
            this.angle = float.Parse(dt[k * 2 + 8]);

        }



        public override bool Inside(Point p)
        {

            p = base.Rotate(base.Center(), p, -angle);

            bool res = false;

            Point[] polygon = new Point[numPoint];
            for (int i = 0; i < numPoint; i++)
                polygon[i] = point[i];

            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(polygon);

            if (isFill)
                res = path.IsVisible(p);
            else
            {
                Pen pen = new Pen(borderColor, width + 2);
                res = path.IsOutlineVisible(p, pen);
            }
            return res;


        }

        public override void Move(Point d)
        {
            for (int i = 0; i < numPoint; i++)
            {
                Point p = new Point(point[i].X + d.X, point[i].Y + d.Y);
                point[i] = p;
            }
        }

        public override bool AtScalePosition(Point p)
        {
            if (angle != 0)
            {
                p = base.Rotate(base.Center(), p, angle);
            }

            int x_min, y_min;
            x_min = point[0].X; y_min = point[0].Y;
            for (int i = 0; i < numPoint; i++)
            {
                if (x_min > point[i].X) x_min = point[i].X;
                if (y_min > point[i].Y) y_min = point[i].Y;
            }

            Point p0 = new Point(x_min, y_min);

            if (Math.Abs(p.X - p0.X) < 5 && Math.Abs(p.Y - p0.Y) < 5)
                return true;
            return false;
        }

        public override bool AtRotatePosition(Point p)
        {
            if (angle != 0)
            {
                p = base.Rotate(base.Center(), p, angle);
            }

            int x_min, y_min;
            x_min = point[0].X; y_min = point[0].Y;
            for (int i = 0; i < numPoint; i++)
            {
                if (x_min > point[i].X) x_min = point[i].X;
                if (y_min > point[i].Y) y_min = point[i].Y;
            }

            Point p0 = new Point(x_min, y_min);

            return (p0.X - p.X > 5 && p0.X - p.X < 15
                && p0.Y - p.Y > 5 && p0.Y - p.Y < 15);

        }
    }
}
