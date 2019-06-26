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
    public partial class frmNewLayer : Form
    {
        #region 构造函数
        public frmNewLayer()
        {
            InitializeComponent();
            cmbType.Items.Add("Point");
            cmbType.Items.Add("Polyline");
            cmbType.Items.Add("Polygon");
            this.cmbType.SelectedIndex = 0;
        }
        #endregion

        #region 字段
        public WEVectorLayer _NewLayer;
        #endregion

        #region 属性

        #endregion
        private void OKbtn_Click(object sender, EventArgs e)
        {
            if (txtName.Text == string.Empty)
                _NewLayer = new WEPointLayer();
            else
                switch(cmbType.SelectedIndex)
                {
                    case 0:
                        _NewLayer = new WEPointLayer();
                        break;
                    case 1:
                        _NewLayer = new WEPolylineLayer();
                        break;
                    case 2:
                        _NewLayer = new WEPolygonLayer();
                        break;
                }
            if (txtName.Text.ToString().Length != 0)
                _NewLayer.LayerName = txtName.Text.ToString();
            else
                _NewLayer.LayerName = "default";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancelbtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
