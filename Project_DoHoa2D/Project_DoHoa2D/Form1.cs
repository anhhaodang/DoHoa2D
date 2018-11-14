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
    public partial class Save : Form
    {
        public Save()
        {
            InitializeComponent();

            List<Button> shapes = new List<Button>();
            #region Add Button Shape
            shapes.Add(btnLine);
            shapes.Add(btnRectangle);
            shapes.Add(btnCircle);
            shapes.Add(btnEllipse);
            shapes.Add(btnPolygon);
            shapes.Add(btnParabol);
            shapes.Add(btnZigzag);
            shapes.Add(btnArc);
            #endregion
            
            List<Button> tools = new List<Button>();
            #region Add Button Tool
            tools.Add(btnFill);
            tools.Add(btnMove);
            tools.Add(btnRotate);
            tools.Add(btnScale);
            #endregion
            
            String[] colors = { "Black", "Silver","Gray", "White", "Maroon","Red","Purple","Fuchsia",
            "Green","Lime","Olive","Yellow","Navy","Blue","Teal","Aqua"};

            #region Set Default Value
            cboDashstyle.SelectedIndex = 0;
            nudWitdh.Value = 1;
            int currentShape = 0; //Line
            shapes[currentShape].BackColor = Color.AliceBlue;
            int currentTool = -1; //Nothing
            int currentColor = 0;
            btnCurrentColor.BackColor = Color.FromName(colors[currentColor]);
            #endregion

            pnlRotateAngel.Visible = false;
            pnlAdjustScale.Visible = false;
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


            MyShape c = new MyRectangle(100,100,200,100,200,200,100,200);
            c.Draw(g);
            c.Save("ahihi.txt");
           // c.Open("Retangle 100 100 200 100 200 200 100 200 Solid 1 -1146130 - 1");
            c.Draw(g);
        }

    }
}
