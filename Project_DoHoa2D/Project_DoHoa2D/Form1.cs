using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            MyLine a = new MyLine(1000, 100, 100, 50);
            a.Draw(g, Color.Violet, 5,1);
            a.Rotation(Math.PI / 4);
            a.Draw(g,Color.Black);

            MyCircle b = new MyCircle(30, 100, 90);
            b.Draw(g);
        }
    }
}
