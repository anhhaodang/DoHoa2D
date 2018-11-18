﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DoHoa2D
{
    class MyEllipse : MyShape
    {
        public MyEllipse()
        {
            point = new List<Point>(2);
            this.point.Add(new Point(10, 10));
            this.point.Add(new Point(20, 20));
        }

        public MyEllipse(Point p1, Point p2)
        {
            point = new List<Point>(2);
            point.Add(p1);
            point.Add(p2);
        }

        public override void Set(Point p, int index)
        {
            this.point[index] = base.Rotate(base.Center(), p, -angle);
        }

        public override Point Get(int index)
        {
            return new Point(0, 0);
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
                    graphics.FillEllipse(new SolidBrush(backgroundColor), r);
                else
                {
                    HatchBrush hatchBrush = new HatchBrush(hatchStyle, backgroundColor);
                    graphics.FillEllipse(hatchBrush, r);

                }
            }
            graphics.DrawEllipse(myPen, r);
            if (isSelected)
            {
                Pen penBound = new Pen(Color.Blue);
                penBound.DashStyle = DashStyle.Dash;

                graphics.DrawRectangle(penBound, r);

                int size = 3;
                graphics.FillEllipse(new SolidBrush(Color.Blue), new Rectangle(pTopLeft.X - size, pTopLeft.Y - size, size * 2, size * 2));
            }
            graphics.ResetTransform();
        }

        public override void Save(string filePath)
        {
            throw new NotImplementedException();
        }

        public override void Open(string data)
        {
            throw new NotImplementedException();
        }

        public override bool Inside(Point p)
        {
            if (angle != 0)
            {
                p = base.Rotate(base.Center(), p, -angle);
            }

            bool res = false;
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(new Rectangle(point[0], new Size(point[1].X - point[0].X, point[1].Y - point[0].Y)));

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

        

       


        
        
    }
}