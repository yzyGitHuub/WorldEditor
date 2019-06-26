using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WEMapObjects
{
    public class WERectangle
    {
        #region 字段

        private double _MinX = 0, _MaxX = 0, _MinY = 0, _MaxY = 0;

        #endregion

        #region 构造函数

        public WERectangle(double minX, double maxX, double minY, double maxY)
        {
            if (minX > maxX || minY > maxY)  //非法输入，强制应用程序中断
            {
                throw new Exception("Invalid rectangle.");
            }
            else
            {
                _MinX = minX;
                _MaxX = maxX;
                _MinY = minY;
                _MaxY = maxY;
            }
        }
        #endregion

        #region 属性

        /// <summary>
        /// 获取最小X坐标
        /// </summary>
        public double MinX
        {
            get { return _MinX; }
        }

        /// <summary>
        /// 获取最小Y坐标
        /// </summary>
        public double MinY
        {
            get { return _MinY; }
        }

        /// <summary>
        /// 获取最大X坐标
        /// </summary>
        public double MaxX
        {
            get { return _MaxX; }
        }

        /// <summary>
        /// 获取最大Y坐标
        /// </summary>
        public double MaxY
        {
            get { return _MaxY; }
        }

        /// <summary>
        /// 获取矩形宽度
        /// </summary>
        public double Width
        {
            get { return (_MaxX - _MinX); }
        }

        /// <summary>
        /// 获取矩形高度
        /// </summary>
        public double Height
        {
            get { return (_MaxY - _MinY); }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 重置外包矩形
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="maxX"></param>
        /// <param name="minY"></param>
        /// <param name="maxY"></param>
        public void Set(double minX, double maxX, double minY, double maxY)
        {
            if (minX > maxX || minY > maxY)  //非法输入，强制应用程序中断
            {
                throw new Exception("Invalid rectangle.");
            }
            else
            {
                _MinX = minX;
                _MaxX = maxX;
                _MinY = minY;
                _MaxY = maxY;
            }
        }

        /// <summary>
        /// 外包矩形合并
        /// </summary>
        /// <param name="one"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static WERectangle operator + (WERectangle one, WERectangle other)
        {
            if (one == null)
                return other;
            if (other == null)
                return one;
            double minx = one.MinX, miny = one.MinY, maxx = one.MaxX, maxy = one.MaxY;
            if (one.MinX > other.MinX)
                minx = other.MinX;
            if (one.MinY > other.MinY)
                miny = other.MinY;
            if (one.MaxX < other.MaxX)
                maxx = other.MaxX;
            if (one.MaxY < other.MaxY)
                maxy = other.MaxY;
            return new WERectangle(minx, maxx, miny, maxy);
        }

        /// <summary>
        /// 外包矩形比较
        /// </summary>
        /// <param name="one"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool operator == (WERectangle one, WERectangle other)
        {
            if ((one as object) == null && (other as object) == null)
                return true;
            else if ((one as object) == null || (other as object) == null)
                return false;
            if (one.MinX == other.MinX && one.MinY == other.MinY && one.MaxX == other.MaxX && one.MaxY == other.MaxY)
                return true;
            return false;
        }

        /// <summary>
        /// 外包矩形比较
        /// </summary>
        /// <param name="one"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool operator !=(WERectangle one, WERectangle other)
        {
            if ((one as object) == null && (other as object) == null)
                return false;
            else if ((one as object) == null || (other as object) == null)
                return true;
            if (one.MinX != other.MinX || one.MinY != other.MinY || one.MaxX != other.MaxX || one.MaxY != other.MaxY)
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            if (this == (WERectangle)obj)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "(" + MinX.ToString() + ',' + MaxX.ToString() +','+ MinY.ToString() +','+ MaxY.ToString() + ')';
        }

        #endregion
    }
}
