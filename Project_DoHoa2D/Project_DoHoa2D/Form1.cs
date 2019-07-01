using Project_DoHoa2D.UndoRedo;
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
        MouseInfo mouse = new MouseInfo();
        UndoRedoManager undoRedoManager;
        ShapeTypeDefine currentTypeShape;
        bool undoRedoFlag = false;

        private Point prevPosition;

        public Form1()
        {
            InitializeComponent();
            shapeButtons = GetListBtnShape();
            InitDefaultValue();
        }

        private void InitDefaultValue()
        {
            btnSelect.BackColor = Color.SkyBlue;
            cmbDashstyle.SelectedIndex = 0;
            trkWidth.Value = 1;
            mode = Mode.Select;
            ckbFill.Checked = false;
            cmbFillStyle.Enabled = false;
            undoRedoManager = new UndoRedoManager();
        }

        public List<Button> GetListBtnShape()
        {
            return new List<Button> { btnSelect, btnLine, btnRectangle, btnCircle, btnEllipse, btnPolygon, btnZigzag, btnBezier };
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
            if (mode == Mode.Select)
            {
                DeselectAll();
                mode = Mode.Select;

                if (mouse.state != StateMouse.Outside)
                {
                    prevPosition = e.Location;
                    AddSelectedShape(mouse.shapeUnder);
                }

                if (mouse.state == StateMouse.Rotate)
                    mode = Mode.Rotating;
                else if (mouse.state == StateMouse.Scale)
                    mode = Mode.Scaling;
                else if (mouse.state == StateMouse.Inside)
                    mode = Mode.Moving;

                pnlMain.Invalidate();
            }

            else if (mode == Mode.WaitingDraw)
            {
                MyShape shape = MyShape.Create(currentTypeShape, e.Location, e.Location);
                shapes.Add(shape);
                mode = Mode.Drawing;

                if (currentTypeShape == ShapeTypeDefine.LINE)
                    shape.Configure(DashStyle: (DashStyle)cmbDashstyle.SelectedIndex, BorderColor: btnBorderColor.BackColor, Width: trkWidth.Value + 1, BackgroundColor: btnBackColor.BackColor);
                else if (currentTypeShape == ShapeTypeDefine.RECTANGLE)
                {
                    if (ckbFill.Checked)
                    {
                        shape.isFill = true;
                        if (cmbFillStyle.SelectedIndex == 0)
                            shape.fillStyle = 0;
                        else if (cmbFillStyle.SelectedIndex > 0)
                        {
                            shape.fillStyle = 1;
                            shape.Configure(DashStyle: (DashStyle)cmbDashstyle.SelectedIndex, BorderColor: btnBorderColor.BackColor, Width: trkWidth.Value + 1, BackgroundColor: btnBackColor.BackColor, HatchStyle: (HatchStyle)cmbFillStyle.SelectedIndex);
                        }
                    }
                    shape.Configure(DashStyle: (DashStyle)cmbDashstyle.SelectedIndex, BorderColor: btnBorderColor.BackColor, Width: trkWidth.Value + 1, BackgroundColor: btnBackColor.BackColor);
                }

                else if (BtnChecked(btnPolygon))
                {
                    if (ckbFill.Checked)
                    {
                        shape.isFill = true;
                        if (cmbFillStyle.SelectedIndex == 0)
                            shape.fillStyle = 0;
                        else if (cmbFillStyle.SelectedIndex > 0)
                        {
                            shape.fillStyle = 1;
                            shape.Configure(DashStyle: (DashStyle)cmbDashstyle.SelectedIndex, BorderColor: btnBorderColor.BackColor, Width: trkWidth.Value + 1, BackgroundColor: btnBackColor.BackColor, HatchStyle: (HatchStyle)cmbFillStyle.SelectedIndex);
                        }
                    }

                    shape.Configure(DashStyle: (DashStyle)cmbDashstyle.SelectedIndex, BorderColor: btnBorderColor.BackColor, Width: trkWidth.Value + 1, BackgroundColor: btnBackColor.BackColor);

                }

                else if (BtnChecked(btnZigzag))
                    shape.Configure(DashStyle: (DashStyle)cmbDashstyle.SelectedIndex, BorderColor: btnBorderColor.BackColor, Width: trkWidth.Value + 1, BackgroundColor: btnBackColor.BackColor);
                else if (BtnChecked(btnBezier))
                    shape.Configure(DashStyle: (DashStyle)cmbDashstyle.SelectedIndex, BorderColor: btnBorderColor.BackColor, Width: trkWidth.Value + 1, BackgroundColor: btnBackColor.BackColor);
                else if (BtnChecked(btnCircle))
                {
                    if (ckbFill.Checked)
                    {
                        shape.isFill = true;
                        if (cmbFillStyle.SelectedIndex == 0)
                            shape.fillStyle = 0;
                        else if (cmbFillStyle.SelectedIndex > 0)
                        {
                            shape.fillStyle = 1;
                            shape.Configure(DashStyle: (DashStyle)cmbDashstyle.SelectedIndex, BorderColor: btnBorderColor.BackColor, Width: trkWidth.Value + 1, BackgroundColor: btnBackColor.BackColor, HatchStyle: (HatchStyle)cmbFillStyle.SelectedIndex);
                        }
                    }
                    shape.Configure(DashStyle: (DashStyle)cmbDashstyle.SelectedIndex, BorderColor: btnBorderColor.BackColor, Width: trkWidth.Value + 1, BackgroundColor: btnBackColor.BackColor);
                }

                else if (BtnChecked(btnEllipse))
                {
                    if (ckbFill.Checked)
                    {
                        shape.isFill = true;
                        if (cmbFillStyle.SelectedIndex == 0)
                            shape.fillStyle = 0;
                        else if (cmbFillStyle.SelectedIndex > 0)
                        {
                            shape.fillStyle = 1;
                            shape.Configure(DashStyle: (DashStyle)cmbDashstyle.SelectedIndex, BorderColor: btnBorderColor.BackColor, Width: trkWidth.Value + 1, BackgroundColor: btnBackColor.BackColor, HatchStyle: (HatchStyle)cmbFillStyle.SelectedIndex);
                        }
                    }
                    shape.Configure(DashStyle: (DashStyle)cmbDashstyle.SelectedIndex, BorderColor: btnBorderColor.BackColor, Width: trkWidth.Value + 1, BackgroundColor: btnBackColor.BackColor);
                }
            }
            else if (mode == Mode.Drawing)
            {
                if (undoRedoFlag)
                {
                    undoRedoManager.setDefauleStatus();
                    undoRedoFlag = false;
                }
                if (BtnChecked(btnPolygon) || BtnChecked(btnZigzag))
                {
                    shapes.Last().Extend_ExtendableShape(e.Location);
                    mode = Mode.Drawing;
                }
                else if (BtnChecked(btnBezier))
                {
                    shapes.Last().Extend_ExtendableShape(e.Location);
                    if (shapes.Last().numPoint == 5)
                    {
                        mode = Mode.WaitingDraw; 
                        shapes.Last().RemoveLastPoint();
                    }
                }
                else
                {
                    shapes.Last().Set(e.Location, 1);
                    mode = Mode.WaitingDraw;

                }
                pnlMain.Invalidate();
            }
            else if (mode == Mode.Moving)
            {
                mode = Mode.Select;
                pnlMain.Invalidate();
            }

        }

        private void AddSelectedShape(MyShape shape)
        {
            if (selectedShape != null && selectedShape != shape)
                DeselectAll();
            selectedShape = shape;
            shape.isSelected = true;
        }

        private void DeselectAll()
        {
            if (selectedShape != null)
            {
                selectedShape.isSelected = false;
                selectedShape = null;
            }
        }

        private void pnlMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (mode == Mode.Drawing)
            {
                if (BtnChecked(btnPolygon) || BtnChecked(btnBezier) || BtnChecked(btnZigzag))
                    shapes.Last().Set(e.Location, shapes.Last().numPoint - 1);
                else
                    shapes.Last().Set(e.Location, 1); 

                pnlMain.Invalidate();
            }
            else if (mode == Mode.Select)
            {
                for (int i = shapes.Count - 1; i >= 0; i--)
                {
                    mouse = shapes[i].CalcMousePosition(e.Location);
                    if (mouse.state != StateMouse.Outside)
                        break;
                }

                if (mouse.state == StateMouse.Rotate)
                    pnlMain.Cursor = Cursors.Hand;
                else if (mouse.state == StateMouse.Inside)
                    pnlMain.Cursor = Cursors.SizeAll;
                else if (mouse.state == StateMouse.Scale)
                {
                    switch (mouse.corner)
                    {
                        case Corner.TopLeft: pnlMain.Cursor = Cursors.SizeNWSE; break;
                        case Corner.TopRight: pnlMain.Cursor = Cursors.SizeNESW; break;
                        case Corner.DownRight: pnlMain.Cursor = Cursors.SizeNWSE; break;
                        case Corner.DownLeft: pnlMain.Cursor = Cursors.SizeNESW; break;
                    }
                }
                else pnlMain.Cursor = Cursors.Default;
            }

            else if (mode == Mode.Moving)
            {
                pnlMain.Cursor = Cursors.SizeAll;
                Size distance = new Size(e.Location) - new Size(prevPosition);
                selectedShape.Move(distance);
                prevPosition = e.Location;

                pnlMain.Invalidate();
            }

            else if (mode == Mode.Rotating)
            {
                float alpha = (float)selectedShape.CalculateAngel(selectedShape.GetCenterPoint(), prevPosition, e.Location);
                prevPosition = e.Location;
                selectedShape.Configure(Angle: alpha);
               
                pnlMain.Invalidate();
            }
            else if (mode == Mode.Scaling)
            {
                Size distance = new Size(e.Location) - new Size(prevPosition);
                selectedShape.Scale(prevPosition, e.Location);
                prevPosition = e.Location;
                pnlMain.Invalidate();
            }
        }

        private void pnlMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (mode != Mode.Drawing)
            {
                undoRedoManager.AddNewStatus(shapes);
            }
            switch (mode)
            {
                case Mode.Scaling:
                    if (selectedShape is MyRectangle || selectedShape is MyCircle)
                        shapes.Last().Normalize();
                    mode = Mode.Select;
                    break;
                case Mode.Rotating:
                case Mode.Moving:
                case Mode.Select:
                    mode = Mode.Select;
                    break;

                case Mode.WaitingDraw:
                    if (BtnChecked(btnRectangle) || BtnChecked(btnCircle) || BtnChecked(btnEllipse))
                        shapes.Last().Normalize();
                    mode = Mode.WaitingDraw;
                    break;
                case Mode.Drawing:
                    mode = Mode.Drawing;
                    break;
            }
        }

        private bool BtnChecked(Button b)
        {
            return (b.BackColor != Color.Transparent);
        }

        private void pnlMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (mode == Mode.Drawing)
            {
                if (BtnChecked(btnPolygon) || BtnChecked(btnZigzag))
                {
                    shapes.Last().Extend_ExtendableShape(e.Location);
                    shapes.Last().RemoveLastPoint();
                    shapes.Last().RemoveLastPoint();
                    mode = Mode.WaitingDraw;
                }
                else if (BtnChecked(btnBezier))
                {
                    shapes.Last().RemoveLastPoint();
                    shapes.Last().RemoveLastPoint();
                    mode = Mode.WaitingDraw;
                }
               
                pnlMain.Invalidate();
            }
        }
        #endregion


        private void btnShape_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < shapeButtons.Count; i++)
                shapeButtons[i].BackColor = Color.Transparent;
            Button clickedShape = sender as Button;
            clickedShape.BackColor = CONST.COLOR_CURRENT_SHAPE;
            btnSelect.BackColor = Color.Transparent;
            mode = Mode.WaitingDraw;
            pnlMain.Cursor = Cursors.Cross;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < shapeButtons.Count; i++)
                shapeButtons[i].BackColor = Color.Transparent;
            btnSelect.BackColor = CONST.COLOR_ACTIVE_SELECT_BUTTON;
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
            undoRedoManager.AddNewStatus(shapes);
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
            undoRedoManager.AddNewStatus(shapes);

            pnlMain.Invalidate();
        }

        private void btnFillableShape_Click(object sender, EventArgs e)
        {
            ckbFill.Enabled = true;
            Button clickedShape = sender as Button;
            currentTypeShape = (ShapeTypeDefine)(ShapeTypeDefine)Int32.Parse((clickedShape.Tag).ToString());
        }

        private void btnUnfillableShape_Click(object sender, EventArgs e)
        {
            ckbFill.Enabled = false;
            cmbFillStyle.Enabled = false;
            Button clickedShape = sender as Button;
            currentTypeShape = (ShapeTypeDefine)(ShapeTypeDefine)Int32.Parse((clickedShape.Tag).ToString());
        }

        private void ckbFill_CheckedChanged(object sender, EventArgs e)
        {
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
            undoRedoManager.AddNewStatus(shapes);
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
                        undoRedoManager.AddNewStatus(shapes);

                        pnlMain.Invalidate();
                    }
            }
            if ((e.KeyCode == Keys.Z && (e.Control) && (e.Shift)) || (e.KeyCode == Keys.Y && (e.Control)))
            {
                shapes = undoRedoManager.HandleRedo();
                undoRedoFlag = true;
                pnlMain.Invalidate();
            }
            if (e.KeyCode == Keys.Z && (e.Control))
            {
                shapes = undoRedoManager.HandleUndo();
                undoRedoFlag = true;
                pnlMain.Invalidate();
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
                        case "Circle":
                            MyShape myCircle = new MyCircle();
                            myCircle.Open(line);
                            shapes.Add(myCircle);
                            break;
                        case "Ellipse":
                            MyShape myEllipse = new MyEllipse();
                            myEllipse.Open(line);
                            shapes.Add(myEllipse);
                            break;
                        case "Polygon":
                            MyShape myPolygon = new MyPolygon();
                            myPolygon.Open(line);
                            shapes.Add(myPolygon);
                            break;
                        case "Bezier":
                            MyShape myBezier = new MyBezier();
                            myBezier.Open(line);
                            shapes.Add(myBezier);
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
            this.Close();
        }

        private void trkWidth_Scroll(object sender, EventArgs e)
        {
            if (selectedShape != null)
            {
                selectedShape.Configure(Width: trkWidth.Value + 1);
                undoRedoManager.AddNewStatus(shapes);
                pnlMain.Invalidate();
            }

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
 * (fixed) - Scale hình chữ nhật, bị ngược điểm
 * (fixed) Di chuyển hình tròn
 * (fixed) Lỗi Scale hcn khi có góc
 * (fixed) Không vẽ được hình tròn theo hướng 2 giờ
 * (fixed) Lỗi khi scale hình tròn
 * - Lỗi điểm neo của hình khi xoay quá nhiều
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