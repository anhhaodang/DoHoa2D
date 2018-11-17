using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Project_DoHoa2D
{
    public abstract class MyShape
    {
        protected List<Point> point;
        protected Color borderColor = Color.Black;
        protected DashStyle dashStyle = DashStyle.Solid;
        protected float width = 1;
        protected int fillStyle = 0;
        protected Color backgroundColor = Color.White;
        public bool isFill = false;


        public float angle = 0;
        public bool isSelected = false;

        public abstract bool Inside(Point p);
        //public abstract bool AtRotatePosition(Point p);
        public abstract bool AtScalePosition(Point p);
        public abstract void Move(Point d);

        public abstract void Set(Point point, int index);
        public abstract Point Get(int index);

        public abstract void Draw(Graphics graphics);

        //public abstract void Translation(Point Src, Point Des);
        //public abstract void Scaling(Point pivotPoint, float Sx, float Sy);
        //public abstract void Rotation(double alpha);

        public abstract void Save(string filePath);
        public abstract void Open(string data);

        public void Configure(Color? BorderColor = null, DashStyle? DashStyle = null, float? Width = null, 
                        float? Angel = null, bool? IsSelected = null, Color? BackgroundColor = null, 
                        int? FillStyle = null, bool? IsFill =null)
        {
            if (BorderColor.HasValue)
                borderColor = BorderColor.Value;
            if (DashStyle.HasValue)
                dashStyle = DashStyle.Value;
            if (Width.HasValue)
                width = Width.Value;
            if (Angel.HasValue)
                angle = Angel.Value;
            if (IsSelected.HasValue)
                isSelected = IsSelected.Value;
            if (BackgroundColor.HasValue)
                backgroundColor = BackgroundColor.Value;
            if (FillStyle.HasValue)
                fillStyle = FillStyle.Value;
            if (IsFill.HasValue)
                isFill = IsFill.Value;
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

        public Point Center()
        {
            int x = 0, y = 0;
            int n = point.Count;
            for (int i = 0; i < n; i++)
            {
                x += point[i].X; y += point[i].Y;
            }
            x /= n; y /= n;
            Point res = new Point(x, y);
            return res ;
        }
    }
}