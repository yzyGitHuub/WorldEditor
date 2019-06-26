using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WEMapObjects
{
    public class WEEntityPoint : WEFeature
    {
        #region 字段

        #endregion

        #region 构造函数

        public WEEntityPoint()
        {
            _FeatureType = FeatureType.WEEntityPoint;
            _Geometries = new WEMultiPoint();
        }

        public WEEntityPoint(int id, WEGeometry points, Dictionary<string, object> attributes)
        {
            _FeatureType = FeatureType.WEEntityPoint;
            _ID = id;
            _Geometries = points.Clone();
            foreach (var i in attributes)
                _Attributes.Add(i.Key, i.Value);
        }

        #endregion

        #region 属性

        #endregion

        #region 方法

        /// <summary>
        /// 复制多点
        /// </summary>
        /// <returns></returns>
        public override WEFeature Clone()
        {
            return new WEEntityPoint(_ID, _Geometries, _Attributes);
        }

        #endregion
    }
}
