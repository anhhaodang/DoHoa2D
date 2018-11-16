﻿using System.Drawing;

namespace Project_DoHoa2D
{
    internal class CONST
    {
        public static Color COLOR_CURRENT_SHAPE = Color.SkyBlue;
        internal static Color COLOR_CURRENT_TOOL = Color.LightCyan;
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