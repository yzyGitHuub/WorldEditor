using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WEMapObjects
{
    /// <summary>
    /// 线要素
    /// </summary>
    public class WEMultiPolyline : WEGeometry
    {
        #region 字段

        private List<WEGeometry> _Polylines = new List<WEGeometry>();

        #endregion

        #region 构造函数

        public WEMultiPolyline()
        {
            _FeatureType = FeatureType.WEMultiPolyline;
            _Polylines.Add(new WEPolyline());
        }

        public WEMultiPolyline(WEGeometry[] polylines)
        {
            foreach (var i in polylines)
                if (i.FeatureType == FeatureType.WEPolyline)
                    _Polylines.Add(i);
                else
                    throw new Exception("不能用 WEPolyline 以外的对象生成 WEMulyiPolyline.");
            _FeatureType = FeatureType.WEMultiPolyline;
            SetMBR();
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置多线
        /// </summary>
        public WEGeometry[] Polylines
        {
            get { return _Polylines.ToArray(); }
            set
            {
                _Polylines.Clear();
                _Polylines.AddRange(value);
                SetMBR();
            }
        }

        /// <summary>
        /// 获取点数
        /// </summary>
        public override int PointCount
        {
            get
            {
                int sum = 0;
                foreach (var i in _Polylines)
                    sum += i.PointCount;
                return sum;
            }
        }

        #endregion
        /// <summary>
        /// 获取外包矩形
        /// </summary>
        /// <returns></returns>

        #region 方法

        /// <summary>
        /// 重置外包矩形
        /// </summary>
        public override WERectangle SetMBR()
        {
            _MBR = null;
            foreach (var i in _Polylines)
                _MBR += i.MBR;
            return _MBR;
        }

        /// <summary>
        /// 移动多线
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public override void Move(double deltaX, double deltaY)
        {
            for (int i = 0; i < _Polylines.Count; i++)
            {
                _Polylines[i].Move(deltaX, deltaY);
            }
            _MBR = new WERectangle(_MBR.MinX + deltaX, _MBR.MaxX + deltaX, _MBR.MinY + deltaY, _MBR.MaxY + deltaY);
        }

        /// <summary>
        /// 移动指定线
        /// </summary>
        /// <param name="index"></param>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public void Move(int index, double deltaX, double deltaY)
        {
            _Polylines[index].Move(deltaX, deltaY);
            SetMBR();
        }

        /// <summary>
        /// 在多线序列末尾增加一条线
        /// </summary>
        /// <param name="polyline"></param>
        public override void Add(WEGeometry polyline)
        {
            if (polyline.FeatureType != FeatureType.WEPolyline)
                throw new Exception("不能给 WEMultiPolyline 添加 WEPolyline 以外的元素.");
            _Polylines.Add(polyline);
            _MBR += polyline.MBR;
        }

        /// <summary>
        /// 删除某条线
        /// </summary>
        /// <param name="index"></param>
        public void DeletePolyline(int index)
        {
            if (index < _Polylines.Count)
            {
                _Polylines.RemoveAt(index);
                SetMBR();
            }
            else
                throw new Exception("Index out of range.");
        }

        /// <summary>
        /// 获取指定索引号的线
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public WEGeometry GetPolyline(int index)
        {
            if (index < _Polylines.Count)
                return _Polylines[index];
            else
                throw new Exception("Index out of range.");
        }

        /// <summary>
        /// 清除所有线
        /// </summary>
        public override void Clear()
        {
            _Polylines.Clear();
            _MBR = null;
        }

        /// <summary>
        /// 复制多线
        /// </summary>
        /// <returns></returns>
        public override WEGeometry Clone()
        {
            WEMultiPolyline newWEMultiPolyline = new WEMultiPolyline(_Polylines.ToArray());
            return newWEMultiPolyline;
        }

        public override void Draw(PaintEventArgs e, WEStyle style, string str)
        {
            Graphics g = e.Graphics;
            foreach (var i in _Polylines)
            {
                //foreach(var poi in ((WEPolyline)i).Points)
                //g.FillRectangle(new SolidBrush(style.FromColor), WEMapTools.FromMapPoint(poi).X - 2, WEMapTools.FromMapPoint(poi).Y - 2, 4, 4);
                Pen penn = new Pen(new SolidBrush(style.FromColor), (float)style.BoundaryWidth);
                switch (style.SymbolStyle)
                {
                    case 0:
                        penn.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                        break;
                    case 1:
                        penn.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                        penn.DashPattern = new float[] { 5, 5 };
                        break;
                }
                for (int j = 0; j < ((WEPolyline)i).Points.Length - 1; j++)
                {
                    g.DrawLine(penn, WEMapTools.FromMapPoint(((WEPolyline)i).Points[j]), WEMapTools.FromMapPoint(((WEPolyline)i).Points[j + 1]));
                }
                
            }
            //g.DrawString(str, new Font("微软雅黑", 8), new SolidBrush(Color.Black), WEMapTools.FromMapPoint(new WEPoint(MBR.MinX + MBR.Width / 2, MBR.MinY + MBR.Height /2)));
        }

        public override bool Cover(WEPoint poi)
        {
            return WEMapTools.IsPointOnMultiPolyline(poi, this);
        }

        /// <summary>
        /// 求多线的中点集合
        /// </summary>
        /// <returns></returns>
        public override WEPoint[] getCenterPoint()
        {
            List<WEPoint> center = new List<WEPoint> { };
            if(PointCount!= 0)
            {
                foreach (var i in _Polylines)
                    center.AddRange(i.getCenterPoint());
            }
            return center.ToArray();
        }

        #endregion
    }
}
