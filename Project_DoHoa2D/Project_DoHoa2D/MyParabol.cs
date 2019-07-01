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
    class MyParabol : MyShape
    {
        public MyParabol()
        {
            point = new List<Point> { new Point(100, 100), new Point(110, 110) };

        }

        public MyParabol(Point p1, Point p2)
        {
            point = new List<Point> { p1, p2 };
        }

        public override bool Inside(Point p)
        {
            p = base.Rotate(base.Center(), p, -angle);

            Point[] points = { point[0], new Point((point[0].X + point[1].X) / 2, point[1].Y), new Point(point[1].X, point[0].Y) };

            bool res = false;
            GraphicsPath path = new GraphicsPath();
            path.AddCurve(points);
            Pen pen = new Pen(borderColor, width + 2);
            res = path.IsOutlineVisible(p, pen);
            return res;
        }

        public override void Move(Point d)
        {
            for (int i = 0; i < 2; i++)
            {
                Point p = new Point(point[i].X + d.X, point[i].Y + d.Y);
                point[i] = p;
            }
        }

        public override void Set(Point p, int index)
        {
            this.point[index] = base.Rotate(base.Center(), p, -angle);
        }
        public override Point Get(int index){ return this.point[index]; }

        public override void Draw(Graphics graphics)
        {
            Point p0 = new Point(Math.Min(point[0].X, point[1].X), Math.Min(point[0].Y, point[1].Y));
            Point p1 = new Point(Math.Max(point[0].X, point[1].X), Math.Max(point[0].Y, point[1].Y));

            graphics.TranslateTransform((p0.X + p1.X) / 2, (p0.Y + p1.Y) / 2);
            graphics.RotateTransform(angle);

            Pen myPen = new Pen(borderColor);
            myPen.DashStyle = dashStyle;
            myPen.Width = width;
            Point pTopLeft = new Point(-(p1.X - p0.X) / 2, -(p1.Y - p0.Y) / 2);
            Point pTopRight = new Point(-pTopLeft.X, pTopLeft.Y);
            Point pBottom = new Point(0, -pTopLeft.Y);

            Point[] p = { pTopLeft, pBottom, pTopRight };
            graphics.DrawCurve(myPen, p);
            if (isSelected)
            {
                int size = 3;
                graphics.FillEllipse(new SolidBrush(Color.Blue), new Rectangle(pTopLeft.X - size, pTopLeft.Y - size, size * 2, size * 2));
            }
            graphics.ResetTransform();
        }
      
       
        public override void Open(string data){
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
            this.angle = float.Parse(dt[8]);
        }

        public override bool AtScalePosition(Point p)
        {
            p = base.Rotate(base.Center(), p, -angle);
            if (Math.Abs(p.X - point[0].X) < 5 && Math.Abs(p.Y - point[0].Y) < 5)
                return true;
            return false;
        }

        public override bool AtRotatePosition(Point p)
        {
            p = base.Rotate(base.Center(), p, -angle);
            return (point[0].X - p.X > 5 && point[0].X - p.X < 15
                && point[0].Y - p.Y > 5 && point[0].Y - p.Y < 15);
        }

        public override void Extend_ExtendableShape(Point p)
        {

        }

        public override string getData()
        {
            Point a = this.Get(0);
            Point b = this.Get(1);
            string data = "Line " + a.X.ToString() + " " + a.Y.ToString() + " " + b.X.ToString() + " " + b.Y.ToString()
                 + " " + dashStyle.ToString() + " " + width.ToString() + " " + borderColor.ToArgb().ToString() + " " + angle.ToString() + "\n";
            return data;
        }
    }
}
