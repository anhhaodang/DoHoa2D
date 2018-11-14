using System.Drawing;
using System.Drawing.Drawing2D;

namespace Project_DoHoa2D
{
    class MyCircle // : MyShape
    {
        private Point center;
        private int radius;
        private Color borderColor;
        private Color fillColor;
        private DashStyle dashStyle;
        private float width;

        public MyCircle()
        {
            //MyShape f = new MyShape();
            this.center = new Point(0, 0);
            this.radius = 10;
            this.borderColor = Color.Black;
            this.dashStyle = DashStyle.Solid;
            this.fillColor = Color.White;
            this.width = 1;
        }

        public MyCircle(int v1, int v2, int v3, Color? BorderColor = null, Color? FillColor = null, DashStyle DashStyle = DashStyle.Solid, float Width = 1)
        {
            this.center = new Point(v1, v2);
            this.radius = v3;
            this.borderColor = BorderColor ?? Color.Black;
            this.fillColor = FillColor?? Color.White;

            this.dashStyle = DashStyle;
            this.width = Width;
        }

        public void SetValue(int? XCenter = null, int? YCenter = null, int? Radius = null)
        {
            if (XCenter.HasValue)
                this.center.X = XCenter.Value;
            if (YCenter.HasValue)
                this.center.Y = YCenter.Value;
            if (Radius.HasValue)
                this.radius = Radius.Value;
        }

        public void ConfigureStyle(Color? BorderColor = null, Color? FillColor = null, DashStyle? DashStyle = null, float? Width = null)
        {
            if (BorderColor.HasValue)
                this.borderColor = BorderColor.Value;
            if (FillColor.HasValue)
                this.fillColor = FillColor.Value;
            if (DashStyle.HasValue)
                this.dashStyle = DashStyle.Value;
            if (Width.HasValue)
                this.width = Width.Value;
        }

        public void Draw(Graphics g)
        {
            Pen myPen = new Pen(this.borderColor);
            myPen.DashStyle = this.dashStyle;
            g.DrawEllipse(myPen, new Rectangle(center.Y - radius, center.X - radius, 2*radius, 2*radius));
        }

        public void Fill(Graphics g)
        {
            g.FillEllipse(new SolidBrush(this.fillColor), center.Y - radius, center.X - radius, 2 * radius, 2 * radius);
        }

        public bool Inside(int x, int y)
        {
            return (x - center.X)* (x - center.X) + (y - center.Y)*(y - center.Y) <= radius*radius;
        }

        public void Move(Point Src, Point Des)
        {
            center.X += Src.X - Des.X;
            center.Y += Src.Y - Des.Y;
        }

        public void Scale (float Rate)
        {
            this.radius = (int)Rate * radius;
        }

        public void SetSelected(bool Selected, Graphics g)
        {
            Color c;
            if (Selected)
                c = this.borderColor;
            else c = Color.Cyan; 
            
            g.DrawRectangle(new Pen(c), center.X, center.Y - radius, 1, 1);
            g.DrawRectangle(new Pen(c), center.X, center.Y + radius, 1, 1);
            g.DrawRectangle(new Pen(c), center.X - radius, center.Y, 1, 1);
            g.DrawRectangle(new Pen(c), center.X + radius, center.Y, 1, 1);
        }
    }
}