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

        public abstract void Set(Point point, int index);
        public abstract Point Get(int index);

        public abstract void Draw(Graphics graphics);
        public abstract void Draw(Graphics graphics, Color borderColor);
        public abstract void Draw(Graphics graphics, Color borderColor, DashStyle dashStyle, float width = 1);

        public abstract void Translation(Point Src, Point Des);
        public abstract void Scaling(Point pivotPoint, float Sx, float Sy);
        public abstract void Rotation(double alpha);

        public abstract void Save(string filePath);
        public abstract void Open(string data);
    }
}