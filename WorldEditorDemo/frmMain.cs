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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            IsMdiContainer = true;

            //weMapControl1.AllLayer.Add(new WEVectorLayer(1, "zhibei", "", FeatureType.WEEntityPolyline, new WEFeature[10], true, true, new WEStyle()));

            treeView1.ContextMenuStrip = contextMenuStrip1;


        }

        /// <summary>
        /// 全部图层
        /// </summary>
        public List<WEVectorLayer> AllLayer = new List<WEVectorLayer> { };

        public int StyleLayerNum = -1;

        public List<int> SelectedID = new List<int>();
        public frmIdentify frmIdentify1 = new frmIdentify();


        #region 窗体或控件事件处理

        //初始化窗体
        private void frmMain_Load(object sender, EventArgs e)
        {

        }




        #endregion

        #region 私有函数


        #endregion

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "ESRI Shapefile(*.shp)|*.shp|所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                WEVectorLayer newLayer = WEIO.ReadLayer(file);
                newLayer.InitializeData();
                newLayer.SelectDisplay();

                AllLayer.Add(newLayer);
                weMapControl1.AllLayer = AllLayer;

                treeView1.Nodes.Clear();
                foreach (var layer in AllLayer)
                {
                    TreeNode node0_0 = new TreeNode();
                    node0_0.Text = layer.LayerName;
                    node0_0.Checked = layer.Visible;
                    treeView1.Nodes.Add(node0_0);
                }
                               
                if(AllLayer.Count() != 0)
                    weMapControl1.FullExitence();

            }
        }

        private void weMapControl1_WEMouseMove(object sender, Point Location)
        {
            WEPoint location = WEMapTools.ToMapPoint(Location);
            tss2.Text = "X: " + location.X.ToString("0.0000") + ", Y: " + location.Y.ToString("0.0000");
            statusStrip1.Refresh();
        }



        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                int seleceted = e.Node.Nodes.Count;
                StyleLayerNum = seleceted;
                for (int i = 0; i < weMapControl1.AllLayer.Count(); i++)
                {
                    weMapControl1.AllLayer[i].Visible = treeView1.Nodes[i].Checked;
                }
                weMapControl1.Refresh();
            }
        }

        private void 选择样式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int mapType = 0;
            FeatureType layerType = weMapControl1.AllLayer[StyleLayerNum].FeatureType;
            if (layerType == FeatureType.WEEntityPoint || layerType == FeatureType.WEMultiPoint ||
                layerType == FeatureType.WEPoint)
                mapType = 1;
            else if (layerType == FeatureType.WEEntityPolyline || layerType == FeatureType.WEMultiPolyline ||
                layerType == FeatureType.WEPolyline)
                mapType = 2;
            else if (layerType == FeatureType.WEEntityPolygon || layerType == FeatureType.WEMultiPolygon ||
                layerType == FeatureType.WEPolygon)
                mapType = 3;
            StyleSetting styleSettingForm = new StyleSetting(mapType, StyleLayerNum, this);
            styleSettingForm.ShowDialog();

            if (styleSettingForm.DialogResult == DialogResult.OK)
            {
                AllLayer[StyleLayerNum].SymbolStyle = styleSettingForm.SetStyle;
                AllLayer[StyleLayerNum].Label = styleSettingForm.SetLabel;
                weMapControl1.AllLayer = AllLayer;
                //weMapControl1.Refresh();
            }

            weMapControl1.Refresh();

        }



        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "图片文件(*.bmp)|*.bmp";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                Bitmap output = new Bitmap(Width, Height);
                weMapControl1.DrawToBitmap(output, new Rectangle(0, 0, weMapControl1.Width, weMapControl1.Height));
                output.Save(saveFileDialog.FileName);
            }
        }

        private void zoomToLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            weMapControl1.FullExitence();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {

        }

        private void weMapControl1_DisplayScaleChanged(object sender)
        {
            toolStripTextBox1.Text = weMapControl1.DisplayScale.ToString("0.00000");
        }



        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            TreeView treev = sender as TreeView;
            Point point = treev.PointToClient(Cursor.Position);
            TreeViewHitTestInfo info = treev.HitTest(point.X, point.Y);
            TreeNode node = info.Node;//获得 右键点击的节点

            if (node != null)
                StyleLayerNum = node.Index;

            if (node != null && e.Button == MouseButtons.Right)//判断你点的是不是右键
            {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode CurrentNode = treeView1.GetNodeAt(ClickPoint);
                if (CurrentNode != null)//判断你点的是不是一个节点
                {
                    CurrentNode.ContextMenuStrip = contextMenuStrip1;
                    treeView1.SelectedNode = CurrentNode;//选中这个节点
                }
                
            }
            else
            {
                contextMenuStrip1.Visible = false;
                
            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            frmData wind = new frmData();
            wind.SelectedFinished += ExcuteSelected;
            wind.RepairData(weMapControl1.AllLayer.Last());
            wind.Owner = this;
            wind.Show();
        }

        private void ExcuteSelected(object sender, int from, int to)
        {
            to = Math.Max(to, 1);
            from = Math.Max(from, 1);
            if(to > from)
            {
                int i = to;
                to = from;
                from = i;
            }
            List<int> demo = new List<int> { };
            for (int i = to; i <= from; i++)
                demo.Add(i - 1);
            weMapControl1.SelectById(demo.ToArray(), StyleLayerNum);
            weMapControl1.Refresh();

        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            weMapControl1.ZoomIn();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            weMapControl1.ZoomOut();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            weMapControl1.Pan();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            weMapControl1.SetCurrentEdit = Convert.ToInt32(toolStripButton19.SelectedIndex);
            weMapControl1.TrackPolygon();
            
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            weMapControl1.SelectFeature();
        }

        private void toolStrip1_TextChanged(object sender, EventArgs e)
        {
            weMapControl1.DisplayScale = Convert.ToDouble(toolStrip1.Text.ToString());
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            weMapControl1.TrackPolygon();
            weMapControl1.NodeEdit = -1;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (AllLayer.Count == 1)
                AllLayer.Add(new WEVectorLayer());
            treeView1.Nodes.Clear();
            foreach (var layer in AllLayer)
            {
                TreeNode node0_0 = new TreeNode();
                node0_0.Text = layer.LayerName;
                node0_0.Checked = layer.Visible;
                treeView1.Nodes.Add(node0_0);
            }
            weMapControl1.AllLayer.RemoveAt(0);
            weMapControl1.Refresh();
            //weMapControl1_LayersChange(sender);
        }

        private void treeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            AllLayer[e.Node.Index].Visible = !e.Node.Checked;
            weMapControl1.AllLayer = AllLayer;
            weMapControl1.Refresh();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            frmNewLayer frmNewLayer1 = new frmNewLayer();
            frmNewLayer1.ShowDialog();

            if(frmNewLayer1.DialogResult == DialogResult.OK)
            {
                frmNewLayer1._NewLayer.ID = AllLayer.Count();
                AllLayer.Add(frmNewLayer1._NewLayer);
                weMapControl1.AllLayer = AllLayer;
                weMapControl1.Refresh();
                treeView1.Nodes.Clear();
                foreach (var layer in AllLayer)
                {
                    TreeNode node0_0 = new TreeNode();
                    node0_0.Text = layer.LayerName;
                    node0_0.Checked = layer.Visible;
                    treeView1.Nodes.Add(node0_0);
                }
            }
            //AllLayer.Add(new WEVectorLayer(, "default", "default", FeatureType.WEEntityPoint, new WEFeature[0], true, true, WEMapTools.DefaultStyle));

        }

        private void weMapControl1_LayersChange(object sender, List<WEVectorLayer> wEVectorLayers)
        {
            AllLayer = wEVectorLayers;
        }



        private void 删除图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllLayer.RemoveAt(StyleLayerNum);
            weMapControl1.AllLayer = AllLayer;
            weMapControl1.Refresh();

            treeView1.Nodes.Clear();
            foreach (var layer in AllLayer)
            {
                TreeNode node0_0 = new TreeNode();
                node0_0.Text = layer.LayerName;
                node0_0.Checked = layer.Visible;
                treeView1.Nodes.Add(node0_0);
            }
        }

        private void 缩放至图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            weMapControl1.ZoomByMBR(AllLayer[StyleLayerNum].MBR);
        }

        private void 编辑工具栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            编辑工具栏ToolStripMenuItem.Checked = !编辑工具栏ToolStripMenuItem.Checked;
            toolStrip2.Visible = !toolStrip2.Visible;

            if(toolStrip2.Visible)
            {
                treeView1.Location = new Point(treeView1.Location.X, treeView1.Location.Y + toolStrip2.Size.Height);
                treeView1.Size = new Size(treeView1.Width, treeView1.Height - toolStrip2.Size.Height);

                weMapControl1.Location = new Point(weMapControl1.Location.X, weMapControl1.Location.Y + toolStrip2.Size.Height);
                weMapControl1.Size = new Size(weMapControl1.Width, weMapControl1.Height - toolStrip2.Size.Height);
            }
            else
            {
                treeView1.Location = new Point(treeView1.Location.X, treeView1.Location.Y - toolStrip2.Size.Height);
                treeView1.Size = new Size(treeView1.Width, treeView1.Height + toolStrip2.Size.Height);

                weMapControl1.Location = new Point(weMapControl1.Location.X, weMapControl1.Location.Y - toolStrip2.Size.Height);
                weMapControl1.Size = new Size(weMapControl1.Width, weMapControl1.Height + toolStrip2.Size.Height);
                
            }
        }

        private void 开始编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            保存编辑ToolStripMenuItem.Enabled = true;
            退出编辑ToolStripMenuItem.Enabled = true;
            toolStripButton9.Enabled = true;
            toolStripButton17.Enabled = true;
            toolStripButton18.Enabled = true;
            toolStripButton16.Enabled = true;
            toolStripButton15.Enabled = true;
            toolStripButton19.Enabled = true;

            toolStripButton19.Items.Clear();
            foreach (var i in AllLayer)
                toolStripButton19.Items.Add(i.LayerName);
        }

        private void 退出编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //weMapControl1.SetCurrentEdit = Convert.ToInt32(toolStripButton19.Text.ToString());
            weMapControl1.MapOpStyle = 0;
            保存编辑ToolStripMenuItem.Enabled = false;
            退出编辑ToolStripMenuItem.Enabled = false;
            toolStripButton9.Enabled = false;
            toolStripButton17.Enabled = false;
            toolStripButton18.Enabled = false;
            toolStripButton16.Enabled = false;
            toolStripButton15.Enabled = false;
            toolStripButton19.Enabled = false;
        }

        private void 保存编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton19_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(toolStripButton19.SelectedIndex > -1 && toolStripButton19.SelectedIndex < AllLayer.Count())
            {
                weMapControl1.SetCurrentEdit = Convert.ToInt32(toolStripButton19.SelectedIndex);
                weMapControl1.MapOpStyle = 4;
            }
            else
            {
                weMapControl1.SetCurrentEdit = -1;
                Console.WriteLine("用户选择的图层索引超出边界.");
            }
            
        }


        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double newScale = 1;
                try
                {

                    newScale = Convert.ToDouble(toolStripTextBox1.Text.ToString());
                    if (newScale < 0.0003 && newScale > 0)
                    {
                        toolStripTextBox1.Text = "0.0005";
                        weMapControl1.DisplayScale = 0.0005;
                    }
                    else
                        weMapControl1.DisplayScale = newScale;
                }
                catch(Exception ex)
                {
                    Console.WriteLine("用户输入了非法的比例尺");
                }
            }
        }

        Point Position = new Point(0, 0);
        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            this.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            string Moveid = "", Dropid = "";

            TreeNode myNode = null;
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                //获得移动节点
                myNode = (TreeNode)(e.Data.GetData(typeof(TreeNode)));
                //获得移动节点的NodeId
                Moveid = (string)myNode.Index.ToString();
            }
            else
            {
                MessageBox.Show("error");
            }

            //将树节点的位置计算成工作区坐标。
            Position.X = e.X;
            Position.Y = e.Y;
            Position = treeView1.PointToClient(Position);

            
            TreeNode DragNode = myNode;
            WEVectorLayer DragLayer = AllLayer[myNode.Index];
            AllLayer.RemoveAt(myNode.Index);
            myNode.Remove();
            //treeView1.Nodes.Add(DragNode);
            TreeNode DropNode = this.treeView1.GetNodeAt(Position);
            //Dropid = (string)DropNode.Index.ToString();
            if (DropNode == null)
            {
                treeView1.Nodes.Add(DragNode);
                AllLayer.Add(DragLayer);
            }
            else if (Convert.ToInt32((string)DropNode.Index.ToString()) == treeView1.Nodes.Count)
            {
                AllLayer.Insert(Convert.ToInt32((string)DropNode.Index.ToString()) - 1, DragLayer);
                treeView1.Nodes.Insert(Convert.ToInt32((string)DropNode.Index.ToString()) - 1, DragNode);
            }
            else
            {
                AllLayer.Insert(Convert.ToInt32((string)DropNode.Index.ToString()), DragLayer);
                treeView1.Nodes.Insert(Convert.ToInt32((string)DropNode.Index.ToString()), DragNode);
            }
            weMapControl1.Refresh();

            /*
            //检索目标节点
            TreeNode DropNode = this.treeView1.GetNodeAt(Position);
            // 1.目标节点不是空。2.目标节点不是被拖拽接点的子节点。3.目标节点不是被拖拽节点本身
            if (DropNode != null && DropNode.Parent != myNode && DropNode != myNode)
            {
                //临时节点
                TreeNode tempNode = myNode;
                // 将被拖拽节点从原来位置删除。
                myNode.Remove();
                // 在目标节点下增加被拖拽节点
                DropNode.Nodes.Add(tempNode);
                //目标节点的NodeId值
                Dropid = (string)DropNode.Index.ToString();

                
                //数据库中更新移动节点的parentID
                string strSQL;
                strSQL = " update TTree set parentId =" + Dropid + "   where  NodeId = " + Moveid + "  ";
                SQLiteDBHelper db = new SQLiteDBHelper(ComPath);
                int i = db.ExecuteNonQuery(strSQL, null);
                if (i < 0)
                {
                    MessageBox.Show("请正确拖动文件");
                }
                

        }
            // 如果目标节点不存在，即拖拽的位置不存在节点，那么就将被拖拽节点放在根节点之下
            if (DropNode == null)
            {
                
                //parentId = 0  表示根节点
                string strSQL;
                strSQL = " update TTree set parentId = 0   where  NodeId = " + Moveid + "  ";
                SQLiteDBHelper db = new SQLiteDBHelper(ComPath);
                int i = db.ExecuteNonQuery(strSQL, null);
                
                
            }
             */

        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            //判断拖动的是否为树节点
            if (e.Data.GetDataPresent(typeof(TreeNode)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// 按属性选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectByAttributesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSelectByAttributes window = new frmSelectByAttributes(AllLayer.ToArray());
            window.Owner = this;
            window.Show();
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            weMapControl1.SelectFeature();
            weMapControl1.IsIdentify = false;
            weMapControl1.NodeEdit = 0;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            保存编辑ToolStripMenuItem_Click(sender, e);
        }

        private void 清除选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            weMapControl1.ClearSelect();
            weMapControl1.Refresh();
        }

        private void 删除选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (weMapControl1.SetCurrentEdit >= 0 && weMapControl1.SetCurrentEdit < AllLayer.Count())
                weMapControl1.DeleteSelect(weMapControl1.SetCurrentEdit, SelectedID.ToArray());
            AllLayer = weMapControl1.AllLayer;
            //weMapControl1.Refresh();
            清除选择ToolStripMenuItem_Click(sender, e);
        }

        private void weMapControl1_SelectingFinished(object sender, int[] ids, int layer, WEFeature feature)
        {
            SelectedID.Clear();
            SelectedID.AddRange(ids);
            if(ids.Length != 0 && weMapControl1.IsIdentify)
            {
                frmIdentify1.Close();
                if (layer == -1)
                    return;
                frmIdentify1 = new frmIdentify(AllLayer[layer].LayerName, feature);
                frmIdentify1.Owner = this;
                frmIdentify1.Location = new Point(50, 50);
                frmIdentify1.Show();
            }
        }


        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            weMapControl1.SelectFeature();
            weMapControl1.IsIdentify = true;
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            weMapControl1.SetCurrentEdit = Convert.ToInt32(toolStripButton19.SelectedIndex);
            weMapControl1.TrackPolygon();
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            weMapControl1.TrackPolygon();
            weMapControl1.NodeEdit = 1;
        }

        private void 查看属性表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmData wind = new frmData();
            wind.SelectedFinished += ExcuteSelected;
            wind.RepairData(weMapControl1.AllLayer[StyleLayerNum]);
            wind.Owner = this;
            wind.ShowDialog();
            List<string> strr = new List<string>();
            strr.AddRange(AllLayer[StyleLayerNum].Field.Keys);
            strr.RemoveAt(0);
            for(int i = 0; i < wind.Data.Rows.Count - 1; i++)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                for (int j = 0; j < wind.dataGridView1.Columns.Count - 12; j++)
                    dic.Add(strr[i], wind.dataGridView1.Rows[i].Cells[j + 1].ToString());
                AllLayer[StyleLayerNum].Features[i].Attributes = dic ;
            }
            AllLayer[StyleLayerNum].InitializeData();
            weMapControl1.AllLayer = AllLayer;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = "";
            str += "GIS设计与应用·第二组·开发者\n\n";
            str += "张溶倩  @1600012408\n\n";
            str += "尹赣闽  @1600012436\n\n";
            str += "姚照原  @1600012406\n\n";
            MessageBox.Show(str, "GIS设计与应用·第二组");
        }

        private void 导出图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saved = new SaveFileDialog();
            if(saved.ShowDialog() == DialogResult.OK)
            {
                WEIO.SaveLayer(AllLayer[StyleLayerNum], saved.FileName);
            }
            
            
        }

        private void 导出文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saved = new SaveFileDialog();
            if (saved.ShowDialog() == DialogResult.OK)
            {
                WEIO.SaveMap(new WEMap(saved.FileName, AllLayer.ToArray(), -180, 180, -90, 90), saved.FileName);
                foreach(var i in AllLayer)
                    WEIO.SaveLayer(i, i.LayerName);
            }
        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "Yao, Zhang, Yin(*.yzy)|*.yzy|所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                WEMap wEMap = WEIO.ReadMap(dialog.FileName);
                AllLayer.Clear();
                AllLayer.AddRange(wEMap.VectorLayers.ToArray());

                weMapControl1.AllLayer = AllLayer;

                treeView1.Nodes.Clear();
                foreach (var layer in AllLayer)
                {
                    TreeNode node0_0 = new TreeNode();
                    node0_0.Text = layer.LayerName;
                    node0_0.Checked = layer.Visible;
                    treeView1.Nodes.Add(node0_0);
                }

                if (AllLayer.Count() != 0)
                    weMapControl1.FullExitence();

            }
        }
    }
}
