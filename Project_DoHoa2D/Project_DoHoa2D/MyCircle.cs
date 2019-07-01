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
            point = new List<Point>(2);
            this.point.Add(new Point(10, 10));
            this.point.Add(new Point(20, 20));
        }

        public MyCircle(Point p1, Point p2)
        {
            point = new List<Point>(2);
            point.Add(p1);
            point.Add(p2);
        }

        public override void Set(Point p, int index)
        {
            int d1 = p.X - point[1 - index].X;
            int d2 = p.Y - point[1 - index].Y;
            int d = Math.Min(Math.Abs(d1), Math.Abs(d2)); //Đường kính
            int dx, dy;
            if (d1 < 0)
                dx = -d;
            else dx = d;

            if (d2 < 0)
                dy = -d;
            else dy = d;

            point[index] = new Point(point[1 - index].X + dx, point[1 - index].Y + dy);

        }

        public override Point Get(int index)
        {
            return this.point[index];
        }

        public override void Draw(Graphics graphics){

            Point p0 = new Point(Math.Min(point[0].X, point[1].X), Math.Min(point[0].Y, point[1].Y));
            Point p1 = new Point(Math.Max(point[0].X, point[1].X), Math.Max(point[0].Y, point[1].Y));

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

        public override bool Inside(Point p)
        {
            if (angle != 0)
            {
                p = base.Rotate(base.Center(), p, -angle);
            }

            bool res = false;
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(new Rectangle(point[0], new Size(point[1].X - point[0].X, point[1].Y - point[0].Y)));

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
            for (int i = 0; i < 2; i++)
            {
                Point p = new Point(point[i].X + d.X, point[i].Y + d.Y);
                point[i] = p;
            }
        }

        public override bool AtScalePosition(Point p)
        {
            if (angle != 0)
            {
                p = base.Rotate(base.Center(), p, -angle);
            }
            Point p0 = new Point(Math.Min(point[0].X, point[1].X), Math.Min(point[0].Y, point[1].Y));

            if (Math.Abs(p.X - p0.X) < 5 && Math.Abs(p.Y - p0.Y) < 5)
                return true;
            return false;
        }

        public override bool AtRotatePosition(Point p)
        {
            return false;
        }

        public override void Extend_ExtendableShape(Point p)
        {
            throw new NotImplementedException();
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