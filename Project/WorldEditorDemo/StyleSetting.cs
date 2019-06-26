using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WEMapObjects;

namespace WEMapDemo
{
    public partial class StyleSetting : Form
    {
        public StyleSetting()
        {
            InitializeComponent();
        }

        private List<Color> ColorTable = new List<Color>{Color.Tomato,Color.LightCoral,Color.LavenderBlush,
        Color.LightSalmon,Color.AntiqueWhite,Color.LemonChiffon,Color.LightSalmon,Color.LightGreen,
        Color.LightSkyBlue,Color.Lavender,Color.PaleGreen,Color.LightCyan};     //常用颜色表

        public WEStyle SetStyle = new WEMapObjects.WEStyle();     // 输出结果用的
        public WELabel SetLabel = new WELabel("FID", new Font("宋体", 5), Color.Black);

        private int _mapType;           //1：点图层；2：线图层；3：面图层
        private frmMain _parentForm;    //父窗体
        private int _styleLayerNum;     //需要设置样式的图层编号
        private DataTable _LayerData;   //该图层的属性表
        private Dictionary<string, int> value_number = new Dictionary<string, int>();   //记录选中字段的唯一值及对应的个数

        public ComboBox.ObjectCollection Items = new ComboBox.ObjectCollection(new ComboBox());

        public StyleSetting(int type, int layerNum, frmMain parentForm)       //重载构造函数
        {
            InitializeComponent();
            _mapType = type;
            _parentForm = parentForm;
            _styleLayerNum = layerNum;
            _LayerData = _parentForm.AllLayer[_styleLayerNum].GetDataTable();
            for (int i = 0; i < _LayerData.Columns.Count; i++)
            {
                string thisColumn = _LayerData.Columns[i].ColumnName;
                UniqVal_Field.Items.Add(thisColumn);
                Class_Field.Items.Add(thisColumn);
                Items.Add(thisColumn);
            }
            if (UniqVal_Field.Items.Count > 0)
                UniqVal_Field.SelectedIndex = 0;
            if (Class_Field.Items.Count > 0)
                Class_Field.SelectedIndex = 0;
            else
            {
                Class_OK.Enabled = false;
            }
            if (_mapType == 1)      //点要素图层
            {
                Single_symbol.Items.Add("  ○  ");
                Single_symbol.Items.Add("  ●  ");
                Single_symbol.Items.Add("  □  ");
                Single_symbol.Items.Add("  ■  ");
                Single_symbol.Items.Add("  △  ");
                Single_symbol.Items.Add("  ▲  ");
                Single_symbol.Items.Add("  ⊙  ");
                Single_symbol.Items.Add("  ◎  ");
                Single_symbol.SelectedIndex = 0;
                Class_symbol.Items.Add("  ○  ");
                Class_symbol.Items.Add("  ●  ");
                Class_symbol.Items.Add("  □  ");
                Class_symbol.Items.Add("  ■  ");
                Class_symbol.Items.Add("  △  ");
                Class_symbol.Items.Add("  ▲  ");
                Class_symbol.Items.Add("  ⊙  ");
                Class_symbol.Items.Add("  ◎  ");
                Class_symbol.SelectedIndex = 0;
            }

            if (_mapType == 2)      //线要素图层
            {
                Single_symbol.Items.Add("  —  ");
                Single_symbol.Items.Add("- - - -");
                Single_symbol.SelectedIndex = 0;
                Class_symbol.Items.Add("  —  ");
                Class_symbol.Items.Add("- - - -");
                Class_symbol.SelectedIndex = 0;
                Single_bdrColor.Enabled = false;
                Single_bdrWidth.Enabled = false;
                Boundary_label.Enabled = false;
                BoundaryColor_label.Enabled = false;
                Class_bdrColor.Enabled = false;
                //Class_bdrWidth.Enabled = false;
                label2.Enabled = false;
                //label3.Enabled = false;
            }

            if (_mapType == 3)      //面要素图层
            {
                Single_symbol.Enabled = false;
                Single_size.Enabled = false;
                Style_label.Enabled = false;
                Size_label.Enabled = false;
                Class_symbol.Enabled = false;
                Class_FromSize.Enabled = false;
                Class_ToSize.Enabled = false;
                label5.Enabled = false;
                label10.Enabled = false;
            }
        }

