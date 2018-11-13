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

            MyCircle b = new MyCircle(200, 300, 40);
            b.ConfigureStyle(BorderColor: Color.Brown, DashStyle: DashStyle.DashDotDot);
            b.Draw(g);
        }
    }
}
