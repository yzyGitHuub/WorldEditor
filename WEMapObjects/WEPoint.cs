using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WEMapObjects
{
    public class WEPoint:WEGeometry
    {
        #region 字段

        private double _X;
        private double _Y;

        #endregion

        #region 构造函数

        public WEPoint()
        {
            _FeatureType = FeatureType.WEPoint;
        }

        public WEPoint(double x, double y)
        {
            _FeatureType = FeatureType.WEPoint;
            _X = x;
            _Y = y;
            SetMBR();
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置X坐标
        /// </summary>
        public double X
        {
            get { return _X; }
            set { _X = value; SetMBR(); }
        }

        /// <summary>
        /// 获取或设置Y坐标
        /// </summary>
        public double Y
        {
            get { return _Y; }
            set { _Y = value; SetMBR(); }
        }

        /// <summary>
        /// 获取点数
        /// </summary>
        public override int PointCount
        {
            get { return 1; }
        }


        #endregion

        #region 方法

        /// <summary>
        /// 获取外包矩形
        /// </summary>
        public override WERectangle SetMBR()
        {
            _MBR = new WERectangle(_X - WEMapTools.Tolerance, _X + WEMapTools.Tolerance, _Y - WEMapTools.Tolerance, _Y + WEMapTools.Tolerance);
            return _MBR;
        }

        /// <summary>
        /// 移动要素
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public override void Move(double deltaX, double deltaY)
        {
            _X += deltaX;
            _Y += deltaY;
            SetMBR();
        }

        /// <summary>
        /// 复制点
        /// </summary>
        /// <returns></returns>
        public override WEGeometry Clone()
        {
            return new WEPoint(_X, _Y);
        }

        public System.Drawing.PointF Convert()
        {
            return new System.Drawing.PointF((float)_X, (float)_Y);
        }

        public override bool Cover(WEPoint poi)
        {
            return WEMapTools.IsPointOnPoint(poi, this);
        }

        public override WEPoint[] getCenterPoint()
        {
            WEPoint center = new WEPoint(_X, _Y);
            return new WEPoint[1] { center };
        }

        public override string ToString()
        {
            return "(" + X.ToString("0.000") + ", " + Y.ToString("0.000") + ")";
        }


        #endregion
    }
}
