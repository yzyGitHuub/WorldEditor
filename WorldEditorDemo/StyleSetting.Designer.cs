namespace WEMapDemo
{
    partial class StyleSetting
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.SinglePage = new System.Windows.Forms.TabPage();
            this.Single_bdrColor = new System.Windows.Forms.PictureBox();
            this.BoundaryColor_label = new System.Windows.Forms.Label();
            this.Single_Cancel = new System.Windows.Forms.Button();
            this.Single_Apply = new System.Windows.Forms.Button();
            this.Single_OK = new System.Windows.Forms.Button();
            this.Single_Color = new System.Windows.Forms.PictureBox();
            this.Color_label = new System.Windows.Forms.Label();
            this.UniqueValPage = new System.Windows.Forms.TabPage();
            this.AttributeTable = new System.Windows.Forms.DataGridView();
            this.UniqVal_Cancel = new System.Windows.Forms.Button();
            this.UniqVal_OK = new System.Windows.Forms.Button();
            this.btn_GetUniqueVal = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.UniqVal_Field = new System.Windows.Forms.ComboBox();
            this.ClassBreakPage = new System.Windows.Forms.TabPage();
            this.Class_symbol = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.Class_bdrColor = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Class_Cancel = new System.Windows.Forms.Button();
            this.Class_OK = new System.Windows.Forms.Button();
            this.Class_ToSize = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Class_FromSize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Class_ToColor = new System.Windows.Forms.PictureBox();
            this.Class_FromColor = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ClassNum = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Class_Field = new System.Windows.Forms.ComboBox();
            this.Single_bdrWidth = new System.Windows.Forms.NumericUpDown();
            this.Boundary_label = new System.Windows.Forms.Label();
            this.Single_size = new System.Windows.Forms.NumericUpDown();
            this.Size_label = new System.Windows.Forms.Label();
            this.Single_symbol = new System.Windows.Forms.ComboBox();
            this.Style_label = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.LabelSetting = new System.Windows.Forms.Button();
            this.Unique_Apply = new System.Windows.Forms.Button();
            this.Class_Apply = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.SinglePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Single_bdrColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Single_Color)).BeginInit();
            this.UniqueValPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AttributeTable)).BeginInit();
            this.ClassBreakPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Class_bdrColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Class_ToColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Class_FromColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Single_bdrWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Single_size)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.SinglePage);
            this.tabControl1.Controls.Add(this.UniqueValPage);
            this.tabControl1.Controls.Add(this.ClassBreakPage);
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.ItemSize = new System.Drawing.Size(100, 35);
            this.tabControl1.Location = new System.Drawing.Point(11, 115);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(372, 333);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // SinglePage
            // 
            this.SinglePage.Controls.Add(this.Single_bdrColor);
            this.SinglePage.Controls.Add(this.BoundaryColor_label);
            this.SinglePage.Controls.Add(this.Single_Cancel);
            this.SinglePage.Controls.Add(this.Single_Apply);
            this.SinglePage.Controls.Add(this.Single_OK);
            this.SinglePage.Controls.Add(this.Single_Color);
            this.SinglePage.Controls.Add(this.Color_label);
            this.SinglePage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SinglePage.Location = new System.Drawing.Point(4, 39);
            this.SinglePage.Margin = new System.Windows.Forms.Padding(2);
            this.SinglePage.Name = "SinglePage";
            this.SinglePage.Padding = new System.Windows.Forms.Padding(2);
            this.SinglePage.Size = new System.Drawing.Size(364, 290);
            this.SinglePage.TabIndex = 0;
            this.SinglePage.Text = "单一符号";
            this.SinglePage.UseVisualStyleBackColor = true;
            // 
            // Single_bdrColor
            // 
            this.Single_bdrColor.BackColor = System.Drawing.Color.Black;
            this.Single_bdrColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Single_bdrColor.Location = new System.Drawing.Point(273, 154);
            this.Single_bdrColor.Margin = new System.Windows.Forms.Padding(2);
            this.Single_bdrColor.Name = "Single_bdrColor";
            this.Single_bdrColor.Size = new System.Drawing.Size(34, 34);
            this.Single_bdrColor.TabIndex = 25;
            this.Single_bdrColor.TabStop = false;
            this.Single_bdrColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Single_bdrColor_MouseClick);
            // 
            // BoundaryColor_label
            // 
            this.BoundaryColor_label.AutoSize = true;
            this.BoundaryColor_label.Location = new System.Drawing.Point(203, 154);
            this.BoundaryColor_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BoundaryColor_label.Name = "BoundaryColor_label";
            this.BoundaryColor_label.Size = new System.Drawing.Size(44, 17);
            this.BoundaryColor_label.TabIndex = 24;
            this.BoundaryColor_label.Text = "边界色";
            // 
            // Single_Cancel
            // 
            this.Single_Cancel.Location = new System.Drawing.Point(261, 258);
            this.Single_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.Single_Cancel.Name = "Single_Cancel";
            this.Single_Cancel.Size = new System.Drawing.Size(57, 23);
            this.Single_Cancel.TabIndex = 21;
            this.Single_Cancel.Text = "取消";
            this.Single_Cancel.UseVisualStyleBackColor = true;
            this.Single_Cancel.Click += new System.EventHandler(this.Single_Cancel_Click);
            // 
            // Single_Apply
            // 
            this.Single_Apply.Location = new System.Drawing.Point(190, 258);
            this.Single_Apply.Margin = new System.Windows.Forms.Padding(2);
            this.Single_Apply.Name = "Single_Apply";
            this.Single_Apply.Size = new System.Drawing.Size(57, 23);
            this.Single_Apply.TabIndex = 20;
            this.Single_Apply.Text = "应用";
            this.Single_Apply.UseVisualStyleBackColor = true;
            this.Single_Apply.Click += new System.EventHandler(this.Single_Apply_Click);
            // 
            // Single_OK
            // 
            this.Single_OK.Location = new System.Drawing.Point(119, 258);
            this.Single_OK.Margin = new System.Windows.Forms.Padding(2);
            this.Single_OK.Name = "Single_OK";
            this.Single_OK.Size = new System.Drawing.Size(57, 23);
            this.Single_OK.TabIndex = 20;
            this.Single_OK.Text = "确定";
            this.Single_OK.UseVisualStyleBackColor = true;
            this.Single_OK.Click += new System.EventHandler(this.Single_OK_Click);
            // 
            // Single_Color
            // 
            this.Single_Color.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Single_Color.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Single_Color.Location = new System.Drawing.Point(107, 154);
            this.Single_Color.Margin = new System.Windows.Forms.Padding(2);
            this.Single_Color.Name = "Single_Color";
            this.Single_Color.Size = new System.Drawing.Size(34, 34);
            this.Single_Color.TabIndex = 19;
            this.Single_Color.TabStop = false;
            this.Single_Color.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Single_Color_MouseClick);
            // 
            // Color_label
            // 
            this.Color_label.AutoSize = true;
            this.Color_label.Location = new System.Drawing.Point(37, 154);
            this.Color_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Color_label.Name = "Color_label";
            this.Color_label.Size = new System.Drawing.Size(32, 17);
            this.Color_label.TabIndex = 17;
            this.Color_label.Text = "颜色";
            // 
            // UniqueValPage
            // 
            this.UniqueValPage.Controls.Add(this.Unique_Apply);
            this.UniqueValPage.Controls.Add(this.AttributeTable);
            this.UniqueValPage.Controls.Add(this.UniqVal_Cancel);
            this.UniqueValPage.Controls.Add(this.UniqVal_OK);
            this.UniqueValPage.Controls.Add(this.btn_GetUniqueVal);
            this.UniqueValPage.Controls.Add(this.label1);
            this.UniqueValPage.Controls.Add(this.UniqVal_Field);
            this.UniqueValPage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UniqueValPage.Location = new System.Drawing.Point(4, 39);
            this.UniqueValPage.Margin = new System.Windows.Forms.Padding(2);
            this.UniqueValPage.Name = "UniqueValPage";
            this.UniqueValPage.Padding = new System.Windows.Forms.Padding(2);
            this.UniqueValPage.Size = new System.Drawing.Size(364, 290);
            this.UniqueValPage.TabIndex = 1;
            this.UniqueValPage.Text = "唯一值";
            this.UniqueValPage.UseVisualStyleBackColor = true;
            // 
            // AttributeTable
            // 
            this.AttributeTable.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.AttributeTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AttributeTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AttributeTable.Location = new System.Drawing.Point(4, 48);
            this.AttributeTable.Margin = new System.Windows.Forms.Padding(2);
            this.AttributeTable.Name = "AttributeTable";
            this.AttributeTable.RowTemplate.Height = 30;
            this.AttributeTable.Size = new System.Drawing.Size(358, 200);
            this.AttributeTable.TabIndex = 12;
            this.AttributeTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AttributeTable_CellClick);
            // 
            // UniqVal_Cancel
            // 
            this.UniqVal_Cancel.Location = new System.Drawing.Point(309, 252);
            this.UniqVal_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.UniqVal_Cancel.Name = "UniqVal_Cancel";
            this.UniqVal_Cancel.Size = new System.Drawing.Size(53, 23);
            this.UniqVal_Cancel.TabIndex = 11;
            this.UniqVal_Cancel.Text = "取消";
            this.UniqVal_Cancel.UseVisualStyleBackColor = true;
            this.UniqVal_Cancel.Click += new System.EventHandler(this.UniqVal_Cancel_Click);
            // 
            // UniqVal_OK
            // 
            this.UniqVal_OK.Location = new System.Drawing.Point(191, 252);
            this.UniqVal_OK.Margin = new System.Windows.Forms.Padding(2);
            this.UniqVal_OK.Name = "UniqVal_OK";
            this.UniqVal_OK.Size = new System.Drawing.Size(53, 23);
            this.UniqVal_OK.TabIndex = 10;
            this.UniqVal_OK.Text = "确定";
            this.UniqVal_OK.UseVisualStyleBackColor = true;
            this.UniqVal_OK.Click += new System.EventHandler(this.UniqVal_OK_Click);
            // 
            // btn_GetUniqueVal
            // 
            this.btn_GetUniqueVal.Location = new System.Drawing.Point(4, 252);
            this.btn_GetUniqueVal.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GetUniqueVal.Name = "btn_GetUniqueVal";
            this.btn_GetUniqueVal.Size = new System.Drawing.Size(100, 23);
            this.btn_GetUniqueVal.TabIndex = 9;
            this.btn_GetUniqueVal.Text = "提取唯一值";
            this.btn_GetUniqueVal.UseVisualStyleBackColor = true;
            this.btn_GetUniqueVal.Click += new System.EventHandler(this.btn_GetUniqueVal_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "字段";
            // 
            // UniqVal_Field
            // 
            this.UniqVal_Field.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UniqVal_Field.FormattingEnabled = true;
            this.UniqVal_Field.Location = new System.Drawing.Point(149, 15);
            this.UniqVal_Field.Margin = new System.Windows.Forms.Padding(2);
            this.UniqVal_Field.Name = "UniqVal_Field";
            this.UniqVal_Field.Size = new System.Drawing.Size(82, 22);
            this.UniqVal_Field.TabIndex = 6;
            this.UniqVal_Field.SelectedIndexChanged += new System.EventHandler(this.UniqVal_Field_SelectedIndexChanged);
            // 
            // ClassBreakPage
            // 
            this.ClassBreakPage.Controls.Add(this.Class_Apply);
            this.ClassBreakPage.Controls.Add(this.Class_symbol);
            this.ClassBreakPage.Controls.Add(this.label10);
            this.ClassBreakPage.Controls.Add(this.Class_bdrColor);
            this.ClassBreakPage.Controls.Add(this.label2);
            this.ClassBreakPage.Controls.Add(this.Class_Cancel);
            this.ClassBreakPage.Controls.Add(this.Class_OK);
            this.ClassBreakPage.Controls.Add(this.Class_ToSize);
            this.ClassBreakPage.Controls.Add(this.label7);
            this.ClassBreakPage.Controls.Add(this.Class_FromSize);
            this.ClassBreakPage.Controls.Add(this.label5);
            this.ClassBreakPage.Controls.Add(this.label4);
            this.ClassBreakPage.Controls.Add(this.Class_ToColor);
            this.ClassBreakPage.Controls.Add(this.Class_FromColor);
            this.ClassBreakPage.Controls.Add(this.label6);
            this.ClassBreakPage.Controls.Add(this.label8);
            this.ClassBreakPage.Controls.Add(this.ClassNum);
            this.ClassBreakPage.Controls.Add(this.label9);
            this.ClassBreakPage.Controls.Add(this.Class_Field);
            this.ClassBreakPage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClassBreakPage.Location = new System.Drawing.Point(4, 39);
            this.ClassBreakPage.Margin = new System.Windows.Forms.Padding(2);
            this.ClassBreakPage.Name = "ClassBreakPage";
            this.ClassBreakPage.Padding = new System.Windows.Forms.Padding(2);
            this.ClassBreakPage.Size = new System.Drawing.Size(364, 290);
            this.ClassBreakPage.TabIndex = 2;
            this.ClassBreakPage.Text = "分级符号";
            this.ClassBreakPage.UseVisualStyleBackColor = true;
            // 
            // Class_symbol
            // 
            this.Class_symbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Class_symbol.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Class_symbol.FormattingEnabled = true;
            this.Class_symbol.Location = new System.Drawing.Point(80, 87);
            this.Class_symbol.Margin = new System.Windows.Forms.Padding(2);
            this.Class_symbol.Name = "Class_symbol";
            this.Class_symbol.Size = new System.Drawing.Size(82, 22);
            this.Class_symbol.TabIndex = 41;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(35, 91);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 40;
            this.label10.Text = "样式";
            // 
            // Class_bdrColor
            // 
            this.Class_bdrColor.BackColor = System.Drawing.Color.Black;
            this.Class_bdrColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Class_bdrColor.Location = new System.Drawing.Point(264, 147);
            this.Class_bdrColor.Margin = new System.Windows.Forms.Padding(2);
            this.Class_bdrColor.Name = "Class_bdrColor";
            this.Class_bdrColor.Size = new System.Drawing.Size(34, 34);
            this.Class_bdrColor.TabIndex = 39;
            this.Class_bdrColor.TabStop = false;
            this.Class_bdrColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Class_bdrColor_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(220, 125);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 38;
            this.label2.Text = "边界色";
            // 
            // Class_Cancel
            // 
            this.Class_Cancel.Location = new System.Drawing.Point(293, 254);
            this.Class_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.Class_Cancel.Name = "Class_Cancel";
            this.Class_Cancel.Size = new System.Drawing.Size(53, 23);
            this.Class_Cancel.TabIndex = 35;
            this.Class_Cancel.Text = "取消";
            this.Class_Cancel.UseVisualStyleBackColor = true;
            this.Class_Cancel.Click += new System.EventHandler(this.Class_Cancel_Click);
            // 
            // Class_OK
            // 
            this.Class_OK.Location = new System.Drawing.Point(150, 254);
            this.Class_OK.Margin = new System.Windows.Forms.Padding(2);
            this.Class_OK.Name = "Class_OK";
            this.Class_OK.Size = new System.Drawing.Size(53, 23);
            this.Class_OK.TabIndex = 34;
            this.Class_OK.Text = "确定";
            this.Class_OK.UseVisualStyleBackColor = true;
            this.Class_OK.Click += new System.EventHandler(this.Class_OK_Click);
            // 
            // Class_ToSize
            // 
            this.Class_ToSize.Location = new System.Drawing.Point(150, 220);
            this.Class_ToSize.Margin = new System.Windows.Forms.Padding(2);
            this.Class_ToSize.Name = "Class_ToSize";
            this.Class_ToSize.Size = new System.Drawing.Size(49, 23);
            this.Class_ToSize.TabIndex = 33;
            this.Class_ToSize.Text = "20";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(109, 222);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 17);
            this.label7.TabIndex = 32;
            this.label7.Text = "to";
            // 
            // Class_FromSize
            // 
            this.Class_FromSize.Location = new System.Drawing.Point(37, 220);
            this.Class_FromSize.Margin = new System.Windows.Forms.Padding(2);
            this.Class_FromSize.Name = "Class_FromSize";
            this.Class_FromSize.Size = new System.Drawing.Size(48, 23);
            this.Class_FromSize.TabIndex = 31;
            this.Class_FromSize.Text = "5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(35, 195);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 30;
            this.label5.Text = "尺寸";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(110, 156);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 17);
            this.label4.TabIndex = 29;
            this.label4.Text = "to";
            // 
            // Class_ToColor
            // 
            this.Class_ToColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Class_ToColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Class_ToColor.Location = new System.Drawing.Point(165, 147);
            this.Class_ToColor.Margin = new System.Windows.Forms.Padding(2);
            this.Class_ToColor.Name = "Class_ToColor";
            this.Class_ToColor.Size = new System.Drawing.Size(34, 34);
            this.Class_ToColor.TabIndex = 28;
            this.Class_ToColor.TabStop = false;
            this.Class_ToColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Class_ToColor_MouseClick);
            // 
            // Class_FromColor
            // 
            this.Class_FromColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Class_FromColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Class_FromColor.Location = new System.Drawing.Point(37, 147);
            this.Class_FromColor.Margin = new System.Windows.Forms.Padding(2);
            this.Class_FromColor.Name = "Class_FromColor";
            this.Class_FromColor.Size = new System.Drawing.Size(34, 34);
            this.Class_FromColor.TabIndex = 27;
            this.Class_FromColor.TabStop = false;
            this.Class_FromColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Class_FromColor_MouseClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(35, 125);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 26;
            this.label6.Text = "颜色";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(220, 41);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "级数";
            // 
            // ClassNum
            // 
            this.ClassNum.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClassNum.FormattingEnabled = true;
            this.ClassNum.Location = new System.Drawing.Point(264, 37);
            this.ClassNum.Margin = new System.Windows.Forms.Padding(2);
            this.ClassNum.Name = "ClassNum";
            this.ClassNum.Size = new System.Drawing.Size(82, 22);
            this.ClassNum.TabIndex = 24;
            this.ClassNum.Text = "5";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(35, 41);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "字段";
            // 
            // Class_Field
            // 
            this.Class_Field.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Class_Field.FormattingEnabled = true;
            this.Class_Field.Location = new System.Drawing.Point(80, 37);
            this.Class_Field.Margin = new System.Windows.Forms.Padding(2);
            this.Class_Field.Name = "Class_Field";
            this.Class_Field.Size = new System.Drawing.Size(82, 22);
            this.Class_Field.TabIndex = 22;
            this.Class_Field.SelectedIndexChanged += new System.EventHandler(this.Class_Field_SelectedIndexChanged);
            // 
            // Single_bdrWidth
            // 
            this.Single_bdrWidth.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Single_bdrWidth.Location = new System.Drawing.Point(279, 66);
            this.Single_bdrWidth.Margin = new System.Windows.Forms.Padding(2);
            this.Single_bdrWidth.Name = "Single_bdrWidth";
            this.Single_bdrWidth.Size = new System.Drawing.Size(94, 23);
            this.Single_bdrWidth.TabIndex = 23;
            this.Single_bdrWidth.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // Boundary_label
            // 
            this.Boundary_label.AutoSize = true;
            this.Boundary_label.Location = new System.Drawing.Point(209, 69);
            this.Boundary_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Boundary_label.Name = "Boundary_label";
            this.Boundary_label.Size = new System.Drawing.Size(53, 12);
            this.Boundary_label.TabIndex = 22;
            this.Boundary_label.Text = "边界宽度";
            // 
            // Single_size
            // 
            this.Single_size.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Single_size.Location = new System.Drawing.Point(78, 69);
            this.Single_size.Margin = new System.Windows.Forms.Padding(2);
            this.Single_size.Name = "Single_size";
            this.Single_size.Size = new System.Drawing.Size(80, 23);
            this.Single_size.TabIndex = 18;
            this.Single_size.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // Size_label
            // 
            this.Size_label.AutoSize = true;
            this.Size_label.Location = new System.Drawing.Point(28, 70);
            this.Size_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Size_label.Name = "Size_label";
            this.Size_label.Size = new System.Drawing.Size(29, 12);
            this.Size_label.TabIndex = 16;
            this.Size_label.Text = "尺寸";
            // 
            // Single_symbol
            // 
            this.Single_symbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Single_symbol.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Single_symbol.FormattingEnabled = true;
            this.Single_symbol.Location = new System.Drawing.Point(78, 25);
            this.Single_symbol.Margin = new System.Windows.Forms.Padding(2);
            this.Single_symbol.Name = "Single_symbol";
            this.Single_symbol.Size = new System.Drawing.Size(82, 22);
            this.Single_symbol.TabIndex = 15;
            // 
            // Style_label
            // 
            this.Style_label.AutoSize = true;
            this.Style_label.Location = new System.Drawing.Point(28, 27);
            this.Style_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Style_label.Name = "Style_label";
            this.Style_label.Size = new System.Drawing.Size(29, 12);
            this.Style_label.TabIndex = 14;
            this.Style_label.Text = "样式";
            // 
            // checkBox1
            // 
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Location = new System.Drawing.Point(211, 25);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(81, 24);
            this.checkBox1.TabIndex = 24;
            this.checkBox1.Text = "显示注记";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // LabelSetting
            // 
            this.LabelSetting.Enabled = false;
            this.LabelSetting.Location = new System.Drawing.Point(308, 24);
            this.LabelSetting.Margin = new System.Windows.Forms.Padding(2);
            this.LabelSetting.Name = "LabelSetting";
            this.LabelSetting.Size = new System.Drawing.Size(65, 23);
            this.LabelSetting.TabIndex = 34;
            this.LabelSetting.Text = "设置注记";
            this.LabelSetting.UseVisualStyleBackColor = true;
            this.LabelSetting.Click += new System.EventHandler(this.LabelSetting_Click);
            // 
            // Unique_Apply
            // 
            this.Unique_Apply.Location = new System.Drawing.Point(248, 252);
            this.Unique_Apply.Margin = new System.Windows.Forms.Padding(2);
            this.Unique_Apply.Name = "Unique_Apply";
            this.Unique_Apply.Size = new System.Drawing.Size(57, 23);
            this.Unique_Apply.TabIndex = 21;
            this.Unique_Apply.Text = "应用";
            this.Unique_Apply.UseVisualStyleBackColor = true;
            this.Unique_Apply.Click += new System.EventHandler(this.Unique_Apply_Click);
            // 
            // Class_Apply
            // 
            this.Class_Apply.Location = new System.Drawing.Point(222, 254);
            this.Class_Apply.Margin = new System.Windows.Forms.Padding(2);
            this.Class_Apply.Name = "Class_Apply";
            this.Class_Apply.Size = new System.Drawing.Size(57, 23);
            this.Class_Apply.TabIndex = 42;
            this.Class_Apply.Text = "应用";
            this.Class_Apply.UseVisualStyleBackColor = true;
            this.Class_Apply.Click += new System.EventHandler(this.Class_Apply_Click);
            // 
            // StyleSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 479);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Boundary_label);
            this.Controls.Add(this.Single_bdrWidth);
            this.Controls.Add(this.Style_label);
            this.Controls.Add(this.LabelSetting);
            this.Controls.Add(this.Single_symbol);
            this.Controls.Add(this.Size_label);
            this.Controls.Add(this.Single_size);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StyleSetting";
            this.Text = "符号样式设置";
            this.tabControl1.ResumeLayout(false);
            this.SinglePage.ResumeLayout(false);
            this.SinglePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Single_bdrColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Single_Color)).EndInit();
            this.UniqueValPage.ResumeLayout(false);
            this.UniqueValPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AttributeTable)).EndInit();
            this.ClassBreakPage.ResumeLayout(false);
            this.ClassBreakPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Class_bdrColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Class_ToColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Class_FromColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Single_bdrWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Single_size)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage SinglePage;
        private System.Windows.Forms.TabPage UniqueValPage;
        private System.Windows.Forms.TabPage ClassBreakPage;
        private System.Windows.Forms.PictureBox Single_bdrColor;
        private System.Windows.Forms.Label BoundaryColor_label;
        private System.Windows.Forms.NumericUpDown Single_bdrWidth;
        private System.Windows.Forms.Label Boundary_label;
        private System.Windows.Forms.Button Single_Cancel;
        private System.Windows.Forms.Button Single_OK;
        private System.Windows.Forms.PictureBox Single_Color;
        private System.Windows.Forms.NumericUpDown Single_size;
        private System.Windows.Forms.Label Color_label;
        private System.Windows.Forms.Label Size_label;
        private System.Windows.Forms.ComboBox Single_symbol;
        private System.Windows.Forms.Label Style_label;
        private System.Windows.Forms.Button UniqVal_Cancel;
        private System.Windows.Forms.Button UniqVal_OK;
        private System.Windows.Forms.Button btn_GetUniqueVal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox UniqVal_Field;
        private System.Windows.Forms.PictureBox Class_bdrColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Class_Cancel;
        private System.Windows.Forms.Button Class_OK;
        private System.Windows.Forms.TextBox Class_ToSize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox Class_FromSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox Class_ToColor;
        private System.Windows.Forms.PictureBox Class_FromColor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox ClassNum;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox Class_Field;
        private System.Windows.Forms.ComboBox Class_symbol;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.DataGridView AttributeTable;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button Single_Apply;
        private System.Windows.Forms.Button LabelSetting;
        private System.Windows.Forms.Button Unique_Apply;
        private System.Windows.Forms.Button Class_Apply;
    }
}