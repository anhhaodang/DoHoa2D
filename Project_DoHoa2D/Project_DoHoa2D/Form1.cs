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
        List<MyShape> shapes = new List<MyShape>();

        Mode mode;

        MyShape selectedShape;

        private Point prevPosition;

        int currentShape;

        private bool isDrawing;

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
            btnSelect.BackColor = Color.SkyBlue;
            cmbDashstyle.SelectedIndex = 0;
            trackBar1.Value = 1;
            isDrawing = false;
            mode = Mode.Select;
            currentShape = -1; //No Shape
            #endregion


        }

        #region Paint's Action
        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            for (int i = 0; i < shapes.Count; i++)
                shapes[i].Draw(e.Graphics);
        }

        private void pnlMain_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;

            if (mode == Mode.Select)
            {
                for (int i = 0; i < shapes.Count; i++)
                    shapes[i].isSelected = false;
                selectedShape = null;
                mode = Mode.Select;

                for (int i = shapes.Count - 1; i>= 0 ; i--)

                    //Nghiên cứu gộp 3 hàm Inside, AtPositionRotate và AtScale thành
                    //StateCursor shapes[i].Calculate(e.Location)
                    //Trả về StateCursor: inside, rotateSW, rotateSE, scaleSW, scaleSE...
                    if (shapes[i].Inside(e.Location))
                    {
                        shapes[i].isSelected = true;
                        selectedShape = shapes[i];
                        mode = Mode.Draging;
                        prevPosition = e.Location;
                        break;
                    }
                    //else if (shapes[i].AtPositionRotate(e.Location))
                    //{
                    //mode = Mode.Rotating;

                    //}
                    //else if (shapes[i].AtPositionScale(e.Location))
                    //{
                    //mode = Mode.Scaling;
                    //}
            }

            else if (mode == Mode.WaitingDraw)
            {
                if (btnLine.BackColor != Color.Transparent)
                {
                    MyLine line = new MyLine(e.Location, e.Location);
                    line.Configure(DashStyle: (DashStyle)cmbDashstyle.SelectedIndex, BorderColor: btnBorderColor.BackColor, Width: 1);
                    shapes.Add(line);
                    //mode = Mode.WaitNewPoint;
                }
                else if (btnRectangle.BackColor != Color.Transparent)
                {
                    MyRectangle rectangle = new MyRectangle(e.Location, e.Location);
                    rectangle.Configure(DashStyle: (DashStyle)cmbDashstyle.SelectedIndex, BorderColor: btnBorderColor.BackColor, Width: 1);
                    shapes.Add(rectangle);
                    //mode = Mode.WaitNewPoint;
                }
            }

            else if (mode == Mode.WaitNewPoint)
            {
                //Xet xem đang vẽ hình gì. Nếu vẽ hình Line, Rect, Circle, Ellipse, HBH
                //Parabol thì thêm điểm e.Location vào 1, rồi về Mode.WaitingDraw
                //Nếu đang vẽ Polygon, PolyLines, Bezier thì set e.location vào điểm cuối,
                //vẫn giữ Mode.WaitNewPoint
                shapes[shapes.Count - 1].Set(e.Location, 1);
                mode = Mode.WaitingDraw;
                pnlMain.Invalidate();
            }
        }

        private void pnlMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (mode == Mode.WaitNewPoint)
            {
                shapes[shapes.Count - 1].Set(e.Location, 1); //Set điểm cuối chứ không phải 1
                pnlMain.Invalidate();
            }
            else if (mode == Mode.Select)
            {
                pnlMain.Cursor = Cursors.Default;
                for (int i = 0; i < shapes.Count; i++)
                    if (shapes[i].Inside(e.Location))
                    {
                        pnlMain.Cursor = Cursors.SizeAll;
                        break;
                    }
            }

            else if(mode == Mode.Draging)
            {
                pnlMain.Cursor = Cursors.SizeAll;
                Point distance = new Point(e.Location.X - prevPosition.X, e.Location.Y - prevPosition.Y);
                selectedShape.Move(distance);
                prevPosition = e.Location;
                pnlMain.Invalidate();
            }

            else if (mode == Mode.Rotating)
            {
                //Rotate thôi
                //Chỉnh chuột lại
            }
            else if (mode == Mode.Scaling)
            {
                //Chỉnh chuột lại
                //Scale bằng cách đổi giá trị boundingbox[0],[1]
            }

            
        }

        private void pnlMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (mode == Mode.WaitingDraw)
                mode = Mode.WaitNewPoint;

            isMouseDown = false;
            mode = Mode.Select;
        } 

        private void pnlMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        #endregion


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

        private void btnShape_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < shapeButtons.Count; i++)
                shapeButtons[i].BackColor = Color.Transparent;
            Button clickedShape = sender as Button;
            clickedShape.BackColor = CONST.COLOR_CURRENT_SHAPE;
            btnSelect.BackColor = Color.Transparent;
            isDrawing = true;
            mode = Mode.WaitingDraw;
            pnlMain.Cursor = Cursors.Cross;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < shapeButtons.Count; i++)
                shapeButtons[i].BackColor = Color.Transparent;
            btnSelect.BackColor = CONST.COLOR_ACTIVE_SELECT_BUTTON;
            isDrawing = false;
            mode = Mode.Select;
            
        }

        private void btnBorderColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnBorderColor.BackColor = colorDialog.Color;
            }

            for (int i=0; i<shapes.Count; i++)
            {
                if (shapes[i].isSelected)
                    shapes[i].Configure(BorderColor: btnBorderColor.BackColor);
            }
            pnlMain.Invalidate();

        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnBackColor.BackColor = colorDialog.Color;
            }

            for (int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i].isSelected && shapes[i].isFill)
                    shapes[i].Configure(BackgroundColor: btnBackColor.BackColor);
            }
            pnlMain.Invalidate();
        }
    }
}
