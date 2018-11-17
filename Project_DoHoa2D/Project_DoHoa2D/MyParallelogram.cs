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
        public MyParallelogram()
        {
            point = new List<Point>(4);
            point.Add(new Point(200, 100));
            point.Add(new Point(500, 100));
            point.Add(new Point(100, 300));
            point.Add(new Point(400, 300));

        }

        public MyParallelogram(Point p1, Point p2, Point p3, Point p4)
        {
            point = new List<Point>(4);
            point.Add(p1);
            point.Add(p2);
            point.Add(p3);
            point.Add(p4);
        }

        public MyParallelogram(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            point = new List<Point>(4);
            point.Add(new Point(x1, y1));
            point.Add(new Point(x2, y2));
            point.Add(new Point(x3, y3));
            point.Add(new Point(x4, y4));
        }
        public Point[] ConvertPoint(List<Point> point, Point pivot)
        {
            Point[] myPoint = new Point[point.Count];
            for (int i=0; i<point.Count; i++)
            {
                myPoint[i].X = point[i].X - pivot.X;
                myPoint[i].Y = point[i].Y - pivot.Y;
            }
            return myPoint;
        }
        public override void Draw(Graphics graphics)
        {

            Point pivot = new Point((point[0].X + point[2].X) / 2, (point[0].Y + point[2].Y) / 2);
            graphics.TranslateTransform(pivot.X, pivot.Y);
            graphics.RotateTransform(angle);

            Pen myPen = new Pen(borderColor);
            myPen.DashStyle = dashStyle;
            myPen.Width = width;

            Point[] parallelogram = ConvertPoint(point, pivot);
            if (isFill)
                graphics.FillPolygon(new SolidBrush(backgroundColor), parallelogram);

            graphics.DrawPolygon(myPen, parallelogram);
            graphics.ResetTransform();
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

       
        public override bool Inside(Point p)
        {
            bool res = false;

            Point[] parallelogram = new Point[4];
            for (int i = 0; i < 4; i++)
                parallelogram[i] = point[i];

            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(parallelogram);

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
            for (int i = 0; i < 4; i++)
                Set(new Point(point[i].X + d.X, point[i].Y + d.Y), i);
        }

        public override bool AtScalePosition(Point p)
        {
            throw new NotImplementedException();
        }

        public override bool AtRotatePosition(Point p)
        {
            throw new NotImplementedException();
        }
    }
}
