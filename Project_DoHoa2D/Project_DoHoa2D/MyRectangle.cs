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
            points = new List<Point> { new Point(100, 100), new Point(110, 110) };
        }

        public MyRectangle(Point p1, Point p2)
        {
            points = new List<Point> { p1, p2 };
        }

        public MyRectangle(int x1, int y1, int x2, int y2)
        {
            points = new List<Point>(2);
            points.Add(new Point(x1, y1));
            points.Add(new Point(x2, y2));
        }

        public override void Draw(Graphics graphics)
        {
            Point p0 = new Point(Math.Min(points[0].X, points[1].X), Math.Min(points[0].Y, points[1].Y));
            Point p1 = new Point(Math.Max(points[0].X, points[1].X), Math.Max(points[0].Y, points[1].Y));

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
                return this.points[index];
        }

        public override void Set(Point p, int index)
        {
                this.points[index] = base.Rotate(base.Center(), p, -angle);
        }

        public override void Save(string filePath)
        {
            Point p1 = this.Get(0);
            Point p2 = this.Get(1);


            string data = "Rectangle " + p1.X.ToString() + " " + p1.Y.ToString() + " " + p2.X.ToString() + " " + p2.Y.ToString()
                 + " " + dashStyle.ToString()
                 + " " + width.ToString() + " " + borderColor.ToArgb().ToString() + " " + backgroundColor.ToArgb().ToString()
                 + " " + fillStyle.ToString() + " " + isFill.ToString() + " " + hatchStyle.GetHashCode() + " " + angle.ToString() +  "\n";

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

            points = new List<Point>(2);
            points.Add(p1); points.Add(p2);

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
            this.isFill = bool.Parse(dt[10]);
            this.hatchStyle = (HatchStyle)(Int32.Parse(dt[11]));
            this.angle = float.Parse(dt[12]);

        }


        public override void Move(Point d)
        {
            for (int i = 0; i < 2; i++)
            {
                Point p = new Point(points[i].X + d.X, points[i].Y + d.Y);
                points[i] = p;
            }
        }

        //public override bool AtScalePosition(Point p)
        //{
        //    p = base.Rotate(base.Center(), p, -angle);
        //    if (Math.Abs(p.X - points[0].X) < 5 && Math.Abs(p.Y - points[0].Y) < 5)
        //            return true;
        //    return false;
        //}

        public override void Extend_ExtendableShape(Point p)
        {
            
        }

        protected override GraphicsPath GetGraphicsPath(Rectangle boundingBox)
        {
            GraphicsPath path = new GraphicsPath();
            //path.AddRectangle(new Rectangle(points[0], new Size(points[1].X - points[0].X, points[1].Y - points[0].Y)));
            path.AddRectangle(boundingBox);
            return path;
        }
    }
}
