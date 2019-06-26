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
    public partial class frmIdentify : Form
    {
        #region 字段

        #endregion

        #region 构造函数
        public frmIdentify()
        {
            InitializeComponent();
        }

        public frmIdentify(string layerName, WEMapObjects.WEFeature feature)
        {
            InitializeComponent();
            //_Map = map;
            textBoxLayers.Text = layerName;
            textBoxLocation.Text = feature.Geometries.getCenterPoint()[0].ToString();
            textBoxMBR.Text = "MinX, MaxX, MinY, MaxY\n" + feature.MBR.ToString();
            dataGridViewAttr.Columns.Clear();
            dataGridViewAttr.Rows.Clear();
            dataGridViewAttr.Columns.Add("KEY", "字段");
            dataGridViewAttr.Columns.Add("VALUE", "值");
            dataGridViewAttr.Rows.Add("FID", feature.ID);
            foreach (var i in feature.Attributes)
            {
                dataGridViewAttr.Rows.Add(i.Key.ToString(), i.Value.ToString());
            }

        }

        #endregion


        #region 属性


        #endregion

    }
}
