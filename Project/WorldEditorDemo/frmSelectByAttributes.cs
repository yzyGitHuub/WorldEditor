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
    public partial class frmSelectByAttributes : Form
    {
        #region 字段

        private WEMapObjects.WEVectorLayer[] _Layers;
        private string _SQLString;
        private int _SelectLayerIndex;

        #endregion

        #region 构造函数
        public frmSelectByAttributes()
        {
            InitializeComponent();
        }

        public frmSelectByAttributes(WEMapObjects.WEVectorLayer[] layers)
        {
            InitializeComponent();
            //_Map = map;
            _Layers = layers;
            for (int i = 0; i < layers.Length; i++)
            {
                comboBoxLayers.Items.Add(layers[i].LayerName);
            }
            comboBoxLayers.SelectedIndex = 0;
            _SelectLayerIndex = 0;
            /*
            for (int i = 0; i < map.VectorLayers[0].FieldCount; i++)
            {
                listBoxFields.Items.Add(map.VectorLayers[0].)
            }*/
            foreach (KeyValuePair<string, object> kvp in layers[0].Field)
            {
                listBoxFields.Items.Add(kvp.Key);
            }
            lblSql.Text = "SELECT * FROM " + layers[0].LayerName + " WHERE ";
        }

        #endregion

        #region 控件响应函数
        /// <summary>
        /// 选择图层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            _SelectLayerIndex = comboBoxLayers.SelectedIndex;
            lblSql.Text = "SELECT * FROM " + _Layers[_SelectLayerIndex].LayerName + " WHERE ";
            listBoxUniqueValues.Items.Clear();
            listBoxFields.Items.Clear();
            foreach (KeyValuePair<string, object> kvp in _Layers[_SelectLayerIndex].Field)
            {
                listBoxFields.Items.Add(kvp.Key);
            }
        }

        /// <summary>
        /// 选择字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxFields_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string selectedFieldName = listBoxFields.SelectedItem.ToString();
            textBoxSql.Text += selectedFieldName;
        }

        /// <summary>
        /// 选择操作符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperator_Click(object sender, MouseEventArgs e)
        {
            string operatorName = ((Button)sender).Text;
            textBoxSql.Text += " " + operatorName + " ";
        }

        /// <summary>
        /// 获取该图层该字段的所有唯一值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetUniqueValues_MouseClick(object sender, MouseEventArgs e)
        {
            listBoxUniqueValues.Items.Clear();
            /*
            string selectSql = "Select Distinct " + listBoxFields.Items[listBoxFields.SelectedIndex]
                + " from " + _Layers[_SelectLayerIndex].LayerName;
            DataRow[] drs = _Layers[_SelectLayerIndex].GetDataTable()//.Select(selectSql);*/
            string selectedFieldName = listBoxFields.Items[listBoxFields.SelectedIndex].ToString();
            DataView dv = _Layers[_SelectLayerIndex].GetDataTable().DefaultView;
            DataTable dt = dv.ToTable(true, selectedFieldName); // Distinct，抽取唯一值
            foreach (DataRow dr in dt.Rows)
            {
                listBoxUniqueValues.Items.Add(dr.ItemArray[0]);
            }
        }

        /// <summary>
        /// 选择字段唯一值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxUniqueValues_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string selectedUniqueValue = listBoxUniqueValues.SelectedItem.ToString();
            string selectedFieldName = listBoxFields.SelectedItem.ToString();
            if (_Layers[_SelectLayerIndex].Field[selectedFieldName].GetType() == typeof(string))
                textBoxSql.Text += "'" + selectedUniqueValue + "'";
            else
                textBoxSql.Text += selectedUniqueValue;
        }

        /// <summary>
        /// 点击OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                _SQLString = textBoxSql.Text;
                DataRow[] drs = _Layers[_SelectLayerIndex].GetDataTable().Select(_SQLString);
                List<int> ids = new List<int> { };
                foreach (DataRow dr in drs)
                {
                    ids.Add(int.Parse(dr[0].ToString()));
                }
                frmMain father = (frmMain)Owner;
                //father.weMapControl1.SelectedGeometries.Clear();
                //father.weMapControl1.SelectedGeometries = father.weMapControl1.SelectById(ids.ToArray(), _SelectLayerIndex);
                father.weMapControl1.SelectById(ids.ToArray(), _SelectLayerIndex);
                father.weMapControl1.Refresh();

                this.Dispose();
            }
            catch
            {
                MessageBox.Show("SQL语句格式错误，请重新输入！", "WorldEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 点击Apply
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                _SQLString = textBoxSql.Text;
                DataRow[] drs = _Layers[_SelectLayerIndex].GetDataTable().Select(_SQLString);
                List<int> ids = new List<int> { };
                foreach (DataRow dr in drs)
                {
                    ids.Add(int.Parse(dr[0].ToString()));
                }
                frmMain father = (frmMain)Owner;
                //father.weMapControl1.SelectedGeometries.Clear();
                //father.weMapControl1.SelectedGeometries = father.weMapControl1.SelectById(ids.ToArray(), _SelectLayerIndex);
                father.SelectedID.Clear();
                father.SelectedID.AddRange(ids);
                father.weMapControl1.SelectById(ids.ToArray(), _SelectLayerIndex);
                father.weMapControl1.Refresh();
            }
            catch
            {
                //throw new Exception("Error");
                MessageBox.Show("SQL语句格式错误，请重新输入！", "WorldEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 点击Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #endregion
    }
}
