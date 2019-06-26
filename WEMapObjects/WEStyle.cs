using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WEMapObjects
{
    public class WEStyle
    {
        #region 字段

        protected int _SymbolMethod;
        protected int _SymbolStyle;
        protected Color _BoundaryColor = new Color();
        protected Color _FromColor = new Color();
        protected Color _ToColor = new Color();
        protected double _BoundaryWidth;
        protected double _Size;


        #endregion

        #region 构造函数

        public WEStyle() { }

        public WEStyle(int symbolmethod, int symbolstyle, Color boundarycolor,Color fromcolor,Color tocolor,double size, double boundaryWidth)
        {
            _SymbolMethod = symbolmethod;
            _SymbolStyle = symbolstyle;
            _BoundaryColor = boundarycolor;
            _FromColor = fromcolor;
            _ToColor = tocolor;
            _Size = size;
            _BoundaryWidth = boundaryWidth;
            LabelVisible = false;
        }

        #endregion

        #region 属性

        public bool LabelVisible = false;

        /// <summary>
        /// 获取或设置样式格式   1 单一样式    2 唯一值渲染     3 分级渲染
        /// </summary>
        public int SymbolMethod
        {
            get { return _SymbolMethod; }
            set { _SymbolMethod = value; }
        }

        /// <summary>
        /// 获取或设置符号     对点或线不同，对面状符号，该值为0
        /// </summary>
        public int SymbolStyle
        {
            get { return _SymbolStyle; }
            set { _SymbolStyle = value; }
        }

        /// <summary>
        /// 获取或设置边界色
        /// </summary>
        public Color BoundaryColor
        {
            get { return _BoundaryColor; }
            set { _BoundaryColor = value; }
        }

        /// <summary>
        /// 获取或设置起始颜色
        /// </summary>
        public Color FromColor
        {
            get { return _FromColor; }
            set { _FromColor = value; }
        }

        /// <summary>
        /// 获取或设置终止颜色
        /// </summary>
        public Color ToColor
        {
            get { return _ToColor; }
            set { _ToColor = value; }
        }

        /// <summary>
        /// 获取或设置尺寸大小
        /// </summary>
        public double Size
        {
            get { return _Size; }
            set { _Size = value; }
        }

        /// <summary>
        /// 获取或设置边界宽度
        /// </summary>
        public double BoundaryWidth
        {
            get { return _BoundaryWidth; }
            set { _BoundaryWidth = value; }
        }

        #endregion

        #region 方法


        #endregion

    }

    public class WELabel
    {
        #region 字段

        private string _Text;   //选择的配置注记的字段
        private Font _Font = new Font("宋体", 5);     //注记字体（样式、大小）
        private Color _Color;   //注记颜色

        #endregion

        #region 构造函数

        public WELabel()
        {

        }

        public WELabel(string text, Font font, Color color)
        {
            _Text = text;
            _Font = font;
            _Color = color;
        }

        #endregion

        #region 属性

        public string Text
        {
            get { return _Text; }
            set { _Text = value.ToString(); }
        }

        public Font Font
        {
            get { return _Font; }
            set { _Font = value; }
        }

        public Color Color
        {
            get { return _Color; }
            set { _Color = value; }
        }

        #endregion

    }

}
