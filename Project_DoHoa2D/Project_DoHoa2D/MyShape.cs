using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

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

        protected abstract GraphicsPath GetGraphicsPath(Rectangle boundingBox);

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

        protected Point GetRotatePosition(Rectangle boudingBox) {
            Point p = boudingBox.Location + new Size(boudingBox.Width / 2, -10);
            return p;
        }

        public abstract void Move(Point d);
        public abstract void Extend_ExtendableShape(Point p);
        public abstract void Set(Point point, int index);
        public abstract Point Get(int index);

        public abstract void Draw(Graphics graphics);

        public abstract void Save(string filePath);
        public abstract void Open(string data);

        public void Configure(Color? BorderColor = null, DashStyle? DashStyle = null, float? Width = null, 
                        float? Angel = null, bool? IsSelected = null, Color? BackgroundColor = null, 
                        int? FillStyle = null, bool? IsFill =null, HatchStyle? HatchStyle = null)
        {
            if (BorderColor.HasValue)
                borderColor = BorderColor.Value;
            if (DashStyle.HasValue)
                dashStyle = DashStyle.Value;
            if (Width.HasValue)
                width = Width.Value;
            if (Angel.HasValue)
            {
                angle += Angel.Value;

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
            return res ;
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
            double res = Math.Acos((A.X - O.X)/Lenght(A - new Size(O)));
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
    }
}