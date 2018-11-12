using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DoHoa2D
{
    class MyLine
    {
        private Point a;
        private Point b;
        
        public MyLine()
        {
            this.a = new Point(0, 0);
            this.b = new Point(0, 0);
        }
        public MyLine(Point a, Point b)
        {
            this.a = a; this.b = b;
        }
        public MyLine(int x1, int y1, int x2, int y2)
        {
            this.a = new Point(x1, y1);
            this.b = new Point(x2, y2);
        }

        public void Draw(Graphics graphics)
        {
            Pen myPen = new Pen(Color.Black);
            graphics.DrawLine(myPen, a, b);
        }

        public void Draw(Graphics graphics, Color color)
        {
            Pen myPen = new Pen(color);
            graphics.DrawLine(myPen, a, b);
        }

        public void Draw(Graphics graphics, Color color, int penStyle=0, float width=1)
        {

            Pen myPen = new Pen(color);
            if (width > 1)
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid; 
            else
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
            if (penStyle >=0 && penStyle <=5)
                graphics.DrawLine(myPen, a, b);
        }
       
    }
}
