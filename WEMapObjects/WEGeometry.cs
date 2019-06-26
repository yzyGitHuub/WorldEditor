using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WEMapObjects
{
    /// <summary>
    /// 地理几何对象
    /// </summary>
    public class WEGeometry
    {

        protected WERectangle _MBR = null;
        protected FeatureType _FeatureType;

        public WEGeometry()
        {
            _FeatureType = FeatureType.WENULL;
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        public FeatureType FeatureType
        {
            get { return _FeatureType; }
        }

        /// <summary>
        /// 获取外包矩形
        /// </summary>
        public WERectangle MBR
        {
            get { return _MBR; }
        }

        public virtual int PointCount
        {
            get { return 0; }
        }

        public virtual void Add(WEGeometry newGeo)
        {

        }

        /// <summary>
        /// 重置外包矩形
        /// </summary>
        public virtual WERectangle SetMBR()
        {
            return _MBR;
        }

        /// <summary>
        /// 移动要素
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public virtual void Move(double deltaX, double deltaY) { }

        /// <summary>
        /// 复制要素
        /// </summary>
        /// <returns></returns>
        public virtual WEGeometry Clone()
        {
            return new WEGeometry();
        }

        /// <summary>
        /// 清空元素内部
        /// </summary>
        public virtual void Clear()
        {

        }

        public virtual void Draw(System.Windows.Forms.PaintEventArgs e, WEStyle style, string str)
        {

        }

        public virtual bool Cover(WEPoint poi)
        {
            return false;
        }

        public virtual WEPoint[] getCenterPoint()
        {
            WEPoint center = new WEPoint();
            return new WEPoint[1] { center };
        }
    }
}
