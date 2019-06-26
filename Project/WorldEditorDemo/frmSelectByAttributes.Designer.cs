namespace WEMapDemo
{
    partial class frmSelectByAttributes
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
            this.lblLayer = new System.Windows.Forms.Label();
            this.comboBoxLayers = new System.Windows.Forms.ComboBox();
            this.lblField = new System.Windows.Forms.Label();
            this.listBoxFields = new System.Windows.Forms.ListBox();
            this.btnEqual = new System.Windows.Forms.Button();
            this.btnNotEqual = new System.Windows.Forms.Button();
            this.btnMore = new System.Windows.Forms.Button();
            this.btnMoreOrEqual = new System.Windows.Forms.Button();
            this.btnLess = new System.Windows.Forms.Button();
            this.btnLessOrMore = new System.Windows.Forms.Button();
            this.btnBracket = new System.Windows.Forms.Button();
            this.listBoxUniqueValues = new System.Windows.Forms.ListBox();
            this.btnGetUniqueValues = new System.Windows.Forms.Button();
            this.lblSql = new System.Windows.Forms.Label();
            this.textBoxSql = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblLayer
            // 
            this.lblLayer.AutoSize = true;
            this.lblLayer.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLayer.Location = new System.Drawing.Point(30, 15);
            this.lblLayer.Name = "lblLayer";
            this.lblLayer.Size = new System.Drawing.Size(56, 20);
            this.lblLayer.TabIndex = 0;
            this.lblLayer.Text = "Layer :";
            // 
            // comboBoxLayers
            // 
            this.comboBoxLayers.FormattingEnabled = true;
            this.comboBoxLayers.Location = new System.Drawing.Point(106, 15);
            this.comboBoxLayers.Name = "comboBoxLayers";
            this.comboBoxLayers.Size = new System.Drawing.Size(264, 23);
            this.comboBoxLayers.TabIndex = 1;
            this.comboBoxLayers.SelectedIndexChanged += new System.EventHandler(this.comboBoxLayers_SelectedIndexChanged);
            // 
            // lblField
            // 
            this.lblField.AutoSize = true;
            this.lblField.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblField.Location = new System.Drawing.Point(30, 47);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(52, 20);
            this.lblField.TabIndex = 2;
            this.lblField.Text = "Field :";
            // 
            // listBoxFields
            // 
            this.listBoxFields.FormattingEnabled = true;
            this.listBoxFields.ItemHeight = 15;
            this.listBoxFields.Location = new System.Drawing.Point(34, 80);
            this.listBoxFields.Name = "listBoxFields";
            this.listBoxFields.Size = new System.Drawing.Size(336, 94);
            this.listBoxFields.TabIndex = 3;
            this.listBoxFields.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxFields_MouseDoubleClick);
            // 
            // btnEqual
            // 
            this.btnEqual.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEqual.Location = new System.Drawing.Point(34, 196);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(52, 27);
            this.btnEqual.TabIndex = 4;
            this.btnEqual.Text = "=";
            this.btnEqual.UseVisualStyleBackColor = true;
            this.btnEqual.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnOperator_Click);
            // 
            // btnNotEqual
            // 
            this.btnNotEqual.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNotEqual.Location = new System.Drawing.Point(106, 196);
            this.btnNotEqual.Name = "btnNotEqual";
            this.btnNotEqual.Size = new System.Drawing.Size(52, 27);
            this.btnNotEqual.TabIndex = 5;
            this.btnNotEqual.Text = "<>";
            this.btnNotEqual.UseVisualStyleBackColor = true;
            this.btnNotEqual.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnOperator_Click);
            // 
            // btnMore
            // 
            this.btnMore.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMore.Location = new System.Drawing.Point(34, 229);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(52, 27);
            this.btnMore.TabIndex = 6;
            this.btnMore.Text = ">";
            this.btnMore.UseVisualStyleBackColor = true;
            this.btnMore.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnOperator_Click);
            // 
            // btnMoreOrEqual
            // 
            this.btnMoreOrEqual.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMoreOrEqual.Location = new System.Drawing.Point(106, 229);
            this.btnMoreOrEqual.Name = "btnMoreOrEqual";
            this.btnMoreOrEqual.Size = new System.Drawing.Size(52, 27);
            this.btnMoreOrEqual.TabIndex = 7;
            this.btnMoreOrEqual.Text = ">=";
            this.btnMoreOrEqual.UseVisualStyleBackColor = true;
            this.btnMoreOrEqual.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnOperator_Click);
            // 
            // btnLess
            // 
            this.btnLess.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLess.Location = new System.Drawing.Point(34, 262);
            this.btnLess.Name = "btnLess";
            this.btnLess.Size = new System.Drawing.Size(52, 27);
            this.btnLess.TabIndex = 8;
            this.btnLess.Text = "<";
            this.btnLess.UseVisualStyleBackColor = true;
            this.btnLess.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnOperator_Click);
            // 
            // btnLessOrMore
            // 
            this.btnLessOrMore.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLessOrMore.Location = new System.Drawing.Point(106, 262);
            this.btnLessOrMore.Name = "btnLessOrMore";
            this.btnLessOrMore.Size = new System.Drawing.Size(52, 27);
            this.btnLessOrMore.TabIndex = 9;
            this.btnLessOrMore.Text = "<=";
            this.btnLessOrMore.UseVisualStyleBackColor = true;
            this.btnLessOrMore.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnOperator_Click);
            // 
            // btnBracket
            // 
            this.btnBracket.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBracket.Location = new System.Drawing.Point(34, 295);
            this.btnBracket.Name = "btnBracket";
            this.btnBracket.Size = new System.Drawing.Size(124, 27);
            this.btnBracket.TabIndex = 10;
            this.btnBracket.Text = "()";
            this.btnBracket.UseVisualStyleBackColor = true;
            this.btnBracket.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnOperator_Click);
            // 
            // listBoxUniqueValues
            // 
            this.listBoxUniqueValues.FormattingEnabled = true;
            this.listBoxUniqueValues.ItemHeight = 15;
            this.listBoxUniqueValues.Location = new System.Drawing.Point(180, 196);
            this.listBoxUniqueValues.Name = "listBoxUniqueValues";
            this.listBoxUniqueValues.Size = new System.Drawing.Size(190, 94);
            this.listBoxUniqueValues.TabIndex = 11;
            this.listBoxUniqueValues.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxUniqueValues_MouseDoubleClick);
            // 
            // btnGetUniqueValues
            // 
            this.btnGetUniqueValues.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGetUniqueValues.Location = new System.Drawing.Point(180, 296);
            this.btnGetUniqueValues.Name = "btnGetUniqueValues";
            this.btnGetUniqueValues.Size = new System.Drawing.Size(190, 27);
            this.btnGetUniqueValues.TabIndex = 12;
            this.btnGetUniqueValues.Text = "Get Unique Values";
            this.btnGetUniqueValues.UseVisualStyleBackColor = true;
            this.btnGetUniqueValues.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnGetUniqueValues_MouseClick);
            // 
            // lblSql
            // 
            this.lblSql.AutoSize = true;
            this.lblSql.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSql.Location = new System.Drawing.Point(30, 338);
            this.lblSql.Name = "lblSql";
            this.lblSql.Size = new System.Drawing.Size(189, 20);
            this.lblSql.TabIndex = 13;
            this.lblSql.Text = "SELECT * FROM * WHERE";
            // 
            // textBoxSql
            // 
            this.textBoxSql.Location = new System.Drawing.Point(34, 370);
            this.textBoxSql.Multiline = true;
            this.textBoxSql.Name = "textBoxSql";
            this.textBoxSql.Size = new System.Drawing.Size(336, 73);
            this.textBoxSql.TabIndex = 14;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(106, 461);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 30);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnApply.Location = new System.Drawing.Point(199, 461);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 30);
            this.btnApply.TabIndex = 16;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(295, 461);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmSelectByAttributes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 514);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.textBoxSql);
            this.Controls.Add(this.lblSql);
            this.Controls.Add(this.btnGetUniqueValues);
            this.Controls.Add(this.listBoxUniqueValues);
            this.Controls.Add(this.btnBracket);
            this.Controls.Add(this.btnLessOrMore);
            this.Controls.Add(this.btnLess);
            this.Controls.Add(this.btnMoreOrEqual);
            this.Controls.Add(this.btnMore);
            this.Controls.Add(this.btnNotEqual);
            this.Controls.Add(this.btnEqual);
            this.Controls.Add(this.listBoxFields);
            this.Controls.Add(this.lblField);
            this.Controls.Add(this.comboBoxLayers);
            this.Controls.Add(this.lblLayer);
            this.Name = "frmSelectByAttributes";
            this.Text = "Select By Attributes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLayer;
        private System.Windows.Forms.ComboBox comboBoxLayers;
        private System.Windows.Forms.Label lblField;
        private System.Windows.Forms.ListBox listBoxFields;
        private System.Windows.Forms.Button btnEqual;
        private System.Windows.Forms.Button btnNotEqual;
        private System.Windows.Forms.Button btnMore;
        private System.Windows.Forms.Button btnMoreOrEqual;
        private System.Windows.Forms.Button btnLess;
        private System.Windows.Forms.Button btnLessOrMore;
        private System.Windows.Forms.Button btnBracket;
        private System.Windows.Forms.ListBox listBoxUniqueValues;
        private System.Windows.Forms.Button btnGetUniqueValues;
        private System.Windows.Forms.Label lblSql;
        private System.Windows.Forms.TextBox textBoxSql;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
    }
}