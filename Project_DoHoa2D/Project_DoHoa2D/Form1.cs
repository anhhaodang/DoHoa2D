using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
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
            shapeButtons = new List<Button> { btnSelect, btnLine, btnRectangle, btnParallelogram, btnCircle, btnEllipse, btnPolygon, btnParabol, btnZigzag, btnArc };
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
            ckbFill.Checked = false;
            cmbFillStyle.Enabled = false;
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

                for (int i = shapes.Count - 1; i >= 0; i--)
                {
                    if (shapes[i].AtScalePosition(e.Location))
                    {
                        shapes[i].isSelected = true;
                        selectedShape = shapes[i];
                        mode = Mode.Scaling;
                    }
                    else if (shapes[i].AtRotatePosition(e.Location))
                    {
                        shapes[i].isSelected = true;
                        prevPosition = e.Location;
                        selectedShape = shapes[i];
                        mode = Mode.Rotating;
                    }

                    else

                   //Nghiên cứu gộp 3 hàm Inside, AtPositionRotate và AtScale thành
                   //StateCursor shapes[i].Calculate(e.Location)
                   //Trả về StateCursor: inside, rotateSW, rotateSE, scaleSW, scaleSE...
                   if (shapes[i].Inside(e.Location))
                    {
                        shapes[i].isSelected = true;
                        pnlMain.Invalidate(); //Vẽ boundingbox hoặc 2 đầu mút
                        selectedShape = shapes[i];
                        mode = Mode.Moving;
                        prevPosition = e.Location;
                        break;
                    }
                }

                pnlMain.Invalidate();



            }

            else if (mode == Mode.WaitingDraw)
            {
                if (btnLine.BackColor != Color.Transparent)
                {
                    MyLine line = new MyLine(e.Location, e.Location);
                    line.Configure(DashStyle: (DashStyle)cmbDashstyle.SelectedIndex, BorderColor: btnBorderColor.BackColor, Width: 1, BackgroundColor: btnBackColor.BackColor);
                    shapes.Add(line);
                }
                else if (btnRectangle.BackColor != Color.Transparent)
                {
                    MyRectangle rectangle = new MyRectangle(e.Location, e.Location);
                    if (ckbFill.Checked)
                    {
                        rectangle.isFill = true;
                        if (cmbFillStyle.SelectedIndex == 0)
                        {
                            rectangle.fillStyle = 0;
                        }
                        else if (cmbFillStyle.SelectedIndex > 0)
                        {
                            rectangle.fillStyle = 1;
                            rectangle.Configure(DashStyle: (DashStyle)cmbDashstyle.SelectedIndex, BorderColor: btnBorderColor.BackColor, Width: 1, BackgroundColor: btnBackColor.BackColor, HatchStyle: (HatchStyle)cmbFillStyle.SelectedIndex);
                        }
                    }

                    rectangle.Configure(DashStyle: (DashStyle)cmbDashstyle.SelectedIndex, BorderColor: btnBorderColor.BackColor, Width: 1, BackgroundColor: btnBackColor.BackColor);

                    shapes.Add(rectangle);
                }

                else if (btnCircle.BackColor != Color.Transparent)
                {
                    MyCircle circle = new MyCircle(e.Location, e.Location);
                    if (ckbFill.Checked)
                        circle.isFill = true;
                    circle.Configure(DashStyle: (DashStyle)cmbDashstyle.SelectedIndex, BorderColor: btnBorderColor.BackColor, Width: 1, BackgroundColor: btnBackColor.BackColor);
                    shapes.Add(circle);
                }
                mode = Mode.Drawing;
            }

            else if (mode == Mode.Drawing)
            {
                //Xet xem đang vẽ hình gì. Nếu vẽ hình Line, Rect, Circle, Ellipse, HBH
                //Parabol thì thêm điểm e.Location vào 1, rồi về Mode.WaitingDraw
                //Nếu đang vẽ Polygon, PolyLines, Bezier thì set e.location vào điểm cuối,
                //vẫn giữ Mode.Drawing
                shapes[shapes.Count - 1].Set(e.Location, 1);
                //  mode = Mode.WaitingDraw;
                pnlMain.Invalidate();
                mode = Mode.WaitingDraw; //Vẽ xong đối tượng
            }
            else if (mode == Mode.Moving)
            {
                mode = Mode.Select;
                pnlMain.Invalidate();
            }
        }

        private void pnlMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (mode == Mode.WaitingDraw)
            {

            }

            else if (mode == Mode.Drawing)
            {
                //if (btnCircle.BackColor != Color.Transparent)
                //{
                //    int d = Math.Min(e.Location.X - );
                //}
                //else
                shapes[shapes.Count - 1].Set(e.Location, 1); //Set điểm cuối chứ không phải 1
                pnlMain.Invalidate();
            }
            else if (mode == Mode.Select)
            {
                pnlMain.Cursor = Cursors.Default;
                for (int i = 0; i < shapes.Count; i++)
                {
                    if (shapes[i].isSelected)
                    {
                        if (shapes[i].AtRotatePosition(e.Location))
                        {
                            pnlMain.Cursor = Cursors.PanEast;
                            break;
                        }
                        if (shapes[i].AtScalePosition(e.Location))
                        {
                            pnlMain.Cursor = Cursors.SizeNESW;
                            break;
                        }
                    }
                    else if (shapes[i].Inside(e.Location))
                    {
                        pnlMain.Cursor = Cursors.SizeAll;
                        break;
                    }
                }
            }

            else if (mode == Mode.Moving)
            {
                pnlMain.Cursor = Cursors.SizeAll;
                Point distance = new Point(e.Location.X - prevPosition.X, e.Location.Y - prevPosition.Y);
                selectedShape.Move(distance);
                prevPosition = e.Location;
                pnlMain.Invalidate();
            }

            else if (mode == Mode.Rotating)
            {
                float alpha = (float)selectedShape.CalculateAngel(selectedShape.Center(), prevPosition, e.Location);
                prevPosition = e.Location;
                selectedShape.Configure(Angel: alpha);
                pnlMain.Invalidate();
            }
            else if (mode == Mode.Scaling)
            {
                selectedShape.Set(e.Location, 0);
                pnlMain.Invalidate();
            }


        }

        private void pnlMain_MouseUp(object sender, MouseEventArgs e)
        {
            switch (mode)
            {
                case Mode.Scaling:
                    if (selectedShape is MyRectangle
                        || selectedShape is MyParallelogram
                        || selectedShape is MyCircle
                        )
                        shapes[shapes.Count - 1].Normalize();
                    mode = Mode.Select;
                    break;
                case Mode.Rotating:
                case Mode.Moving:
                case Mode.Select:
                    mode = Mode.Select;
                    break;

                case Mode.WaitingDraw: //Vẽ xong hình, cần Normalize
                    if (BtnChecked(btnRectangle)
                        || BtnChecked(btnParallelogram)
                        || BtnChecked(btnCircle)
                        || BtnChecked(btnEllipse)
                        || BtnChecked(btnParabol)
                        )
                        shapes[shapes.Count - 1].Normalize();
                    mode = Mode.WaitingDraw;
                    break;
                case Mode.Drawing:
                    mode = Mode.Drawing;
                    break;
            }

            isMouseDown = false;
        }

        private bool BtnChecked(Button b)
        {
            return (b.BackColor != Color.Transparent);
        }

        private void pnlMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        #endregion


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

            for (int i = 0; i < shapes.Count; i++)
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

        private void btnFillableShape_Click(object sender, EventArgs e)
        {
            ckbFill.Enabled = true;
        }

        private void btnUnfillableShape_Click(object sender, EventArgs e)
        {
            ckbFill.Enabled = false;
            cmbFillStyle.Enabled = false;
        }

        private void ckbFill_CheckedChanged(object sender, EventArgs e)
        {
            //if (isCheck)
            //{
            //    ckbFill.Checked = false;
            //    cmbFillStyle.Enabled = false;
            //    isCheck = false;
            //}
            //else
            //{
            //    ckbFill.Checked = true;
            //    cmbFillStyle.Enabled = true;
            //    isCheck = true;
            //}

            cmbFillStyle.Enabled = ckbFill.Checked;
        }

        private void cmbFillStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i].isSelected && shapes[i].isFill && cmbFillStyle.SelectedIndex > 0)
                {
                    shapes[i].Configure(HatchStyle: (HatchStyle)cmbFillStyle.SelectedIndex);
                    shapes[i].Configure(FillStyle: 1); // chọn tô theo fill style
                }
                if (shapes[i].isSelected && shapes[i].isFill && cmbFillStyle.SelectedIndex == 0)
                    shapes[i].Configure(FillStyle: 0); // chon solid color
            }
            pnlMain.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && selectedShape != null)
            {
                for (int i = 0; i < shapes.Count; i++)
                    if (shapes[i].isSelected)
                    {
                        shapes.RemoveAt(i);
                        selectedShape = null;
                        pnlMain.Invalidate();
                    }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = "d:\\";
            saveFile.Filter = "Txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFile.FilterIndex = 2;
            saveFile.RestoreDirectory = true;
            saveFile.FileName = "MyShapes.txt";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < shapes.Count; i++)
                {
                    shapes[i].Save(saveFile.FileName);
                }
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = "d:\\";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string filename = openFile.FileName;
                StreamReader streamReader = new StreamReader(filename);
                string line, firstWord = null;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (line != "")
                        firstWord = line.Substring(0, line.IndexOf(" "));
                    switch (firstWord)
                    {

                        case "Line":
                            MyShape myLine = new MyLine();
                            myLine.Open(line);
                            shapes.Add(myLine);
                            break;

                        case "Rectangle":
                            MyShape myRectangle = new MyRectangle();
                            myRectangle.Open(line);
                            shapes.Add(myRectangle);
                            break;
                        case "Parallelogram":
                            MyShape myParallelogram = new MyParallelogram();
                            myParallelogram.Open(line);
                            shapes.Add(myParallelogram);
                            break;
                        case "Circle":
                            MyShape myCircle = new MyCircle();
                            myCircle.Open(line);
                            shapes.Add(myCircle);
                            break;
                        case "Polygon":
                            MyShape myPolygon = new MyPolygon();
                            myPolygon.Open(line);
                            shapes.Add(myPolygon);
                            break;
                        case "Polyline":
                            MyShape myPolyline = new MyPolyline();
                            myPolyline.Open(line);
                            shapes.Add(myPolyline);
                            break;
                    }
                    pnlMain.Invalidate();
                    firstWord = "";
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}


#region Những việc còn lại
/*
 * (Added) Thêm chức năng Rotate
 * Thêm các đối tượng Ellipse, Parabol
 * Fix bug đang có:
 * (fixed) - Không chọn được hình chữ nhật vẽ ngược
 * (fixed) - Khong chon được hình sau khi scale hoặc xoay
 * - Di chuyển hình tròn
 * - Scale hình chữ nhật, bị ngược điểm
 * - Không vẽ được hình tròn theo hướng 2 giờ
 * - Lỗi khi scale hình tròn
 * - Lỗi khi shape được chọn nằm chồng lên shape khác thì không scale được
 * - Chỉnh sửa hàm Draw  (thêm thuộc tính fill Style)
 * 
 * Thêm các thuộc tính của các hình có thể tô
 * Hoàn thiện chức năng Scale, Rotate của những thằng còn lại ngoại trừ Line, Rect, Circle
 * Thêm chức năng Save, Load.
 * 
 * Thêm Hyperbol
 * Thêm 1/2 circle, 1/2 ellipse
 */
#endregion