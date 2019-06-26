namespace WEMapDemo
{
    partial class frmIdentify
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
            this.lblLayers = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.lblAttributes = new System.Windows.Forms.Label();
            this.dataGridViewAttr = new System.Windows.Forms.DataGridView();
            this.lblFeatures = new System.Windows.Forms.Label();
            this.textBoxLayers = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMBR = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttr)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLayers
            // 
            this.lblLayers.AutoSize = true;
            this.lblLayers.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLayers.Location = new System.Drawing.Point(44, 14);
            this.lblLayers.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLayers.Name = "lblLayers";
            this.lblLayers.Size = new System.Drawing.Size(44, 17);
            this.lblLayers.TabIndex = 0;
            this.lblLayers.Text = "图层：";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLocation.Location = new System.Drawing.Point(8, 52);
            this.lblLocation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(80, 17);
            this.lblLocation.TabIndex = 2;
            this.lblLocation.Text = "中心经纬度：";
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.Location = new System.Drawing.Point(92, 48);
            this.textBoxLocation.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.ReadOnly = true;
            this.textBoxLocation.Size = new System.Drawing.Size(176, 21);
            this.textBoxLocation.TabIndex = 3;
            // 
            // lblAttributes
            // 
            this.lblAttributes.AutoSize = true;
            this.lblAttributes.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAttributes.Location = new System.Drawing.Point(24, 132);
            this.lblAttributes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAttributes.Name = "lblAttributes";
            this.lblAttributes.Size = new System.Drawing.Size(71, 17);
            this.lblAttributes.TabIndex = 4;
            this.lblAttributes.Text = "Attributes :";
            // 
            // dataGridViewAttr
            // 
            this.dataGridViewAttr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAttr.Location = new System.Drawing.Point(27, 160);
            this.dataGridViewAttr.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewAttr.Name = "dataGridViewAttr";
            this.dataGridViewAttr.ReadOnly = true;
            this.dataGridViewAttr.RowTemplate.Height = 27;
            this.dataGridViewAttr.Size = new System.Drawing.Size(239, 152);
            this.dataGridViewAttr.TabIndex = 5;
            // 
            // lblFeatures
            // 
            this.lblFeatures.AutoSize = true;
            this.lblFeatures.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFeatures.Location = new System.Drawing.Point(27, 322);
            this.lblFeatures.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFeatures.Name = "lblFeatures";
            this.lblFeatures.Size = new System.Drawing.Size(135, 17);
            this.lblFeatures.TabIndex = 6;
            this.lblFeatures.Text = "No identified features";
            // 
            // textBoxLayers
            // 
            this.textBoxLayers.Location = new System.Drawing.Point(92, 14);
            this.textBoxLayers.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxLayers.Name = "textBoxLayers";
            this.textBoxLayers.ReadOnly = true;
            this.textBoxLayers.Size = new System.Drawing.Size(176, 21);
            this.textBoxLayers.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(24, 82);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "外包矩形：";
            // 
            // textBoxMBR
            // 
            this.textBoxMBR.Location = new System.Drawing.Point(92, 82);
            this.textBoxMBR.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxMBR.Multiline = true;
            this.textBoxMBR.Name = "textBoxMBR";
            this.textBoxMBR.ReadOnly = true;
            this.textBoxMBR.Size = new System.Drawing.Size(176, 48);
            this.textBoxMBR.TabIndex = 3;
            // 
            // frmIdentify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 350);
            this.Controls.Add(this.lblFeatures);
            this.Controls.Add(this.dataGridViewAttr);
            this.Controls.Add(this.lblAttributes);
            this.Controls.Add(this.textBoxLayers);
            this.Controls.Add(this.textBoxMBR);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxLocation);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.lblLayers);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmIdentify";
            this.Text = "Identify";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLayers;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.Label lblAttributes;
        private System.Windows.Forms.DataGridView dataGridViewAttr;
        private System.Windows.Forms.Label lblFeatures;
        private System.Windows.Forms.TextBox textBoxLayers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMBR;
    }
}