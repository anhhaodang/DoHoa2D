using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DoHoa2D
{
    class MyRectangle 
    {
        private Point p1, p2, p3, p4;
        private Color borderColor = Color.Black;
        private Color backgroundColor = Color.White;
        private int dashStyle = 0;
        private float width = 1;

        public MyRectangle()
        {
            this.p1 = new Point(0, 0);
            this.p2 = new Point(10, 0);
            this.p3 = new Point(10, 10);
            this.p4 = new Point(0, 10);
        }

        public MyRectangle(Point p1, Point p2, Point p3, Point p4)
        {
            this.p1 = p1; this.p2 = p2; this.p3 = p3; this.p4 = p4;
        }

        public MyRectangle(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            this.p1 = new Point(x1, y1);
            this.p2 = new Point(x2, y2);
            this.p3 = new Point(x3, y3);
            this.p4 = new Point(x4, y4);
        }

        public void Draw(Graphics graphics)
        {
            Pen myPen = new Pen(this.borderColor);
            graphics.DrawLine(myPen, p1, p2);
            graphics.DrawLine(myPen, p2, p3);
            graphics.DrawLine(myPen, p3, p4);
            graphics.DrawLine(myPen, p4, p1);
           
            

        }
        public void Draw(Graphics graphics, Color borderColor)
        {
            Pen myPen = new Pen(borderColor);
            this.borderColor = borderColor; 
            graphics.DrawLine(myPen, p1, p2);
            graphics.DrawLine(myPen, p2, p3);
            graphics.DrawLine(myPen, p3, p4);
            graphics.DrawLine(myPen, p4, p1);

        }

        public void Draw(Graphics graphics, Color borderColor, int penStyle = 0, float width = 1)
        {

            Pen myPen = new Pen(borderColor);
            this.borderColor = borderColor;
            this.dashStyle = penStyle;
            this.width = width;
            if (width > 1)
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            else
            {
                switch (penStyle)
                {
                    case 0: myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid; break;
                    case 1: myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash; break;
                    case 2: myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot; break;
                    case 3: myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot; break;
                    case 4: myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot; break;
                    case 5: myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom; break;
                }
                myPen.Width = width;
            }
            if (penStyle >= 0 && penStyle <= 5)
            {
                graphics.DrawLine(myPen, p1, p2);
                graphics.DrawLine(myPen, p2, p3);
                graphics.DrawLine(myPen, p3, p4);
                graphics.DrawLine(myPen, p4, p1);
            }
        }

    }
}
