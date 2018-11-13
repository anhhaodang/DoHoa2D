using System.Drawing;
using System.Drawing.Drawing2D;

namespace Project_DoHoa2D
{
    internal class MyCircle
    {
        private Point p;
        private int r;
        private Color outlineColor;
        private Color fillColor;
        private DashStyle dashStyle;
        private float width;

        public MyCircle()
        {
            this.p = new Point(0, 0);
            this.r = 10;
            this.outlineColor = Color.Black;
            this.dashStyle = DashStyle.Solid;
            this.fillColor = Color.White;
            this.width = 1;
        }

        public MyCircle(int v1, int v2, int v3, Color? ocolor, Color? fcolor, DashStyle? d, float? w)
        {
            this.p = new Point(v1, v2);
            this.r = v3;
            this.outlineColor = ocolor.HasValue ? ocolor.Value : Color.Black;
            this.fillColor = fcolor.HasValue ? fcolor.Value : Color.White;
            this.dashStyle = d.HasValue ? d.Value : DashStyle.Solid;
            this.width = w.HasValue ? w.Value : 1;
        }

        public void ConfigureStyle(Color? ocolor, Color? fcolor, DashStyle? d, float? w)
        {
            this.outlineColor = ocolor.HasValue ? ocolor.Value : Color.Black;
            this.fillColor = fcolor.HasValue ? fcolor.Value : Color.White;
            this.dashStyle = d.HasValue ? d.Value : DashStyle.Solid;
            this.width = w.HasValue ? w.Value : 1;
        }

        public void Draw(Graphics g)
        {
            Pen myPen = new Pen(this.outlineColor);
            g.DrawEllipse(myPen, new Rectangle(p.Y - r, p.X - r, 2*r, 2*r));
        }

        public void Fill(Graphics g)
        {

        }
        //Còn: các phép biến đổi: tịnh tiến, tỷ lệ, biến dạng?

    }
}