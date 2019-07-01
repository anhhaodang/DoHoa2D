using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DoHoa2D
{
    class MyGroupShape : MyShape
    {
        protected List<MyShape> listShape = new List<MyShape>();

        public MyGroupShape()
        {
            listShape = new List<MyShape>();
        }

        public override void Draw(Graphics graphics)
        {
            foreach(MyShape shape in listShape)
                shape.Draw(graphics);
        }


        public new void Configure(Color? BorderColor = null, DashStyle? DashStyle = null, float? Width = null,
                        float? Angle = null, bool? IsSelected = null, Color? BackgroundColor = null,
                        int? FillStyle = null, bool? IsFill = null, HatchStyle? HatchStyle = null)
        {
            foreach (MyShape shape in listShape)
                shape.Configure(BorderColor, DashStyle, Width, Angle, IsSelected, BackgroundColor, FillStyle, IsFill, HatchStyle);
        }

        public override void Extend_ExtendableShape(Point p)
        {
        }

        public override string getData()
        {
            throw new NotImplementedException();
        }

        public override void Open(string data)
        {
            throw new NotImplementedException();
        }

        

        protected override GraphicsPath GetGraphicsPath(Rectangle boundingBox)
        {
            throw new NotImplementedException();
        }
    }
}
