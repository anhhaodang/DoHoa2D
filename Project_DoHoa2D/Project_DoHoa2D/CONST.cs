﻿using System.Drawing;

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
        WaitingDraw,
        Drawing,
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

    public enum ShapeTypeDefine : int
    {
        LINE = 0,
        RECTANGLE = 1,
        CIRCLE = 2,
        POLYGON = 3,
        POLYLINE = 4,
        CUSTOM = 5,
        UNDEFINED = -1
    }

    public class MouseInfo
    {
        public StateMouse state;
        public MyShape shapeUnder;
        public Corner corner;

        public MouseInfo()
        {
            state = StateMouse.Outside;
            shapeUnder = null;
            corner = Corner.Undefined;
        }
    }

    public enum StateMouse : int
    {
        Rotate,
        Scale,
        Move,
        Outside,
        Inside,
        Draw
    }

    public enum Corner : int
    {
        TopLeft = 0,
        DownLeft = 1,
        DownRight = 2,
        TopRight = 3,
        Undefined = -1
    }
}