using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DoHoa2D
{
    class MyParabol : MyShape
    {
        private bool isFill;
        private Color fillColor;

        MyParabol()
        {
            this.point.Add(new Point(10, 10));
            this.point.Add(new Point(20, 20));
        }

        public override bool Inside(Point p)
        {
            Point[] points =  { point[0], new Point((point[0].X + point[1].X) / 2, point[1].Y), new Point(point[1].X, point[0].Y) };

            bool res = false;
            GraphicsPath path = new GraphicsPath();
            path.AddCurve(points);

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
                Set(new Point(point[i].X + d.X, point[i].Y + d.Y), i);
        }

        public override void Set(Point point, int index){ }
        public override Point Get(int index){ return new Point(0, 0); }

        public override void Draw(Graphics graphics)
        {
            int w = point[1].X - point[0].X; 
            int h = point[1].Y - point[0].Y;
            graphics.TranslateTransform(point[0].X + w / 2, point[0].Y + h / 2);
            graphics.RotateTransform(angle);

            Point[] points = { new Point(-w / 2, -h / 2), new Point(0, 0), new Point(w / 2, -h / 2) };

            if (isFill)
                graphics.FillClosedCurve(new SolidBrush(fillColor), points);

            Pen p = new Pen(borderColor);
            p.DashStyle = dashStyle;
            p.Width = width;
            graphics.DrawCurve(p, points);
            graphics.ResetTransform();
        }
      
        public override void Save(string filePath){ }
        public override void Open(string data){ }

        public override void Fill(Graphics graphics, Color backgroundColor, int fillStyle)
        {
            throw new NotImplementedException();
        }
    }
}
