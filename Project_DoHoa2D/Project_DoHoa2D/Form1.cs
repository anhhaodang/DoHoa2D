﻿using System;
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
            MyLine a = new MyLine(100, 100, 100, 50);
            a.Draw(g, Color.Violet, 5,1);
            a.Rotation(Math.PI / 4);
            a.Draw(g,Color.Black);

            MyRectangle b = new MyRectangle(10, 10, 50, 10, 50, 50, 10, 50);
            b.Draw(g,Color.Blue,3,3);
            
        }
    }
}
