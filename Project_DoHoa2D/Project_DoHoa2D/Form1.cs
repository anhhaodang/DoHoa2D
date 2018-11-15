﻿using System;
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
    public partial class Save : Form
    {
        List<Button> shapes = new List<Button>();
        List<Button> tools = new List<Button>();
        List<Button> colors = new List<Button>();
        String[] colorList = { "Black", "Silver","Gray", "White", "Maroon","Red","Purple","Fuchsia",
            "Green","Lime","Olive","Yellow","Navy","Blue","Teal","Aqua"};
        int currentShape, currentTool, currentColor;
        public Save()
        {
            InitializeComponent();
     
            #region Add Shape Buttons
            shapes.Add(btnLine);
            shapes.Add(btnRectangle);
            shapes.Add(btnCircle);
            shapes.Add(btnEllipse);
            shapes.Add(btnPolygon);
            shapes.Add(btnParabol);
            shapes.Add(btnZigzag);
            shapes.Add(btnArc);
            #endregion
                 
            #region Add Tool Buttons
            tools.Add(btnFill);
            tools.Add(btnMove);
            tools.Add(btnRotate);
            tools.Add(btnScale);
            #endregion

            #region Add Color Buttons
            for (int i = 0; i < colorList.Length; i++)
                colors.Add(
                    (Button)this.Controls.Find("btnColor" + colorList[i], true)[0]
                    );
            #endregion

            #region Set Default Value
            cboDashstyle.SelectedIndex = 0;
            nudWitdh.Value = 1;
            currentShape = 0; //Line
            shapes[currentShape].BackColor = CONST.COLOR_CURRENT_SHAPE;
            currentTool = -1; //Nothing
            currentColor = 0;
            btnCurrentColor.BackColor = Color.FromName(colorList[currentColor]);
            #endregion

            pnlRotateAngel.Visible = false;
            pnlAdjustScale.Visible = false;
        }

        private void updatePanelToolChildren(int currentTool)
        {

        }

        private void button_Shape_Click(object sender, EventArgs e)
        {
            if (currentTool != -1)
            {
                tools[currentTool].BackColor = Color.White;
                currentTool = -1;
                pnlAdjustScale.Visible = false;
                pnlRotateAngel.Visible = false;
            }
           
            Button clikedShapeButton = sender as Button;
            shapes[currentShape].BackColor = Color.Transparent;
            for (int i = 0; i < shapes.Count; i++)
            if (clikedShapeButton == shapes[i])
            {
                currentShape = i;
                shapes[currentShape].BackColor = CONST.COLOR_CURRENT_SHAPE;
                break;
            }
        }

        private void button_Tool_Click(object sender, EventArgs e)
        {
            Button clikedButton = sender as Button;
            if (currentTool != -1)
                tools[currentTool].BackColor = Color.White;
            for(int i = 0; i < tools.Count; i++)
                if(clikedButton == tools[i])
                {
                    currentTool = i;
                    tools[currentTool].BackColor = CONST.COLOR_CURRENT_TOOL;
                    break;
                }

            pnlRotateAngel.Visible = (clikedButton == btnRotate);
            pnlAdjustScale.Visible = (clikedButton == btnScale);
        }

        private void button_Color_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            for (int i = 0; i < colors.Count; i++)
                if (clickedButton == colors[i])
                {
                    currentColor = i;
                    btnCurrentColor.BackColor = Color.FromName(colorList[i]);
                    break;
                }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            
            Point point1 = new Point(30, 600);
            Point point2 = new Point(40, 300);
            Point point3 = new Point(50, 200);
            Point point4 = new Point(60, 300);
            Point point5 = new Point(70, 600);
            
                
            Point[] curvePoints = { point1, point3, point5 };
            Point[] curvePoints2 = { point1, point2, point3, point4, point5};
            g.DrawCurve(new Pen(Color.Black), curvePoints);
            g.DrawCurve(new Pen(Color.Blue), curvePoints2);



            MyShape hcn = new MyRectangle(200, 200, 500, 200, 500, 500, 200, 500);
            hcn.Draw(g);
            g.ScaleTransform(0.5f, 0.5f);
            hcn.Draw(g, Color.Blue);

            
            
            g.ResetTransform();
            MyShape hcn1 = new MyRectangle(200, 200, 300, 200, 300, 300, 200, 300);
            hcn1.Draw(g);

        }

    }
}
