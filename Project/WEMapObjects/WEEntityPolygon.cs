using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WEMapObjects
{
    public class WEEntityPolygon : WEFeature
    {
        #region 字段

        #endregion

        #region 构造函数

        public WEEntityPolygon()
        {
            _FeatureType = FeatureType.WEEntityPolygon;
            _Geometries = new WEMultiPolygon();
        }

        public WEEntityPolygon(int id, WEMultiPolygon polygons, Dictionary<string, object> attributes)
        {
            _FeatureType = FeatureType.WEEntityPolygon;
            _ID = id;
            _Geometries = polygons;
            foreach (var i in attributes)
                _Attributes.Add(i.Key, i.Value);
        }

        #endregion

        #region 属性

        #endregion

        #region 方法

        /// <summary>
        /// 复制复合多边形
        /// </summary>
        /// <returns></returns>
        public override WEFeature Clone()
        {
            return new WEEntityPolygon(_ID, (WEMultiPolygon)_Geometries, _Attributes);
        }

        #endregion
    }
}
