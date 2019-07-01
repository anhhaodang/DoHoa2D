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
            Pen myPen = new Pen(borderColor);
            myPen.DashStyle = dashStyle;
            myPen.Width = width;

            graphics.TranslateTransform((points[0].X + points[1].X) / 2, (points[0].Y + points[1].Y) / 2);
            graphics.RotateTransform(angle);

            int a = points[0].X - (points[0].X + points[1].X) / 2;
            int b = points[0].Y - (points[0].Y + points[1].Y) / 2;
            int c = points[1].X - (points[0].X + points[1].X) / 2;
            int d = points[1].Y - (points[0].Y + points[1].Y) / 2;
            
            graphics.DrawLine(myPen, a, b, c, d);
            if (isSelected)
            {
                int size = 3;
                graphics.FillEllipse(new SolidBrush(Color.Blue), new Rectangle(a - size, b - size, size * 2, size * 2));
                //graphics.FillEllipse(new SolidBrush(Color.Blue), new Rectangle(c - size, d - size, size * 2, size * 2));
            }
            graphics.ResetTransform();
        }

        
        public override void Set(Point p, int index)
        {
            this.points[index] = base.Rotate(base.Center(), p, -angle);
        }
        public override Point Get(int index)
        {
            return this.points[index];
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

        public override void Move(Point d)
        {
            for (int i = 0; i < 2; i++)
                this.Set(new Point(points[i].X + d.X, points[i].Y + d.Y), i);
        }

        //public override bool AtScalePosition(Point p)
        //{
        //    if (angle != 0)
        //    {
        //        p = base.Rotate(base.Center(), p, -angle);
        //    }

        //    if (Math.Abs(p.X - points[0].X) < 5 && Math.Abs(p.Y - points[0].Y) < 5)
        //        return true;
        //    return false;
        //}

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
