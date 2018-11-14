using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DoHoa2D
{
    class MyRectangle : MyShape
    {
        private Color backgroundColor = Color.White;

        private int numPoint = 4;

        public MyRectangle()
        {
            point = new List<Point>(4);
            point.Add(new Point(0, 0));
            point.Add(new Point(10, 0));
            point.Add(new Point(10, 10));
            point.Add(new Point(0, 10));

            this.numPoint = 4;
        }

        public MyRectangle(Point p1, Point p2, Point p3, Point p4)
        {
            point = new List<Point>(4);
            point.Add(p1);
            point.Add(p2);
            point.Add(p3);
            point.Add(p4);
            this.numPoint = 4;
        }

        public MyRectangle(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            point = new List<Point>(4);
            point.Add(new Point(x1, y1));
            point.Add(new Point(x2, y2));
            point.Add(new Point(x3, y3));
            point.Add(new Point(x4, y4));
            this.numPoint = 4;
        }

        public override void Draw(Graphics graphics)
        {
            Pen myPen = new Pen(borderColor);
            if (width > 1)
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            else
                myPen.DashStyle = dashStyle;
            myPen.Width = width;
            graphics.DrawLine(myPen, point[0], point[1]);
            graphics.DrawLine(myPen, point[1], point[2]);
            graphics.DrawLine(myPen, point[2], point[3]);
            graphics.DrawLine(myPen, point[3], point[0]);


        }
        public override void Draw(Graphics graphics, Color borderColor)
        {
            Pen myPen = new Pen(borderColor);
            this.borderColor = borderColor;
            graphics.DrawLine(myPen, point[0], point[1]);
            graphics.DrawLine(myPen, point[1], point[2]);
            graphics.DrawLine(myPen, point[2], point[3]);
            graphics.DrawLine(myPen, point[3], point[0]);

        }

        public override void Draw(Graphics graphics, Color borderColor, DashStyle dashStyle, float width = 1)
        {

            Pen myPen = new Pen(borderColor);
            this.borderColor = borderColor;
            this.dashStyle = dashStyle;
            this.width = width;
            if (width > 1)
                myPen.DashStyle = DashStyle.Solid;
            else
            {
                myPen.DashStyle = dashStyle;
            }
            myPen.Width = width;
            // if (penStyle >= 0 && penStyle <= 5)
            graphics.DrawLine(myPen, point[0], point[1]);
            graphics.DrawLine(myPen, point[1], point[2]);
            graphics.DrawLine(myPen, point[2], point[3]);
            graphics.DrawLine(myPen, point[3], point[0]);

        }

        public override Point Get(int index)
        {
            return this.point[index];
        }

        public override void Set(Point point, int index)
        {
            this.point[index] = point;
        }

        public override void Open(string data)
        {
            char delimiters = ' ';
            string[] dt = data.Split(delimiters);
            Point p1= new Point(Int32.Parse(dt[1]), Int32.Parse(dt[2]));
            Point p2= new Point(Int32.Parse(dt[3]), Int32.Parse(dt[4]));
            Point p3= new Point(Int32.Parse(dt[5]), Int32.Parse(dt[6]));
            Point p4= new Point(Int32.Parse(dt[7]), Int32.Parse(dt[8]));

            point = new List<Point>(4);
            point.Add(p1); point.Add(p2); point.Add(p3); point.Add(p4);

            switch (dt[9])
            {
                case "Dash": this.dashStyle = DashStyle.Dash; break;
                case "DashDot": this.dashStyle = DashStyle.DashDot; break;
                case "DashDotDot": this.dashStyle = DashStyle.DashDotDot; break;
                case "Dot": this.dashStyle = DashStyle.Dot; break;
                case "Solid": this.dashStyle = DashStyle.Solid; break;
                case "Custom": this.dashStyle = DashStyle.Custom; break;
            }
            this.width = Int32.Parse(dt[10]);
            this.borderColor = Color.FromArgb(Convert.ToInt32(dt[11]));
            this.backgroundColor = Color.FromArgb(Convert.ToInt32(dt[12]));

        }

        public override void Save(string filePath)
        {
            Point p1 = this.Get(0);
            Point p2 = this.Get(1);
            Point p3 = this.Get(2);
            Point p4 = this.Get(3);

            string data = "Retangle " + p1.X.ToString() + " " + p1.Y.ToString() + " " + p2.X.ToString() + " " + p2.Y.ToString()
                 +" " + p3.X.ToString() + " " + p3.Y.ToString() + " " + p4.X.ToString() + " " + p4.Y.ToString() + " " +  dashStyle.ToString() 
                 + " " + width.ToString() + " " + borderColor.ToArgb().ToString()+ " " + backgroundColor.ToArgb().ToString() + "\n";

            StreamWriter sw = File.AppendText(filePath);
            sw.WriteLine(data);
            sw.Close();
        }

        public override void Translation(Point Src, Point Des)
        {
            throw new NotImplementedException();
        }

        public override void Scaling(Point pivotPoint, float Sx, float Sy)
        {
            throw new NotImplementedException();
        }

        public override void Rotation(double alpha)
        {
            throw new NotImplementedException();
        }
     
    }
}
