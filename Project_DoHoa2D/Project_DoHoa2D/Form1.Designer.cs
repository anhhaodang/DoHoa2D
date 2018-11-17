namespace Project_DoHoa2D
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnLine = new System.Windows.Forms.Button();
            this.btnRectangle = new System.Windows.Forms.Button();
            this.btnCircle = new System.Windows.Forms.Button();
            this.btnEllipse = new System.Windows.Forms.Button();
            this.btnPolygon = new System.Windows.Forms.Button();
            this.btnParabol = new System.Windows.Forms.Button();
            this.pnlShape = new System.Windows.Forms.Panel();
            this.btnArc = new System.Windows.Forms.Button();
            this.btnZigzag = new System.Windows.Forms.Button();
            this.btnParallelogram = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDashstyle = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBorderColor = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnBackColor = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.ckbFill = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbFillStyle = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlMain = new Project_DoHoa2D.DrawArea();
            this.pnlShape.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLine
            // 
            this.btnLine.BackColor = System.Drawing.Color.White;
            this.btnLine.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnLine.FlatAppearance.BorderSize = 0;
            this.btnLine.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnLine.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnLine.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLine.Image = ((System.Drawing.Image)(resources.GetObject("btnLine.Image")));
            this.btnLine.Location = new System.Drawing.Point(3, 2);
            this.btnLine.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(32, 32);
            this.btnLine.TabIndex = 0;
            this.btnLine.UseVisualStyleBackColor = false;
            this.btnLine.Click += new System.EventHandler(this.btnUnfillableShape_Click);
            this.btnLine.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnShape_MouseClick);
            // 
            // btnRectangle
            // 
            this.btnRectangle.BackColor = System.Drawing.Color.White;
            this.btnRectangle.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnRectangle.FlatAppearance.BorderSize = 0;
            this.btnRectangle.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnRectangle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnRectangle.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnRectangle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRectangle.Image = ((System.Drawing.Image)(resources.GetObject("btnRectangle.Image")));
            this.btnRectangle.Location = new System.Drawing.Point(41, 2);
            this.btnRectangle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(32, 32);
            this.btnRectangle.TabIndex = 0;
            this.btnRectangle.UseVisualStyleBackColor = false;
            this.btnRectangle.Click += new System.EventHandler(this.btnFillableShape_Click);
            this.btnRectangle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnShape_MouseClick);
            // 
            // btnCircle
            // 
            this.btnCircle.BackColor = System.Drawing.Color.White;
            this.btnCircle.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCircle.FlatAppearance.BorderSize = 0;
            this.btnCircle.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnCircle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCircle.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnCircle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCircle.Image = ((System.Drawing.Image)(resources.GetObject("btnCircle.Image")));
            this.btnCircle.Location = new System.Drawing.Point(119, 2);
            this.btnCircle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(32, 32);
            this.btnCircle.TabIndex = 0;
            this.btnCircle.UseVisualStyleBackColor = false;
            this.btnCircle.Click += new System.EventHandler(this.btnFillableShape_Click);
            this.btnCircle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnShape_MouseClick);
            // 
            // btnEllipse
            // 
            this.btnEllipse.BackColor = System.Drawing.Color.White;
            this.btnEllipse.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnEllipse.FlatAppearance.BorderSize = 0;
            this.btnEllipse.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnEllipse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnEllipse.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnEllipse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEllipse.Image = ((System.Drawing.Image)(resources.GetObject("btnEllipse.Image")));
            this.btnEllipse.Location = new System.Drawing.Point(157, 2);
            this.btnEllipse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEllipse.Name = "btnEllipse";
            this.btnEllipse.Size = new System.Drawing.Size(32, 32);
            this.btnEllipse.TabIndex = 0;
            this.btnEllipse.UseVisualStyleBackColor = false;
            this.btnEllipse.Click += new System.EventHandler(this.btnFillableShape_Click);
            this.btnEllipse.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnShape_MouseClick);
            // 
            // btnPolygon
            // 
            this.btnPolygon.BackColor = System.Drawing.Color.White;
            this.btnPolygon.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPolygon.FlatAppearance.BorderSize = 0;
            this.btnPolygon.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnPolygon.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnPolygon.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnPolygon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPolygon.Image = ((System.Drawing.Image)(resources.GetObject("btnPolygon.Image")));
            this.btnPolygon.Location = new System.Drawing.Point(3, 41);
            this.btnPolygon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Size = new System.Drawing.Size(32, 32);
            this.btnPolygon.TabIndex = 0;
            this.btnPolygon.UseVisualStyleBackColor = false;
            this.btnPolygon.Click += new System.EventHandler(this.btnFillableShape_Click);
            this.btnPolygon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnShape_MouseClick);
            // 
            // btnParabol
            // 
            this.btnParabol.BackColor = System.Drawing.Color.White;
            this.btnParabol.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnParabol.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnParabol.FlatAppearance.BorderSize = 0;
            this.btnParabol.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnParabol.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnParabol.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnParabol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnParabol.Image = ((System.Drawing.Image)(resources.GetObject("btnParabol.Image")));
            this.btnParabol.Location = new System.Drawing.Point(41, 41);
            this.btnParabol.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnParabol.Name = "btnParabol";
            this.btnParabol.Size = new System.Drawing.Size(32, 32);
            this.btnParabol.TabIndex = 0;
            this.btnParabol.UseVisualStyleBackColor = false;
            this.btnParabol.Click += new System.EventHandler(this.btnFillableShape_Click);
            this.btnParabol.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnShape_MouseClick);
            // 
            // pnlShape
            // 
            this.pnlShape.BackColor = System.Drawing.Color.White;
            this.pnlShape.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlShape.Controls.Add(this.btnPolygon);
            this.pnlShape.Controls.Add(this.btnArc);
            this.pnlShape.Controls.Add(this.btnZigzag);
            this.pnlShape.Controls.Add(this.btnParabol);
            this.pnlShape.Controls.Add(this.btnParallelogram);
            this.pnlShape.Controls.Add(this.btnEllipse);
            this.pnlShape.Controls.Add(this.btnLine);
            this.pnlShape.Controls.Add(this.btnCircle);
            this.pnlShape.Controls.Add(this.btnRectangle);
            this.pnlShape.Location = new System.Drawing.Point(79, 42);
            this.pnlShape.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlShape.Name = "pnlShape";
            this.pnlShape.Size = new System.Drawing.Size(198, 80);
            this.pnlShape.TabIndex = 2;
            // 
            // btnArc
            // 
            this.btnArc.BackColor = System.Drawing.Color.White;
            this.btnArc.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnArc.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnArc.FlatAppearance.BorderSize = 0;
            this.btnArc.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnArc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnArc.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnArc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArc.Image = ((System.Drawing.Image)(resources.GetObject("btnArc.Image")));
            this.btnArc.Location = new System.Drawing.Point(119, 41);
            this.btnArc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnArc.Name = "btnArc";
            this.btnArc.Size = new System.Drawing.Size(32, 32);
            this.btnArc.TabIndex = 0;
            this.btnArc.UseVisualStyleBackColor = false;
            this.btnArc.Click += new System.EventHandler(this.btnUnfillableShape_Click);
            this.btnArc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnShape_MouseClick);
            // 
            // btnZigzag
            // 
            this.btnZigzag.BackColor = System.Drawing.Color.White;
            this.btnZigzag.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnZigzag.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnZigzag.FlatAppearance.BorderSize = 0;
            this.btnZigzag.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnZigzag.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnZigzag.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnZigzag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZigzag.Image = ((System.Drawing.Image)(resources.GetObject("btnZigzag.Image")));
            this.btnZigzag.Location = new System.Drawing.Point(80, 41);
            this.btnZigzag.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnZigzag.Name = "btnZigzag";
            this.btnZigzag.Size = new System.Drawing.Size(32, 32);
            this.btnZigzag.TabIndex = 0;
            this.btnZigzag.UseVisualStyleBackColor = false;
            this.btnZigzag.Click += new System.EventHandler(this.btnUnfillableShape_Click);
            this.btnZigzag.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnShape_MouseClick);
            // 
            // btnParallelogram
            // 
            this.btnParallelogram.BackColor = System.Drawing.Color.White;
            this.btnParallelogram.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnParallelogram.FlatAppearance.BorderSize = 0;
            this.btnParallelogram.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnParallelogram.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnParallelogram.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnParallelogram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnParallelogram.Image = ((System.Drawing.Image)(resources.GetObject("btnParallelogram.Image")));
            this.btnParallelogram.Location = new System.Drawing.Point(80, 2);
            this.btnParallelogram.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnParallelogram.Name = "btnParallelogram";
            this.btnParallelogram.Size = new System.Drawing.Size(32, 32);
            this.btnParallelogram.TabIndex = 0;
            this.btnParallelogram.UseVisualStyleBackColor = false;
            this.btnParallelogram.Click += new System.EventHandler(this.btnFillableShape_Click);
            this.btnParallelogram.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnShape_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "DashStyle";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Width";
            // 
            // cmbDashstyle
            // 
            this.cmbDashstyle.FormattingEnabled = true;
            this.cmbDashstyle.Items.AddRange(new object[] {
            "Solid",
            "Dot",
            "Dash",
            "DashDot",
            "DashDotDot"});
            this.cmbDashstyle.Location = new System.Drawing.Point(83, 22);
            this.cmbDashstyle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbDashstyle.Name = "cmbDashstyle";
            this.cmbDashstyle.Size = new System.Drawing.Size(121, 24);
            this.cmbDashstyle.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1181, 28);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 24);
            this.toolStripMenuItem1.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(120, 26);
            this.newToolStripMenuItem.Text = "New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(120, 26);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(120, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // btnBorderColor
            // 
            this.btnBorderColor.BackColor = System.Drawing.Color.Black;
            this.btnBorderColor.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnBorderColor.FlatAppearance.BorderSize = 0;
            this.btnBorderColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorderColor.ForeColor = System.Drawing.Color.White;
            this.btnBorderColor.Location = new System.Drawing.Point(887, 49);
            this.btnBorderColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBorderColor.Name = "btnBorderColor";
            this.btnBorderColor.Size = new System.Drawing.Size(40, 39);
            this.btnBorderColor.TabIndex = 0;
            this.btnBorderColor.UseVisualStyleBackColor = false;
            this.btnBorderColor.Click += new System.EventHandler(this.btnBorderColor_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSelect.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSelect.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnSelect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnSelect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.Location = new System.Drawing.Point(28, 62);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(32, 32);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnBackColor
            // 
            this.btnBackColor.BackColor = System.Drawing.Color.Red;
            this.btnBackColor.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnBackColor.FlatAppearance.BorderSize = 0;
            this.btnBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackColor.ForeColor = System.Drawing.Color.White;
            this.btnBackColor.Location = new System.Drawing.Point(965, 49);
            this.btnBackColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.Size = new System.Drawing.Size(40, 39);
            this.btnBackColor.TabIndex = 13;
            this.btnBackColor.UseVisualStyleBackColor = false;
            this.btnBackColor.Click += new System.EventHandler(this.btnBackColor_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(69, 65);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(144, 56);
            this.trackBar1.TabIndex = 14;
            // 
            // ckbFill
            // 
            this.ckbFill.AutoSize = true;
            this.ckbFill.Location = new System.Drawing.Point(13, 22);
            this.ckbFill.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ckbFill.Name = "ckbFill";
            this.ckbFill.Size = new System.Drawing.Size(47, 21);
            this.ckbFill.TabIndex = 15;
            this.ckbFill.Text = "Fill";
            this.ckbFill.UseVisualStyleBackColor = true;
            this.ckbFill.CheckedChanged += new System.EventHandler(this.ckbFill_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(883, 92);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 34);
            this.label5.TabIndex = 16;
            this.label5.Text = "Border\r\nColor";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(941, 92);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 34);
            this.label6.TabIndex = 16;
            this.label6.Text = "Background\r\nColor";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbDashstyle);
            this.panel1.Location = new System.Drawing.Point(303, 33);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(252, 113);
            this.panel1.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmbFillStyle);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.ckbFill);
            this.panel2.Location = new System.Drawing.Point(588, 33);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(267, 113);
            this.panel2.TabIndex = 18;
            // 
            // cmbFillStyle
            // 
            this.cmbFillStyle.FormattingEnabled = true;
            this.cmbFillStyle.Items.AddRange(new object[] {
            "SolidColor",
            "Vertical",
            "ForwardDiagonal",
            "BackwardDiagonal",
            "Cross",
            "DiagonalCross",
            "Percent05",
            "Percent10",
            "Percent20",
            "Percent25",
            "Percent30",
            "Percent40",
            "Percent50",
            "Percent60",
            "Percent70",
            "Percent75",
            "Percent80",
            "Percent90",
            "LightDownwardDiagonal",
            "LightUpwardDiagonal",
            "DarkDownwardDiagonal",
            "DarkUpwardDiagonal",
            "WideDownwardDiagonal",
            "WideUpwardDiagonal",
            "LightVertical",
            "LightHorizontal",
            "NarrowVertical",
            "NarrowHorizontal",
            "DarkVertical",
            "DarkHorizontal",
            "DashedDownwardDiagonal",
            "DashedUpwardDiagonal",
            "DashedHorizontal",
            "DashedVertical"});
            this.cmbFillStyle.Location = new System.Drawing.Point(75, 55);
            this.cmbFillStyle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbFillStyle.Name = "cmbFillStyle";
            this.cmbFillStyle.Size = new System.Drawing.Size(160, 24);
            this.cmbFillStyle.TabIndex = 17;
            this.cmbFillStyle.SelectedIndexChanged += new System.EventHandler(this.cmbFillStyle_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 60);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "Fill Style";
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMain.Location = new System.Drawing.Point(0, 153);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1181, 606);
            this.pnlMain.TabIndex = 12;
            this.pnlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMain_Paint);
            this.pnlMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseDoubleClick);
            this.pnlMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseDown);
            this.pnlMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseMove);
            this.pnlMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 750);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnBackColor);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.pnlShape);
            this.Controls.Add(this.btnBorderColor);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "MyPaint";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.pnlShape.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnPolygon;
        private System.Windows.Forms.Button btnEllipse;
        private System.Windows.Forms.Button btnCircle;
        private System.Windows.Forms.Button btnParabol;
        private System.Windows.Forms.Button btnRectangle;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Panel pnlShape;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbDashstyle;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button btnBorderColor;
        private System.Windows.Forms.Button btnArc;
        private System.Windows.Forms.Button btnZigzag;
        private DrawArea pnlMain;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnBackColor;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.CheckBox ckbFill;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFillStyle;
        private System.Windows.Forms.Button btnParallelogram;
    }
}

