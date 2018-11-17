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

        public override void Set(Point p, int index){
            int d1 = p.X - point[0].X;
            int d2 = p.Y - point[0].Y;
            int d = Math.Min(Math.Abs(d1), Math.Abs(d2)); //Đường kính

            point[index] = new Point(point[0].X + (d1 < 0 ? -d : d), point[1 - index].Y + (d2 < 0 ? -d : d));
            //this.point[index] = p;

        }
        public override Point Get(int index){ return new Point(0, 0); }

        public override void Draw(Graphics graphics){

            if (isSelected)
            {
                //vẽ bao
                Point p0 = new Point(Math.Min(point[0].X, point[1].X), Math.Min(point[0].Y, point[1].Y));
                Point p1 = new Point(Math.Max(point[0].X, point[1].X), Math.Max(point[0].Y, point[1].Y));

                Pen penBound = new Pen(Color.Blue);
                penBound.DashStyle = DashStyle.Dash;

                graphics.DrawRectangle(penBound, new Rectangle(p0, new Size(p1.X - p0.X, p1.Y - p0.Y)));
                int size = 3;
                graphics.FillEllipse(new SolidBrush(Color.Blue), new Rectangle(p0.X - size, p0.Y - size, size * 2, size * 2));
            }

            int d = point[1].X - point[0].X; //Đường kính đường tròn

            Rectangle r = new Rectangle(point[0].X, point[0].Y, d, d);

            if (isFill)
                graphics.FillEllipse(new SolidBrush(backgroundColor), r);

            Pen p = new Pen(borderColor);
            p.DashStyle = dashStyle;
            p.Width = width;
            graphics.DrawEllipse(p, r);
        }

        public override void Save(string filePath)
        {
            Point a = this.Get(0);
            Point b = this.Get(1);
            string data = "Circle " + a.X.ToString() + " " + a.Y.ToString() + " " + b.X.ToString() + " " + b.Y.ToString()
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

        public override bool Inside(Point p)
        {
            if (angle != 0)
            {
                p = base.Rotate(base.Center(), p, angle);
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
                Set(new Point(point[i].X + d.X, point[i].Y + d.Y), i);
        }

        public override bool AtScalePosition(Point p)
        {
            if (angle != 0)
            {
                p = base.Rotate(base.Center(), p, angle);
            }
            Point p0 = new Point(Math.Min(point[0].X, point[1].X), Math.Min(point[0].Y, point[1].Y));

            if (Math.Abs(p.X - p0.X) < 5 && Math.Abs(p.Y - p0.Y) < 5)
                return true;
            return false;
        }

        public override bool AtRotatePosition(Point p)
        {
            throw new NotImplementedException();
        }
    }
}