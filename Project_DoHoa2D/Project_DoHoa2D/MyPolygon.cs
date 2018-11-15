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
    class MyPolygon: MyShape
    {
        private int numPoint;
        public MyPolygon()
        {
            point = new List<Point>(4);
            point.Add(new Point(100, 100));
            point.Add(new Point(110, 100));
            point.Add(new Point(110, 110));
            point.Add(new Point(100, 110));

            this.numPoint = 4;
        }

        public MyPolygon(params Point[] p)
        {
            int n = p.Length;
            point = new List<Point>(n);
            foreach (Point pi in p)
                point.Add(pi);
            this.numPoint = n;
        }

        public override void Draw(Graphics graphics)
        {
            Pen myPen = new Pen(borderColor);
            if (width > 1)
                myPen.DashStyle = DashStyle.Solid;
            else
                myPen.DashStyle = dashStyle;
            myPen.Width = width;
            for (int i = 0; i < numPoint - 1; i++)
                graphics.DrawLine(myPen, point[i], point[i+1]);
            graphics.DrawLine(myPen, point[numPoint - 1], point[0]);

        }

        public override void Draw(Graphics graphics, Color borderColor)
        {
            Pen myPen = new Pen(borderColor);
            this.borderColor = borderColor;
            for (int i = 0; i < numPoint - 1; i++)
                graphics.DrawLine(myPen, point[i], point[i + 1]);
            graphics.DrawLine(myPen, point[numPoint - 1], point[0]);
        }

        public override void Draw(Graphics graphics, Color borderColor, DashStyle dashStyle, float width = 1)
        {
            Pen myPen = new Pen(borderColor);
            this.borderColor = borderColor;
            this.dashStyle = dashStyle;
            this.width = width;
            if (width > 1)
                myPen.DashStyle = DashStyle.Solid;
            else
            {
                myPen.DashStyle = dashStyle;
            }
            myPen.Width = width;
            for (int i = 0; i < numPoint - 1; i++)
                graphics.DrawLine(myPen, point[i], point[i + 1]);
            graphics.DrawLine(myPen, point[numPoint - 1], point[0]);

        }

        public override void Set(Point point, int index)
        {
            if (index >=0 && index <=numPoint -1)
                this.point[index] = point;
        }

        public override Point Get(int index)
        {
            if (index >= 0 && index <= numPoint - 1)
                return this.point[index];
            return this.point[0];
        }

        public override void Save(string filePath)
        {
            Point[] p = new Point[numPoint];
            for (int i = 0; i < numPoint; i++)
                p[i] = this.Get(i);

            string data = "Polygon ";
            for (int i = 0; i < numPoint; i++)
            {
                data += p[i].X.ToString() + " ";
                data += p[i].Y.ToString() + " ";
            }
            data += dashStyle.ToString() + " " + width.ToString() + " " + borderColor.ToArgb().ToString() 
                + " " + backgroundColor.ToArgb().ToString() + " " + fillStyle.ToString() + "\n";
            StreamWriter sw = File.AppendText(filePath);
            sw.WriteLine(data);
            sw.Close();
        }

        public override void Open(string data)
        {
            char delimiters = ' ';
            string[] dt = data.Split(delimiters);
            int numberString = dt.Length;
            this.numPoint = (numberString - 6) / 2;

            Point[] p = new Point[numPoint];
            for (int i = 0, j = 0; i < numPoint; i++, j+=2)
                p[i] = new Point(Int32.Parse(dt[(j+1)]), Int32.Parse(dt[(j+1)+1]));

            point = new List<Point>(numPoint);
            for (int i = 0; i < numPoint; i++)
                point.Add(p[i]);

            switch (dt[numPoint * 2 +1])
            {
                case "Dash": this.dashStyle = DashStyle.Dash; break;
                case "DashDot": this.dashStyle = DashStyle.DashDot; break;
                case "DashDotDot": this.dashStyle = DashStyle.DashDotDot; break;
                case "Dot": this.dashStyle = DashStyle.Dot; break;
                case "Solid": this.dashStyle = DashStyle.Solid; break;
                case "Custom": this.dashStyle = DashStyle.Custom; break;
            }
            this.width = Int32.Parse(dt[numPoint * 2 + 2]);
            this.borderColor = Color.FromArgb(Convert.ToInt32(numPoint * 2 + 3));
            this.backgroundColor = Color.FromArgb(Convert.ToInt32(numPoint * 2 + 4));
            this.fillStyle = Int32.Parse(dt[numPoint * 2 + 5]);
        }

        public override void Translation(Point Src, Point Des)
        {
            Point[] p = new Point[numPoint];
            for (int i = 0; i < numPoint; i++)
                p[i] = this.Get(i);

            for (int i = 0; i < numPoint; i++)
            {
                p[i].X += Des.X - Src.X;
                p[i].Y += Des.Y - Src.Y;
            }

            for (int i = 0; i < numPoint; i++)
                this.Set(p[i], i);
        }

        public override void Scaling(Point pivotPoint, float Sx, float Sy)
        {
            Point[] p = new Point[numPoint];
            for (int i = 0; i < numPoint; i++)
                p[i] = this.Get(i);

            int x = 0, y = 0;
            for (int i = 0; i < numPoint; i++)
            {
                x += p[i].X; y += p[i].Y;
            }
            x /= numPoint; y /= numPoint;

            Point mid = new Point(x,y);

            Translation(mid, new Point(0, 0));
            for (int i = 0; i < numPoint; i++)
                p[i] = this.Get(i);


            for (int i = 0; i < numPoint; i++)
            {
                p[i].X = (int)(Sx * p[i].X);
                p[i].Y = (int)(Sy * p[i].Y);
            }

            for (int i = 0; i < numPoint; i++)
                this.Set(p[i], i);

            Translation(new Point(0, 0), mid);
        }

        public override void Rotation(double alpha)
        {
            Point[] p = new Point[numPoint];
            for (int i = 0; i < numPoint; i++)
                p[i] = this.Get(i);

            int x = 0, y = 0;
            for (int i = 0; i < numPoint; i++)
            {
                x += p[i].X; y += p[i].Y;
            }
            x /= numPoint; y /= numPoint;

            Point mid = new Point(x, y);

            Translation(mid, new Point(0, 0));
            for (int i = 0; i < numPoint; i++)
                p[i] = this.Get(i);
            alpha = -alpha;


            for (int i = 0; i < numPoint; i++)
            {
                x = Convert.ToInt32(Math.Cos(alpha) * p[i].X - Math.Sin(alpha) * p[i].Y);
                y = Convert.ToInt32(Math.Sin(alpha) * p[i].X + Math.Cos(alpha) * p[i].Y);
                p[i].X = x; p[i].Y = y;
            }

            for (int i = 0; i < numPoint; i++)
                this.Set(p[i], i);

            Translation(new Point(0, 0), mid);
        }

        public override void Fill(Graphics g, Color backgroundColor, int fillStyle)
        {
            Point[] p = new Point[numPoint];
            for (int i = 0; i < numPoint; i++)
                p[i] = this.Get(i);


            SolidBrush solidBrush;
            HatchBrush myHatchBrush;
            switch (fillStyle)
            {
                case 0:
                    solidBrush = new SolidBrush(backgroundColor);
                    g.FillPolygon(solidBrush, p); break;
                case 1:
                    myHatchBrush = new HatchBrush(HatchStyle.Horizontal, Color.Beige, backgroundColor);
                    g.FillPolygon(myHatchBrush, p); break;
                case 2:
                    myHatchBrush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Beige, backgroundColor);
                    g.FillPolygon(myHatchBrush, p); break;
                case 3:
                    myHatchBrush = new HatchBrush(HatchStyle.Cross, Color.Beige, backgroundColor);
                    g.FillPolygon(myHatchBrush, p); break;
                case 4:
                    myHatchBrush = new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.Beige, backgroundColor);
                    g.FillPolygon(myHatchBrush, p); break;
                case 5:
                    myHatchBrush = new HatchBrush(HatchStyle.DarkHorizontal, Color.Beige, backgroundColor);
                    g.FillPolygon(myHatchBrush, p); break;
                case 6:
                    myHatchBrush = new HatchBrush(HatchStyle.DarkUpwardDiagonal, Color.Beige, backgroundColor);
                    g.FillPolygon(myHatchBrush, p); break;
                case 7:
                    myHatchBrush = new HatchBrush(HatchStyle.DarkVertical, Color.Beige, backgroundColor);
                    g.FillPolygon(myHatchBrush, p); break;
                case 8:
                    myHatchBrush = new HatchBrush(HatchStyle.DashedDownwardDiagonal, Color.Beige, backgroundColor);
                    g.FillPolygon(myHatchBrush, p); break;
            }
        }

    }
}
