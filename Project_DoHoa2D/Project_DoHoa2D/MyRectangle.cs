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
    class MyRectangle : MyShape
    {

        public MyRectangle()
        {
            point = new List<Point>(2);
            point.Add(new Point(100, 100));
            point.Add(new Point(110, 110));

        }

        public MyRectangle(Point p1, Point p2)
        {
            point = new List<Point>(2);
            point.Add(p1);
            point.Add(p2);
        }

        public MyRectangle(int x1, int y1, int x2, int y2)
        {
            point = new List<Point>(2);
            point.Add(new Point(x1, y1));
            point.Add(new Point(x2, y2));
        }

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
            Rectangle r = new Rectangle(pTopLeft, new Size(p1.X - p0.X, p1.Y - p0.Y));

            if (isFill)
            {
                if (fillStyle == 0)
                    graphics.FillRectangle(new SolidBrush(backgroundColor), r);
                else
                {
                    HatchBrush hatchBrush = new HatchBrush(hatchStyle, backgroundColor);
                    graphics.FillRectangle(hatchBrush, r);

                }
            }
            graphics.DrawRectangle(myPen, r);
            if (isSelected)
            {
                int size = 3;
                graphics.FillEllipse(new SolidBrush(Color.Blue), new Rectangle(pTopLeft.X - size, pTopLeft.Y - size, size * 2, size * 2));

            }
            graphics.ResetTransform();
        }
       

        public override Point Get(int index)
        {
                return this.point[index];
        }

        public override void Set(Point point, int index)
        {
                this.point[index] = point;
        }

        public void Configure(Color? BorderColor = null, DashStyle? DashStyle = null, 
            float? Width = null, float? Angel = null, bool? IsSelected = null, bool? IsFill = null, Color? BackgroundColor = null)
        {
            base.Configure(BorderColor, DashStyle, Width, Angel, IsSelected);
            if (IsFill.HasValue)
                isFill = IsFill.Value;
            if (BackgroundColor.HasValue)
                backgroundColor = BackgroundColor.Value;
        }

        public override void Save(string filePath)
        {
            Point p1 = this.Get(0);
            Point p2 = this.Get(1);


            string data = "Retangle " + p1.X.ToString() + " " + p1.Y.ToString() + " " + p2.X.ToString() + " " + p2.Y.ToString()
                 + " " + dashStyle.ToString()
                 + " " + width.ToString() + " " + borderColor.ToArgb().ToString() + " " + backgroundColor.ToArgb().ToString()
                 + " " + fillStyle.ToString() + "\n";

            StreamWriter sw = File.AppendText(filePath);
            sw.WriteLine(data);
            sw.Close();
        }

        public override void Open(string data)
        {
            char delimiters = ' ';
            string[] dt = data.Split(delimiters);
            Point p1= new Point(Int32.Parse(dt[1]), Int32.Parse(dt[2]));
            Point p2= new Point(Int32.Parse(dt[3]), Int32.Parse(dt[4]));

            point = new List<Point>(2);
            point.Add(p1); point.Add(p2);

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
            this.backgroundColor = Color.FromArgb(Convert.ToInt32(dt[8]));
            this.fillStyle = Int32.Parse(dt[9]);
        }


        public override bool Inside(Point p)
        {
            if (angle != 0)
            {
                p = base.Rotate(base.Center(), p, -angle);
            }

            bool res = false;
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new Rectangle(point[0], new Size(point[1].X - point[0].X, point[1].Y - point[0].Y)));


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
            for (int i = 0; i < point.Count; i++)
                Set(new Point(point[i].X + d.X, point[i].Y + d.Y), i);
        }

        public override bool AtScalePosition(Point p)
        {
            if (angle != 0)
            {
                p = base.Rotate(base.Center(), p, angle);
            }

            if (Math.Abs(p.X - point[0].X) < 5 && Math.Abs(p.Y - point[0].Y) < 5)
                    return true;
            return false;
        }

        public override bool AtRotatePosition(Point p)
        {
            if (angle != 0)
            {
                p = base.Rotate(base.Center(), p, angle);
            }

            return (point[0].X - p.X > 5 && point[0].X - p.X < 15 
                && point[0].Y - p.Y > 5 && point[0].Y - p.Y < 15);
        }
    }
}
