using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WEMapObjects
{
    public enum FeatureType
    {
        WENULL,
        WEPoint,
        WEMultiPoint,
        WEPolyline,
        WEMultiPolyline,
        WEPolygon,
        WEMultiPolygon,
        WEEntityPoint,
        WEEntityPolyline,
        WEEntityPolygon,
        WERaster,
    }

    /// <summary>
    /// 地理要素对象
    /// </summary>
    public class WEFeature
    {
        #region 字段

        protected int _ID;
        protected FeatureType _FeatureType;
        protected WEGeometry _Geometries= new WEGeometry();
        protected Dictionary<string, object> _Attributes = new Dictionary<string, object>();
        protected WERectangle _MBR;
        protected WELabel _Label = new WELabel();

        #endregion

        #region 构造函数

        public WEFeature()
        {
            _FeatureType = FeatureType.WENULL;
        }

        public WEFeature(int id, WEGeometry fea, Dictionary<string, object> attributes)
        {
            _FeatureType = FeatureType.WENULL;
            _ID = id;
            _Geometries = fea;
            foreach (var i in attributes)
                _Attributes.Add(i.Key, i.Value);
        }
        
        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置ID
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        /// <summary>
        /// 获取外包矩形
        /// </summary>
        public WERectangle MBR
        {
            get { return _Geometries.MBR; }
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
        /// 获取或重置内部要素
        /// </summary>
        public WEGeometry Geometries
        {
            get
            {
                return _Geometries;
            }
            set { _Geometries = value; }
        }

        /// <summary>
        /// 获取属性字段
        /// </summary>
        public Dictionary<string, object> Attributes
        {
            get { return _Attributes; }
            set { _Attributes = value; }
        }

        #endregion

        #region 公有的方法

        /// <summary>
        /// 获取某一字段值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetField(string key)
        {
            return _Attributes[key];
        }

        /// <summary>
        /// 增加字段
        /// </summary>
        /// <param name="newField"></param>
        public void AddField(string newField)
        {
            _Attributes.Add(newField, "");
        }

        /// <summary>
        /// 删除字段
        /// </summary>
        /// <param name="index"></param>
        public void DeleteField(string key)
        {
            _Attributes.Remove(key);
        }

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newValue"></param>
        public void AlterField(string key, object newValue)
        {
            _Attributes[key] = newValue;
        }

        #endregion

        #region 需要重写的方法

        public virtual WEFeature Clone()
        {
            return new WEFeature(_ID, _Geometries, _Attributes);
        }

        public virtual void Draw(System.Windows.Forms.PaintEventArgs e)
        {
            if (_Geometries  == null)
                return;
            _Geometries.Draw(e,WEMapTools.DefaultStyle,"");
        }


        public virtual void Draw(System.Windows.Forms.PaintEventArgs e, bool i)
        {
            if (_Geometries == null)
                return;
            try
            {
                _Geometries.Draw(e, new WEStyle(1, 1, Color.Cyan, Color.Red, Color.Red, 10, 2), "");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // 查询渲染等等的依赖项

        #endregion

    }


}
