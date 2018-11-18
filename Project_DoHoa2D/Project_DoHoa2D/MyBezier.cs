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
    class MyBezier : MyShape
    {

        public MyBezier()
        {
            point = new List<Point> { new Point(10, 10), new Point(20, 20) };
            numPoint = 2;
        }

        public MyBezier(Point p1, Point p2)
        {
            point = new List<Point> { p1, p2 };
            numPoint = 2;
        }

        public Point[] ConvertPoint(List<Point> point, Point pivot)
        {
            Point[] myPoint = new Point[point.Count];
            for (int i = 0; i < point.Count; i++)
            {
                myPoint[i].X = point[i].X - pivot.X;
                myPoint[i].Y = point[i].Y - pivot.Y;
            }
            return myPoint;
        }

        public override bool Inside(Point p){

            p = base.Rotate(base.Center(), p, -angle);

            bool res = false;

            GraphicsPath path = new GraphicsPath();
            List<Point> pointArray = new List<Point>();
            int i = 0;
            while (pointArray.Count < 4)
            {
                pointArray.Add(point[i]);
                if (i < point.Count - 1)
                    i++;
            }
            path.AddBezier(pointArray[0], pointArray[1], pointArray[2], pointArray[3]);

            if (isFill)
                res = path.IsVisible(p);
            else
            {
                Pen pen = new Pen(borderColor, width + 2);
                res = path.IsOutlineVisible(p, pen);
            }
            return res;
        }

        public override void Move(Point d){
            for (int i = 0; i < point.Count; i++)
            {
                Point p = new Point(point[i].X + d.X, point[i].Y + d.Y);
                point[i] = p;
            }
        }

        public override void Set(Point p, int index)
        {
            this.point[index] = base.Rotate(base.Center(), p, -angle);
        }

        public override Point Get(int index)
        {
            if (index >= 0 && index <= numPoint - 1)
                return this.point[index];
            return this.point[0];
        }

        public override void Draw(Graphics graphics){

            int x_min, y_min, x_max, y_max;
            x_min = x_max = point[0].X;
            y_min = y_max = point[0].Y;

            for (int i = 1; i < point.Count; i++)
            {
                if (x_min > point[i].X) x_min = point[i].X;
                if (y_min > point[i].Y) y_min = point[i].Y;
                if (x_max < point[i].X) x_max = point[i].X;
                if (y_max < point[i].Y) y_max = point[i].Y;
            }

            Point p0 = new Point(x_min, y_min);
            Point p1 = new Point(x_max, y_max);

            if (isSelected)
            {
                //vẽ bao

                Pen penBound = new Pen(Color.Blue);
                penBound.DashStyle = DashStyle.Dash;

                graphics.DrawRectangle(penBound, new Rectangle(p0, new Size(p1.X - p0.X, p1.Y - p0.Y)));
                int size = 3;
                graphics.FillEllipse(new SolidBrush(Color.Blue), new Rectangle(p0.X - size, p0.Y - size, size * 2, size * 2));
            }

            Point pivot = base.Center();
            

            graphics.TranslateTransform(pivot.X, pivot.Y);
            graphics.RotateTransform(angle);

            Pen myPen = new Pen(borderColor);
            myPen.DashStyle = dashStyle;
            myPen.Width = width;

            List<Point> pointArray = new List<Point>();
            int idx = 0;
            while (pointArray.Count < 4)
            {
                pointArray.Add(point[idx]);
                if (idx < point.Count - 1)
                    idx++;
            }
            Point[] bezier = ConvertPoint(pointArray, pivot);
            graphics.DrawBezier(myPen, bezier[0], bezier[1], bezier[2], bezier[3]);
            graphics.ResetTransform();

        }

        public override void Save(string filePath){
            Point[] p = new Point[numPoint];
            for (int i = 0; i < numPoint; i++)
                p[i] = this.Get(i);

            string data = "Bezier ";
            data += point.Count.ToString() + " ";

            for (int i = 0; i < numPoint; i++)
            {
                data += p[i].X.ToString() + " ";
                data += p[i].Y.ToString() + " ";
            }
            data += dashStyle.ToString() + " " + width.ToString() + " " + borderColor.ToArgb().ToString() + " " + angle.ToString() + "\n";
            StreamWriter sw = File.AppendText(filePath);
            sw.WriteLine(data);
            sw.Close();
        }
        public override void Open(string data){
            char delimiters = ' ';
            string[] dt = data.Split(delimiters);
            this.numPoint = Int32.Parse(dt[1]);

            Point[] p = new Point[numPoint];
            for (int i = 0, j = 1; i < numPoint; i++, j += 2)
                p[i] = new Point(Int32.Parse(dt[(j + 1)]), Int32.Parse(dt[(j + 1) + 1]));

            point = new List<Point>(numPoint);
            for (int i = 0; i < numPoint; i++)
                point.Add(p[i]);

            switch (dt[numPoint * 2 + 2])
            {
                case "Dash": this.dashStyle = DashStyle.Dash; break;
                case "DashDot": this.dashStyle = DashStyle.DashDot; break;
                case "DashDotDot": this.dashStyle = DashStyle.DashDotDot; break;
                case "Dot": this.dashStyle = DashStyle.Dot; break;
                case "Solid": this.dashStyle = DashStyle.Solid; break;
                case "Custom": this.dashStyle = DashStyle.Custom; break;
            }
            this.width = Int32.Parse(dt[numPoint * 2 + 3]);
            this.borderColor = Color.FromArgb(Convert.ToInt32(dt[numPoint * 2 + 4]));
            this.angle = 0;//float.Parse(dt[numPoint * 2 + 5]);
        }

        public override bool AtScalePosition(Point p)
        {
            return false;
        }

        public override bool AtRotatePosition(Point p)
        {
            return false;
        }

        public override void Extend_ExtendableShape(Point p)
        {
            point.Add(p);
            this.numPoint += 1;
        }
    }
}
