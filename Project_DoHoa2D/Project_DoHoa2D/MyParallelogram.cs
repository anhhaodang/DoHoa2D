﻿using System;
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
            point = new List<Point>(2);
            point.Add(new Point(500, 100));
            point.Add(new Point(100, 300));

        }

        public MyParallelogram(Point p1, Point p2)
        {
            point = new List<Point>(2);
            point.Add(p1);
            point.Add(p2);
        }

        public MyParallelogram(int x1, int y1, int x2, int y2)
        {
            point = new List<Point>(2);
            point.Add(new Point(x1, y1));
            point.Add(new Point(x2, y2));
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

            Point p0 = new Point(Math.Min(point[0].X, point[1].X), Math.Min(point[0].Y, point[1].Y));
            Point p1 = new Point(Math.Max(point[0].X, point[1].X), Math.Max(point[0].Y, point[1].Y));

            Point pivot = new Point((p0.X + p1.X)/2, (p0.Y + p1.Y) / 2);

            graphics.TranslateTransform(pivot.X, pivot.Y);
            graphics.RotateTransform(angle);

            Pen myPen = new Pen(borderColor);
            myPen.DashStyle = dashStyle;
            myPen.Width = width;


            Point[] parallelogram = ConvertPoint(point, pivot);
            Point[] parallelogramToDraw = new Point[4];
            parallelogramToDraw[0] = parallelogram[0]; parallelogramToDraw[2] = parallelogram[1];
            parallelogramToDraw[1] = new Point((parallelogram[0].X+parallelogram[1].X) / 2, parallelogram[0].Y);
            parallelogramToDraw[3] = new Point((parallelogram[0].X + parallelogram[1].X) / 2, parallelogram[1].Y);

            Point pTopLeft = new Point(-(p1.X - p0.X) / 2, -(p1.Y - p0.Y) / 2);


            if (isFill)
            {
                if (fillStyle == 0)
                    graphics.FillPolygon(new SolidBrush(backgroundColor), parallelogramToDraw);
                else
                {
                    HatchBrush hatchBrush = new HatchBrush(hatchStyle, backgroundColor);
                    graphics.FillPolygon(hatchBrush, parallelogramToDraw);

                }
            }

            if (isSelected)
            {
                int size = 3;
                graphics.FillEllipse(new SolidBrush(Color.Blue), new Rectangle(pTopLeft.X - size, pTopLeft.Y - size, size * 2, size * 2));
            }

            graphics.DrawPolygon(myPen, parallelogramToDraw);
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


        public override void Open(string data)
        {
            char delimiters = ' ';
            string[] dt = data.Split(delimiters);
            Point p1 = new Point(Int32.Parse(dt[1]), Int32.Parse(dt[2]));
            Point p2 = new Point(Int32.Parse(dt[3]), Int32.Parse(dt[4]));

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
            this.isFill = bool.Parse(dt[10]);
            this.hatchStyle = (HatchStyle)(Int32.Parse(dt[11]));
            this.angle = float.Parse(dt[12]);

        }


        public override bool Inside(Point p)
        {
            p = base.Rotate(base.Center(), p, -angle);

            bool res = false;

            Point[] parallelogramToDraw = new Point[4];
            parallelogramToDraw[0] = point[0]; parallelogramToDraw[2] = point[1];
            parallelogramToDraw[1] = new Point((point[0].X + point[1].X) / 2, point[0].Y);
            parallelogramToDraw[3] = new Point((point[0].X + point[1].X) / 2, point[1].Y);

            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(parallelogramToDraw);

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
            {
                Point p = new Point(point[i].X + d.X, point[i].Y + d.Y);
                point[i] = p;
            }
        }

        public override bool AtScalePosition(Point p)
        {
            p = base.Rotate(base.Center(), p, -angle);
            if (Math.Abs(p.X - point[0].X) < 5 && Math.Abs(p.Y - point[0].Y) < 5)
                return true;
            return false;
        }

        public override bool AtRotatePosition(Point p)
        {
            p = base.Rotate(base.Center(), p, -angle);
            return (point[0].X - p.X > 5 && point[0].X - p.X < 15
                && point[0].Y - p.Y > 5 && point[0].Y - p.Y < 15);
        }

        public override void Extend_ExtendableShape(Point p)
        {
            throw new NotImplementedException();
        }

        public override string getData()
        {
            Point p1 = this.Get(0);
            Point p2 = this.Get(1);

            string data = "Parallelogram " + p1.X.ToString() + " " + p1.Y.ToString() + " " + p2.X.ToString() + " " + p2.Y.ToString()
                  + " " + dashStyle.ToString()
                 + " " + width.ToString() + " " + borderColor.ToArgb().ToString() + " " + backgroundColor.ToArgb().ToString()
                + " " + fillStyle.ToString() + " " + isFill.ToString() + " " + hatchStyle.GetHashCode() + " " + angle.ToString() + "\n";

            return data;
        }
    }
}
