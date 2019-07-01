using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace Project_DoHoa2D
{
    class MyCircle : MyShape
    {

        public MyCircle()
        {
            points = new List<Point>(2);
            this.points.Add(new Point(10, 10));
            this.points.Add(new Point(20, 20));
        }

        public MyCircle(Point p1, Point p2)
        {
            points = new List<Point>(2);
            points.Add(p1);
            points.Add(p2);
        }

        public override void Set(Point p, int index)
        {
            int d1 = p.X - points[1 - index].X;
            int d2 = p.Y - points[1 - index].Y;
            int d = Math.Min(Math.Abs(d1), Math.Abs(d2)); //Đường kính
            int dx, dy;
            if (d1 < 0)
                dx = -d;
            else dx = d;

            if (d2 < 0)
                dy = -d;
            else dy = d;

            points[index] = new Point(points[1 - index].X + dx, points[1 - index].Y + dy);

        }

        public override Point Get(int index)
        {
            return this.points[index];
        }

        public override void Draw(Graphics graphics){

            Point p0 = new Point(Math.Min(points[0].X, points[1].X), Math.Min(points[0].Y, points[1].Y));
            Point p1 = new Point(Math.Max(points[0].X, points[1].X), Math.Max(points[0].Y, points[1].Y));

            if (isSelected)
            {
                //vẽ bao

                Pen penBound = new Pen(Color.Blue);
                penBound.DashStyle = DashStyle.Dash;

                graphics.DrawRectangle(penBound, new Rectangle(p0, new Size(p1.X - p0.X, p1.Y - p0.Y)));
                int size = 3;
                graphics.FillEllipse(new SolidBrush(Color.Blue), new Rectangle(p0.X - size, p0.Y - size, size * 2, size * 2));
            }

            int d = p1.X - p0.X; //Đường kính đường tròn

            Rectangle r = new Rectangle(p0.X, p0.Y, d, d);

            if (isFill)
            {
                if (fillStyle == 0)
                    graphics.FillEllipse(new SolidBrush(backgroundColor), r);
                else
                {
                    HatchBrush hatchBrush = new HatchBrush(hatchStyle, backgroundColor);
                    graphics.FillEllipse(hatchBrush, r);

                }
            }


            Pen p = new Pen(borderColor);
            p.DashStyle = dashStyle;
            p.Width = width;
            graphics.DrawEllipse(p, r);
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

        }
        
        public override void Extend_ExtendableShape(Point p)
        {
            throw new NotImplementedException();
        }

        protected override GraphicsPath GetGraphicsPath(Rectangle boundingBox)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(new Rectangle(points[0], new Size(points[1].X - points[0].X, points[1].Y - points[0].Y)));
            return path;
        }

        public override string getData()
        {
            Point p1 = this.Get(0);
            Point p2 = this.Get(1);

            string data = "Circle " + p1.X.ToString() + " " + p1.Y.ToString() + " " + p2.X.ToString() + " " + p2.Y.ToString()
                 + " " + dashStyle.ToString()
                 + " " + width.ToString() + " " + borderColor.ToArgb().ToString() + " " + backgroundColor.ToArgb().ToString()
                 + " " + fillStyle.ToString() + " " + isFill.ToString() + " " + hatchStyle.GetHashCode() + "\n";
            return data;
        }
    }
}