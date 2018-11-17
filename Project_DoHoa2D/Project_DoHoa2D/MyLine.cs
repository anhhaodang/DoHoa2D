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
            point = new List<Point>(2);
            point.Add(new Point(0, 0));
            point.Add(new Point(1, 1));
        }
        public MyLine(Point a, Point b)
        {
            point = new List<Point>(2);
            point.Add(a);
            point.Add(b);
        }
        public MyLine(int x1, int y1, int x2, int y2)
        {
            point = new List<Point>(2);
            point.Add(new Point(x1, y1));
            point.Add(new Point(x2, y2));
        }

        public override void Draw(Graphics graphics)
        {
            Pen myPen = new Pen(borderColor);
            myPen.DashStyle = dashStyle;
            myPen.Width = width;

            graphics.TranslateTransform((point[0].X + point[1].X) / 2, (point[0].Y + point[1].Y) / 2);
            graphics.RotateTransform(angle);

            int a = point[0].X - (point[0].X + point[1].X) / 2;
            int b = point[0].Y - (point[0].Y + point[1].Y) / 2;
            int c = point[1].X - (point[0].X + point[1].X) / 2;
            int d = point[1].Y - (point[0].Y + point[1].Y) / 2;
            
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
            this.point[index] = base.Rotate(base.Center(), p, -angle);
        }
        public override Point Get(int index)
        {
            return this.point[index];
        }

        public override void Save(string filePath)
        {
            Point a = this.Get(0);
            Point b = this.Get(1);
            string data = "Line " +  a.X.ToString() + " " + a.Y.ToString() + " " + b.X.ToString() + " " + b.Y.ToString()
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

        //public override bool AtRotatePosition(Point p)
        //{

        //    return true;
        //}

        //public override bool AtBoundingBox(Point p)
        //{

        //    return true;
        //}

        public override bool Inside(Point p)
        {
            if (angle != 0)
            {
                p = base.Rotate(base.Center(), p, -angle);
            }
            bool res = false;
            GraphicsPath path = new GraphicsPath();
            path.AddLine(point[0], point[1]);

            Pen pen = new Pen(borderColor, width + 2);
            res = path.IsOutlineVisible(p, pen);

            return res;

        }

        public override void Move(Point d)
        {
            for (int i = 0; i < 2; i++)
                this.Set(new Point(point[i].X + d.X, point[i].Y + d.Y), i);
        }

        public override bool AtScalePosition(Point p)
        {
            if (angle != 0)
            {
                p = base.Rotate(base.Center(), p, -angle);
            }

            if (Math.Abs(p.X - point[0].X) < 5 && Math.Abs(p.Y - point[0].Y) < 5)
                return true;
            return false;
        }

        public override bool AtRotatePosition(Point p)
        {
            if (angle != 0)
            {
                p = base.Rotate(base.Center(), p, -angle);
            }

            return (point[0].X - p.X > 5 && point[0].X - p.X < 15
                && point[0].Y - p.Y > 5 && point[0].Y - p.Y < 15);
        }
    }
}
