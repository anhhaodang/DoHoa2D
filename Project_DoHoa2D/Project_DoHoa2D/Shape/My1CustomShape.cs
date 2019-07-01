using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DoHoa2D.Shape
{
    class My1CustomShape : MyGroupShape
    {
        public My1CustomShape() : base()
        {
            MyCircle circle = new MyCircle(new Point(50, 50), new Point(100, 100));
            MyLine line = new MyLine(100, 0, 50, 100);
            listShape.Add(circle);
            listShape.Add(line);
        }
    }
}
