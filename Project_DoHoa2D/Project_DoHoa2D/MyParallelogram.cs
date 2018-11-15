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
    class MyParallelogram : MyShape
    {
        private int numPoint = 4;

        public MyParallelogram()
        {
            point = new List<Point>(4);
            point.Add(new Point(200, 100));
            point.Add(new Point(500, 100));
            point.Add(new Point(100, 300));
            point.Add(new Point(400, 300));

            this.numPoint = 4;
        }

        public MyParallelogram(Point p1, Point p2, Point p3, Point p4)
        {
            point = new List<Point>(4);
            point.Add(p1);
            point.Add(p2);
            point.Add(p3);
            point.Add(p4);
            this.numPoint = 4;
        }

        public MyParallelogram(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            point = new List<Point>(4);
            point.Add(new Point(x1, y1));
            point.Add(new Point(x2, y2));
            point.Add(new Point(x3, y3));
            point.Add(new Point(x4, y4));
            this.numPoint = 4;
        }
        public override void Draw(Graphics graphics)
        {
            Pen myPen = new Pen(borderColor);
            if (width > 1)
                myPen.DashStyle = DashStyle.Solid;
            else
                myPen.DashStyle = dashStyle;
            myPen.Width = width;
            graphics.DrawLine(myPen, point[0], point[1]);
            graphics.DrawLine(myPen, point[1], point[2]);
            graphics.DrawLine(myPen, point[2], point[3]);
            graphics.DrawLine(myPen, point[3], point[0]);
        }

        public override void Draw(Graphics graphics, Color borderColor)
        {
            Pen myPen = new Pen(borderColor);
            this.borderColor = borderColor;
            graphics.DrawLine(myPen, point[0], point[1]);
            graphics.DrawLine(myPen, point[1], point[2]);
            graphics.DrawLine(myPen, point[2], point[3]);
            graphics.DrawLine(myPen, point[3], point[0]);
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
            // if (penStyle >= 0 && penStyle <= 5)
            graphics.DrawLine(myPen, point[0], point[1]);
            graphics.DrawLine(myPen, point[1], point[2]);
            graphics.DrawLine(myPen, point[2], point[3]);
            graphics.DrawLine(myPen, point[3], point[0]);
        }



        public override void Set(Point point, int index)
        {
            this.point[index] = point;
        }

        public override Point Get(int index)
        {
            return this.point[index];
        }


        public override void Save(string filePath)
        {
            Point p1 = this.Get(0);
            Point p2 = this.Get(1);
            Point p3 = this.Get(2);
            Point p4 = this.Get(3);

            string data = "Parallelogram " + p1.X.ToString() + " " + p1.Y.ToString() + " " + p2.X.ToString() + " " + p2.Y.ToString()
                 + " " + p3.X.ToString() + " " + p3.Y.ToString() + " " + p4.X.ToString() + " " + p4.Y.ToString() + " " + dashStyle.ToString()
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
            Point p1 = new Point(Int32.Parse(dt[1]), Int32.Parse(dt[2]));
            Point p2 = new Point(Int32.Parse(dt[3]), Int32.Parse(dt[4]));
            Point p3 = new Point(Int32.Parse(dt[5]), Int32.Parse(dt[6]));
            Point p4 = new Point(Int32.Parse(dt[7]), Int32.Parse(dt[8]));

            point = new List<Point>(4);
            point.Add(p1); point.Add(p2); point.Add(p3); point.Add(p4);

            switch (dt[9])
            {
                case "Dash": this.dashStyle = DashStyle.Dash; break;
                case "DashDot": this.dashStyle = DashStyle.DashDot; break;
                case "DashDotDot": this.dashStyle = DashStyle.DashDotDot; break;
                case "Dot": this.dashStyle = DashStyle.Dot; break;
                case "Solid": this.dashStyle = DashStyle.Solid; break;
                case "Custom": this.dashStyle = DashStyle.Custom; break;
            }
            this.width = Int32.Parse(dt[10]);
            this.borderColor = Color.FromArgb(Convert.ToInt32(dt[11]));
            this.backgroundColor = Color.FromArgb(Convert.ToInt32(dt[12]));
            this.fillStyle = Int32.Parse(dt[13]);
        }

        public override void Translation(Point Src, Point Des)
        {
            Point p1 = this.Get(0); Point p2 = this.Get(1);
            Point p3 = this.Get(2); Point p4 = this.Get(3);

            p1.X += Des.X - Src.X; p1.Y += Des.Y - Src.Y;
            p2.X += Des.X - Src.X; p2.Y += Des.Y - Src.Y;
            p3.X += Des.X - Src.X; p3.Y += Des.Y - Src.Y;
            p4.X += Des.X - Src.X; p4.Y += Des.Y - Src.Y;

            this.Set(p1, 0); this.Set(p2, 1);
            this.Set(p3, 2); this.Set(p4, 3);
        }

        public override void Rotation(double alpha)
        {
            Point p1 = this.Get(0); Point p2 = this.Get(1);
            Point p3 = this.Get(2); Point p4 = this.Get(3);

            Point mid = new Point((p1.X + p3.X) / 2, (p1.Y + p3.Y) / 2);
            Translation(mid, new Point(0, 0));
            p1 = this.Get(0); p2 = this.Get(1);
            p3 = this.Get(2); p4 = this.Get(3);
            alpha = -alpha;

            int x, y;
            x = Convert.ToInt32(Math.Cos(alpha) * p1.X - Math.Sin(alpha) * p1.Y);
            y = Convert.ToInt32(Math.Sin(alpha) * p1.X + Math.Cos(alpha) * p1.Y);
            p1.X = x; p1.Y = y;

            x = Convert.ToInt32(Math.Cos(alpha) * p2.X - Math.Sin(alpha) * p2.Y);
            y = Convert.ToInt32(Math.Sin(alpha) * p2.X + Math.Cos(alpha) * p2.Y);
            p2.X = x; p2.Y = y;

            x = Convert.ToInt32(Math.Cos(alpha) * p3.X - Math.Sin(alpha) * p3.Y);
            y = Convert.ToInt32(Math.Sin(alpha) * p3.X + Math.Cos(alpha) * p3.Y);
            p3.X = x; p3.Y = y;

            x = Convert.ToInt32(Math.Cos(alpha) * p4.X - Math.Sin(alpha) * p4.Y);
            y = Convert.ToInt32(Math.Sin(alpha) * p4.X + Math.Cos(alpha) * p4.Y);
            p4.X = x; p4.Y = y;


            this.Set(p1, 0); this.Set(p2, 1);
            this.Set(p3, 2); this.Set(p4, 3);

            Translation(new Point(0, 0), mid);
        }


        public override void Scaling(Point pivotPoint, float Sx, float Sy)
        {
            Point p1 = this.Get(0); Point p2 = this.Get(1);
            Point p3 = this.Get(2); Point p4 = this.Get(3);

            Point mid = new Point((p1.X + p3.X) / 2, (p1.Y + p3.Y) / 2);
            Translation(mid, new Point(0, 0));
            p1 = this.Get(0); p2 = this.Get(1);
            p3 = this.Get(2); p4 = this.Get(3);

            p1.X = (int)(Sx * p1.X); p1.Y = (int)(Sy * p1.Y);
            p2.X = (int)(Sx * p2.X); p2.Y = (int)(Sy * p2.Y);
            p3.X = (int)(Sx * p3.X); p3.Y = (int)(Sy * p3.Y);
            p4.X = (int)(Sx * p4.X); p4.Y = (int)(Sy * p4.Y);


            this.Set(p1, 0); this.Set(p2, 1);
            this.Set(p3, 2); this.Set(p4, 3);

            Translation(new Point(0, 0), mid);
        }


        public override void Fill(Graphics g, Color backgroundColor, int fillStyle)
        {
            Point[] myPoint = new Point[4];
            for (int i = 0; i < numPoint; i++)
            {
                myPoint[i] = this.Get(i);
            }

            SolidBrush solidBrush;
            HatchBrush myHatchBrush;
            switch (fillStyle)
            {
                case 0:
                    solidBrush = new SolidBrush(backgroundColor);
                    g.FillPolygon(solidBrush, myPoint); break;
                case 1:
                    myHatchBrush = new HatchBrush(HatchStyle.Horizontal, Color.Beige, backgroundColor);
                    g.FillPolygon(myHatchBrush, myPoint); break;
                case 2:
                    myHatchBrush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Beige, backgroundColor);
                    g.FillPolygon(myHatchBrush, myPoint); break;
                case 3:
                    myHatchBrush = new HatchBrush(HatchStyle.Cross, Color.Beige, backgroundColor);
                    g.FillPolygon(myHatchBrush, myPoint); break;
                case 4:
                    myHatchBrush = new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.Beige, backgroundColor);
                    g.FillPolygon(myHatchBrush, myPoint); break;
                case 5:
                    myHatchBrush = new HatchBrush(HatchStyle.DarkHorizontal, Color.Beige, backgroundColor);
                    g.FillPolygon(myHatchBrush, myPoint); break;
                case 6:
                    myHatchBrush = new HatchBrush(HatchStyle.DarkUpwardDiagonal, Color.Beige, backgroundColor);
                    g.FillPolygon(myHatchBrush, myPoint); break;
                case 7:
                    myHatchBrush = new HatchBrush(HatchStyle.DarkVertical, Color.Beige, backgroundColor);
                    g.FillPolygon(myHatchBrush, myPoint); break;
                case 8:
                    myHatchBrush = new HatchBrush(HatchStyle.DashedDownwardDiagonal, Color.Beige, backgroundColor);
                    g.FillPolygon(myHatchBrush, myPoint); break;
            }

        }

    }
}
