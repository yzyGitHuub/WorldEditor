using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WEMapObjects
{
    /// <summary>
    /// 点要素
    /// </summary>
    public class WEMultiPoint : WEGeometry
    {
        #region 字段

        private List<WEPoint> _Points = new List<WEPoint>();

        #endregion

        #region 构造函数

        public WEMultiPoint()
        {
            _FeatureType = FeatureType.WEMultiPoint;
        }

        public WEMultiPoint(WEGeometry[] points)
        {
            _FeatureType = FeatureType.WEMultiPoint;
            foreach (var i in points)
                if (i.FeatureType == FeatureType.WEPoint)
                    _Points.Add((WEPoint)i);
                else
                    throw new Exception("不能用 WEPoint 以外的对象生成 WEMultiPoint.");
            SetMBR();
        }

        #endregion

        #region 属性
        
        /// <summary>
        /// 获取多点数目
        /// </summary>
        public override int PointCount
        {
            get { return _Points.Count; }
        }

        /// <summary>
        /// 获取或设置点序列
        /// </summary>
        public WEGeometry[] Points
        {
            get { return _Points.ToArray(); }
            set
            {
                _Points.Clear();
                _Points.AddRange((WEPoint[])value);
                SetMBR();
            }
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
            minX = ((WEPoint)_Points[0]).X;
            minY = ((WEPoint)_Points[0]).Y;
            maxX = minX;
            maxY = minY;
            for (int i = 0; i < _Points.Count; i++)
            {
                WEPoint poi = (WEPoint)_Points[i];
                if (poi.X < minX)
                    minX = poi.X;
                if (poi.X > maxX)
                    maxX = poi.X;
                if (poi.Y < minY)
                    minY = poi.Y;
                if (poi.Y > maxY)
                    maxY = poi.Y;
            }
            _MBR = new WERectangle(minX, maxX, minY, maxY);
            return _MBR;
        }

        /// <summary>
        /// 移动多点
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public override void Move(double deltaX, double deltaY)
        {
            for(int i=0;i<_Points.Count;i++)
            {
                _Points[i].Move(deltaX, deltaY);
            }
            _MBR = new WERectangle(_MBR.MinX + deltaX, _MBR.MaxX + deltaX, _MBR.MinY + deltaY, _MBR.MaxY + deltaY);
        }

        /// <summary>
        /// 移动指定点
        /// </summary>
        /// <param name="index"></param>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public void Move(int index, double deltaX, double deltaY)
        {
            _Points[index].Move(deltaX, deltaY);
            SetMBR();
        }

        /// <summary>
        /// 增加一个点
        /// </summary>
        /// <param name="point"></param>
        public override void Add(WEGeometry point)
        {
            if (point.FeatureType != FeatureType.WEPoint)
                throw new Exception("不能给 WEMultiPoint 添加 WEPoint 以外的元素.");
            _Points.Add((WEPoint)point);
            _MBR += point.MBR;
        }

        /// <summary>
        /// 删除某一点
        /// </summary>
        /// <param name="index"></param>
        public void DeletePoint(int index)
        {
            if (index < _Points.Count)
            {
                _Points.RemoveAt(index);
                SetMBR();
            }
            else
                throw new Exception("Index out of range.");
        }

        /// <summary>
        /// 获取指定索引号的点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public WEGeometry GetPoint(int index)
        {
            if (index < _Points.Count)
                return _Points[index];
            else
                throw new Exception("Index out of range.");
        }

        /// <summary>
        /// 清除所有点
        /// </summary>
        public override void Clear()
        {
            _Points.Clear();
            _MBR = null;
        }

        /// <summary>
        /// 复制多点
        /// </summary>
        /// <returns></returns>
        public override WEGeometry Clone()
        {
            WEMultiPoint newWEMultiPoint = new WEMultiPoint(_Points.ToArray());
            return newWEMultiPoint;
        }

        public override void Draw(PaintEventArgs e, WEStyle style, string str)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            PointF tem;
            foreach (var i in _Points)
            {
                switch(style.SymbolStyle)
                {
                    case 1:
                        g.DrawEllipse(new Pen(style.FromColor), WEMapTools.FromMapPoint(i).X - (float)style.Size / 2, WEMapTools.FromMapPoint(i).Y - (float)style.Size / 2, (float)style.Size, (float)style.Size);
                        break;
                    case 2:
                        g.FillEllipse(new SolidBrush(style.FromColor), WEMapTools.FromMapPoint(i).X - (float)style.Size / 2, WEMapTools.FromMapPoint(i).Y - (float)style.Size / 2, (float)style.Size, (float)style.Size);
                        break;
                    case 3:
                        g.DrawRectangle(new Pen(style.FromColor), WEMapTools.FromMapPoint(i).X - (float)style.Size / 2, WEMapTools.FromMapPoint(i).Y - (float)style.Size / 2, (float)style.Size, (float)style.Size);
                        break;
                    case 4:
                        g.FillRectangle(new SolidBrush(style.FromColor), WEMapTools.FromMapPoint(i).X - (float)style.Size / 2, WEMapTools.FromMapPoint(i).Y - (float)style.Size / 2, (float)style.Size, (float)style.Size);
                        break;
                    case 5:
                        tem = WEMapTools.FromMapPoint(i);
                        g.DrawLines(new Pen(style.FromColor), new PointF[4] {
                            new PointF(tem.X, tem.Y - (float)style.Size * 2 / 3),
                            new PointF(tem.X - (float)style.Size / 2, tem.Y + (float)style.Size / 3),
                            new PointF(tem.X + (float)style.Size / 3, tem.Y + (float)style.Size / 3),
                            new PointF(tem.X, tem.Y - (float)style.Size * 2 / 3),
                        });
                        break;
                    case 6:
                        tem = WEMapTools.FromMapPoint(i);
                        g.FillPolygon(new SolidBrush(style.FromColor), new PointF[4] {
                            new PointF(tem.X, tem.Y - (float)style.Size * 2 / 3),
                            new PointF(tem.X - (float)style.Size / 2, tem.Y + (float)style.Size / 3),
                            new PointF(tem.X + (float)style.Size / 3, tem.Y + (float)style.Size / 3),
                            new PointF(tem.X, tem.Y - (float)style.Size * 2 / 3),
                        });
                        break;
                    case 7:
                        g.DrawEllipse(new Pen(style.FromColor), WEMapTools.FromMapPoint(i).X - (float)style.Size / 2, WEMapTools.FromMapPoint(i).Y - (float)style.Size / 2, (float)style.Size, (float)style.Size);
                        g.FillRectangle(new SolidBrush(style.FromColor), WEMapTools.FromMapPoint(i).X - 0.5f, WEMapTools.FromMapPoint(i).Y - 0.5f, 1, 1);
                        break;
                    case 8:
                        g.DrawEllipse(new Pen(style.FromColor), WEMapTools.FromMapPoint(i).X - (float)style.Size / 2, WEMapTools.FromMapPoint(i).Y - (float)style.Size / 2, (float)style.Size, (float)style.Size);
                        g.DrawEllipse(new Pen(style.FromColor), WEMapTools.FromMapPoint(i).X - (float)style.Size / 3, WEMapTools.FromMapPoint(i).Y - (float)style.Size / 3, (float)style.Size / 1.5f, (float)style.Size / 1.5f);
                        break;
                }
            }


        }

        /// <summary>
        /// 求多点的中点集合
        /// </summary>
        /// <returns></returns>
        public override WEPoint[] getCenterPoint()
        {
            List<WEPoint> center = new List<WEPoint> { };
            if (PointCount != 0)
            {
                foreach (var i in _Points)
                    center.AddRange(i.getCenterPoint());
            }
            return center.ToArray();
        }

        public override bool Cover(WEPoint poi)
        {
            return WEMapTools.IsPointOnMultiPoint(poi, this);
        }

        #endregion
    }
}
