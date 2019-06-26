using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WEMapObjects
{
    public class WEPolygon : WEGeometry
    {
        #region 字段

        private List<WEPoint> _Points = new List<WEPoint>();

        #endregion

        #region 构造函数

        public WEPolygon()
        {
            _FeatureType = FeatureType.WEPolygon;
        }


        public WEPolygon(WEPoint[] points)

        {
            _FeatureType = FeatureType.WEPolygon;
            _Points = new List<WEPoint>(points);
            SetMBR();
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置组成多边形的线
        /// </summary>
        public WEPoint[] Points
        {
            get { return _Points.ToArray(); }
            set { _Points = value.ToList(); SetMBR(); }
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

        public void Add(WEPoint poi)
        {
            _Points.Add(poi);
            _MBR += poi.MBR;
        }

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

        /// <summary>
        /// 移动多边形
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public override void Move(double deltaX,double deltaY)
        {
            foreach(WEGeometry i in _Points)
                i.Move(deltaX, deltaY);
            SetMBR();
        }

        /// <summary>
        /// 复制多边形
        /// </summary>
        /// <returns></returns>
        public override WEGeometry Clone()
        {
            return new WEPolygon(_Points.ToArray());
        }

        public override bool Cover(WEPoint poi)
        {
            return WEMapTools.IsPointInPolygon(poi, this);
        }

        /// <summary>
        /// 求多边形注记的位置（重心位置）
        /// </summary>
        /// <returns></returns>
        public override WEPoint[] getCenterPoint()
        {
            WEPoint center = new WEPoint(0, 0);
            return new WEPoint[1] { new WEPoint((_MBR.MinX + _MBR.MaxX) / 2, (_MBR.MinY + _MBR.MaxY) / 2) };
            /*
            for (int i = 0; i < _Points.Count; i++)
            {
                center.X += _Points[i].X;
                center.Y += _Points[i].Y;
            }
            center.X = center.X / _Points.Count;
            center.Y = center.Y / _Points.Count;
            return new WEPoint[1] { center };
            */
        }

        #endregion
    }
}
