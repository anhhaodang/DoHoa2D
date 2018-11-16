using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DoHoa2D
{
    class Curve : MyShape
    {
        List<Point> BoundingBox;

        Curve()
        {

        }

        public override bool Inside(Point point){
            return true;
        }
        public override void Move(Point d){
            for (int i = 0; i < point.Count; i++)
                Set(new Point(point[i].X + d.X, point[i].Y + d.Y), i);
        }

        public override void Set(Point point, int index){ }
        public override Point Get(int index){ return new Point(0, 0); }

        public override void Draw(Graphics graphics){
            if (BoundingBox.Count == 0 && point.Count != 1)
            {
                int maxX, maxY, minX, minY;
                maxX = minX = point[0].X;
                maxY = minY = point[0].Y;
                for (int i = 1; i < point.Count;i++)
                {
                    if (maxX < point[i].X)
                        maxX = point[i].X;
                    if (maxY < point[i].Y)
                        maxY = point[i].Y;

                    if (minX > point[i].X)
                        minX = point[i].X;
                    if (minY > point[i].Y)
                        minY = point[i].Y;
                }
                Point point1 = new Point(minX, minY);
                Point point2 = new Point(maxX, maxY);
                BoundingBox = new List<Point> { point1, point2 };
            }
            int w = BoundingBox[1].X - BoundingBox[0].X;
            int h = BoundingBox[1].Y - BoundingBox[0].Y;
            graphics.TranslateTransform(point[0].X + w / 2, point[0].Y + h / 2);
            graphics.RotateTransform(angel);

        }
        public override void Draw(Graphics graphics, Color borderColor){ }
        public override void Draw(Graphics graphics, Color borderColor, DashStyle dashStyle, float width = 1){ }

        public override void Translation(Point Src, Point Des){ }
        public override void Scaling(Point pivotPoint, float Sx, float Sy){ }
        public override void Rotation(double alpha){ }

        public override void Save(string filePath){ }
        public override void Open(string data){ }

        public override void Fill(Graphics graphics, Color backgroundColor, int fillStyle){ }
    }
}
