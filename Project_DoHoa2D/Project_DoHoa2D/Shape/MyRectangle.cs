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
            Rectangle r = GetBoundingBox();
            Point pCenter = r.Location + new Size(r.Width / 2, r.Height / 2);
            r.Location = new Point(-r.Width / 2, -r.Height / 2);

            graphics.TranslateTransform(pCenter.X, pCenter.Y);
            graphics.RotateTransform(angle);

            Pen myPen = new Pen(borderColor);
            myPen.DashStyle = dashStyle;
            myPen.Width = width;

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
                DrawBoudingBox(graphics, r);
            graphics.ResetTransform();
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

        public override string getData()
        {
            Point p1 = this.Get(0);
            Point p2 = this.Get(1);


            string data = "Rectangle " + p1.X.ToString() + " " + p1.Y.ToString() + " " + p2.X.ToString() + " " + p2.Y.ToString()
                 + " " + dashStyle.ToString()
                 + " " + width.ToString() + " " + borderColor.ToArgb().ToString() + " " + backgroundColor.ToArgb().ToString()
                 + " " + fillStyle.ToString() + " " + isFill.ToString() + " " + hatchStyle.GetHashCode() + " " + angle.ToString() + "\n";
            return data;
        }
    }
}
