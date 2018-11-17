using System.Drawing;

namespace Project_DoHoa2D
{
    internal class CONST
    {
        public static Color COLOR_CURRENT_SHAPE = Color.SkyBlue;
        internal static Color COLOR_CURRENT_TOOL = Color.LightCyan;
        internal static Color COLOR_ACTIVE_SELECT_BUTTON = Color.SkyBlue;
    }

    public enum Mode
    {
        Select,
        Drawing,
        WaitingDraw,
        WaitNewPoint,
        Draging,
        Moving,
        Scaling,
        Rotating
    }

    public enum CurrentShape
    {
        Line,
        Rectangle,
        Ellipse,
        Circle,
        Curve,
        Bezier,
        Parabol,
        Hyperbol,
        Parallelogram,
        Polyline,
        Polygon,
        NoDrawing
    }
}