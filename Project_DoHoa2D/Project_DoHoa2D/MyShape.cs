using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Project_DoHoa2D
{
    public abstract class MyShape
    {
        public abstract GraphicsPath path { get; }
        protected List<Point> point;
        protected Color borderColor = Color.Black;
        protected DashStyle dashStyle = DashStyle.Solid;
        protected float width = 1;

        public float angel = 0;
        public bool isSelected = false;


        public abstract bool Inside(Point p);
        public abstract void Move(Point d);

        public abstract void Set(Point point, int index);
        public abstract Point Get(int index);

        public abstract void Draw(Graphics graphics);

        //public abstract void Translation(Point Src, Point Des);
        //public abstract void Scaling(Point pivotPoint, float Sx, float Sy);
        //public abstract void Rotation(double alpha);

        public abstract void Save(string filePath);
        public abstract void Open(string data);

        public abstract void Fill(Graphics graphics, Color backgroundColor, int fillStyle);

        public void Configure(Color? BorderColor, DashStyle? DashStyle, float? Width, float? Angel, bool? IsSelected)
        {
            if (BorderColor.HasValue)
                borderColor = BorderColor.Value;
            if (DashStyle.HasValue)
                dashStyle = DashStyle.Value;
            if (Width.HasValue)
                width = Width.Value;
            if (Angel.HasValue)
                angel = Angel.Value;
            if (IsSelected.HasValue)
                isSelected = IsSelected.Value;
        }
    }
}