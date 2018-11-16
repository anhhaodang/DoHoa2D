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
        List<Button> shapeButtons;
        List<MyShape> shapes;

        int currentShape;

        private bool isMouseDown;
        private bool isDrawingCurve;
        private bool isDrawingPolygon;
        private bool isDrawingBezier;
        private bool isMovingShape;

        public Form1()
        {
            InitializeComponent();

            #region Add Shape Buttons
            shapeButtons = new List<Button> {btnSelect, btnLine, btnRectangle, btnCircle, btnEllipse, btnPolygon, btnParabol, btnZigzag, btnArc };
            #endregion

            #region Add Tool Buttons
            //toolButtons = new List<Button> { btnFill, btnMove, btnRotate, btnScale };
            #endregion

            #region Add Color Buttons
            //colorButtons = new List<Button>();
            //for (int i = 0; i < colorList.Length; i++)
            //    colorButtons.Add(
            //        (Button)this.Controls.Find("btnColor" + colorList[i], true)[0]
            //        );
            #endregion
      
            #region Set Default Value
            cmbDashstyle.SelectedIndex = 0;
            trackBar1.Value = 1;
            currentShape = (int)CurrentShape.Line; //Line
            shapeButtons[0].BackColor = CONST.COLOR_CURRENT_SHAPE;
            currentTool = -1; //Nothing
            currentColor = 0;
            btnBorderColor.BackColor = Color.FromName(colorList[currentColor]);
           

            //pnlRotateAngel.Visible = false;
            //pnlAdjustScale.Visible = false;
            #endregion
        }

        #region Paint's Action
        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //for (int i = 0; i < shapes.Count; i++)
            //    shapes[i].Draw(e.Graphics);

            Graphics g = e.Graphics;
            MyShape hcn = new MyPolyline(new Point(20, 20), new Point(60, 20), new Point(60, 60), new Point( 50, 80) , new Point(20, 60));
            hcn.Draw(g);

            hcn.angle = -30;
            hcn.Draw(g);
            g.ResetTransform();

            //MyShape line = new MyLine(140, 120, 200, 200);
            //line.Draw(g);

            //line.angle = -60;
            //line.Draw(g);

            //g.ResetTransform();
            //MyShape hcn1 = new MyRectangle(200, 200, 300, 200, 300, 300, 200, 300);
            //hcn1.Draw(g);
        }

        private void pnlMain_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pnlMain_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void pnlMain_MouseUp(object sender, MouseEventArgs e)
        {

        }

        #endregion

        private void updatePanelToolChildren(int currentTool)
        {

        }

       
     

        private void btnColor_MouseDown(object sender, MouseEventArgs e)
        {
            Button clickedButton = sender as Button;
            for (int i = 0; i < colorButtons.Count; i++)
                if (clickedButton == colorButtons[i])
                {
                    currentColor = i;
                    if (e.Button == MouseButtons.Left)
                        btnBorderColor.BackColor = Color.FromName(colorList[i]);
                    else btnBackColor.BackColor = Color.FromName(colorList[i]);
                    break;
                }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g = e.Graphics;
            
            
            //Point point1 = new Point(30, 600);
            //Point point2 = new Point(40, 300);
            //Point point3 = new Point(50, 200);
            //Point point4 = new Point(60, 300);
            //Point point5 = new Point(70, 600);
            
                
            //Point[] curvePoints = { point1, point3, point5 };
            //Point[] curvePoints2 = { point1, point2, point3, point4, point5};
            //g.DrawCurve(new Pen(Color.Black), curvePoints);
            //g.DrawCurve(new Pen(Color.Blue), curvePoints2);



           

        }

    }
}
