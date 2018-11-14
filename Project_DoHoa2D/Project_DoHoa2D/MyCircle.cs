using System.Drawing;
using System.Drawing.Drawing2D;

namespace Project_DoHoa2D
{
    class MyCircle : MyShape
    {
        private Color fillColor;
        private FillMode fillMode;

        public MyCircle()
        {
            this.point.Add(new Point(10, 10));
            this.point.Add(new Point(20, 20));
        }

        public MyCircle(Point p1, Point p2)
        {
            point.Add(p1);
            point.Add(p2);
        }

        public override void Set(Point point, int index){ }
        public override Point Get(int index){ return new Point(0, 0); }

        public override void Draw(Graphics graphics){ }
        public override void Draw(Graphics graphics, Color borderColor){ }
        public override void Draw(Graphics graphics, Color borderColor, DashStyle dashStyle, float width = 1){ }

        public override void Translation(Point Src, Point Des){ }
        public override void Scaling(Point pivotPoint, float Sx, float Sy){ }
        public override void Rotation(double alpha){ }

        public override void Save(string filePath){ }
        public override void Open(string data){ }
    }
}