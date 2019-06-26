using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WEMapDemo
{
    public partial class UniqueSymbolSetting : Form
    {
        public WEMapObjects.WEUniqueValueRender newStyle = new WEMapObjects.WEUniqueValueRender();
        public int _mapType;        //1：点图层；2：线图层；3：面图层
        public Dictionary<string, int> value_number = new Dictionary<string, int>();

        public UniqueSymbolSetting()
        {
            InitializeComponent();
        }

        public DataTable _dataTable;
        public UniqueSymbolSetting(int mapType, DataTable dataTable)
        {
            InitializeComponent();
            _dataTable = dataTable;
            _mapType = mapType;
            for(int i=0;i<_dataTable.Columns.Count;i++)
            {
                string thisColumn = _dataTable.Columns[i].ColumnName;
                Field_comboBox.Items.Add(thisColumn);
            }
            Field_comboBox.SelectedIndex = 0;
        }

        private void btn_GetUniqueVal_Click(object sender, EventArgs e)
        {
            AttributeTable.Columns.Add("符号", 200, HorizontalAlignment.Center);
            AttributeTable.Columns.Add("值", 180, HorizontalAlignment.Center);
            AttributeTable.Columns.Add("数量", 120, HorizontalAlignment.Center);
            string field = Field_comboBox.SelectedItem.ToString();
            for(int j=0;j<_dataTable.Rows.Count;j++)    //用字典统计该字段的各个唯一值出现的个数
            {
                string value = _dataTable.Rows[j][field].ToString();
                value_number[value]++;
            }
            foreach(var i in value_number)
            {
                ListViewItem lvi = new ListViewItem();
                Random r = new Random();
                Color randColor = Color.FromArgb(r.Next(100, 255), r.Next(80, 255), r.Next(50, 255));
                lvi.SubItems.Add(" ");
                lvi.SubItems[0].BackColor = randColor;      //第一列填入的内容为空，仅显示颜色              
                lvi.SubItems.Add(i.Key);
                lvi.SubItems.Add(i.Value.ToString());
                AttributeTable.Items.Add(lvi);
                newStyle.Symbols.Add(new WEMapObjects.WEStyle(2, 0, Color.Black, randColor, randColor, 2, 2));
                //生成随机的符号，仅随机改变其中fromcolor、tocolor属性
            }
            
        }

        private void OK_button_Click(object sender, EventArgs e)
        {
            newStyle.Field = Field_comboBox.SelectedItem.ToString();
            newStyle.UniqueValue = value_number.Keys.ToList();
            this.Close();
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
