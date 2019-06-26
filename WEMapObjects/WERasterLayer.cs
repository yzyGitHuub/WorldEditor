using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WEMapObjects
{
    public class WERasterLayer:WELayer
    {
        #region 字段

        // 对象级属性
        protected List<List<List<double>>> _Data;               // 栅格数据
        protected int _Width, _Height, _BandCount;              // 地图大小
        protected List<List<double>> _LAT, _LON;                // 经纬度格式
        protected double _FromLat, _ToLat, _FromLon, _ToLon;    // 经纬度信息

        #endregion

        #region 构造函数

        public WERasterLayer()
        {
            _FeatureType = FeatureType.WENULL;
        }

        public WERasterLayer(int id, string layername, string description, FeatureType type,
            List<List<List<double>>> data, bool visible, bool selectable, WEStyle style)
        {
            _ID = id;
            _LayerName = layername;
            _Description = description;
            _FeatureType = type;
            _Visible = visible;
            _Selectable = selectable;
            _LayerStyle = style;
            _Data = data;
            _BandCount = _Data.Count();
            _Width = _Data[0][0].Count();
            _Height = _Data[0].Count();
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取图层范围
        /// </summary>
        public double[] Size
        {
            get { return new double[3] {_BandCount, _Width, _Height }; }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 复制图层
        /// </summary>
        /// <returns></returns>
        public WERasterLayer Clone()
        {
            return new WERasterLayer(_ID, _LayerName, _Description, _FeatureType, _Data, _Visible, _Selectable, _LayerStyle);
        }

        #endregion
    }
}
