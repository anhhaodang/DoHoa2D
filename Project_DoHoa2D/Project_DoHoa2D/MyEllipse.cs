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
    class MyEllipse : MyShape
    {
        public MyEllipse()
        {
            points = new List<Point>(2);
            this.points.Add(new Point(10, 10));
            this.points.Add(new Point(20, 20));
        }

        public MyEllipse(Point p1, Point p2)
        {
            points = new List<Point>(2);
            points.Add(p1);
            points.Add(p2);
        }

        public override void Set(Point p, int index)
        {
            this.points[index] = base.Rotate(base.Center(), p, -angle);
        }

        public override Point Get(int index)
        {
            return this.points[index];
        }

        public override void Draw(Graphics graphics)
        {
            Rectangle r = GetBoundingBox();
            r.Location = new Point(-r.Width / 2, -r.Height / 2);

            graphics.TranslateTransform(this.Center().X, this.Center().Y);
            graphics.RotateTransform(angle);

            Pen myPen = new Pen(borderColor);
            myPen.DashStyle = dashStyle;
            myPen.Width = width;

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
            graphics.DrawEllipse(myPen, r);
            if (isSelected)
                DrawBoudingBox(graphics, r);
            graphics.ResetTransform();
        }

       
        public override void Open(string data)
        {
            char delimiters = ' ';
            string[] dt = data.Split(delimiters);
            Point p1 = new Point(Int32.Parse(dt[1]), Int32.Parse(dt[2]));
            Point p2 = new Point(Int32.Parse(dt[3]), Int32.Parse(dt[4]));

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
        //        return true;
        //    return false;
        //}


        public override void Extend_ExtendableShape(Point p)
        {
            throw new NotImplementedException();
        }

        public override string getData()
        {
            Point p1 = this.Get(0);
            Point p2 = this.Get(1);

            string data = "Ellipse " + p1.X.ToString() + " " + p1.Y.ToString() + " " + p2.X.ToString() + " " + p2.Y.ToString()
                 + " " + dashStyle.ToString()
                 + " " + width.ToString() + " " + borderColor.ToArgb().ToString() + " " + backgroundColor.ToArgb().ToString()
                 + " " + fillStyle.ToString() + " " + isFill.ToString() + " " + hatchStyle.GetHashCode() + " " + angle.ToString() + "\n";
            return data;
        }

        protected override GraphicsPath GetGraphicsPath(Rectangle boudingBox)
        {
            GraphicsPath path = new GraphicsPath();
            //path.AddEllipse(new Rectangle(points[0], new Size(points[1].X - points[0].X, points[1].Y - points[0].Y)));
            path.AddEllipse(boudingBox);
            return path;
        }
    }
}
