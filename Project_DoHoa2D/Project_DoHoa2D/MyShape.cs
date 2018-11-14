using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Project_DoHoa2D
{
    public abstract class MyShape
    {
        private List<Point> point;
        private Color borderColor;
        private DashStyle dashStyle;
        private float width;

        public MyShape()
        {
        }

        public abstract void Draw();
    }
}