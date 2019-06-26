using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WEMapObjects
{
    public class WEEntityPolyline : WEFeature
    {
        #region 字段

        #endregion

        #region 构造函数

        public WEEntityPolyline()
        {
            _FeatureType = FeatureType.WEEntityPolyline;
            _Geometries = new WEMultiPolyline();
        }

        public WEEntityPolyline(int id, WEMultiPolyline polylines, Dictionary<string, object> attributes)
        {
            _FeatureType = FeatureType.WEEntityPolyline;
            _ID = id;
            _Geometries = polylines;
            foreach (var i in attributes)
                _Attributes.Add(i.Key, i.Value);
        }

        #endregion

        #region 属性

        #endregion

        #region 方法

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public override WEFeature Clone()
        {
            return new WEEntityPolyline(_ID, (WEMultiPolyline)_Geometries, _Attributes);
        }

        #endregion
    }
}
