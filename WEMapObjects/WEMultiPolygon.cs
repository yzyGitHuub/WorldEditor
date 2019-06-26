using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WEMapObjects
{
    /// <summary>
    /// 面要素
    /// </summary>
    public class WEMultiPolygon : WEGeometry
    {
        #region 字段

        private List<WEGeometry> _Polygons = new List<WEGeometry>();
        
        #endregion

        #region 构造函数

        public WEMultiPolygon()
        {
            _FeatureType = FeatureType.WEMultiPolygon;
            _Polygons = new List<WEGeometry> { new WEPolygon() };
        }

        public WEMultiPolygon(WEGeometry[] polygons)
        {
            if(polygons.Count() == 0)
                _Polygons = new List<WEGeometry> { new WEPolygon() };
            foreach (var i in polygons)
                if (i.FeatureType == FeatureType.WEPolygon)
                    _Polygons.Add(i);
                else
                    throw new Exception("不能用 WEPolygon 以外的对象生成 WEMulyiPolygon.");
            _FeatureType = FeatureType.WEMultiPolygon;
            SetMBR();
        }

        #endregion

        #region 属性
        
        /// <summary>
        /// 获取或设置组成复合多边形的多边形
        /// </summary>
        public WEGeometry[] Polygons
        {
            get { return _Polygons.ToArray(); }
            set { _Polygons.AddRange(value); SetMBR(); }
        }

        /// <summary>
        /// 获取点数
        /// </summary>
        public override int PointCount
        {
            get
            {
                int sum = 0;
                foreach (var i in _Polygons)
                    sum += i.PointCount;
                return sum;
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
            _MBR = null;
            foreach (var i in _Polygons)
                _MBR += i.MBR;
            return _MBR;
        }

        /// <summary>
        /// 移动复合多边形
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public override void Move(double deltaX, double deltaY)
        {
            for (int i = 0; i < _Polygons.Count; i++)
            {
                _Polygons[i].Move(deltaX, deltaY);
            }
            _MBR = new WERectangle(_MBR.MinX + deltaX, _MBR.MaxX + deltaX, _MBR.MinY + deltaY, _MBR.MaxY + deltaY);
        }

        /// <summary>
        /// 移动指定多边形
        /// </summary>
        /// <param name="index"></param>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public void Move(int index, double deltaX, double deltaY)
        {
            _Polygons[index].Move(deltaX, deltaY);
            SetMBR();
        }

        /// <summary>
        /// 在复合多边形序列末尾增加一个简单多边形边界
        /// </summary>
        /// <param name="polygon"></param>
        public override void Add(WEGeometry polygon)
        {
            if (polygon.FeatureType != FeatureType.WEPolygon)
                throw new Exception("不能给 WEMultiPolygon 添加 WEPolygon 以外的元素.");
            _Polygons.Add(polygon);
            _MBR += polygon.MBR;
        }

        /// <summary>
        /// 删除某个简单多边形
        /// </summary>
        /// <param name="index"></param>
        public void DeletePolygon(int index)
        {
            if (index < _Polygons.Count)
            {
                _Polygons.RemoveAt(index);
                SetMBR();
            }
            else
                throw new Exception("Index out of range.");
        }

        /// <summary>
        /// 获取指定索引号的简单多边形
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public WEGeometry GetPolygon(int index)
        {
            if (index < _Polygons.Count)
                return _Polygons[index];
            else
                throw new Exception("Index out of range.");
        }

        /// <summary>
        /// 清除所有多边形
        /// </summary>
        public override void Clear()
        {
            _Polygons.Clear();
            _MBR = null;
        }

        /// <summary>
        /// 复制复合多边形
        /// </summary>
        /// <returns></returns>
        public override WEGeometry Clone()
        {
            WEMultiPolygon newWEMultiPolygon = new WEMultiPolygon(_Polygons.ToArray());
            return newWEMultiPolygon;
        }

        public override void Draw(PaintEventArgs e, WEStyle style, string str)
        {
            Graphics g = e.Graphics;
            GraphicsPath path = new GraphicsPath();
            if (PointCount == 0)
                return;
            foreach (var i in _Polygons)
            {
                if (i.PointCount == 0)
                    continue;
                //foreach (var poi in ((WEPolygon)i).Points)
                    //g.FillRectangle(new SolidBrush(style.FromColor), WEMapTools.FromMapPoint(poi).X - 2, WEMapTools.FromMapPoint(poi).Y - 2, 4, 4);

                GraphicsPath _path = new GraphicsPath();
                List<PointF> temp = new List<PointF>();
                foreach (var j in ((WEPolygon)i).Points)
                    temp.Add(WEMapTools.FromMapPoint(j));
                temp.Add(temp.First());
                _path.AddLines(temp.ToArray());
                /*
                for (int j = 0; j < ((WEPolygon)i).Points.Length - 1; j++)
                {
                    _path.AddLine(WEMapTools.FromMapPoint(((WEPolygon)i).Points[j]), WEMapTools.FromMapPoint(((WEPolygon)i).Points[j + 1]));
                    //g.DrawLine(new Pen(new SolidBrush(Color.Black), 1), WEMapTools.FromMapPoint(((WEPolygon)i).Points[j]), WEMapTools.FromMapPoint(((WEPolygon)i).Points[j + 1]));
                }
                _path.AddLine(WEMapTools.FromMapPoint(((WEPolygon)i).Points.Last()), WEMapTools.FromMapPoint(((WEPolygon)i).Points.First()));
                //g.DrawLine(new Pen(new SolidBrush(Color.Black), 1), WEMapTools.FromMapPoint(((WEPolygon)i).Points.Last()), WEMapTools.FromMapPoint(((WEPolygon)i).Points.First()));
                */
                path.AddPath(_path, false);
            }
            g.FillPath(new SolidBrush(style.FromColor), path);
            g.DrawPath(new Pen(new SolidBrush(style.BoundaryColor), (float)style.BoundaryWidth), path);
            //g.DrawString(str, new Font("微软雅黑", 8), new SolidBrush(Color.Black), WEMapTools.FromMapPoint(new WEPoint(MBR.MinX + MBR.Width / 2, MBR.MinY + MBR.Height / 2)));
        }

        /// <summary>
        /// 求多面的中点集合
        /// </summary>
        /// <returns></returns>
        public override WEPoint[] getCenterPoint()
        {
            List<WEPoint> center = new List<WEPoint> { };
            if (PointCount != 0)
            {
                foreach (var i in _Polygons)
                    center.AddRange(i.getCenterPoint());
            }
            return center.ToArray();
        }

        public override bool Cover(WEPoint poi)
        {
            return WEMapTools.IsPointInMultiPolygon(poi, this);
        }

        #endregion
    }
}
