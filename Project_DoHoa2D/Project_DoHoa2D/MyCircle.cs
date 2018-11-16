using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace Project_DoHoa2D
{
    class MyCircle : MyShape
    {
        protected int fillStyle = 0;
        private bool isFill;
        private Color backgroundColor;

        public MyCircle()
        {
            this.point.Add(new Point(10, 10));
            this.point.Add(new Point(20, 20));
        }

        public MyCircle(Point p1, Point p2)
        {
            point.Add(p1);
            point.Add(p2);
        }

        public override void Set(Point point, int index){ }
        public override Point Get(int index){ return new Point(0, 0); }

        public override void Draw(Graphics graphics){

            int d = point[1].X - point[0].X; //Đường kính đường tròn
            graphics.TranslateTransform(point[0].X + d / 2, point[0].Y + d / 2);
            graphics.RotateTransform(angel);

            Rectangle r = new Rectangle(-d / 2, -d / 2, d, d);

            if (isFill)
                graphics.FillEllipse(new SolidBrush(backgroundColor), r);

            Pen p = new Pen(borderColor);
            p.DashStyle = dashStyle;
            p.Width = width;
            graphics.DrawEllipse(p, r);
            graphics.ResetTransform();
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

        public override void Fill(Graphics graphics, Color backgroundColor, int fillStyle) { }
    }
}