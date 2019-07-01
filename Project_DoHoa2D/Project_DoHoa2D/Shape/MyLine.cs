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
        public MyLine()
        {
            points = new List<Point>(2);
            points.Add(new Point(0, 0));
            points.Add(new Point(1, 1));
        }
        public MyLine(Point a, Point b)
        {
            points = new List<Point>(2);
            points.Add(a);
            points.Add(b);
        }
        public MyLine(int x1, int y1, int x2, int y2)
        {
            points = new List<Point>(2);
            points.Add(new Point(x1, y1));
            points.Add(new Point(x2, y2));
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

            Pen myPen = new Pen(borderColor);
            myPen.DashStyle = dashStyle;
            myPen.Width = width;

            int a = points[0].X - (points[0].X + points[1].X) / 2;
            int b = points[0].Y - (points[0].Y + points[1].Y) / 2;
            int c = points[1].X - (points[0].X + points[1].X) / 2;
            int d = points[1].Y - (points[0].Y + points[1].Y) / 2;
            
            graphics.DrawLine(myPen, a, b, c, d);
            graphics.ResetTransform();
        }

        public override void Open(string data)
        {
            char delimiters = ' ';
            string[] dt = data.Split(delimiters);
            Point a = new Point(Int32.Parse(dt[1]), Int32.Parse(dt[2]));
            Point b = new Point(Int32.Parse(dt[3]), Int32.Parse(dt[4]));

            points = new List<Point>(2);
            points.Add(a);
            points.Add(b);

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
            this.angle = float.Parse(dt[8]);
        }

        public override void Extend_ExtendableShape(Point p)
        {
            
        }

        protected override GraphicsPath GetGraphicsPath(Rectangle boundingBox)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine(points[0], points[1]);
            return path;
        }

        public override string getData()
        {
            Point a = this.Get(0);
            Point b = this.Get(1);
            string data = "Line " + a.X.ToString() + " " + a.Y.ToString() + " " + b.X.ToString() + " " + b.Y.ToString()
                 + " " + dashStyle.ToString() + " " + width.ToString() + " " + borderColor.ToArgb().ToString()
                 + " " + angle.ToString() + "\n";
            return data;
        }
    }
}
