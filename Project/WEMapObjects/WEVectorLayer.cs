using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace WEMapObjects
{
    public class WEVectorLayer : WELayer
    {
        #region 字段

        // 对象级属性
        protected List<WEFeature> _Features = new List<WEFeature> { };            // 图层包含的对象
        protected List<WEFeature> _ShowFeatures = new List<WEFeature> { };            // 图层包含的对象
        protected DataTable Data;                      // 图层属性表
        protected Dictionary<string, object> _Field = new Dictionary<string, object> { };
        private WELabel _Label = new WELabel();

        #endregion

        #region 构造函数

        public WEVectorLayer()
        {
            _FeatureType = FeatureType.WENULL;
            _LayerStyle = WEMapTools.DefaultStyle;
            _Visible = true;
            _Selectable = true;
            _Description = "helo world";
        }

        public WEVectorLayer(int id,string layername,string description,FeatureType type,
            WEFeature[] geometries,bool visible,bool selectable,WEStyle style)
        {
            _ID = id;
            _LayerName = layername;
            _Description = description;
            _FeatureType = type;
            _Features.AddRange(geometries);
            _Visible = visible;
            _Selectable = selectable;
            _LayerStyle = style;
        }

        #endregion

        #region 属性


        public WELabel Label
        {
            get { return _Label; }
            set { _Label = value; }
        }

        /// <summary>
        /// 获取要素数量
        /// </summary>
        public int GeometryCount
        {
            get { return _Features.Count; }
        }

        public WEFeature[] Features
        {
            get { return _Features.ToArray(); }
        }

        public Dictionary<string, object> Field
        {
            get { return _Field; }
            set { _Field = value; }
        }

        public int FieldCount
        {
            get { return _Field.Keys.Count(); }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 重置外包矩形
        /// </summary>
        public  void SetMBR()
        {
            /* 重置外包矩形的简单写法
            _MBR = null;
            foreach (var i in _Features)
                _MBR += i.MBR;
            */
            double minX, maxX, minY, maxY;
            minX = _Features[0].MBR.MinX;
            minY = _Features[0].MBR.MinY;
            maxX = minX;
            maxY = minY;
            for (int i = 0; i < _Features.Count; i++)
            {
                if (_Features[i].MBR.MinX < minX)
                    minX = _Features[i].MBR.MinX;
                if (_Features[i].MBR.MaxX > maxX)
                    maxX = _Features[i].MBR.MaxX;
                if (_Features[i].MBR.MinY < minY)
                    minY = _Features[i].MBR.MinY;
                if (_Features[i].MBR.MaxY > maxY)
                    maxY = _Features[i].MBR.MaxY;
            }
            _MBR = new WERectangle(minX, maxX, minY, maxY);
        }

        /// <summary>
        /// 移动图层中的所有要素
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public  void Move(double deltaX, double deltaY)
        {
            for (int i = 0; i < _Features.Count; i++)
            {
                _Features[i].Geometries.Move(deltaX, deltaY);
            }
            _MBR = new WERectangle(_MBR.MinX + deltaX, _MBR.MaxX + deltaX, _MBR.MinY + deltaY, _MBR.MaxY + deltaY);
        }

        /// <summary>
        /// 删除所有要素
        /// </summary>
        public void DeleteAll()
        {
            _Features.Clear();
            _MBR = null;
        }

        /// <summary>
        /// 添加要素
        /// </summary>
        /// <param name="geometry"></param>
        public void AddFeature(WEFeature geometry)
        {
            _Features.Add(geometry);
            _MBR += geometry.MBR;
        }

        /// <summary>
        /// 删除指定索引号的要素
        /// </summary>
        /// <param name="index"></param>
        public void DeleteFeature(int index)
        {
            if (index < _Features.Count)
            {
                _Features.RemoveAt(index);
                SetMBR();
            }
            else
                throw new Exception("Index out of range.");
        }

        /// <summary>
        /// 获取指定索引号的要素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public WEFeature GetFeature(int index)
        {
            if (index < _Features.Count)
                return _Features[index];
            else
                throw new Exception("Index out of range.");
        }

        /// <summary>
        /// 增加字段
        /// </summary>
        /// <param name="newField"></param>
        public void AddField(string newField)
        {
            for (int i = 0; i < _Features.Count; i++)
            {
                _Features[i].AddField(newField);
            }
            _Field = _Features[0].Attributes;
        }

        /// <summary>
        /// 删除字段
        /// </summary>
        /// <param name="index"></param>
        public void DeleteField(string key)
        {
            for (int i = 0; i < _Features.Count; i++)
            {
                _Features[i].DeleteField(key);
            }
        }

        /// <summary>
        /// 判断图层里的点是否在矩形盒内并返回部分在矩形盒内的结果
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        public override WEFeature[] SelectByBox(WERectangle box)
        {
            List<WEFeature> result = new List<WEFeature> { };
            switch(_FeatureType)
            {
                case FeatureType.WEEntityPoint:
                    foreach (var i in _Features)
                        if (WEMapTools.IsMultiPointPartiallyWithinBox((WEMultiPoint)i.Geometries, box))
                            result.Add(i);
                    break;
                //case FeatureType.WEEntityPolyline:
                case FeatureType.WEMultiPolyline:
                    foreach (var i in _Features)
                        if (WEMapTools.IsMultiPolylinePartiallyWithinBox((WEMultiPolyline)i.Geometries, box))
                            result.Add(i);
                    break;
                //case FeatureType.WEEntityPolygon:
                case FeatureType.WEMultiPolygon:
                    foreach (var i in _Features)
                        if (WEMapTools.IsMultiPolygonPartiallyWithinBox((WEMultiPolygon)i.Geometries, box))
                            result.Add(i);
                    break;
            }
            return result.ToArray();
        }

        /// <summary>
        /// 复制图层
        /// </summary>
        /// <returns></returns>
        public WEVectorLayer Clone()
        {
            return new WEVectorLayer(_ID, _LayerName, _Description, _FeatureType, _Features.ToArray(), _Visible, _Selectable, _LayerStyle);
        }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="e"></param>
        public override void Draw(PaintEventArgs e)
        {
            if (!Visible)
                return;
            //Graphics g = e.Graphics;
            switch (_LayerStyle.SymbolMethod)
            {
                case 1:
                    foreach (var i in _ShowFeatures)
                        i.Geometries.Draw(e, _LayerStyle, "");
                    break;
                case 2:
                    foreach (var i in _ShowFeatures)
                        i.Geometries.Draw(e, ((WEUniqueValueRender)_LayerStyle).Symbols[
                            ((WEUniqueValueRender)_LayerStyle).UniqueValue.IndexOf(i.Attributes[((WEUniqueValueRender)_LayerStyle).Field].ToString())
                            ], "");
                    break;
                case 3:
                    WEClassBreaksRender style = (WEClassBreaksRender)_LayerStyle;
                    if (_ShowFeatures.Count == 0)
                        return;
                    if(style.Field == "FID")
                    {
                        double diff = _Features.Count() * 1.0 / style.BreakCount;
                        for(int i = 0; i < _ShowFeatures.Count; i++)
                        {
                            int rank = (int)Math.Ceiling(_ShowFeatures[i].ID / diff + 0.1) - 1;
                            Color FromColor = Color.FromArgb(
                                (int)style.FromColor.R + ((int)style.ToColor.R - (int)style.FromColor.R) / (style.BreakCount - rank),
                                (int)style.FromColor.G + ((int)style.ToColor.G - (int)style.FromColor.G) / (style.BreakCount - rank),
                                (int)style.FromColor.B + ((int)style.ToColor.B - (int)style.FromColor.B) / (style.BreakCount - rank)
                                );

                            WEStyle newSty = new WEStyle(style.SymbolMethod, style.SymbolStyle, style.BoundaryColor,
                                FromColor, FromColor, style.Size, style.BoundaryWidth);
                            _Features[i].Geometries.Draw(e, newSty, "");
                        }
                    }
                    else
                    {
                        double min = Convert.ToDouble(_ShowFeatures[0].Attributes[style.Field]), max = Convert.ToDouble(_ShowFeatures[0].Attributes[style.Field]);
                        //double min = (double)_ShowFeatures[0].Attributes[style.Field], max = (double)_ShowFeatures[0].Attributes[style.Field];
                        for (int i = 0; i < _Features.Count(); i++)
                        {
                            double helo = Convert.ToDouble(_Features[i].Attributes[style.Field]);
                            if (helo > max)
                                max = helo;
                            if (helo < min)
                                min = helo;
                        }
                        double diff = (max - min) / style.BreakCount;
                        foreach (var i in _ShowFeatures)
                        {
                            int rank = (int)((Convert.ToDouble(i.Attributes[style.Field]) - min - 0.0000001) / diff);
                            Color FromColor = Color.FromArgb(
                                (int)style.FromColor.R + ((int)style.ToColor.R - (int)style.FromColor.R) / (style.BreakCount - rank),
                                (int)style.FromColor.G + ((int)style.ToColor.G - (int)style.FromColor.G) / (style.BreakCount - rank),
                                (int)style.FromColor.B + ((int)style.ToColor.B - (int)style.FromColor.B) / (style.BreakCount - rank)
                                );

                            WEStyle newSty = new WEStyle(style.SymbolMethod, style.SymbolStyle, style.BoundaryColor,
                                FromColor, FromColor, style.Size, style.BoundaryWidth);
                            //_Features[i].Geometries.Draw(e, newSty, "");

                            //WEStyle newSty = style.Symbols[(int)(((double)(i.Attributes[style.Field]) - min - 0.0000001) / diff)];
                            i.Geometries.Draw(e, newSty, "");
                        }
                    }
                    break;
            }
            if (_LayerStyle.LabelVisible)
                DrawLabel(e);
        }

        /// <summary>
        /// 绘制注记
        /// </summary>
        /// <param name="e"></param>
        public void DrawLabel(PaintEventArgs e)
        {
            
            Graphics g = e.Graphics;
            string field = _Label.Text;
            for (int i = 0; i < _Features.Count; i++)      //对图层中每一个要素绘制注记
            {
                string text = Data.Rows[i][field].ToString();
                PointF location = WEMapTools.FromMapPoint(_Features[i].Geometries.getCenterPoint()[0]);
                if (_FeatureType == FeatureType.WEEntityPoint || _FeatureType == FeatureType.WEPoint)
                {
                    location.Y -= Label.Font.Size;  //点要素，将注记向下移半个字体大小
                    location.X -= text.Length * Label.Font.Size / 2;
                }
                g.DrawString(text, Label.Font, new SolidBrush(Label.Color), location.X, location.Y);
            }
        }

        /// <summary>
        /// 重选要展示的对象
        /// </summary>
        public void SelectDisplay()
        {
            _ShowFeatures.Clear();
            _ShowFeatures.AddRange(SelectByBox(WEMapTools.DisplayMBR));
        }

        /// <summary>
        /// 重新加载属性数据
        /// </summary>
        public void InitializeData()
        {
            Data = new DataTable(_LayerName);
            foreach(var i in _Field)
                Data.Columns.Add(i.Key, i.Value.GetType());
            foreach (var i in _Features)
                Data.Rows.Add(i.Attributes.Values.ToArray());
        }

        public void ReSetID()
        {
            for (int i = 0; i < _Features.Count; i++)
                _Features[i].ID = i;
        }

        /// <summary>
        /// 获取属性数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable()
        {
            Data = new DataTable();
            Data.Columns.Add("FID");
            foreach (var i in Field)
                Data.Columns.Add(i.Key);
            for (int i = 0; i < Features.Length; i++)
            {
                Data.Rows.Add();
                Data.Rows[i][0] = Features[i].ID;
                for (int j = 1; j <= Features[i].Attributes.Count; j++)
                {
                    Data.Rows[i][j] = Features[i].Attributes.Values.ToArray()[j - 1].ToString();
                }
            }
            /*
            foreach(var i in Features)
            {
                //Data.Rows.Add("FID", i.ID);
                //Data.Rows.Add(i.Attributes.Values.ToArray());
                Data.Rows
                for ()
            }*/
            return Data;
        }

        /// <summary>
        /// 返回指定id数组对应的要素，2019/06/11，by Ganmin Yin
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public WEFeature[] SelectById(int[] ids)
        {
            List<WEFeature> result = new List<WEFeature> { };
            foreach (int id in ids)
            {
                foreach (WEFeature feature in _Features)
                {
                    if (feature.ID == id)
                    {
                        result.Add(feature);
                        break;
                    }
                }
            }
            return result.ToArray();
        }

        #endregion

    }

    public class WEPointLayer : WEVectorLayer
    {
        public WEPointLayer()
        {
            _FeatureType = FeatureType.WEEntityPoint;
        }
    }
    public class WEPolylineLayer : WEVectorLayer
    {
        public WEPolylineLayer()
        {
            _FeatureType = FeatureType.WEEntityPolyline;
        }
    }
    public class WEPolygonLayer : WEVectorLayer
    {
        public WEPolygonLayer()
        {
            _FeatureType = FeatureType.WEEntityPolygon;
        }
    }
}
