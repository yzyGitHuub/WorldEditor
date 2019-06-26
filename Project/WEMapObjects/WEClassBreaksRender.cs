using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WEMapObjects
{
    public class WEClassBreaksRender:WEStyle
    {
        #region 字段

        private string _Field;
        private int _BreakCount;
        private double _FromSize;
        private double _ToSize;

        private List<Tuple<double, double>> _ClassValue;
        private List<WEStyle> _Symbols;

        #endregion

        #region 构造函数

        public WEClassBreaksRender()
        {
            _SymbolMethod = 3;
        }

        public WEClassBreaksRender(string field,int breakcount,double fromSize,double toSize)
        {
            _Field = field;
            _BreakCount = breakcount;
            _FromSize = fromSize;
            _ToSize = toSize;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置分级字段
        /// </summary>
        public string Field
        {
            get { return _Field; }
            set { _Field = value; }
        }

        /// <summary>
        /// 获取或设置级数
        /// </summary>
        public int BreakCount
        {
            get { return _BreakCount; }
            set { _BreakCount = value; }
        }

        /// <summary>
        /// 获取或设置最小尺寸
        /// </summary>
        public double FromSize
        {
            get { return +_FromSize; }
            set { _FromSize = value; }
        }

        /// <summary>
        /// 获取或设置最大尺寸
        /// </summary>
        public double ToSize
        {
            get { return _ToSize; }
            set { _ToSize = value; }
        }

        /// <summary>
        /// 获取或设置唯一值数组
        /// </summary>
        public List<Tuple<double, double>> ClassValue
        {
            get { return _ClassValue; }
            set { _ClassValue = value; }
        }

        /// <summary>
        /// 获取或设置对应的符号样式
        /// </summary>
        public List<WEStyle> Symbols
        {
            get { return _Symbols; }
            set { _Symbols = value; }
        }

        #endregion


    }
}
