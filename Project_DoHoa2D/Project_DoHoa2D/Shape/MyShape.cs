﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace Project_DoHoa2D
{
    public abstract class MyShape
    {
        protected List<Point> points;
        public int numPoint;
        protected Color borderColor = Color.Black;
        protected DashStyle dashStyle = DashStyle.Solid;
        protected float width = 1;
        public int fillStyle = 0;
        protected HatchStyle hatchStyle = HatchStyle.BackwardDiagonal;
        protected Color backgroundColor = Color.White;
        public bool isFill = false;

        public float angle = 0;
        public bool isSelected = false;

        public static MyShape Create(ShapeTypeDefine shapeType, Point p1, Point p2)
        {
            MyShape shape = null;
            switch (shapeType)
            {
                case ShapeTypeDefine.LINE:
                    shape = new MyLine(p1, p2);
                    break;
                case ShapeTypeDefine.CIRCLE:
                    shape = new MyCircle(p1, p2);
                    break;
                case ShapeTypeDefine.ELLIPSE:
                    shape = new MyEllipse(p1, p2);
                    break;
                case ShapeTypeDefine.POLYGON:
                    shape = new MyPolygon(p1, p2);
                    break;
                case ShapeTypeDefine.POLYLINE:
                    shape = new MyPolyline(p1, p2);
                    break;
                case ShapeTypeDefine.BEZIER:
                    shape = new MyBezier(p1, p2);
                    break;
                case ShapeTypeDefine.RECTANGLE:
                    shape = new MyRectangle(p1, p2);
                    break;
                default:
                    shape = new MyRectangle(p1, p2);
                    break;
            }
            return shape;
        }

        #region adjust attribute

        internal void SetGraphicsAttribute(GraphicAttribute attributes)
        {
            if (attributes[AttributeType.borderColor] != null)
                borderColor = attributes[AttributeType.borderColor];

            if (attributes[AttributeType.dashStyle] != null)
                dashStyle = (DashStyle)attributes[AttributeType.dashStyle];

            if (attributes[AttributeType.width] != null)
                width = attributes[AttributeType.width];

            if (attributes[AttributeType.backgroundColor] != null)
                backgroundColor = attributes[AttributeType.backgroundColor];

            if (attributes[AttributeType.isFill] != null)
                isFill = attributes[AttributeType.isFill];

            if (attributes[AttributeType.fillStyle] != null)
                fillStyle = attributes[AttributeType.fillStyle];

            if (attributes[AttributeType.width] != null)
                width = attributes[AttributeType.width];

            if (attributes[AttributeType.hatchStyle] != null)
                hatchStyle = (HatchStyle)attributes[AttributeType.hatchStyle];
        }

        public void Configure(Color? BorderColor = null, DashStyle? DashStyle = null, float? Width = null,
                       float? Angle = null, bool? IsSelected = null, Color? BackgroundColor = null,
                       int? FillStyle = null, bool? IsFill = null, HatchStyle? HatchStyle = null)
        {
            if (BorderColor.HasValue)
                borderColor = BorderColor.Value;
            if (DashStyle.HasValue)
                dashStyle = DashStyle.Value;
            if (Width.HasValue)
                width = Width.Value;
            if (Angle.HasValue)
            {
                angle += Angle.Value;

                if (angle < -180)
                    angle = 360 + angle;
                else if (angle > 180)
                    angle -= 360;
            }
            if (IsSelected.HasValue)
                isSelected = IsSelected.Value;
            if (BackgroundColor.HasValue)
                backgroundColor = BackgroundColor.Value;
            if (FillStyle.HasValue)
                fillStyle = FillStyle.Value;
            if (IsFill.HasValue)
                isFill = IsFill.Value;
            if (HatchStyle.HasValue)
                hatchStyle = HatchStyle.Value;
        }

        #endregion

        #region calculate mouse position

        public bool Inside(Point p, Rectangle boundingBox)
        {
            bool res = false;
            GraphicsPath path = this.GetGraphicsPath(boundingBox);

            if (isFill)
                res = path.IsVisible(p);
            else
            {
                Pen pen = new Pen(borderColor, width + 5);
                res = path.IsOutlineVisible(p, pen);
            }
            return res;
        }

        public bool AtRotatePosition(Point p, Rectangle boundingBox)
        {
            Point rotatePosition = this.GetRotatePosition(boundingBox);
            return Utils.Near(p, rotatePosition, 5);
        }

        public int AtScalePosition(Point p, Rectangle boundingBox)
        {
            List<Point> corners = new List<Point>();
            corners.Add(boundingBox.Location);
            corners.Add(boundingBox.Location + new Size(0, boundingBox.Height));
            corners.Add(boundingBox.Location + boundingBox.Size);
            corners.Add(boundingBox.Location + new Size(boundingBox.Width, 0));

            for (int i = 0; i < 4; i++)
            {
                if (Utils.Near(p, corners[i]))
                    return i;
            }
            return -1;
        }

        protected Point GetRotatePosition(Rectangle boudingBox)
        {
            Point p = boudingBox.Location + new Size(boudingBox.Width / 2, -10);
            return p;
        }

        internal MouseInfo CalcMousePosition(Point mousePos)
        {
            Rectangle boundingBox = this.GetBoundingBox();

            Rectangle originalBoundingBox = boundingBox;

            Point pCenter = boundingBox.Location + new Size(boundingBox.Width / 2, boundingBox.Height / 2);
            boundingBox.Location = new Point(-boundingBox.Width / 2, -boundingBox.Height / 2);
            mousePos = this.Rotate(pCenter, mousePos, -angle);
            Point originalMousePos = mousePos;

            mousePos -= new Size(pCenter);

            MouseInfo mouseInfo = new MouseInfo();
            if (isSelected)
            {
                if (AtRotatePosition(mousePos, boundingBox))
                {
                    mouseInfo.state = StateMouse.Rotate;
                    mouseInfo.shapeUnder = this;
                    return mouseInfo;
                }

                int corner = AtScalePosition(mousePos, boundingBox);
                if (corner >= 0)
                {
                    mouseInfo.state = StateMouse.Scale;
                    mouseInfo.shapeUnder = this;
                    mouseInfo.corner = (Corner)corner;
                    return mouseInfo;
                }
            }

            if (Inside(originalMousePos, originalBoundingBox))
            {
                mouseInfo.state = StateMouse.Inside;
                mouseInfo.shapeUnder = this;
                return mouseInfo;
            }

            return mouseInfo;
        }
        #endregion

        #region method relative with boundingBox
        protected Rectangle GetBoundingBox()
        {
            int xMin, xMax, yMin, yMax;
            xMin = points[0].X;
            yMin = points[0].Y;
            xMax = points[0].X;
            yMax = points[0].Y;
            for (int i = 1; i < points.Count; i++)
            {
                Point p = points[i];
                if (p.X > xMax)
                    xMax = p.X;
                if (p.Y > yMax)
                    yMax = p.Y;
                if (p.X < xMin)
                    xMin = p.X;
                if (p.Y < yMin)
                    yMin = p.Y;
            }

            return new Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);
        }

        protected void DrawBoudingBox(Graphics graphics, Rectangle boundingBox)
        {
            List<Point> corners = new List<Point>();
            corners.Add(boundingBox.Location);
            corners.Add(boundingBox.Location + new Size(boundingBox.Width, 0));
            corners.Add(boundingBox.Location + new Size(0, boundingBox.Height));
            corners.Add(boundingBox.Location + boundingBox.Size);

            Pen penBound = new Pen(Color.Blue);
            penBound.DashStyle = DashStyle.Dash;

            graphics.DrawRectangle(penBound, boundingBox);

            int size = 3;

            for (int i = 0; i < 4; i++)
                graphics.FillEllipse(new SolidBrush(Color.Blue), new Rectangle(corners[i] - new Size(size, size), new Size(size * 2, size * 2)));
            Point rotatePoint = this.GetRotatePosition(boundingBox);
            graphics.FillEllipse(new SolidBrush(Color.Blue), new Rectangle(rotatePoint - new Size(size, size), new Size(size * 2, size * 2)));
        }
        #endregion

        #region Transferomation
        public void Move(Size d)
        {
            for (int i = 0; i < points.Count; i++)
                points[i] += d;
        }

        public Point Rotate(Point origin, Point p, float alpha)
        {
            int x = p.X - origin.X;
            int y = p.Y - origin.Y;

            alpha = (float)Math.PI / 180 * alpha;

            int tempX = Convert.ToInt32(Math.Cos(alpha) * x - Math.Sin(alpha) * y);
            int tempY = Convert.ToInt32(Math.Sin(alpha) * x + Math.Cos(alpha) * y);

            x = tempX + origin.X;
            y = tempY + origin.Y;

            Point res = new Point(x, y);
            return res;
        }

        public Point GetCenterPoint()
        {
            int x = 0, y = 0;
            int n = points.Count;
            for (int i = 0; i < n; i++)
            {
                x += points[i].X; y += points[i].Y;
            }
            x /= n; y /= n;
            Point res = new Point(x, y);
            return res;
        }

        internal void Scale(Point srcPos, Point desPos, Point? anchor = null)
        {
            Rectangle r = this.GetBoundingBox();
            if (!anchor.HasValue)
                anchor = r.Location + new Size(r.Width / 2, r.Height / 2);
            Point anchorPoint = (Point)anchor;
            float ratioX, ratioY;
            float distance = (srcPos.X - anchorPoint.X);
            if (distance == 0)
                distance = 0.01F;
            ratioX = (float)1.0 * (desPos.X - anchorPoint.X) / distance;

            distance = (srcPos.Y - anchorPoint.Y);
            if (distance == 0)
                distance = 0.01F;
            ratioY = (float)1.0 * (desPos.Y - anchorPoint.Y) / distance;

            for (int i = 0; i < points.Count; i++)
            {
                int newX = (int)Math.Round(ratioX * (points[i].X - anchorPoint.X) + anchorPoint.X);
                int newY = (int)Math.Round(ratioY * (points[i].Y - anchorPoint.Y) + anchorPoint.Y);
                points[i] = new Point(newX, newY);
            }
        }

        #endregion

        #region Save Load Undo Redo
        public abstract string getData();
        public void Save(string filePath)
        {
            string data = getData();
            StreamWriter sw = File.AppendText(filePath);
            sw.WriteLine(data);
            sw.Close();
        }
        public abstract void Open(string data);
        #endregion

        protected abstract GraphicsPath GetGraphicsPath(Rectangle boundingBox);
        public abstract void Extend_ExtendableShape(Point p);
        public abstract void Draw(Graphics graphics);

        public void Set(Point point, int index)
        {
            this.points[index] = Rotate(GetCenterPoint(), point, -angle);
        }

        public Point Get(int index)
        {
            return this.points[index];
        }

        public void Normalize()
        {
            Point p0 = new Point(Math.Min(points[0].X, points[1].X), Math.Min(points[0].Y, points[1].Y));
            Point p1 = new Point(Math.Max(points[0].X, points[1].X), Math.Max(points[0].Y, points[1].Y));

            points[0] = p0;
            points[1] = p1;
        }

        private double Lenght(Point p)
        {
            return Math.Sqrt(p.X * p.X + p.Y * p.Y);
        }

        private double CalcAngleAOx(Point O, Point A)
        {
            double res = Math.Acos((A.X - O.X) / Lenght(A - new Size(O)));
            if (A.Y < O.Y)
                res = -res;
            return res * 180 / Math.PI;
        }

        public double CalculateAngel(Point O, Point A, Point B) //Tính góc AOB
        {
            double AOx = CalcAngleAOx(O, A);
            double BOx = CalcAngleAOx(O, B);
            return BOx - AOx;
        }

        public void RemoveLastPoint()
        {
            points.RemoveAt(points.Count - 1);
            numPoint--;
        }
    }
}