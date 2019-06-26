using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WEMapObjects
{
    public class WEMap
    {

        #region 字段

        private string _MapName = "defalutMap";
        private string _MapDescription = "";
        private int _LayerCount = 0;
        private List<WEVectorLayer> _VectorLayers = new List<WEVectorLayer>();
        private double _Left=-180, _Right=180, _Top=90, _Bottom=-90;
        
        #endregion

        #region 构造函数

        public WEMap() { }

        public WEMap(string mapname,WEVectorLayer[] vectorLayers,double l,double r,double t,double b)
        {
            _MapName = mapname;
            _VectorLayers.AddRange(vectorLayers);
            _Left = l;
            _Right = r;
            _Top = t;
            _Bottom = b;            
        }

        #endregion


        #region 属性

        /// <summary>
        /// 获取或设置地图名称
        /// </summary>
        public string MapName
        {
            get { return _MapName; }
            set { _MapName = value; }
        }

        public string MapDescription
        {
            get { return _MapDescription; }
            set { _MapDescription = value; }
        }

        public int LayerCount
        {
            get { return _VectorLayers.Count; }
        }

        /// <summary>
        /// 获取或设置地图范围
        /// </summary>
        public double MapLeft
        {
            get { return _Left; }
            set { _Left = value; }
        }

        public double MapRight
        {
            get { return _Right; }
            set { _Right = value; }
        }

        public double MapTop
        {
            get { return _Top; }
            set { _Top = value; }
        }

        public double MapBottom
        {
            get { return _Bottom; }
            set { _Bottom = value; }
        }
        /// <summary>
        /// 获取或设置要素图层
        /// </summary>
        public WEVectorLayer[] VectorLayers
        {
            get { return _VectorLayers.ToArray(); }
            set { _VectorLayers.AddRange(value); }
        }

        /// <summary>
        /// 获取地图的外包矩形
        /// </summary>
        public WERectangle GetMBR
        {
            get { return new WERectangle(_Left, _Right, _Top, _Bottom); }            
        }

        #endregion

        #region 方法

        /// <summary>
        /// 添加要素图层
        /// </summary>
        /// <param name="vectorLayer"></param>
        public void AddLayer(WEVectorLayer vectorLayer)
        {
            _VectorLayers.Add(vectorLayer);
        }

        /// <summary>
        /// 删除所有图层
        /// </summary>
        public void DeleteAllLayers()
        {
            _VectorLayers.Clear();
        }

        /// <summary>
        /// 删除指定索引号的图层
        /// </summary>
        /// <param name="index"></param>
        public void DeleteLayer(int index)
        {
            if (index < _VectorLayers.Count)
                _VectorLayers.RemoveAt(index);
            else
                throw new Exception("Index out of range.");
        }

        /// <summary>
        /// 设置指定要素图层是否可见
        /// </summary>
        /// <param name="index"></param>
        /// <param name="IsVisible"></param>
        public void SetVisible(int index, bool IsVisible)
        {
            _VectorLayers[index].Visible = IsVisible;
        }

        /// <summary>
        /// 设置指定要素图层是否可选
        /// </summary>
        /// <param name="index"></param>
        /// <param name="IsSelectable"></param>
        public void SetSelectable(int index, bool IsSelectable)
        {
            _VectorLayers[index].Selectable = IsSelectable;
        }

        /// <summary>
        /// 改变图层顺序
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void SetLayerSequence(int from,int to)
        {
            WEVectorLayer movedLayer = _VectorLayers[from].Clone();
            _VectorLayers.RemoveAt(from);
            _VectorLayers.Insert(to, movedLayer);
        }

        /// <summary>
        /// 复制地图
        /// </summary>
        /// <returns></returns>
        public WEMap Clone()
        {
            return new WEMap(_MapName, _VectorLayers.ToArray(), _Left, _Right, _Top, _Bottom);
        }

        #endregion
    }
}
