using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WEMapObjects
{
    public partial class WEMapControl : UserControl
    {
        #region 字段

        /// <summary>
        /// 全部矢量图层
        /// </summary>
        public List<WEVectorLayer> AllLayer = new List<WEVectorLayer> { };

        public int NodeEdit = 0;
        private int isGravitationCaptured = -1;
        private int helpGravitationCaptured = -1;

        /// <summary>
        /// 当前处于编辑状态的图层
        /// </summary>
        private int CurrentEdit = -1;

        private WEFeature EditFeature = new WEFeature(0, new WEGeometry(), new Dictionary<string, object>());
        private List<PointF> NewPoints = new List<PointF>();
        private List<WEFeature > _SelectedGeometries = new List<WEFeature>();      // 选中的要素
        private List<WELabel> _Labels = new List<WELabel>();                        // 注记

        //内部变量

        /// <summary>
        /// 地图当前操作类型
        /// <para>0：无, 1：放大, 2：缩小, 3：漫游, 4：输入要素, 5：选择</para>
        /// </summary>
        public int MapOpStyle = 0;
        private WEGeometry mTrackingGeometry = new WEGeometry();            // 用户正在输入的要素
        private PointF mStartPoint = new PointF();                          // 鼠标当前位置，用于漫游和拉框
        private WEPoint CursorMap = new WEPoint(180, 180);
        private List<WEGeometry> _TempSelectedGeometries = new List<WEGeometry>();

        //光标

        private Cursor mCur_Cross = new Cursor(WEMapObjects.Properties.Resources.Cross.ToBitmap().GetHicon());
        private Cursor mCur_ZoomIn = new Cursor(WEMapObjects.Properties.Resources.ZoomIn.ToBitmap().GetHicon());
        private Cursor mCur_ZoomOut = new Cursor(WEMapObjects.Properties.Resources.ZoomOut.ToBitmap().GetHicon());
        private Cursor mCur_PanUp = new Cursor(WEMapObjects.Properties.Resources.PanUp.ToBitmap().GetHicon());

        //常量

        private Color mcSelectingBoxColor = Color.DarkGreen;                // 选择盒颜色
        private const float mcSelectingBoxWidth = 2F;                       // 选择盒边界宽度
        private Color mcSelectionColor = Color.Cyan;                        // 选中要素的颜色

        #endregion

        #region 构造函数

        public WEMapControl()
        {
            InitializeComponent();
            this.MouseWheel += WEMapControl_MouseWheel;
            WEMapTools.DisplayWidth = this.Width;
            WEMapTools.DisplayHeight = this.Height;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 显示器 X 方向每英寸像素数
        /// </summary>
        public double DPIX
        {
            get
            {
                using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
                {
                    return graphics.DpiX;
                }
            }
        }

        /// <summary>
        /// 显示器 Y 方向每英寸像素数
        /// </summary>
        public double DPIY
        {
            get
            {
                using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
                {
                    return graphics.DpiY;
                }
            }
        }

        /// <summary>
        /// 获取或设置显示比例尺的倒数
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public double DisplayScale
        {
            get { return WEMapTools.DisplayMBR.Width * 1 / (this.Width / DPIX); }
            set
            {
                double newWidth = this.Width * value / DPIX, newHeight = this.Height * value / DPIY;
                double newMinX = WEMapTools.DisplayMBR.MinX - (newWidth - WEMapTools.DisplayMBR.Width) / 2,
                    newMinY = WEMapTools.DisplayMBR.MinY - (newHeight - WEMapTools.DisplayMBR.Height) / 2,
                    newMaxX = WEMapTools.DisplayMBR.MaxX + (newWidth - WEMapTools.DisplayMBR.Width) / 2,
                    newMaxY = WEMapTools.DisplayMBR.MaxY + (newHeight - WEMapTools.DisplayMBR.Height) / 2;
                ZoomByMBR(new WERectangle(newMinX, newMaxX, newMinY, newMaxY));
            }
        }

        /// <summary>
        /// 设置或获取当前编辑状态图层编号
        /// </summary>
        public int SetCurrentEdit
        {
            get { return CurrentEdit; }
            set
            {
                try
                {
                    CurrentEdit = value;
                    switch (AllLayer[CurrentEdit].FeatureType)
                    {
                        case FeatureType.WEMultiPoint:
                        case FeatureType.WEEntityPoint:
                            EditFeature = new WEEntityPoint();
                            break;
                        case FeatureType.WEEntityPolyline:
                        case FeatureType.WEMultiPolyline:
                            EditFeature = new WEEntityPolyline();
                            break;
                        case FeatureType.WEEntityPolygon:
                        case FeatureType.WEMultiPolygon:
                            EditFeature = new WEEntityPolygon();
                            break;
                    }
                    NewPoints = new List<PointF>();
                }
                catch(Exception e)
                {
                    MapOpStyle = 1;
                    Console.WriteLine(e.Message);
                }
            }
            
        }

        public bool IsIdentify = false;

        #endregion

        #region 方法

        /// <summary>
        /// 缩放地图到指定的矩形区域
        /// </summary>
        /// <param name="newMBR"></param>
        public void ZoomByMBR(WERectangle newMBR)
        {
            if (newMBR == null)
                return;
            if(newMBR.Width / newMBR.Height > WEMapTools.DisplayWidth * 1.0 / WEMapTools.DisplayHeight)
            {
                // 宽
                double delta_y = (newMBR.Width * this.Height / this.Width - newMBR.Height) / 2.0;
                newMBR = new WERectangle(newMBR.MinX, newMBR.MaxX, newMBR.MinY - delta_y, newMBR.MaxY + delta_y);
                //double newMaxY = newMBR.MinY + newMBR.Width * this.Height / this.Width;
                //newMBR = new WERectangle(newMBR.MinX, newMBR.MaxX, newMBR.MinY, newMaxY);
            }
            else
            {
                // 高
                double delta_x = (newMBR.Height * this.Width / this.Height - newMBR.Width) / 2.0;
                newMBR = new WERectangle(newMBR.MinX - delta_x, newMBR.MaxX + delta_x, newMBR.MinY, newMBR.MaxY);
                //double newMaxX = newMBR.MinX + newMBR.Height * this.Width / this.Height;
                //newMBR = new WERectangle(newMBR.MinX, newMaxX, newMBR.MinY, newMBR.MaxY);
            }
            if(newMBR != WEMapTools.DisplayMBR && DisplayScaleChanged != null)
                DisplayScaleChanged(this);
            WEMapTools.DisplayMBR = newMBR;
            foreach (var i in AllLayer)
                i.SelectDisplay();
            WEMapTools.Tolerance = 0.01 * DisplayScale;
            Refresh();
        }

        /// <summary>
        /// 根据提供的中心和缩放级别进行缩放
        /// </summary>
        /// <param name="center">缩放的屏幕中心</param>
        /// <param name="ratio">缩放的比例，大于 1 放大，小于 1 缩小</param>
        public void ZoomByCenter(PointF center, double ratio = 1.2)
        {
            PointF poimin = new PointF(center.X * (float)(ratio - 1), center.Y * (float)(ratio - 1));// 0,0
            PointF poimax = new PointF(WEMapTools.DisplayWidth - (WEMapTools.DisplayWidth - center.X) * (float)(ratio - 1), WEMapTools.DisplayHeight - (WEMapTools.DisplayHeight - center.Y) * (float)(ratio - 1));// width, height
            //PointF poimin = new PointF(center.X * (float)(ratio - 1), center.Y * (float)(ratio - 1));// 0,0
            //PointF poimax = new PointF(Width - center.X * (float)(ratio - 1), Height - center.Y * (float)(ratio - 1));// width, height
            WEPoint p1 = WEMapTools.ToMapPoint(poimin), p2 = WEMapTools.ToMapPoint(poimax);
            ZoomByMBR(new WERectangle(p1.X, p2.X, p2.Y, p1.Y));
        }

        /// <summary>
        /// 向当前编辑的图层中添加一个几何对象
        /// </summary>
        public void AddGeometry(WEGeometry newGeo)
        {
            if (CurrentEdit == -1)
            {
                Console.WriteLine("非编辑状态");
                throw new Exception("非编辑状态");
            }
            WEFeature newFea = new WEFeature();
            WEVectorLayer _lay = (WEVectorLayer)(AllLayer[CurrentEdit]);
            int id = _lay.Features.Count();
            switch (AllLayer[CurrentEdit].FeatureType)
            {
                case FeatureType.WEEntityPoint:
                    newFea = new WEEntityPoint(id, newGeo, _lay.Field);
                    break;
                case FeatureType.WEEntityPolyline:
                    newFea = new WEEntityPolyline(id, (WEMultiPolyline)newGeo, _lay.Field);
                    break;
                case FeatureType.WEEntityPolygon:
                    newFea = new WEEntityPolygon(id, (WEMultiPolygon)newGeo, _lay.Field);
                    break;
            } 
            _lay.AddFeature(newFea);
            AllLayer[CurrentEdit] = _lay;
        }

        /// <summary>
        /// 在当前编辑的图层中删除索引的几何对象
        /// </summary>
        /// <param name="index"></param>
        public void DeleteGeometry(int index)
        {
            WEVectorLayer _lay = (WEVectorLayer)(AllLayer[CurrentEdit]);
            _lay.DeleteFeature(index);
            AllLayer[CurrentEdit] = _lay;
            this.Refresh();
            Refresh();
        }

        /// <summary>
        /// 根据矩形盒进行选择，返回选中要素集合
        /// </summary>
        /// <param name="box">矩形盒</param>
        /// <returns></returns>
        public WEFeature[] SelectByBox(WERectangle box)
        {
            if (box.Width == 0 || box.Height == 0)
            {
                WEPoint p1 = new WEPoint((box.MinX+box.MaxX)/2, (box.MinY+box.MaxY)/2);
                bool flag = false;
                foreach (var layer in AllLayer)
                {
                    if (flag)
                        break;
                    foreach (WEFeature fea in layer.Features)
                    {
                        if (fea.Geometries.Cover(p1))
                        {
                            _SelectedGeometries.Add(fea);
                            flag = true;
                            break;
                        }
                    }
                }
                return _SelectedGeometries.ToArray();
            }
            else
            {
                List<WEFeature> selected = new List<WEFeature> { };
                foreach (WELayer lay in AllLayer)
                    selected.AddRange(lay.SelectByBox(box));
                return selected.ToArray();
            }
        }

        /// <summary>
        /// 根据id数组进行选择，返回选中要素集合，2019/06/11，by Ganmin Yin
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="layerIndex"></param>
        public void SelectById(int[] ids, int layerIndex)
        {
            _SelectedGeometries.Clear();
            _SelectedGeometries.AddRange(AllLayer[layerIndex].SelectById(ids));
        }

        /// <summary>
        /// 缩放至全图
        /// </summary>
        public void FullExitence()
        {
            if (AllLayer.Count == 0)
                return;
            WERectangle newRect = AllLayer[0].MBR;
            foreach (var i in AllLayer)
                newRect += i.MBR;
            ZoomByMBR(newRect);
        }

        /// <summary>
        /// 将地图操作状态设置为放大
        /// </summary>
        public void ZoomIn()
        {
            MapOpStyle = 1;
            this.Cursor = mCur_ZoomIn;
            IsIdentify = false;
        }

        /// <summary>
        /// 将地图操作状态设置为缩小
        /// </summary>
        public void ZoomOut()
        {
            MapOpStyle = 2;
            this.Cursor = mCur_ZoomOut;
            IsIdentify = false;
        }

        /// <summary>
        /// 将地图操作状态设置为漫游
        /// </summary>
        public void Pan()
        {
            MapOpStyle = 3;
            this.Cursor = mCur_PanUp;
            IsIdentify = false;
        }

        /// <summary>
        /// 将地图操作状态设置为跟踪多边形
        /// </summary>
        public void TrackPolygon()
        {
            MapOpStyle = 4;
            //CurrentEdit = -1;
            this.Cursor = mCur_Cross;
            IsIdentify = false;
        }

        /// <summary>
        /// 将地图操作状态设置为选择
        /// </summary>
        public void SelectFeature()
        {
            MapOpStyle = 5;
            this.Cursor = Cursors.Arrow;
            IsIdentify = false;
        }

        public void ClearSelect()
        {
            _SelectedGeometries.Clear();
            Refresh();
        }

        public void DeleteSelect(int index, int[] selected)
        {
            for (int i = selected.Count() - 1; i >= 0; i--)
                AllLayer[index].DeleteFeature( selected[i]);
            AllLayer[index].InitializeData();
            AllLayer[index].ReSetID();
        }

        #endregion

        #region 事件

        public delegate void LayersChangeHandle(object sender, List<WEVectorLayer> wEVectorLayers);
        /// <summary>
        /// 用户输入多边形完毕
        /// </summary>
        public event LayersChangeHandle LayersChange;

        public delegate void DisplayScaleChangedHandle(object sender);
        /// <summary>
        /// 地图显示比例尺发生变化
        /// </summary>
        public event DisplayScaleChangedHandle DisplayScaleChanged;

        public delegate void SelectingFinishedHandle(object sender, int[] ids, int layer, WEFeature feature0);
        /// <summary>
        /// 用户选择完毕
        /// </summary>
        public event SelectingFinishedHandle SelectingFinished;

        public delegate void WEMouseMoveHandle(object sender, Point location);
        /// <summary>
        /// 用户选择完毕
        /// </summary>
        public event WEMouseMoveHandle WEMouseMove;

        #endregion

        #region 母版事件处理

        // 绘制全部图层
        private void WEMapControl_Paint(object sender, PaintEventArgs e)
        {
            for (int i = AllLayer.Count - 1; i >= 0; i--)
                AllLayer[i].Draw(e);

            //EditFeature.Geometries = new WEGeometry()
            if (MapOpStyle == 4 && EditFeature.Geometries.PointCount != 0)
                EditFeature.Draw(e);

            // 绘制选中的几何要素
            foreach (var i in _SelectedGeometries)
                i.Draw(e,true);

        }

        //鼠标移动
        private void WEMapControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (WEMouseMove != null)
                WEMouseMove(this, e.Location);
            switch(MapOpStyle)
            {
                case 0:
                    break;
                case 1:             //放大
                    if (e.Button == MouseButtons.Left)
                    {
                        Refresh();
                        Graphics g = Graphics.FromHwnd(this.Handle);
                        Pen sBoxPen = new Pen(mcSelectingBoxColor, mcSelectingBoxWidth);
                        float sMinX = Math.Min(mStartPoint.X, e.Location.X);
                        float sMinY = Math.Min(mStartPoint.Y, e.Location.Y);
                        float sMaxX = Math.Max(mStartPoint.X, e.Location.X);
                        float sMaxY = Math.Max(mStartPoint.Y, e.Location.Y);
                        g.DrawRectangle(sBoxPen, sMinX, sMinY, sMaxX - sMinX, sMaxY - sMinY);
                        g.Dispose();
                    }
                    break;
                case 2:             //缩小
                    if (e.Button == MouseButtons.Left)
                    {
                        Refresh();
                        Graphics g = Graphics.FromHwnd(this.Handle);
                        Pen sBoxPen = new Pen(mcSelectingBoxColor, mcSelectingBoxWidth);
                        float sMinX = Math.Min(mStartPoint.X, e.Location.X);
                        float sMinY = Math.Min(mStartPoint.Y, e.Location.Y);
                        float sMaxX = Math.Max(mStartPoint.X, e.Location.X);
                        float sMaxY = Math.Max(mStartPoint.Y, e.Location.Y);
                        g.DrawRectangle(sBoxPen, sMinX, sMinY, sMaxX - sMinX, sMaxY - sMinY);
                        g.Dispose();
                    }
                    break;
                case 3:             //漫游
                    if (e.Button == MouseButtons.Left)
                    {
                        //PointF sPreMouseLocation = new PointF(mStartPoint.X, mStartPoint.Y);
                        //WEPoint sPrePoint = WEMapTools.ToMapPoint(sPreMouseLocation);
                        WEPoint sPrePoint = WEMapTools.ToMapPoint(mStartPoint);
                        //PointF sCurMouseLocation = new PointF(e.Location.X, e.Location.Y);
                        //WEPoint sCurPoint = WEMapTools.ToMapPoint(sCurMouseLocation);
                        WEPoint sCurPoint = WEMapTools.ToMapPoint(e.Location);
                        WEPoint offset = WEMapTools.ToMapPoint(new PointF(e.Location.X - mStartPoint.X, e.Location.Y - mStartPoint.Y));
                        //修改offset
                        double mOffsetX = sPrePoint.X - sCurPoint.X;
                        double mOffsetY = sPrePoint.Y - sCurPoint.Y;
                        WERectangle newRect = new WERectangle(WEMapTools.DisplayMBR.MinX + mOffsetX, WEMapTools.DisplayMBR.MaxX + mOffsetX,
                            WEMapTools.DisplayMBR.MinY + mOffsetY, WEMapTools.DisplayMBR.MaxY + mOffsetY);
                        WEMapTools.DisplayMBR = newRect;
                        ZoomByMBR(WEMapTools.DisplayMBR);
                        mStartPoint = e.Location;
                        /*
                        //修改offset
                        mOffsetX = (mStartPoint.X - e.Location.X) * WEMapTools.DisplayMBR.Width / WEMapTools.DisplayWidth;
                        mOffsetY = (e.Location.Y - mStartPoint.Y) * WEMapTools.DisplayMBR.Height / WEMapTools.DisplayHeight;
                        newRect = new WERectangle(WEMapTools.DisplayMBR.MinX + mOffsetX, WEMapTools.DisplayMBR.MaxX + mOffsetX,
                            WEMapTools.DisplayMBR.MinY + mOffsetY, WEMapTools.DisplayMBR.MaxY + mOffsetY);
                        WEMapTools.DisplayMBR = newRect;
                        ZoomByMBR(WEMapTools.DisplayMBR);
                        mStartPoint.X = e.Location.X;
                        mStartPoint.Y = e.Location.Y;
                        */
                    }
                    break;
                case 4:
                    mStartPoint = e.Location;
                    List<WEPoint> tem = new List<WEPoint>();
                    if (CurrentEdit < 0 || CurrentEdit > AllLayer.Count())
                        break;
                    if(NodeEdit == 0)
                    {
                        switch (AllLayer[CurrentEdit].FeatureType)
                        {
                            case FeatureType.WEMultiPoint:
                            case FeatureType.WEEntityPoint:
                                if (NewPoints.Count() != 0)
                                {
                                    NewPoints[0] = e.Location;
                                    EditFeature.Geometries = new WEMultiPoint(new WEPoint[1] { WEMapTools.ToMapPoint(NewPoints[0]) });
                                }
                                else
                                {
                                    NewPoints.Add(e.Location);
                                }
                                //NewPoints.Clear();
                                //EditFeature = AllLayer[CurrentEdit].Features.Last();
                                //AllLayer[CurrentEdit].Features[AllLayer[CurrentEdit].Features.Count() - 1].Geometries = new WEMultiPoint(new WEGeometry[1] {  });
                                break;
                            case FeatureType.WEEntityPolyline:
                            case FeatureType.WEMultiPolyline:
                                NewPoints.Add(e.Location);

                                WEMultiPolyline temm = (WEMultiPolyline)(EditFeature.Geometries);
                                WEPolyline temmm = (WEPolyline)temm.Polylines[temm.Polylines.Count() - 1];
                                //tem.AddRange(temmm.Points);
                                foreach (var i in NewPoints)
                                    tem.Add(WEMapTools.ToMapPoint(i));
                                temmm.Points = (WEPoint[])tem.ToArray();
                                temm.DeletePolyline(temm.Polylines.Count() - 1);
                                temm.Add(temmm);
                                EditFeature.Geometries = temm;

                                NewPoints.RemoveAt(NewPoints.Count - 1);

                                break;
                            case FeatureType.WEEntityPolygon:
                            case FeatureType.WEMultiPolygon:
                                NewPoints.Add(e.Location);

                                WEMultiPolygon temm3 = (WEMultiPolygon)(EditFeature.Geometries);
                                WEPolygon temmm3 = (WEPolygon)temm3.Polygons[temm3.Polygons.Count() - 1];
                                //tem.AddRange(temmm3.Points);
                                foreach (var i in NewPoints)
                                    tem.Add(WEMapTools.ToMapPoint(i));
                                temmm3.Points = (WEPoint[])tem.ToArray();
                                temm3.DeletePolygon(temm3.Polygons.Count() - 1);
                                temm3.Add(temmm3);
                                EditFeature.Geometries = temm3;

                                NewPoints.RemoveAt(NewPoints.Count - 1);

                                break;
                        }
                        Refresh();
                    }
                    else if(EditFeature.Geometries.PointCount != 0)
                    {
                        CursorMap = WEMapTools.ToMapPoint(e.Location);
                        switch(EditFeature.FeatureType)
                        {
                            case FeatureType.WEMultiPoint:
                            case FeatureType.WEEntityPoint:
                                isGravitationCaptured = -1;
                                for(int i = 0; i < EditFeature.Geometries.PointCount; i++)
                                {
                                    WEPoint poi = (WEPoint)((WEMultiPoint)EditFeature.Geometries).Points[i];
                                    if(WEMapTools.IsPointOnPoint(CursorMap, poi))
                                    {
                                        isGravitationCaptured = i;
                                        break;
                                    }
                                }
                                break;
                            case FeatureType.WEEntityPolyline:
                            case FeatureType.WEMultiPolyline:
                                isGravitationCaptured = -1;
                                for (int i = 0; i < EditFeature.Geometries.PointCount; i++)
                                {
                                    WEPolyline polyline = (WEPolyline)((WEMultiPolyline)EditFeature.Geometries).Polylines[i];
                                    for(int j = 0; j < polyline.PointCount; j++)
                                    {
                                        if (WEMapTools.IsPointOnPoint(CursorMap, polyline.Points[j]))
                                        {
                                            isGravitationCaptured = j;
                                            break;
                                        }
                                    }
                                    if(isGravitationCaptured!=-1)
                                    {
                                        helpGravitationCaptured = i;
                                        break;
                                    }
                                    if (polyline.PointCount == 1)
                                        continue;
                                    for (int j = 1; j < polyline.PointCount; j++)
                                    {
                                        if (WEMapTools.IsPointOnPolyline(CursorMap, new WEPolyline(new WEPoint[2] { polyline.Points[j], polyline.Points[j-1] })));
                                        {
                                            isGravitationCaptured = j;
                                            break;
                                        }
                                    }
                                    if (isGravitationCaptured != -1)
                                    {
                                        helpGravitationCaptured = -i;
                                        break;
                                    }
                                }
                                break;
                            case FeatureType.WEEntityPolygon:
                            case FeatureType.WEMultiPolygon:
                                isGravitationCaptured = -1;
                                for (int i = 0; i < EditFeature.Geometries.PointCount; i++)
                                {
                                    WEPolygon polygon = (WEPolygon)((WEMultiPolyline)EditFeature.Geometries).Polylines[i];
                                    for (int j = 0; j < polygon.PointCount; j++)
                                    {
                                        if (WEMapTools.IsPointOnPoint(CursorMap, polygon.Points[j]))
                                        {
                                            isGravitationCaptured = j;
                                            break;
                                        }
                                    }
                                    if (isGravitationCaptured != -1)
                                    {
                                        helpGravitationCaptured = i;
                                        break;
                                    }
                                    if (polygon.PointCount == 1)
                                        continue;
                                    for (int j = 1; j < polygon.PointCount; j++)
                                    {
                                        if (WEMapTools.IsPointOnPolyline(CursorMap, new WEPolyline(new WEPoint[2] { polygon.Points[j], polygon.Points[j - 1] }))) ;
                                        {
                                            isGravitationCaptured = j;
                                            break;
                                        }
                                    }
                                    if (WEMapTools.IsPointOnPolyline(CursorMap, new WEPolyline(new WEPoint[2] { polygon.Points.Last(), polygon.Points.First() }))) ;
                                        isGravitationCaptured = polygon.PointCount;
                                    if (isGravitationCaptured != -1)
                                    {
                                        helpGravitationCaptured = -i;
                                        break;
                                    }
                                }
                                break;
                            default:
                                isGravitationCaptured = -1;
                                break;
                        }
                        if (isGravitationCaptured != -1)
                        {
                            Cursor = Cursors.SizeAll;
                            using (Graphics g = Graphics.FromHwnd(this.Handle))
                            {
                                g.FillRectangle(new SolidBrush(Color.Black), 0, 0, 100, 100);
                            }
                        }
                        else
                            Cursor = Cursors.Cross;
                    }
                    if(isGravitationCaptured!=-1)
                    {

                    }
                        
                    break;
                case 5:             //选择要素
                    if (e.Button == MouseButtons.Left)
                    {
                        Refresh();
                        Graphics g = Graphics.FromHwnd(this.Handle);
                        Pen sBoxPen = new Pen(mcSelectingBoxColor, mcSelectingBoxWidth);
                        float sMinX = Math.Min(mStartPoint.X, e.Location.X);
                        float sMinY = Math.Min(mStartPoint.Y, e.Location.Y);
                        float sMaxX = Math.Max(mStartPoint.X, e.Location.X);
                        float sMaxY = Math.Max(mStartPoint.Y, e.Location.Y);
                        g.DrawRectangle(sBoxPen, sMinX, sMinY, sMaxX - sMinX, sMaxY - sMinY);
                        g.Dispose();
                    }
                    break;
                default:
                    mStartPoint = e.Location;
                    break;
            }
        }

        //鼠标按下
        private void WEMapControl_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                mStartPoint = e.Location;
                switch (MapOpStyle)
                {
                    case 4:
                        NewPoints.Add(e.Location);
                        Refresh();
                        break;
                    case 5:
                        _SelectedGeometries.Clear();
                        break;
                }
            }
        }

        //鼠标双击
        private void WEMapControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && MapOpStyle == 4)
            {
                finishSketch();
            }
        }

        //鼠标松开
        private void WEMapControl_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Refresh();
                PointF poimin, poimax;
                WEPoint p1, p2;
                switch (MapOpStyle)
                {
                    case 0:
                        break;
                    case 1:
                        if (DisplayScale < 0.0005 && DisplayScale > 0)
                            return;
                        if (mStartPoint == e.Location)
                            ZoomByCenter(e.Location, 1.1);
                        else
                        {
                            poimin = new PointF(Math.Min(mStartPoint.X, e.Location.X), Math.Min(mStartPoint.Y, e.Location.Y));// 0,0
                            poimax = new PointF(Math.Max(mStartPoint.X, e.Location.X), Math.Max(mStartPoint.Y, e.Location.Y));
                            p1 = WEMapTools.ToMapPoint(poimin);
                            p2 = WEMapTools.ToMapPoint(poimax);
                            ZoomByMBR(new WERectangle(p1.X, p2.X, p2.Y, p1.Y));
                        }
                        break;
                    case 2:
                        if (mStartPoint == e.Location)
                            ZoomByCenter(e.Location, 1 / 1.1);
                        else
                        {
                            PointF center = new PointF((mStartPoint.X + e.Location.X) / (float)2.0, (mStartPoint.Y + e.Location.Y) / (float)2.0);
                            poimin = new PointF(Math.Min(mStartPoint.X, e.Location.X), Math.Min(mStartPoint.Y, e.Location.Y));// 0,0
                            poimax = new PointF(Math.Max(mStartPoint.X, e.Location.X), Math.Max(mStartPoint.Y, e.Location.Y));
                            ZoomByCenter(center, Math.Min((poimax.X - poimin.X) / WEMapTools.DisplayWidth, (poimax.Y - poimin.Y) / WEMapTools.DisplayHeight));
                        }
                        break;
                    case 3:
                        break;
                    case 4://输入多边形
                        break;
                    case 5://选择要素
                        int flag = -1;
                        if (mStartPoint == e.Location)
                        {
                            p1 = WEMapTools.ToMapPoint(e.Location);
                            for (int i = 0; i <  AllLayer.Count;i++)
                            {
                                var layer = AllLayer[i];
                                if (!layer.Visible)
                                    continue;
                                if (flag != -1)
                                    break;
                                foreach (WEFeature fea in layer.Features)
                                {
                                    if (fea.Geometries.Cover(p1))
                                    {
                                        _SelectedGeometries.Add(fea);
                                        flag = i;
                                        break;
                                    }
                                }
                            }
                            Refresh();
                        }
                        else
                        {
                            poimin = new PointF(Math.Min(mStartPoint.X, e.Location.X), Math.Min(mStartPoint.Y, e.Location.Y));// 0,0
                            poimax = new PointF(Math.Max(mStartPoint.X, e.Location.X), Math.Max(mStartPoint.Y, e.Location.Y));
                            p1 = WEMapTools.ToMapPoint(poimin);
                            p2 = WEMapTools.ToMapPoint(poimax);
                            for (int i = 0; i < AllLayer.Count;i++)
                            {
                                var layer = AllLayer[i];
                                if (!layer.Visible)
                                    continue;
                                _SelectedGeometries.AddRange(layer.SelectByBox(new WERectangle(p1.X, p2.X, p2.Y, p1.Y)).ToArray());
                            }
                                
                            Refresh();
                            
                            //if (SelectingFinished != null)
                            //SelectingFinished(this);
                        }
                        List<int> ids = new List<int> { };
                        foreach (var i in _SelectedGeometries)
                            ids.Add(i.ID);
                        if (SelectingFinished != null)
                        {
                            if (_SelectedGeometries.Count != 0)
                            {
                                EditFeature = _SelectedGeometries[0];
                                SelectingFinished(this, ids.ToArray(), flag, _SelectedGeometries[0]);
                            }
                            else
                                SelectingFinished(this, ids.ToArray(), flag, null);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        //鼠标滑轮事件
        private void WEMapControl_MouseWheel(object sender, MouseEventArgs e)
        {
                if (e.Delta > 0)    //前滚
                {
                    if (DisplayScale < 0.0003 && DisplayScale > 0)
                        return;
                    //PointF sCenterPoint = new PointF(this.ClientSize.Width / 2, this.ClientSize.Height / 2);//控件中心点屏幕坐标
                    ZoomByCenter(e.Location, 1.1);//放大
                }
                else//后滚
                {
                    //PointF sCenterPoint = new PointF(this.ClientSize.Width / 2, this.ClientSize.Height / 2);//控件中心点屏幕坐标
                    ZoomByCenter(e.Location, 1 / 1.1);//缩小
                }
        }

        // 控件大小改变
        private void WEMapControl_Resize(object sender, EventArgs e)
        {
            WEMapTools.DisplayWidth = this.Width;
            WEMapTools.DisplayHeight = this.Height;
            Refresh();
        }

        // 结束绘制
        private void finishPartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finishPart();
        }

        // 结束绘制
        private void finishSketchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finishSketch();
        }

        // 按键处理
        private void WEMapControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z && MapOpStyle == 4)
            {

                if (NewPoints.Count() == 0)
                    System.Media.SystemSounds.Beep.Play();
                else
                    NewPoints.RemoveAt(NewPoints.Count() - 1);
                Refresh();
            }
        }


        #endregion

        #region 私有函数

        private void finishPart()
        {
            if (CurrentEdit < 0 || CurrentEdit > AllLayer.Count())
                return;
            List<WEPoint> tem = new List<WEPoint>();
            if (NewPoints.Count() != 0)
                switch (AllLayer[CurrentEdit].FeatureType)
                {
                    case FeatureType.WEMultiPoint:
                    case FeatureType.WEEntityPoint:
                        break;
                    case FeatureType.WEEntityPolyline:
                    case FeatureType.WEMultiPolyline:
                        WEMultiPolyline temm = (WEMultiPolyline)(EditFeature.Geometries);
                        WEPolyline temmm = (WEPolyline)temm.Polylines[temm.Polylines.Count() - 1];
                        //tem.AddRange(temmm.Points);
                        foreach (var i in NewPoints)
                            tem.Add(WEMapTools.ToMapPoint(i));
                        temmm.Points = (WEPoint[])tem.ToArray();
                        temm.DeletePolyline(temm.Polylines.Count() - 1);
                        temm.Add(temmm);
                        temm.Add(new WEPolyline());
                        EditFeature.Geometries = temm;
                        NewPoints.Clear();

                        break;
                    case FeatureType.WEEntityPolygon:
                    case FeatureType.WEMultiPolygon:
                        WEMultiPolygon temm3 = (WEMultiPolygon)(EditFeature.Geometries);
                        WEPolygon temmm3 = (WEPolygon)temm3.Polygons[temm3.Polygons.Count() - 1];
                        //tem.AddRange(temmm3.Points);
                        foreach (var i in NewPoints)
                            tem.Add(WEMapTools.ToMapPoint(i));
                        temmm3.Points = (WEPoint[])tem.ToArray();
                        temm3.DeletePolygon(temm3.Polygons.Count() - 1);
                        temm3.Add(temmm3);
                        temm3.Add(new WEPolygon());
                        EditFeature.Geometries = temm3;
                        NewPoints.Clear();
                        break;
                    default:
                        throw new Exception("不能识别的图层类型");
                }
            else
                System.Media.SystemSounds.Beep.Play();
        }

        private void finishSketch()
        {
            if (CurrentEdit < 0 || CurrentEdit > AllLayer.Count())
                return;
            if (NewPoints.Count() != 0)
                finishPart();
            if (EditFeature.Geometries.PointCount != 0)
            {
                foreach (var i in AllLayer[CurrentEdit].Field)
                    EditFeature.AddField(i.Key);
                AllLayer[CurrentEdit].AddFeature(EditFeature);
                AllLayer[CurrentEdit].SelectDisplay();
                if (LayersChange != null)
                    LayersChange(this, AllLayer);
                SetCurrentEdit = CurrentEdit;
                Refresh();
            }
        }

        #endregion

    }
}
