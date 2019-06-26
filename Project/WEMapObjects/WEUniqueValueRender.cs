using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WEMapObjects
{
    public class WEUniqueValueRender:WEStyle
    {
        #region 字段

        private string _Field;
        private List<string> _UniqueValue;
        private List<WEStyle> _Symbols;

        #endregion

        #region 构造函数

        public WEUniqueValueRender()
        {
            _SymbolMethod = 2;
        }

        public WEUniqueValueRender(string field,List<string> uniqueVal,List<WEStyle> symbols)
        {
            _Field = field;
            _UniqueValue = uniqueVal;
            _Symbols = symbols;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置绑定字段
        /// </summary>
        public string Field
        {
            get { return _Field; }
            set { _Field = value; }
        }

        /// <summary>
        /// 获取或设置唯一值数组
        /// </summary>
        public List<string> UniqueValue
        {
            get { return _UniqueValue; }
            set { _UniqueValue = value; }
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
