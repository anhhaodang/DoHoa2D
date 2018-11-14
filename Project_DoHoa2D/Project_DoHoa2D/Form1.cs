using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Project_DoHoa2D
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int y = 115;
            MyLine a = new MyLine(0, y, 1000, y);
            a.Draw(g);
            
            Point point1 = new Point(30, 600);
            Point point2 = new Point(40, 300);
            Point point3 = new Point(50, 200);
            Point point4 = new Point(60, 300);
            Point point5 = new Point(70, 600);
            
                
            Point[] curvePoints = { point1, point3, point5 };
            Point[] curvePoints2 = { point1, point2, point3, point4, point5};
            g.DrawCurve(new Pen(Color.Black), curvePoints);
            g.DrawCurve(new Pen(Color.Blue), curvePoints2);

            MyCircle b = new MyCircle(200, 300, 40);
            b.ConfigureStyle(BorderColor: Color.Brown, DashStyle: DashStyle.DashDotDot);
            b.Draw(g);
        }
    }
}
