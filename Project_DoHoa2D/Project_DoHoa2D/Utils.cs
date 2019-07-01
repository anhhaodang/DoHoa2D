using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        public static Point ConvertInnerPoint(Point p, Point anchor)
        {
            Point res = p - new Size(anchor);
            return res;
        }
    }

    public class GraphicAttribute
    {
        public Dictionary<string, dynamic> attributes = new Dictionary<string, dynamic>();

        public Color strokeColor;
        public int strokeWeight;
        public DashStyle dashStyle;

        public Color backgroundColor;
        public int fillStyle;

        public float angle = 0f;

        public GraphicAttribute()
        {
            attributes[AttributeType.borderColor] = Color.Black;
            attributes[AttributeType.width] = 1;
            attributes[AttributeType.dashStyle] = 0;
        }

        public GraphicAttribute(GraphicAttribute attribute)
        {
            attributes.Add(AttributeType.borderColor, (Color)attribute[AttributeType.borderColor]);
            attributes.Add(AttributeType.width, (Color)attribute[AttributeType.width]);
            attributes.Add(AttributeType.dashStyle, (int)attribute[AttributeType.dashStyle]);
        }

        public dynamic this[string key]
        {
            get
            {
                if (attributes.ContainsKey(key))
                    return attributes[key];
                return null;
            }

            set
            {
                if (attributes.ContainsKey(key))
                    attributes[key] = value;
                else attributes.Add(key, value);
            }
        }

        public GraphicAttribute Clone()
        {
            GraphicAttribute res = new GraphicAttribute();
            res.strokeColor = this.strokeColor;
            res.strokeWeight = this.strokeWeight;
            res.backgroundColor = this.backgroundColor;
            res.fillStyle = this.fillStyle;
            res.dashStyle = this.dashStyle;
            res.angle = this.angle;
            return res;
        }
    }

}
