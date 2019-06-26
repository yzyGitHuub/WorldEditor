using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WEMapObjects
{
    public class WELayer
    {
        #region 字段

        // 文件级属性
        protected int _ID;                              // 图层在地图中的编号
        protected string _LayerName;                    // 图层在地图中显示的名字
        protected string _FileName;                     // 图层的物理存储
        protected string _Description = "Layer";        // 图层描述
        protected FeatureType _FeatureType;             // 图层类型

        // 图层级属性
        protected WEStyle _LayerStyle = WEMapTools.DefaultStyle;                  // 图层渲染样式
        protected bool _Visible = true;                 // 是否可见
        protected bool _Selectable = true;              // 是否可选
        protected WERectangle _MBR = null;              // 图层的外包矩形

        #endregion

        #region 构造函数

        public WELayer()
        {
            _FeatureType = FeatureType.WENULL;
        }

        public WELayer(string filePath, FeatureType layerType = FeatureType.WENULL)
        {
            string fileType = filePath.Split('.').Last();
            if (fileType == "shp")
            {
                WELayer newLayer = WEIO.ReadLayer(filePath);
                this._ID = newLayer.ID;
                this._LayerName = newLayer.LayerName;
                this._FileName = filePath;
                this._Description = newLayer.Description;
                this._FeatureType = newLayer.FeatureType;
                this._LayerStyle = newLayer.SymbolStyle;
                this._MBR = newLayer._MBR;
                this._Visible = true;
                this._Selectable = true;
            }
            else if (fileType == "tif")
                ;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置ID
        /// </summary>        
        public int ID
        {
            get { return _ID; }
            set
            {
                if (value >= 0)
                    _ID = value;
                else
                    throw new Exception("错误：图层的 ID 应该是非负整数.");
            }
        }

        /// <summary>
        /// 获取或设置图层名
        /// </summary>
        public string LayerName
        {
            get { return _LayerName; }
            set { _LayerName = value; }
        }

        /// <summary>
        /// 获取要素类型
        /// </summary>
        public FeatureType FeatureType
        {
            get { return _FeatureType; }
            set
            {
                _FeatureType = value;
                Console.WriteLine("FeatureType 被修改，可能会导致意外的错误.");
            }
        }

        /// <summary>
        /// 获取或设置要素存储路径
        /// </summary>
        public string LayerPath
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        /// <summary>
        /// 获取或设置图层描述
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        /// <summary>
        /// 获取或设置图层是否可见
        /// </summary>
        public bool Visible
        {
            get { return _Visible; }
            set { _Visible = value; }
        }

        /// <summary>
        /// 获取或者设置图层是否可以执行选择操作
        /// </summary>
        public bool Selectable
        {
            get { return _Selectable; }
            set { _Selectable = value; }
        }

        /// <summary>
        /// 获取或设置图层样式
        /// </summary>
        public WEStyle SymbolStyle
        {
            get { return _LayerStyle; }
            set { _LayerStyle = value; }
        }

        /// <summary>
        /// 获取图层外包矩形
        /// </summary>
        public WERectangle MBR
        {
            get { return _MBR; }
            set
            {
                if (_MBR == null)
                    _MBR = value;
                else
                    throw new Exception("MBR 只有在空的情况下才能重置.");
            }
        }

        #endregion

        #region 方法

        public virtual WEFeature[] SelectByBox(WERectangle box)
        {
            throw new Exception("不能直接对未定义的对象进行选择.");
        }

        public virtual void Draw(System.Windows.Forms.PaintEventArgs e)
        {
            if (_MBR == null)
                return;
        }


        #endregion
    }
}
