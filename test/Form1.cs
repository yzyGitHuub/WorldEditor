using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WEMapObjects;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "ESRI Shapefile(*.shp)|*.shp|所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                //weMapControl1.AddLayer(WEIO.ReadLayer(file));

                //weMapControl1.AllLayer[2] = new WEVectorLayer(2, "d", "d", FeatureType.WEEntityPoint,
                //new WEFeature[1] { new WEEntityPoint(0, new WEMultiPoint(new WEPoint[] { new WEPoint(50, 50) }), new Dictionary<string, object>()) },
                //true, true, WEMapTools.DefaultStyle);
                //weMapControl1.ToPaint.Add(2);
                weMapControl1.ZoomByMBR(weMapControl1.AllLayer[0].MBR);
                //weMapControl1.Refresh();
            }


            WEMultiPoint poi = new WEMultiPoint(new WEPoint[2] { new WEPoint(10, 10), new WEPoint(50, 50) });

            //weMapControl1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            weMapControl1.ZoomByMBR(weMapControl1.AllLayer[0].MBR);
        }
    }
}
