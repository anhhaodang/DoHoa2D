using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DoHoa2D
{
    public static class Utils
    {
        public static bool Near(Point p1, Point p2, int? radius = null)
        {
            if (!radius.HasValue)
                radius = 5;
            return Math.Abs(p1.X - p2.X) < radius && Math.Abs(p1.Y - p2.Y) < radius;
        }
    }
}
