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
    public partial class frmData : Form
    {
        public frmData()
        {
            InitializeComponent();
        }

        public DataTable Data = new DataTable();

        public List<int> _SelectedIndexs = new List<int> { };

        public delegate void WEDataSelectedHandle(object sender, int from, int to);
        /// <summary>
        /// 用户选择完毕
        /// </summary>
        public event WEDataSelectedHandle SelectedFinished;


        public void RepairData(WEVectorLayer layer)
        {
            Data = layer.GetDataTable();
            this.dataGridView1.DataSource = Data;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Data.Columns.Add(toolStripButton2.Text.ToString());
            frmMain father = (frmMain)Owner;
            father.weMapControl1.AllLayer[father.weMapControl1.AllLayer.Count() - 1].AddField(toolStripButton2.Text.ToString());
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.ColumnIndex == -1 && e.RowIndex != -1)//选行
            {

                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                _SelectedIndexs.Clear();
                _SelectedIndexs.Add(e.RowIndex);
                //for (int c = 0; c < dataGridView1.ColumnCount; c++)
                    //columnSelected[c] = false;
            }
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == -1 && e.RowIndex != -1)//行
            {
                //_SelectedIndexs.Clear();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                _SelectedIndexs.Add(e.RowIndex);
            }
            if (SelectedFinished != null&& _SelectedIndexs.Count() != 0)
                if(_SelectedIndexs[0] == 0)
                    SelectedFinished(this, 1, _SelectedIndexs[1]);
                else
                    SelectedFinished(this, _SelectedIndexs[0], _SelectedIndexs[1]);

        }
    }
}