        #region 单一符号渲染
        private void Single_Color_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                    Single_Color.BackColor = colorDialog1.Color;
            }
        }

        private void Single_bdrColor_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                    Single_bdrColor.BackColor = colorDialog1.Color;
            }
        }

        private void Single_OK_Click(object sender, EventArgs e)
        {
            WEMapObjects.WEStyle newStyle = new WEMapObjects.WEStyle();
            newStyle.SymbolMethod = 1;
            if (_mapType == 1)
            {
                newStyle.SymbolStyle = Single_symbol.SelectedIndex + 1; //符号
                newStyle.Size = Convert.ToDouble(Single_size.Value);    //尺寸
                newStyle.FromColor = newStyle.ToColor = Single_Color.BackColor;     //颜色
                newStyle.BoundaryWidth = Convert.ToDouble(Single_bdrWidth.Value);   //边界宽度
                newStyle.BoundaryColor = Single_bdrColor.BackColor;                 //边界颜色
            }
            if (_mapType == 2)
            {
                newStyle.SymbolStyle = Single_symbol.SelectedIndex; //符号
                newStyle.Size = Convert.ToDouble(Single_size.Value);    //尺寸
                newStyle.FromColor = newStyle.ToColor = Single_Color.BackColor; //颜色
            }
            if (_mapType == 3)
            {
                newStyle.FromColor = newStyle.ToColor = Single_Color.BackColor;     //颜色
                newStyle.BoundaryWidth = Convert.ToDouble(Single_bdrWidth.Value);   //边界宽度
                newStyle.BoundaryColor = Single_bdrColor.BackColor;                 //边界颜色
            }
            newStyle.LabelVisible = checkBox1.Checked;
            DialogResult = DialogResult.OK;
            SetStyle = newStyle;
            _parentForm.AllLayer[_styleLayerNum].Label = SetLabel;
            //_parentForm.AllLayer[_styleLayerNum].SymbolStyle = newStyle;
            this.Close();
        }

        private void Single_Apply_Click(object sender, EventArgs e)
        {
            WEMapObjects.WEStyle newStyle = new WEMapObjects.WEStyle();
            newStyle.SymbolMethod = 1;
            if (_mapType == 1)
            {
                newStyle.SymbolStyle = Single_symbol.SelectedIndex + 1; //符号
                newStyle.Size = Convert.ToDouble(Single_size.Value);    //尺寸
                newStyle.FromColor = newStyle.ToColor = Single_Color.BackColor;     //颜色
                newStyle.BoundaryWidth = Convert.ToDouble(Single_bdrWidth.Value);   //边界宽度
                newStyle.BoundaryColor = Single_bdrColor.BackColor;                 //边界颜色
            }
            if (_mapType == 2)
            {
                newStyle.SymbolStyle = Single_symbol.SelectedIndex; //符号
                newStyle.Size = Convert.ToDouble(Single_size.Value);    //尺寸
                newStyle.FromColor = newStyle.ToColor = Single_Color.BackColor; //颜色
            }
            if (_mapType == 3)
            {
                newStyle.FromColor = newStyle.ToColor = Single_Color.BackColor;     //颜色
                newStyle.BoundaryWidth = Convert.ToDouble(Single_bdrWidth.Value);   //边界宽度
                newStyle.BoundaryColor = Single_bdrColor.BackColor;                 //边界颜色
            }
            newStyle.LabelVisible = checkBox1.Checked;
            //DialogResult = DialogResult.OK;
            SetStyle = newStyle;
            _parentForm.AllLayer[_styleLayerNum].SymbolStyle = SetStyle;
            _parentForm.AllLayer[_styleLayerNum].Label = SetLabel;
            _parentForm.weMapControl1.AllLayer = _parentForm.AllLayer;
            _parentForm.weMapControl1.Refresh();

            //_parentForm.AllLayer[]
        }

        private void Single_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion        

        #region 唯一值渲染
        private void btn_GetUniqueVal_Click(object sender, EventArgs e)
        {
            AttributeTable.Rows.Clear();
            AttributeTable.ColumnCount = 3;
            AttributeTable.ColumnHeadersVisible = true;
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 9, FontStyle.Bold);
            AttributeTable.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            AttributeTable.Columns[0].Name = "符号";
            AttributeTable.Columns[1].Name = "值";
            AttributeTable.Columns[2].Name = "数目";
            AttributeTable.RowsDefaultCellStyle.Font = new Font("宋体", 9);
            for (int k = 0; k < value_number.Count; k++)
            {
                int index = AttributeTable.Rows.Add();
                Random r = new Random();
                int colorIndex = r.Next(0, ColorTable.Count);
                colorIndex = (colorIndex * k * k) % ColorTable.Count;
                Color cellColor = ColorTable[colorIndex];
                AttributeTable.Rows[index].Cells[0].Style.BackColor = cellColor;
                AttributeTable.Rows[index].Cells[1].Value = value_number.Keys.ElementAt(k);
                AttributeTable.Rows[index].Cells[2].Value = value_number[value_number.Keys.ElementAt(k)];
            }
        }

        private void UniqVal_Field_SelectedIndexChanged(object sender, EventArgs e)
        {
            string field = UniqVal_Field.SelectedItem.ToString();
            value_number.Clear();
            for (int j = 0; j < _LayerData.Rows.Count; j++)    //用字典统计该字段的各个唯一值出现的个数
            {
                string value = _LayerData.Rows[j][field].ToString();
                if (value_number.Keys.Contains(value))
                    value_number[value]++;
                else
                    value_number.Add(value, 1);
            }
        }

        private void UniqVal_OK_Click(object sender, EventArgs e)
        {
            WEMapObjects.WEUniqueValueRender newStyle = new WEMapObjects.WEUniqueValueRender();
            newStyle.SymbolMethod = 2;
            newStyle.Field = UniqVal_Field.SelectedItem.ToString();
            List<string> UniqVals = new List<string>();
            List<WEMapObjects.WEStyle> styles = new List<WEMapObjects.WEStyle>();

            if (_mapType == 1)
            {
                newStyle.SymbolStyle = Single_symbol.SelectedIndex + 1; //符号
                newStyle.Size = Convert.ToDouble(Single_size.Value);    //尺寸
                newStyle.FromColor = newStyle.ToColor = Single_Color.BackColor;     //颜色
                newStyle.BoundaryWidth = Convert.ToDouble(Single_bdrWidth.Value);   //边界宽度
                newStyle.BoundaryColor = Single_bdrColor.BackColor;                 //边界颜色
            }
            else if (_mapType == 2)
            {
                newStyle.SymbolStyle = Single_symbol.SelectedIndex; //符号
                newStyle.Size = Convert.ToDouble(Single_size.Value);    //尺寸
                newStyle.FromColor = newStyle.ToColor = Single_Color.BackColor; //颜色
            }
            else if (_mapType == 3)
            {
                newStyle.FromColor = newStyle.ToColor = Single_Color.BackColor;     //颜色
                newStyle.BoundaryWidth = Convert.ToDouble(Single_bdrWidth.Value);   //边界宽度
                newStyle.BoundaryColor = Single_bdrColor.BackColor;                 //边界颜色
            }

            for (int i = 0; i < AttributeTable.RowCount - 1; i++)
            {
                UniqVals.Add(AttributeTable.Rows[i].Cells[1].Value.ToString());
                WEMapObjects.WEStyle s = new WEMapObjects.WEStyle();
                s.FromColor = s.ToColor = AttributeTable.Rows[i].Cells[0].Style.BackColor;  //仅设置了样式的颜色
                s.Size = newStyle.Size;
                s.SymbolStyle = newStyle.SymbolStyle;
                s.BoundaryColor = newStyle.BoundaryColor;
                s.BoundaryWidth = newStyle.BoundaryWidth;
                styles.Add(s);
            }
            newStyle.UniqueValue = UniqVals;
            newStyle.Symbols = styles;
            newStyle.LabelVisible = checkBox1.Checked;
            DialogResult = DialogResult.OK;
            SetStyle = newStyle;
            this.Close();
        }

        private void Unique_Apply_Click(object sender, EventArgs e)
        {
            WEMapObjects.WEUniqueValueRender newStyle = new WEMapObjects.WEUniqueValueRender();
            newStyle.SymbolMethod = 2;
            newStyle.Field = UniqVal_Field.SelectedItem.ToString();
            List<string> UniqVals = new List<string>();
            List<WEMapObjects.WEStyle> styles = new List<WEMapObjects.WEStyle>();

            if (_mapType == 1)
            {
                newStyle.SymbolStyle = Single_symbol.SelectedIndex + 1; //符号
                newStyle.Size = Convert.ToDouble(Single_size.Value);    //尺寸
                newStyle.FromColor = newStyle.ToColor = Single_Color.BackColor;     //颜色
                newStyle.BoundaryWidth = Convert.ToDouble(Single_bdrWidth.Value);   //边界宽度
                newStyle.BoundaryColor = Single_bdrColor.BackColor;                 //边界颜色
            }
            else if (_mapType == 2)
            {
                newStyle.SymbolStyle = Single_symbol.SelectedIndex; //符号
                newStyle.Size = Convert.ToDouble(Single_size.Value);    //尺寸
                newStyle.FromColor = newStyle.ToColor = Single_Color.BackColor; //颜色
            }
            else if (_mapType == 3)
            {
                newStyle.FromColor = newStyle.ToColor = Single_Color.BackColor;     //颜色
                newStyle.BoundaryWidth = Convert.ToDouble(Single_bdrWidth.Value);   //边界宽度
                newStyle.BoundaryColor = Single_bdrColor.BackColor;                 //边界颜色
            }

            for (int i = 0; i < AttributeTable.RowCount - 1; i++)
            {
                UniqVals.Add(AttributeTable.Rows[i].Cells[1].Value.ToString());
                WEMapObjects.WEStyle s = new WEMapObjects.WEStyle();
                s.FromColor = s.ToColor = AttributeTable.Rows[i].Cells[0].Style.BackColor;  //仅设置了样式的颜色
                s.Size = newStyle.Size;
                s.SymbolStyle = newStyle.SymbolStyle;
                s.BoundaryColor = newStyle.BoundaryColor;
                s.BoundaryWidth = newStyle.BoundaryWidth;
                styles.Add(s);
            }
            newStyle.UniqueValue = UniqVals;
            newStyle.Symbols = styles;
            newStyle.LabelVisible = checkBox1.Checked;
            //DialogResult = DialogResult.OK;
            SetStyle = newStyle;
            _parentForm.AllLayer[_styleLayerNum].SymbolStyle = SetStyle;
            _parentForm.AllLayer[_styleLayerNum].Label = SetLabel;
            _parentForm.weMapControl1.AllLayer = _parentForm.AllLayer;
            _parentForm.weMapControl1.Refresh();

            //_parentForm.AllLayer[]
        }

        private void UniqVal_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 分级渲染
        private void Class_FromColor_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                    Class_FromColor.BackColor = colorDialog1.Color;
            }
        }

        private void Class_ToColor_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                    Class_ToColor.BackColor = colorDialog1.Color;
            }
        }

        private void Class_bdrColor_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                    Class_bdrColor.BackColor = colorDialog1.Color;
            }
        }

        private void Class_Field_SelectedIndexChanged(object sender, EventArgs e)
        {
            string field = Class_Field.SelectedItem.ToString();
            ClassNum.Items.Clear();
            value_number.Clear();
            for (int j = 0; j < _LayerData.Rows.Count; j++)    //用字典统计该字段的各个唯一值出现的个数
            {
                string value = _LayerData.Rows[j][field].ToString();
                if (value_number.Keys.Contains(value))
                    value_number[value]++;
                else
                    value_number.Add(value, 1);
            }
            for (int k = 0; k < value_number.Count; k++)
            {
                ClassNum.Items.Add(k + 1);
            }
        }

        private void Class_OK_Click(object sender, EventArgs e)
        {
            WEMapObjects.WEClassBreaksRender newStyle = new WEMapObjects.WEClassBreaksRender();
            newStyle.SymbolMethod = 3;
            newStyle.Field = Class_Field.SelectedItem.ToString();
            newStyle.BreakCount = Convert.ToInt32(ClassNum.SelectedItem);
            if (_mapType == 1)
            {
                newStyle.SymbolStyle = Class_symbol.SelectedIndex + 1;
                newStyle.FromColor = Class_FromColor.BackColor;     //起始颜色
                newStyle.ToColor = Class_ToColor.BackColor;         //终止颜色
                newStyle.BoundaryWidth = Convert.ToDouble(Single_bdrWidth.Value);    //边界线宽
                newStyle.BoundaryColor = Class_bdrColor.BackColor;                  //边界颜色
                newStyle.FromSize = Convert.ToDouble(Class_FromSize);   //起始尺寸
                newStyle.ToSize = Convert.ToDouble(Class_ToSize);       //终止尺寸                
            }
            if (_mapType == 2)
            {
                newStyle.SymbolStyle = Class_symbol.SelectedIndex;
                newStyle.FromColor = Class_FromColor.BackColor;     //起始颜色
                newStyle.ToColor = Class_ToColor.BackColor;         //终止颜色
                newStyle.FromSize = Convert.ToDouble(Class_FromSize);   //起始尺寸
                newStyle.ToSize = Convert.ToDouble(Class_ToSize);       //终止尺寸
            }
            if (_mapType == 3)
            {
                newStyle.FromColor = Class_FromColor.BackColor;     //起始颜色
                newStyle.ToColor = Class_ToColor.BackColor;         //终止颜色
                newStyle.BoundaryWidth = Convert.ToDouble(Single_bdrWidth.Value);    //边界线宽
                newStyle.BoundaryColor = Class_bdrColor.BackColor;                  //边界颜色
            }
            newStyle.LabelVisible = checkBox1.Checked;
            _parentForm.AllLayer[_styleLayerNum].SymbolStyle = newStyle;
            DialogResult = DialogResult.OK;
            SetStyle = newStyle;
            this.Close();
        }

        private void Class_Apply_Click(object sender, EventArgs e)
        {
            WEMapObjects.WEClassBreaksRender newStyle = new WEMapObjects.WEClassBreaksRender();
            newStyle.SymbolMethod = 3;
            newStyle.Field = Class_Field.SelectedItem.ToString();
            newStyle.BreakCount = Convert.ToInt32(ClassNum.SelectedItem);
            if (_mapType == 1)
            {
                newStyle.SymbolStyle = Class_symbol.SelectedIndex + 1;
                newStyle.FromColor = Class_FromColor.BackColor;     //起始颜色
                newStyle.ToColor = Class_ToColor.BackColor;         //终止颜色
                newStyle.BoundaryWidth = Convert.ToDouble(Single_bdrWidth.Value);    //边界线宽
                newStyle.BoundaryColor = Class_bdrColor.BackColor;                  //边界颜色
                newStyle.FromSize = Convert.ToDouble(Class_FromSize);   //起始尺寸
                newStyle.ToSize = Convert.ToDouble(Class_ToSize);       //终止尺寸                
            }
            if (_mapType == 2)
            {
                newStyle.SymbolStyle = Class_symbol.SelectedIndex;
                newStyle.FromColor = Class_FromColor.BackColor;     //起始颜色
                newStyle.ToColor = Class_ToColor.BackColor;         //终止颜色
                newStyle.FromSize = Convert.ToDouble(Class_FromSize);   //起始尺寸
                newStyle.ToSize = Convert.ToDouble(Class_ToSize);       //终止尺寸
            }
            if (_mapType == 3)
            {
                newStyle.FromColor = Class_FromColor.BackColor;     //起始颜色
                newStyle.ToColor = Class_ToColor.BackColor;         //终止颜色
                newStyle.BoundaryWidth = Convert.ToDouble(Single_bdrWidth.Value);    //边界线宽
                newStyle.BoundaryColor = Class_bdrColor.BackColor;                  //边界颜色
            }
            newStyle.LabelVisible = checkBox1.Checked;
            //DialogResult = DialogResult.OK;
            SetStyle = newStyle;
            _parentForm.AllLayer[_styleLayerNum].SymbolStyle = SetStyle;
            _parentForm.AllLayer[_styleLayerNum].Label = SetLabel;
            _parentForm.weMapControl1.AllLayer = _parentForm.AllLayer;
            _parentForm.weMapControl1.Refresh();

            //_parentForm.AllLayer[]
        }

        private void Class_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void AttributeTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (AttributeTable.Rows[e.RowIndex].Cells[0].Style.BackColor != Color.White)
                {
                    ColorDialog sDialog = new ColorDialog();
                    sDialog.Color = AttributeTable.Rows[e.RowIndex].Cells[0].Style.BackColor;
                    if (sDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        AttributeTable.Rows[e.RowIndex].Cells[0].Style.BackColor = sDialog.Color;
                    }
                    sDialog.Dispose();
                }
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 2)
                Single_size.Enabled = false;
            else
                Single_size.Enabled = true;
        }



        private void LabelSetting_Click(object sender, EventArgs e)
        {
            frmLabelSetting labelSetting1 = new frmLabelSetting(_parentForm.AllLayer[_styleLayerNum].Label.Font, this);
            labelSetting1.ShowDialog();

            if(labelSetting1.DialogResult == DialogResult.OK)
            {
                SetLabel.Font = labelSetting1.LabelFont;
                SetLabel.Color = labelSetting1.LabelColor;
                SetLabel.Text = labelSetting1.LabelField;
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            LabelSetting.Enabled = checkBox1.Checked;
        }

    }
}
