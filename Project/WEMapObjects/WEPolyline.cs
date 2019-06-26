using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WEMapObjects
{
    public class WEPolyline:WEGeometry
    {
        #region 字段
       
        private List<WEPoint> _Points = new List<WEPoint>();
        
        #endregion

        #region 构造函数

        public WEPolyline()
        {
            _FeatureType = FeatureType.WEPolyline;
        }

        public WEPolyline(WEPoint[] points)
        {
            _FeatureType = FeatureType.WEPolyline;
            _Points = new List<WEPoint>(points);
            SetMBR();
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置点序列
        /// </summary>
        public WEPoint[] Points
        {
            get { return _Points.ToArray(); }
            set
            {
                _Points.Clear();
                _Points.AddRange(value);
                SetMBR();
            }
        }

        /// <summary>
        /// 获取点数
        /// </summary>
        public override int PointCount
        {
            get { return _Points.Count; }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 重置外包矩形
        /// </summary>
        /// <returns></returns>
        public override WERectangle SetMBR()
        {
            double minX, maxX, minY, maxY;
            minX = _Points[0].X;
            minY = _Points[0].Y;
            maxX = minX;
            maxY = minY;
            for (int i = 0; i < _Points.Count; i++)
            {
                if (_Points[i].X < minX)
                    minX = _Points[i].X;
                if (_Points[i].X > maxX)
                    maxX = _Points[i].X;
                if (_Points[i].Y < minY)
                    minY = _Points[i].Y;
                if (_Points[i].Y > maxY)
                    maxY = _Points[i].Y;
            }
            _MBR = new WERectangle(minX, maxX, minY, maxY);
            return _MBR;
        }

        public override void Add(WEGeometry newGeo)
        {
            _Points.Add((WEPoint)newGeo);
            _MBR += newGeo.MBR;
        }

        /// <summary>
        /// 移动线
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public override void Move(double deltaX, double deltaY)
        {
            for (int i = 0; i < _Points.Count; i++)
            {
                _Points[i].Move(deltaX, deltaY);
            }
            SetMBR();
        }

        /// <summary>
        /// 复制线
        /// </summary>
        /// <returns></returns>
        public override WEGeometry Clone()
        {
            return new WEPolyline(_Points.ToArray());
        }

        public override bool Cover(WEPoint poi)
        {
            return WEMapTools.IsPointOnPolyline(poi, this);
        }

        /// <summary>
        /// 求线段长度的中点
        /// </summary>
        /// <returns></returns>
        public override WEPoint[] getCenterPoint()
        {
            WEPoint center = new WEPoint();
            double length = 0;
            for (int i = 0; i < _Points.Count - 1; i++)      //获取polyline全长
            {
                length += WEMapTools.GetDistance(_Points[i], _Points[i + 1]);
            }
            double calLength = 0;
            for (int i = 0; i < _Points.Count - 1; i++)
            {
                calLength += WEMapTools.GetDistance(_Points[i], _Points[i + 1]);
                if (calLength >= length / 2)     //长度中点在当前段
                {
                    double thisLength = WEMapTools.GetDistance(_Points[i], _Points[i + 1]);
                    double diffLength = calLength - thisLength;
                    center.X = (_Points[i + 1].X - _Points[i].X) * diffLength / thisLength + _Points[i].X;
                    center.Y = (_Points[i + 1].Y - _Points[i].Y) * diffLength / thisLength + _Points[i].Y;
                    break;
                }
            }
            return new WEPoint[1] { center };
        }

        #endregion
    }
}
