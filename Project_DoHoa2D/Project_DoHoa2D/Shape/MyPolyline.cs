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
            Rectangle r = GetBoundingBox();
            Point pCenter = r.Location + new Size(r.Width / 2, r.Height / 2);
            r.Location = new Point(-r.Width / 2, -r.Height / 2);

            graphics.TranslateTransform(pCenter.X, pCenter.Y);
            graphics.RotateTransform(angle);

            if (isSelected)
                DrawBoudingBox(graphics, r);

            Point[] polyline = ConvertPoint(points, pCenter);

            Pen myPen = new Pen(borderColor);
            myPen.DashStyle = dashStyle;
            myPen.Width = width;

            graphics.DrawLines(myPen, polyline);
            graphics.ResetTransform();
        }

        public new Point Get(int index)
        {
            if (index >= 0 && index <= numPoint - 1)
                return this.points[index];
            return this.points[0];
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

        public override string getData()
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
            return data;
        }
    }
}
