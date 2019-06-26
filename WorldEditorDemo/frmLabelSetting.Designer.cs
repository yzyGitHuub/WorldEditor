namespace WEMapDemo
{
    partial class frmLabelSetting
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LabelFontText = new System.Windows.Forms.Button();
            this.btnColor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFieldName = new System.Windows.Forms.ComboBox();
            this.Clsbtn = new System.Windows.Forms.Button();
            this.btnLabelFont = new System.Windows.Forms.Button();
            this.OKbtn = new System.Windows.Forms.Button();
            this.LabelText = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LabelFontText);
            this.groupBox1.Controls.Add(this.btnColor);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbFieldName);
            this.groupBox1.Controls.Add(this.Clsbtn);
            this.groupBox1.Controls.Add(this.btnLabelFont);
            this.groupBox1.Controls.Add(this.OKbtn);
            this.groupBox1.Controls.Add(this.LabelText);
            this.groupBox1.Location = new System.Drawing.Point(15, 34);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(324, 218);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // LabelFontText
            // 
            this.LabelFontText.Enabled = false;
            this.LabelFontText.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LabelFontText.Location = new System.Drawing.Point(126, 116);
            this.LabelFontText.Name = "LabelFontText";
            this.LabelFontText.Size = new System.Drawing.Size(121, 23);
            this.LabelFontText.TabIndex = 10;
            this.LabelFontText.Text = "宋体";
            this.LabelFontText.UseVisualStyleBackColor = true;
            // 
            // btnColor
            // 
            this.btnColor.BackColor = System.Drawing.Color.Black;
            this.btnColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnColor.Location = new System.Drawing.Point(126, 71);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(120, 23);
            this.btnColor.TabIndex = 10;
            this.btnColor.UseVisualStyleBackColor = false;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.label3.Location = new System.Drawing.Point(42, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "注记颜色";
            // 
            // cmbFieldName
            // 
            this.cmbFieldName.FormattingEnabled = true;
            this.cmbFieldName.Location = new System.Drawing.Point(126, 26);
            this.cmbFieldName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbFieldName.Name = "cmbFieldName";
            this.cmbFieldName.Size = new System.Drawing.Size(121, 20);
            this.cmbFieldName.TabIndex = 6;
            // 
            // Clsbtn
            // 
            this.Clsbtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Clsbtn.Location = new System.Drawing.Point(240, 179);
            this.Clsbtn.Margin = new System.Windows.Forms.Padding(2);
            this.Clsbtn.Name = "Clsbtn";
            this.Clsbtn.Size = new System.Drawing.Size(72, 26);
            this.Clsbtn.TabIndex = 5;
            this.Clsbtn.Text = "取消";
            this.Clsbtn.UseVisualStyleBackColor = true;
            this.Clsbtn.Click += new System.EventHandler(this.Clsbtn_Click);
            // 
            // btnLabelFont
            // 
            this.btnLabelFont.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLabelFont.Location = new System.Drawing.Point(45, 116);
            this.btnLabelFont.Margin = new System.Windows.Forms.Padding(2);
            this.btnLabelFont.Name = "btnLabelFont";
            this.btnLabelFont.Size = new System.Drawing.Size(53, 26);
            this.btnLabelFont.TabIndex = 4;
            this.btnLabelFont.Text = "字体";
            this.btnLabelFont.UseVisualStyleBackColor = true;
            this.btnLabelFont.Click += new System.EventHandler(this.btnLabelFont_Click);
            // 
            // OKbtn
            // 
            this.OKbtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OKbtn.Location = new System.Drawing.Point(150, 179);
            this.OKbtn.Margin = new System.Windows.Forms.Padding(2);
            this.OKbtn.Name = "OKbtn";
            this.OKbtn.Size = new System.Drawing.Size(78, 26);
            this.OKbtn.TabIndex = 4;
            this.OKbtn.Text = "确定";
            this.OKbtn.UseVisualStyleBackColor = true;
            this.OKbtn.Click += new System.EventHandler(this.OKbtn_Click);
            // 
            // LabelText
            // 
            this.LabelText.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelText.Location = new System.Drawing.Point(24, 26);
            this.LabelText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelText.Name = "LabelText";
            this.LabelText.Size = new System.Drawing.Size(90, 19);
            this.LabelText.TabIndex = 0;
            this.LabelText.Text = "注记名称";
            this.LabelText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmLabelSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 262);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLabelSetting";
            this.Text = "frmLabelSetting";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button LabelFontText;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFieldName;
        private System.Windows.Forms.Button Clsbtn;
        private System.Windows.Forms.Button btnLabelFont;
        private System.Windows.Forms.Button OKbtn;
        private System.Windows.Forms.Label LabelText;
    }
}