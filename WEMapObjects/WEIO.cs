using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace WEMapObjects
{
    public static class WEIO
    {
        //private static int LayerID = 0;

        #region 接口函数

        /// <summary>
        /// 读取图层数据
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static WEVectorLayer ReadLayer(string filename)
        {
            WEVectorLayer layer = new WEVectorLayer();

            layer.LayerPath = filename;

            ReadShp(ref layer, filename);
            ReadDbf(ref layer, filename);
            // 好像不太用得上？？这里还是写了下，万一用得上呢：），，
            ReadShx(ref layer, filename);

            return layer;
            //return new WEVectorLayer();
        }

        /// <summary>
        /// 保存图层数据
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="filename"></param>
        public static void SaveLayer(WEVectorLayer layer, string filename)
        {
            SaveShp(layer, filename);
            SaveDbf(layer, filename);
            SaveShx(layer, filename);
        }

        #region 自定义图层IO函数
        /// <summary>
        /// 读取自定义图层数据
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static WEVectorLayer ReadWELayer(string filename)
        {
            WEVectorLayer layer = new WEVectorLayer();

            ReadWEShp(ref layer, filename);
            ReadWEDbf(ref layer, filename);

            return layer;
        }

        /// <summary>
        /// 保存自定义图层数据
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="filename"></param>
        public static void SaveWELayer(WEVectorLayer layer, string filename)
        {
            SaveWEShp(layer, filename);
            SaveWEDbf(layer, filename);
        }

        #endregion

        /// <summary>
        /// 读取地图数据
        /// </summary>
        /// <param name="filename"></param>
        public static WEMap ReadMap(string filename)
        {
            WEMap map = new WEMap();
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            map.MapName = sr.ReadLine();                    // 读取地图名称
            map.MapDescription = sr.ReadLine();             // 读取地图描述
            int mapLayerCount = Int32.Parse(sr.ReadLine()); // 读取地图图层数
            // 逐行读取图层路径，创建图层并添加到地图中
            string layerPath;
            WEVectorLayer layer = new WEVectorLayer();
            for (int i = 0; i < mapLayerCount; i++)
            {
                try
                {
                    layerPath = sr.ReadLine();
                    layer = ReadLayer(layerPath);
                    map.AddLayer(layer);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            // 读取外包矩形信息
            map.MapTop = Double.Parse(sr.ReadLine());
            map.MapBottom = Double.Parse(sr.ReadLine());
            map.MapLeft = Double.Parse(sr.ReadLine());
            map.MapRight = Double.Parse(sr.ReadLine());

            sr.Dispose();
            fs.Dispose();

            return map;
        }

        /// <summary>
        /// 保存地图数据
        /// </summary>
        /// <param name="map"></param>
        /// <param name="filename"></param>
        public static void SaveMap(WEMap map, string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            string mapName = Regex.Replace(map.MapName, @"[/n/r]", "");
            string mapDescription = Regex.Replace(map.MapDescription, @"[/n/r]", "");
            sw.WriteLine(mapName);          // 写入地图名称
            sw.WriteLine(mapDescription);   // 写入地图描述
            sw.WriteLine(map.LayerCount);   // 写入地图图层数
            // 逐行写入各图层的路径
            for (int i = 0; i < map.LayerCount; i++)
            {
                sw.WriteLine(map.VectorLayers[i].LayerPath);
            }
            // 写入外包矩形信息
            sw.WriteLine(map.MapTop);
            sw.WriteLine(map.MapBottom);
            sw.WriteLine(map.MapLeft);
            sw.WriteLine(map.MapRight);

            sw.Dispose();
            fs.Dispose();
        }

        /// <summary>
        /// 导出地图为bmp图像
        /// </summary>
        /// <param name="map"></param>
        /// <param name="filename"></param>
        public static void Export2Bmp(WEMap map, string filename)
        {
            // 这里应该还要再考虑一下，，感觉bitmap不知道从哪里来，，
            // 
            //Bitmap bmp = new Bitmap()
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 大端整数转小端整数
        /// </summary>
        /// <param name="big"></param>
        /// <returns></returns>
        private static Int32 OnChangeByteOrder(Int32 big)
        {
            byte[] a = new byte[4];
            a[3] = (byte)(big & 0xFF);
            a[2] = (byte)((big & 0xFF00) >> 8);
            a[1] = (byte)((big & 0xFF0000) >> 16);
            a[0] = (byte)((big >> 24) & 0xFF);
            Int32 little = BitConverter.ToInt32(a, 0);
            return little;
            //return BitConverter.ToInt32(a, 0);
        }

        /// <summary>
        /// 读取shp文件
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="filename"></param>
        private static void ReadShp(ref WEVectorLayer layer, string filename)
        {
            int index = filename.LastIndexOf("\\");
            string layerName = filename.Substring(index + 1, filename.Length - index - 1 - 4);
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            // 1.读文件头

            Int32 fileMode = OnChangeByteOrder(br.ReadInt32());     // 文件编码
            br.ReadBytes(20);   // 跳过5*4个无用字节
            Int32 fileLength = OnChangeByteOrder(br.ReadInt32());   // 文件长度
            Int32 versionNum = br.ReadInt32();  // 版本号
            Int32 featureType = br.ReadInt32(); // 要素几何类型
            Double Xmin = br.ReadDouble();      // Xmin
            Double Ymin = br.ReadDouble();      // Ymin
            Double Xmax = br.ReadDouble();      // Xmax
            Double Ymax = br.ReadDouble();      // Ymax
            br.ReadBytes(32);   // 跳过Zmin、Zmax、Mmin、Mmax（double），共4*8个字节

            //layer.ID = WEVectorLayer.LayerID;
            layer.LayerName = layerName;
            layer.Visible = true;
            layer.MBR = new WERectangle(Xmin, Xmax, Ymin, Ymax);

            // 2.读实体信息

            switch (featureType)
            {
                case 1: // 点要素
                    layer.FeatureType = FeatureType.WEEntityPoint;
                    ReadShpPoint(br, ref layer);
                    break;

                case 3: // 线要素
                    layer.FeatureType = FeatureType.WEMultiPolyline;
                    ReadShpPolyline(br, ref layer);
                    break;

                case 5: // 多边形要素
                    layer.FeatureType = FeatureType.WEMultiPolygon;
                    ReadShpPolygon(br, ref layer);
                    break;
                // 暂时未找到shp文件多点要素的存储结构，好像并不用得上？
                /*
                case 8: // 多点要素
                    layer.FeatureType = FeatureType.WEMultiPoint;
                    ReadShpMultiPoint(br, ref layer);
                    break;
                */
                default: // 其他类型要素
                    throw new Exception("Unsupported feature type!");
            }

            br.Dispose();
            fs.Dispose();

        }

        #region 自定义图层IO函数
        /// <summary>
        /// 读取自定义图层的shp文件
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="filename"></param>
        private static void ReadWEShp(ref WEVectorLayer layer, string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            Int32 featureType = br.ReadInt32(); // 读取要素类型
            br.ReadBytes(8);    // 跳过中间字符
            Int32 recordNum = br.ReadInt32();   // 读取要素数量
            br.ReadBytes(8);    // 跳过中间字符
            // 读取MBR
            Double Xmin = br.ReadDouble();  // Xmin
            Double Ymin = br.ReadDouble();  // Ymin
            Double Xmax = br.ReadDouble();  // Xmax
            Double Ymax = br.ReadDouble();  // Ymax
            br.ReadBytes(8);    // 跳过中间字符

            layer.Visible = true;
            layer.MBR = new WERectangle(Xmin, Xmax, Ymin, Ymax);

            //int counter = 0;
            switch (featureType)
            {
                case 0: // 点要素，multipoint
                    layer.FeatureType = FeatureType.WEEntityPoint;
                    //ReadWEShpPoint(br, ref layer);
                    int counter = 0;
                    for (int i = 0; i < recordNum; i++)
                    {
                        Int32 partNum = br.ReadInt32();    // 读取每个多点要素的点数目
                        Double x, y;
                        WEPoint[] points = new WEPoint[partNum];
                        for (int j = 0; j < partNum; j++)
                        {
                            br.ReadByte();  // 跳过间隔字符
                            x = br.ReadDouble(); // x坐标
                            y = br.ReadDouble(); // y坐标
                            points[j] = new WEPoint(x, y);
                        }
                        WEMultiPoint multiPoint = new WEMultiPoint(points.ToArray());
                        WEEntityPoint point = new WEEntityPoint(counter, multiPoint, new Dictionary<string, object> { });  // 创建点要素
                        layer.AddFeature(point);    // 加入到图层中
                        counter++;

                        br.ReadBytes(4);    // 跳过间隔字符，位于多点要素之间
                    }
                    break;

                case 1: // 线要素，multipolyline
                    layer.FeatureType = FeatureType.WEMultiPolyline;
                    //ReadWEShpPolyline(br, ref layer);
                    counter = 0;
                    for (int i = 0; i < recordNum; i++)
                    {
                        Int32 partNum = br.ReadInt32(); // 读取每个多线要素的子线段数目
                        List<WEPolyline> polylines = new List<WEPolyline>(partNum);
                        for (int j = 0; j < partNum; j++)  // 对各个子线段的点坐标进行读取
                        {
                            br.ReadByte();  // 跳过间隔字符
                            Int32 pointNum = br.ReadInt32();    // 每个子线段的点数目
                            WEPoint[] points = new WEPoint[pointNum];
                            for (int k = 0; k < pointNum; k++)
                            {
                                br.ReadByte();  // 跳过间隔字符
                                Double x = br.ReadDouble(); // x坐标
                                Double y = br.ReadDouble(); // y坐标
                                points[k] = new WEPoint(x, y);
                            }
                            polylines.Add(new WEPolyline(points));
                        }
                        WEMultiPolyline multiPolyline = new WEMultiPolyline(polylines.ToArray());
                        WEEntityPolyline newPol = new WEEntityPolyline(counter, multiPolyline, new Dictionary<string, object>());
                        layer.AddFeature(newPol);
                        counter++;
                        br.ReadBytes(4);    // 跳过间隔字符
                    }
                    break;

                case 2: // 多边形要素，multipolygon
                    layer.FeatureType = FeatureType.WEMultiPolygon;
                    //ReadWEShpPolygon(br, ref layer);
                    counter = 0;
                    for (int i = 0; i < recordNum; i++)
                    {
                        Int32 partNum = br.ReadInt32(); // 读取每个多多边形要素的子环数目
                        List<WEPolygon> polygons = new List<WEPolygon>(partNum);
                        for (int j = 0; j < partNum; j++)  // 对各个子环的点坐标进行读取
                        {
                            br.ReadByte();  // 跳过间隔字符
                            Int32 pointNum = br.ReadInt32();    // 每个子环的点数目
                            WEPoint[] points = new WEPoint[pointNum - 1];   // 首尾点重合
                            for (int k = 0; k < pointNum - 1; k++)
                            {
                                br.ReadByte();  // 跳过间隔字符
                                Double x = br.ReadDouble(); // x坐标
                                Double y = br.ReadDouble(); // y坐标
                                points[k] = new WEPoint(x, y);
                            }
                            polygons.Add(new WEPolygon(points));
                            br.ReadBytes(1 + 8*2);   // 跳过末尾点
                        }
                        WEMultiPolygon multiPolygon = new WEMultiPolygon(polygons.ToArray());
                        WEEntityPolygon newPol = new WEEntityPolygon(counter, multiPolygon, new Dictionary<string, object>());
                        layer.AddFeature(newPol);
                        counter++;
                        br.ReadBytes(4);    // 跳过间隔字符
                    }
                    break;

                default:
                    throw new Exception("Unsupported feature type!");
            }
            br.Dispose();
            fs.Dispose();

        }   

        /// <summary>
        /// 保存自定义图层的shp文件
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="filename"></param>
        private static void SaveWEShp(WEVectorLayer layer, string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            if (layer.FeatureType == FeatureType.WEEntityPoint)
            {
                bw.Write(0);    // featureType = 0，点要素
                bw.Write((Double)0);    // 写入间隔字符
                bw.Write(layer.GeometryCount);  // 写入要素数目
                bw.Write((Double)0);    // 写入间隔字符
                // 写入MBR
                bw.Write((Double)layer.MBR.MinX);
                bw.Write((Double)layer.MBR.MinY);
                bw.Write((Double)layer.MBR.MaxX);
                bw.Write((Double)layer.MBR.MaxY);
                bw.Write((Double)0);    // 写入间隔字符

                for (int i = 0; i < layer.GeometryCount; i++)
                {
                    WEEntityPoint point = (WEEntityPoint)layer.GetFeature(i);
                    Int32 partNum = point.Geometries.PointCount;
                    bw.Write(partNum);
                    for (int j = 0; j < partNum; j++)
                    {
                        bw.Write((Byte)0);  // 写入间隔字符
                        WEMultiPoint multiPoint = (WEMultiPoint)(point.Geometries);
                        bw.Write((Double)((WEPoint)multiPoint.Points[j]).X);
                        bw.Write((Double)((WEPoint)multiPoint.Points[j]).Y);
                        //WEPoint point = (WEPoint)((WEMultiPoint)((WEEntityPoint)layer.GetFeature(i)).Geometries).Points[0];
                        //bw.Write(point.X);
                        //bw.Write(point.Y);
                        //bw.Write(point.Geometries)
                    }
                    bw.Write(0);    // 写入间隔字符
                }
            }
            else if (layer.FeatureType == FeatureType.WEMultiPolyline)
            {
                bw.Write(1);    // featureType = 1，线要素
                bw.Write((Double)0);    // 写入间隔字符
                bw.Write(layer.GeometryCount);  // 写入要素数目
                bw.Write((Double)0);    // 写入间隔字符
                // 写入MBR
                bw.Write((Double)layer.MBR.MinX);
                bw.Write((Double)layer.MBR.MinY);
                bw.Write((Double)layer.MBR.MaxX);
                bw.Write((Double)layer.MBR.MaxY);
                bw.Write((Double)0);    // 写入间隔字符

                for (int i = 0; i < layer.GeometryCount; i++)
                {
                    WEMultiPolyline multiPolyline = (WEMultiPolyline)layer.GetFeature(i).Geometries;
                    Int32 partNum = multiPolyline.Polylines.Length;
                    bw.Write(partNum);
                    for (int j = 0; j < partNum; j++)
                    {
                        bw.Write((Byte)0);
                        Int32 pointNum = multiPolyline.Polylines[j].PointCount;
                        bw.Write(pointNum);
                        for (int k = 0; k < pointNum; k++)
                        {
                            bw.Write((Byte)0);
                            bw.Write((Double)((WEPolyline)multiPolyline.GetPolyline(j)).Points[k].X);
                            bw.Write((Double)((WEPolyline)multiPolyline.GetPolyline(j)).Points[k].Y);
                        }
                    }
                    bw.Write(0);
                    
                }
            }
            else if (layer.FeatureType == FeatureType.WEMultiPolygon)
            {
                bw.Write(2);    // featureType = 3，多边形要素
                bw.Write((Double)0);    // 写入间隔字符
                bw.Write(layer.GeometryCount);  // 写入要素数目
                bw.Write((Double)0);    // 写入间隔字符
                // 写入MBR
                bw.Write((Double)layer.MBR.MinX);
                bw.Write((Double)layer.MBR.MinY);
                bw.Write((Double)layer.MBR.MaxX);
                bw.Write((Double)layer.MBR.MaxY);
                bw.Write((Double)0);    // 写入间隔字符

                for (int i = 0; i < layer.GeometryCount; i++)
                {
                    WEMultiPolygon multiPolygon = (WEMultiPolygon)layer.GetFeature(i).Geometries;
                    Int32 partNum = multiPolygon.Polygons.Length;
                    bw.Write(partNum);
                    for (int j = 0; j < partNum; j++)
                    {
                        bw.Write((Byte)0);
                        Int32 pointNum = multiPolygon.Polygons[j].PointCount;
                        bw.Write(pointNum + 1);
                        for (int k = 0; k < pointNum; k++)
                        {
                            bw.Write((Byte)0);
                            bw.Write((Double)((WEPolygon)multiPolygon.GetPolygon(j)).Points[k].X);
                            bw.Write((Double)((WEPolygon)multiPolygon.GetPolygon(j)).Points[k].Y);
                        }
                        bw.Write((Byte)0);
                        bw.Write((Double)((WEPolygon)multiPolygon.GetPolygon(j)).Points[0].X);
                        bw.Write((Double)((WEPolygon)multiPolygon.GetPolygon(j)).Points[0].Y);
                    }
                    bw.Write(0);

                }
            }
            else
            {
                throw new Exception("Unsupported feature type!");
            }
            bw.Dispose();
            fs.Dispose();
        }

        /// <summary>
        /// 读取自定义图层的dbf文件
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="filename"></param>
        private static void ReadWEDbf(ref WEVectorLayer layer, string filename)
        {
            string dbfpath = filename.Substring(0, filename.Length - 4) + ".wed";   // dbf文件路径
            FileStream fs = new FileStream(dbfpath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            // 1.读取基本信息
            Int32 recordNum = br.ReadInt32();   // 记录总数
            br.ReadBytes(8);    // 跳过间隔字符
            Int32 fieldNum = br.ReadInt32();    // 字段总数
            br.ReadBytes(8);    // 跳过间隔字符

            string[] fieldsName = new string[fieldNum];
            Int32[] fieldsType = new Int32[fieldNum];
            // 2.读取字段信息
            for (int i = 0; i < fieldNum; i++)
            {
                Byte[] fieldNameAscii = br.ReadBytes(20);   // 字段名默认为20个字节
                string fieldName = Encoding.GetEncoding("GBK").GetString(fieldNameAscii).Trim();    // 将字段名称（Ascii）转为字符串
                fieldsName[i] = fieldName;
                br.ReadByte();  // 跳过间隔字符
                fieldsType[i] = br.ReadInt32(); // 读取字段类型
                br.ReadBytes(4);    // 跳过间隔字符

                layer.AddField(fieldName);  // 添加字段
            }
            // 3.读取文件记录
            for (int i = 0; i < recordNum; i++)
            {
                for (int j = 0; j < fieldNum; j++)
                {
                    if (fieldsType[j] == 0) // 0代表int
                    {
                        Int32 fieldValue = br.ReadInt32();
                        layer.GetFeature(i).Attributes[fieldsName[j]] = fieldValue;
                        br.ReadByte();  // 跳过间隔字符
                    }
                    else if (fieldsType[j] == 1)    // 1代表double
                    {
                        Double fieldValue = br.ReadDouble();
                        layer.GetFeature(i).Attributes[fieldsName[j]] = fieldValue;
                        br.ReadByte();
                    }
                    else if (fieldsType[j] == 2)    // 2代表string
                    {
                        Byte[] fieldValueAscii = br.ReadBytes(20);
                        string fieldValue = Encoding.GetEncoding("GBK").GetString(fieldValueAscii).Trim();
                        layer.GetFeature(i).Attributes[fieldsName[j]] = fieldValue;
                        br.ReadByte();
                    }
                }
                br.ReadBytes(4);
            }
            br.Dispose();
            fs.Dispose();

        }

        /// <summary>
        /// 保存自定义图层的dbf文件
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="filename"></param>
        private static void SaveWEDbf(WEVectorLayer layer, string filename)
        {
            string dbfpath = filename.Substring(0, filename.Length - 4) + ".wed";   // dbf文件路径
            FileStream fs = new FileStream(dbfpath, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            // 1.写入基本信息
            bw.Write(layer.GeometryCount);  // 写入记录总数
            bw.Write((Double)0);
            bw.Write(layer.FieldCount);     // 写入字段总数
            bw.Write((Double)0);
            // 创建两个数组，为方便写入文件记录信息时用到
            string[] fieldsName = new string[layer.FieldCount];
            int[] fieldsType = new int[layer.FieldCount];       
            // 2.写入字段信息
            int fieldIndex = 0;
            foreach (KeyValuePair<string, object> kvp in layer.GetFeature(0).Attributes)//layer.Field.Keys)
            {
                string fieldName = kvp.Key;
                fieldName = fieldName.PadRight(20, '\0');
                fieldsName[fieldIndex] = fieldName;
                Byte[] fieldNameAscii = System.Text.Encoding.ASCII.GetBytes(fieldName); // 将字段名称转为Ascii码
                bw.Write(fieldNameAscii);
                bw.Write((Byte)0);
                Type fieldType = kvp.Value.GetType();//layer.Field[_fieldName].GetType();
                if (fieldType == typeof(Int32))
                {
                    bw.Write(0);
                    fieldsType[fieldIndex] = 0;
                }
                else if (fieldType == typeof(Double))
                {
                    bw.Write(1);
                    fieldsType[fieldIndex] = 1;
                }
                else if (fieldType == typeof(string))
                {
                    bw.Write(2);
                    fieldsType[fieldIndex] = 2;
                }
                else
                    throw new Exception("Unsupported feature type!");
                bw.Write(0);
                fieldIndex++;
            }

            // 3.写入文件记录
            for (int i = 0; i < layer.GeometryCount; i++)
            {
                for (int j = 0; j < layer.FieldCount; j++)
                {
                    if (fieldsType[j] == 0)
                    {
                        Int32 fieldValue = (Int32)layer.GetFeature(i).Attributes[fieldsName[j]];
                        bw.Write(fieldValue);
                        bw.Write((Byte)0);
                    }
                    else if (fieldsType[j] == 1)
                    {
                        Double fieldValue = (Double)layer.GetFeature(i).Attributes[fieldsName[j]];
                        bw.Write(fieldValue);
                        bw.Write((Byte)0);
                    }
                    else if (fieldsType[j] == 2)
                    {
                        string fieldValue = ((string)layer.GetFeature(i).Attributes[fieldsName[j]]).PadRight(20, '\0');
                        Byte[] fieldValueAscii = System.Text.Encoding.ASCII.GetBytes(fieldValue); // 将字段值转为Ascii码
                        bw.Write(fieldValueAscii);
                        bw.Write((Byte)0);
                    }
                    else
                        throw new Exception("Unsupported feature type!");
                }
                bw.Write(0);
            }
            bw.Dispose();
            fs.Dispose();

        }

        #endregion

        /// <summary>
        /// 读取dbf文件
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="filename"></param>
        private static void ReadDbf(ref WEVectorLayer layer, string filename)
        {
            string dbfpath = filename.Substring(0, filename.Length - 4) + ".dbf";   // dbf文件路径
            FileStream fs = new FileStream(dbfpath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);


            // 1.读文件头

            Byte version = br.ReadByte();   // 版本信息
            // 文件更新日期
            Byte year = br.ReadByte();      // 年
            Byte month = br.ReadByte();     // 月
            Byte date = br.ReadByte();      // 日

            Int32 recordNum = br.ReadInt32();       // 文件中的记录条数
            Int16 byteNum = br.ReadInt16();         // 文件头中的字节数
            Int16 recordLength = br.ReadInt16();    // 一条记录中的字节长度

            br.ReadBytes(2);    // 跳过保留字节
            br.ReadBytes(1);    // 跳过表示未完成的操作信息
            br.ReadBytes(1);    // 跳过dBase IV编密码标记
            br.ReadBytes(12);   // 跳过保留字节
            br.ReadBytes(1);    // 跳过dbf文件的MDX标识
            br.ReadBytes(1);    // 跳过language driver id
            br.ReadBytes(2);    // 跳过保留字节
            
            // 读取字段信息
            int fieldsCount = (byteNum - 32) / 32;          // 字段个数
            string[] fieldsName = new string[fieldsCount];  // 记录每个字段的名称
            Byte[] fieldsType = new Byte[fieldsCount];      // 记录每个字段的类型
            Byte[] fieldsLength = new Byte[fieldsCount];    // 记录每个字段的长度
            Byte[] fieldsDecimal = new byte[fieldsCount];   // 记录每个字段的精度
            for (int i = 0; i < fieldsCount; i++)
            {
                Byte[] fieldNameAscii = br.ReadBytes(11);    // 字段名称（Ascii）
                string fieldName = Encoding.GetEncoding("GBK").GetString(fieldNameAscii).Trim();  // 将字段名称（Ascii）转为字符串
                fieldsName[i] = fieldName;
                fieldsType[i] = br.ReadByte(); // 字段数据类型
                br.ReadBytes(4);    // 跳过保留字节
                fieldsLength[i] = br.ReadByte();    // 字段长度，二进制型
                fieldsDecimal[i] = br.ReadByte();   // 字段精度，二进制型
                br.ReadBytes(2);    // 跳过保留字节
                br.ReadBytes(1);    // 跳过工作区id
                br.ReadBytes(10);   // 跳过保留字节
                br.ReadBytes(1);    // 跳过MDX标识

                layer.AddField(fieldName);  // 添加字段
            }

            Byte terminator = br.ReadByte();    // 读取终止符


            // 2.读取文件记录

            for (int i = 0; i < recordNum; i++)
            {
                br.ReadByte();  // 跳过每条记录开头的空字符
                for (int j = 0; j < fieldsCount; j++)
                {
                    Byte[] fieldValue = br.ReadBytes(fieldsLength[j]);
                    // 原则上来说，所有B、C、D、G、N、L、M都可以认为是字符型，这里将数值型（N）单独提出，数值型依精度大小再分为整型和浮点型
                    if (fieldsType[j] == 78)    // 字段类型为数值型
                    {
                        if (fieldsDecimal[j] == 0)  // 字段类型为整型
                        {
                            try
                            {
                                string fieldValueStr = Encoding.ASCII.GetString(fieldValue);    // 字段值转成字符串
                                if (Int32.TryParse(fieldValueStr, out Int32 fieldValueInt))     // 字符型转整型
                                {
                                    layer.GetFeature(i).Attributes[fieldsName[j]] = fieldValueInt;  // 给要素设置字段值（属性值）
                                }
                            }
                            catch (Exception)   // 可能遇到浮点数或非法字符，或转换或忽略
                            {
                                try
                                {
                                    string fieldValueStr = Encoding.ASCII.GetString(fieldValue);
                                    if (Double.TryParse(fieldValueStr, out Double fieldValueDouble))
                                    {
                                        layer.GetFeature(i).Attributes[fieldsName[j]] = fieldValueDouble;
                                    }
                                }
                                catch (Exception) { }
                            }

                        }
                        else   // 字段类型为浮点型
                        {
                            try
                            {
                                string fieldValueStr = Encoding.ASCII.GetString(fieldValue);
                                if (Double.TryParse(fieldValueStr, out Double fieldValueDouble))
                                {
                                    layer.GetFeature(i).Attributes[fieldsName[j]] = fieldValueDouble;
                                }
                            }
                            catch (Exception) { }
                        }
                    }
                    else   // 其他类型，都归为字符型
                    {
                        string fieldValueStr = Encoding.GetEncoding("GBK").GetString(fieldValue).Trim();
                        layer.GetFeature(i).Attributes[fieldsName[j]] = fieldValueStr;
                    }
                }
            }

            br.Dispose();
            fs.Dispose();

        }

        /// <summary>
        /// 读取shx文件
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="filename"></param>
        private static void ReadShx(ref WEVectorLayer layer, string filename)
        {
            string shxpath = filename.Substring(0, filename.Length - 4) + ".shx";   // shx文件路径
            FileStream fs = new FileStream(shxpath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            // 1.读文件头
            // shx文件的文件头与shp文件基本一致
            Int32 fileMode = OnChangeByteOrder(br.ReadInt32());     // 文件编码
            br.ReadBytes(20);   // 跳过5*4个无用字节
            Int32 fileLength = OnChangeByteOrder(br.ReadInt32());   // 文件长度
            Int32 versionNum = br.ReadInt32();  // 版本号
            Int32 featureType = br.ReadInt32(); // 要素几何类型
            Double Xmin = br.ReadDouble();      // Xmin
            Double Ymin = br.ReadDouble();      // Ymin
            Double Xmax = br.ReadDouble();      // Xmax
            Double Ymax = br.ReadDouble();      // Ymax
            br.ReadBytes(32);   // 跳过Zmin、Zmax、Mmin、Mmax（double），共4*8个字节

            // 2.读实体信息
            // 发现并没有什么卵用，，，
            //Int32[] offsets;
            //Int32[] contentLengths;
            Int32 offset;
            Int32 contentLength;
            while (true)            // 逐个记录进行读取，直到文件末尾
            {
                // try-catch结构，当检测到流末尾错误时，表明已读到文件末尾，停止读取
                try
                {
                    offset = OnChangeByteOrder(br.ReadInt32());
                }
                catch (EndOfStreamException)
                {
                    break;
                }
                contentLength = OnChangeByteOrder(br.ReadInt32());
                
            }

            br.Dispose();
            fs.Dispose();

        }

        /// <summary>
        /// 保存shp文件
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="filename"></param>
        private static void SaveShp(WEVectorLayer layer, string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            // 1.写文件头
            
            bw.Write(OnChangeByteOrder(9994));
            for (int i = 0; i < 5; i++)
            {
                bw.Write(0);
            }
            FeatureType layerType = layer.FeatureType;   // 获取图层的要素类型
            Int32 typeCode;     // 要素类型对应的编码
            Int32 fileLength = 100;  // 文件长度，初值为头文件长度100
            Int32 recordNum = layer.GeometryCount; //要素个数
            
            // 获取要素类型编码和文件总长度
            if (layerType == FeatureType.WEEntityPoint)
            {
                typeCode = 1;
                fileLength += 28 * recordNum;   // recordNum(Int32), contentLength(Int32), shapeType(Int32), x/y(Double), 4*3+8*2 = 28
            }
            else if (layerType == FeatureType.WEMultiPolyline)
            {
                typeCode = 3;
                for (int i = 0; i < recordNum; i++)
                {
                    WEEntityPolyline multiPolyline = (WEEntityPolyline)layer.GetFeature(i);
                    fileLength += 28 + (4 + 8 * 2) * multiPolyline.Geometries.PointCount;  // 
                }
                
            }
            else if (layerType == FeatureType.WEMultiPolygon)
            {
                typeCode = 5;
                for (int i = 0; i < recordNum; i++)
                {
                    WEEntityPolygon multiPolygon = (WEEntityPolygon)layer.GetFeature(i);
                    fileLength += 28 + (4 + 8 * 2) * multiPolygon.Geometries.PointCount;   // 在定义该属性时，需要注意包含最后一个点在内，值应为polyline.pointcount+1
                }
            }
            else
            {
                throw new Exception("Unsupported feature type!");
            }

            bw.Write(OnChangeByteOrder(fileLength));    // 写入文件长度
            bw.Write(1000);     // 写入版本号
            bw.Write(typeCode); // 写入几何类型
            // 写入MBR数据
            bw.Write(layer.MBR.MinX);
            bw.Write(layer.MBR.MinY);
            bw.Write(layer.MBR.MaxX);
            bw.Write(layer.MBR.MaxY);
            // Z, M值都设为0
            for (int i = 0; i < 4; i++)
            {
                bw.Write((double)0);
            }

            // 写实体信息
            switch (typeCode)
            {
                case 1: // 点要素                  
                    SaveShpPoint(bw, layer);
                    break;
                case 3: // 线要素
                    SaveShpPolyline(bw, layer);
                    break;
                case 5: // 多边形要素
                    SaveShpPolygon(bw, layer);
                    break;
                default:
                    throw new FileLoadException("Unsupported feature type!");
            }

            bw.Dispose();
            fs.Dispose();

        }

        /// <summary>
        /// 保存dbf文件
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="filename"></param>
        private static void SaveDbf(WEVectorLayer layer, string filename)
        {
            string dbfpath = filename.Substring(0, filename.Length - 4) + ".dbf";   // dbf文件路径
            FileStream fs = new FileStream(dbfpath, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            // 1.写文件头
            bw.Write((byte)3);  // 写入版本信息
            // 写入更新时间
            Byte year;
            if (DateTime.Now.Year <= 2008)
                year = (Byte)(DateTime.Now.Year - 1900);
            else
                year = (Byte)(DateTime.Now.Year - 2000);
            Byte mouth = (Byte)DateTime.Now.Month;
            Byte day = (Byte)DateTime.Now.Day;
            bw.Write(year);
            bw.Write(mouth);
            bw.Write(day);

            // 写入记录数
            Int32 recordNum = layer.GeometryCount;
            bw.Write(recordNum);    
            // 获得各字段数据长度、精度、类型，计算每条记录的长度
            Int16 fieldCount = (Int16)(layer.FieldCount);   // 字段数
            bw.Write((Int16)(32 + 32 * fieldCount + 1));    // 头文件字节数
            Byte[] fieldsLength = new Byte[fieldCount];     // 字段长度数组
            Byte[] fieldsDecimal = new Byte[fieldCount];    // 字段精度数组
            Byte[] fieldsType = new Byte[fieldCount];       // 字段类型数组
            string[] fieldsName = new string[fieldCount];   // 字段名称数组
            Int16 recordLength = 1;                         // 每条记录的长度

            int index = 0;
            foreach (KeyValuePair<string, object> kvp in layer.GetFeature(0).Attributes)
            {
                Type fieldType = kvp.Value.GetType();
                fieldsName[index] = kvp.Key;
                if (fieldType == typeof(int))
                {
                    fieldsType[index] = 78;
                    fieldsLength[index] = 11;   // 这个值有点搞不懂了，，
                    fieldsDecimal[index] = 0;
                }
                else if (fieldType == typeof(Double))
                {
                    fieldsType[index] = 78;
                    fieldsLength[index] = 31;   // 这个值有点搞不懂了，，
                    fieldsDecimal[index] = 15;
                }
                else
                {
                    fieldsType[index] = 67;
                    fieldsLength[index] = 20;   // 这里先随便设一个字符串长度值，后面可以更改
                    fieldsDecimal[index] = 0;
                }
                recordLength += (Int16)fieldsLength[index];
                index++;
            }

            bw.Write(recordLength);     // 写入每条记录的长度
            for (int i = 0; i < 4; i++) // 写入保留字节及无关字节，共4*4个字节，可能会有一些问题
                bw.Write(0);
            bw.Write((byte)0);      // 写入MDX标识
            bw.Write((byte)0x4D);   // 写入Language driver ID
            bw.Write((Int16)0);     // 写入保留字节
            // 写入记录项信息描述数组
            foreach (KeyValuePair<string, object> kvp in layer.GetFeature(0).Attributes)
            for (int i = 0; i < fieldCount; i++)
            {
                // 写入字段名称
                string fieldName = fieldsName[i].PadRight(11, '\0');
                Byte[] fieldNameAscii = System.Text.Encoding.ASCII.GetBytes(fieldName);
                bw.Write(fieldNameAscii);
                
                bw.Write(fieldsType[i]);    // 写入字段数据类型
                bw.Write(0);    // 写入保留字节
                bw.Write(fieldsLength[i]);   // 写入字段数据长度
                bw.Write(fieldsDecimal[i]);  // 写入字段数据精度
                // 写入保留字节
                bw.Write((Int16)0);
                bw.Write(0);
                bw.Write(0);
                bw.Write(0);
            }
            bw.Write((byte)13); // 写入记录项终止标识

            // 2.写记录信息
            for (int i = 0; i < recordNum; i++)
            {
                bw.Write((byte)32); // 每条记录开头是一个空字符
                for (int j = 0; j < fieldCount; j++)    // 读取记录中的每个字段值
                {
                    // 将每个值转成字符串，再转为GBK编码，写入文件，并用空格补齐
                    string fieldName = fieldsName[j];
                    string fieldValueStr = layer.GetFeature(i).Attributes[fieldName].ToString();
                    Byte[] fieldValueByte = Encoding.GetEncoding("GBK").GetBytes(fieldValueStr);
                    bw.Write(fieldValueByte);
                    for (int k = 0; k < fieldsLength[j] - fieldValueByte.Count(); k++)
                    {
                        bw.Write((byte)32);
                    }
                }
            }
            bw.Write((byte)0x1A);   // dbf文件结尾
            bw.Dispose();
            fs.Dispose();

        }

        /// <summary>
        /// 保存shx文件
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="filename"></param>
        private static void SaveShx(WEVectorLayer layer, string filename)
        {
            string shxpath = filename.Substring(0, filename.Length - 4) + ".shx";   // shx文件路径
            FileStream fs = new FileStream(shxpath, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            // 1.写文件头
            bw.Write(OnChangeByteOrder(9994));
            for (int i = 0; i < 5; i++)
            {
                bw.Write(0);
            }
            FeatureType layerType = layer.FeatureType;  // 获取图层的要素类型
            Int32 recordNum = layer.GeometryCount;      // 要素个数
            Int32 fileLength = 100 + recordNum * 8;     // 文件长度
            bw.Write(OnChangeByteOrder(fileLength));    // 写入文件长度
            bw.Write(1000); // 写入版本号
            // 写入几何类型编码
            if (layerType == FeatureType.WEEntityPoint)
            {
                bw.Write(1);
            }
            else if (layerType == FeatureType.WEMultiPolyline)
            {
                bw.Write(3);
            }
            else if (layerType == FeatureType.WEMultiPolygon)
            {
                bw.Write(5);
            }
            else
            {
                throw new Exception("Unsupported feature type!");
            }
            // 写入MBR数据
            bw.Write(layer.MBR.MinX);
            bw.Write(layer.MBR.MinY);
            bw.Write(layer.MBR.MaxX);
            bw.Write(layer.MBR.MaxY);
            // Z值、M值都设为0
            for (int i = 0; i < 4; i++)
            {
                bw.Write((double)0);
            }

            // 2.写实体信息
            Int32 offset = 50;          // 坐标文件中对应记录的起始位置相对文件起始位置的偏移量，第一条记录为50
            Int32 contentLength = 0;    // 坐标文件中对应记录的长度
            for (int i = 0; i < recordNum; i++)
            {
                bw.Write(OnChangeByteOrder(offset));    // 写入偏移量
                // 获取对应记录的长度                
                if (layerType == FeatureType.WEEntityPoint)
                    contentLength = 28;
                else if (layerType == FeatureType.WEMultiPolyline)
                {
                    WEEntityPolyline multiPolyline = (WEEntityPolyline)layer.GetFeature(i);
                    contentLength = 28 + (4 + 8 * 2) * multiPolyline.Geometries.PointCount;
                }
                else if (layerType == FeatureType.WEMultiPolygon)
                {
                    WEEntityPolygon multiPolygon = (WEEntityPolygon)layer.GetFeature(i);
                    contentLength = 28 + (4 + 8 * 2) * multiPolygon.Geometries.PointCount;
                }
                
                bw.Write(OnChangeByteOrder(contentLength)); // 写入记录长度
                offset += (contentLength + 4);  // 更新偏移量
            }
            bw.Dispose();
            fs.Dispose();

        }

        /// <summary>
        /// 读取shp文件中的点要素
        /// </summary>
        /// <param name="br"></param>
        /// <param name="layer"></param>
        private static void ReadShpPoint(BinaryReader br, ref WEVectorLayer layer)
        {
            Int32 recordNum;        // 记录号
            Int32 contentLength;    // 坐标记录长度
            Int32 shapeType;        // 几何类型
            Double x, y;            // x,y坐标
            int counter = 0;
            while (true)            // 逐个记录进行读取，直到文件末尾
            {
                // try-catch结构，当检测到流末尾错误时，表明已读到文件末尾，停止读取
                try
                {
                    recordNum = OnChangeByteOrder(br.ReadInt32());
                }
                catch (EndOfStreamException)
                {
                    break;
                }
                contentLength = OnChangeByteOrder(br.ReadInt32());
                shapeType = br.ReadInt32();
                x = br.ReadDouble();
                y = br.ReadDouble();
                WEEntityPoint point = new WEEntityPoint(counter,new WEMultiPoint(new WEPoint[1] { new WEPoint(x, y) }), new Dictionary<string, object> { });  // 创建点要素
                layer.AddFeature(point);    // 加入到图层中
                counter++;
            }
        }

        /// <summary>
        /// 读取shp文件中的线要素
        /// </summary>
        /// <param name="br"></param>
        /// <param name="layer"></param>
        private static void ReadShpPolyline(BinaryReader br, ref WEVectorLayer layer)
        {
            Int32 recordNum;        // 记录号
            Int32 contentLength;    // 坐标记录长度
            int counter = 0;
            while (true)            // 逐个记录进行读取，直到文件末尾
            {
                // try-catch结构，当检测到流末尾错误时，表明已读到文件末尾，停止读取
                try
                {
                    recordNum = OnChangeByteOrder(br.ReadInt32());
                }
                catch (EndOfStreamException)
                {
                    break;
                }
                contentLength = OnChangeByteOrder(br.ReadInt32());
                Int32 shapeType = br.ReadInt32();   // 几何类型
                
                // 读取外包矩形数据
                Double Xmin = br.ReadDouble();
                Double Ymin = br.ReadDouble();
                Double Xmax = br.ReadDouble();
                Double Ymax = br.ReadDouble();
                WERectangle box = new WERectangle(Xmin, Xmax, Ymin, Ymax);  // 可以直接读取外包矩形数据，但需要给定义的要素类加入MBR字段和属性

                Int32 numParts = br.ReadInt32();    // 构成当前线目标的子线段个数
                Int32 numPoints = br.ReadInt32();   // 构成当前线目标的坐标点个数
                Int32[] parts = new Int32[numParts + 1];    // 记录每个子线段的起始坐标点在所有坐标点中的位置,parts数组末尾加入1项，代表坐标点总个数
                // 读取parts数组，代表每个子线段起始点的索引的数组
                for (int i = 0; i < numParts; i++)
                {
                    parts[i] = br.ReadInt32();
                }
                parts[numParts] = numPoints;

                // 读取points数组，代表当前子线段坐标点的数组
                // 以此构造多线对象
                List<WEPolyline> polylines = new List<WEPolyline>(numParts);
                for (int i = 0; i < numParts; i++)  //对各个子线段的点坐标进行读取
                {
                    int pointNum = parts[i + 1] - parts[i]; //当前子线段的坐标点个数
                    WEPoint[] points = new WEPoint[pointNum];
                    for (int j = 0; j < pointNum; j++)
                    {
                        Double x = br.ReadDouble(); // x坐标
                        Double y = br.ReadDouble(); // y坐标
                        points[j] = new WEPoint(x, y);
                    }
                    polylines.Add(new WEPolyline(points));
                }
                WEMultiPolyline multiPolyline = new WEMultiPolyline(polylines.ToArray());
                WEEntityPolyline newPol = new WEEntityPolyline(counter, multiPolyline, new Dictionary<string, object>());
                layer.AddFeature(newPol);
                counter++;
            }
        }

        /// <summary>
        /// 读取shp文件中的多边形要素
        /// </summary>
        /// <param name="br"></param>
        /// <param name="layer"></param>
        private static void ReadShpPolygon(BinaryReader br, ref WEVectorLayer layer)
        {
            Int32 recordNum;        // 记录号
            Int32 contentLength;    // 坐标记录长度
            int counter = 0;
            while (true)            // 逐个记录进行读取，直到文件末尾
            {
                // try-catch结构，当检测到流末尾错误时，表明已读到文件末尾，停止读取
                try
                {
                    recordNum = OnChangeByteOrder(br.ReadInt32());
                }
                catch (EndOfStreamException)
                {
                    break;
                }
                contentLength = OnChangeByteOrder(br.ReadInt32());
                Int32 shapeType = br.ReadInt32();   // 几何类型
                
                // 读取外包矩形数据
                Double Xmin = br.ReadDouble();
                Double Ymin = br.ReadDouble();
                Double Xmax = br.ReadDouble();
                Double Ymax = br.ReadDouble();
                WERectangle box = new WERectangle(Xmin, Xmax, Ymin, Ymax);  // 可以直接读取外包矩形数据，但需要给定义的要素类加入MBR字段和属性

                Int32 numParts = br.ReadInt32();    // 构成当前多边形目标的子环个数
                Int32 numPoints = br.ReadInt32();   // 构成当前多边形目标的坐标点个数
                Int32[] parts = new Int32[numParts + 1];    // 记录每个子环的起始坐标点在所有坐标点中的位置,parts数组末尾加入1项，代表坐标点总个数
                // 读取parts数组，代表每个子环起始点的索引的数组
                for (int i = 0; i < numParts; i++)
                {
                    parts[i] = br.ReadInt32();
                }
                parts[numParts] = numPoints;

                // 读取points数组，代表当前子环坐标点的数组
                // 以此构造多多边形对象
                List<WEPolygon> polygons = new List<WEPolygon>(numParts);
                for (int i = 0; i < numParts; i++)  // 对各个子环的点坐标进行读取
                {
                    int pointNum = parts[i + 1] - parts[i]; // 当前子环的点个数
                    WEPoint[] points = new WEPoint[pointNum - 1];   // 首尾点重合
                    for (int j = 0; j < pointNum - 1; j++)
                    {
                        Double x = br.ReadDouble(); // x坐标
                        Double y = br.ReadDouble(); // y坐标
                        points[j] = new WEPoint(x, y);
                    }
                    br.ReadBytes(16);   // 首尾点重合，跳过最后一个点
                    WEPolygon polygon = new WEPolygon(points);            // 构造polygon对象
                    polygons.Add(polygon);
                }
                WEMultiPolygon multiPolygon = new WEMultiPolygon(polygons.ToArray());
                WEEntityPolygon newPol = new WEEntityPolygon(counter, multiPolygon, new Dictionary<string, object> { });
                layer.AddFeature(newPol);
                counter++;
            }
        }
        /*
        private static void ReadShpMultiPoint(BinaryReader br, ref WEVectorLayer layer)
        {

        }
        */

        /// <summary>
        /// 保存点要素至shp文件
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="layer"></param>
        private static void SaveShpPoint(BinaryWriter bw, WEVectorLayer layer)
        {
            int recordNum = layer.GeometryCount;
            for (int i = 0; i < recordNum; i++)
            {
                bw.Write(OnChangeByteOrder(i + 1)); // 写入记录号
                bw.Write(OnChangeByteOrder(20));    // 写入坐标记录长度
                bw.Write(1);    // 写入几何类型，点要素
                // 写入坐标信息
                WEPoint point = (WEPoint)((WEMultiPoint)((WEEntityPoint)layer.GetFeature(i)).Geometries).Points[0];
                bw.Write(point.X);
                bw.Write(point.Y);
            }
        }

        /// <summary>
        /// 保存线要素至shp文件
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="layer"></param>
        private static void SaveShpPolyline(BinaryWriter bw, WEVectorLayer layer)
        {
            int recordNum = layer.GeometryCount;
            for (int i = 0; i < recordNum; i++)
            {
                WEMultiPolyline multiPolyline = (WEMultiPolyline)layer.GetFeature(i).Geometries;
                bw.Write(OnChangeByteOrder(i + 1)); // 写入记录号
                bw.Write(OnChangeByteOrder(28 + 20 * multiPolyline.PointCount));      // 写入坐标记录长度
                bw.Write(3);    // 写入几何类型，线要素
                // 写入MBR数据
                bw.Write(multiPolyline.MBR.MinX);
                bw.Write(multiPolyline.MBR.MinY);
                bw.Write(multiPolyline.MBR.MaxX);
                bw.Write(multiPolyline.MBR.MaxY);

                Int32 numParts = multiPolyline.Polylines.Count();
                bw.Write(numParts);     // 写入子线段个数
                Int32 numPoints = multiPolyline.PointCount;
                bw.Write(numPoints);    // 写入总的点个数
                // 写入各子线段起始点索引信息
                Int32 startPos = 0;
                bw.Write(startPos); // 写入每个子线段的起始点在所有点中的索引，第一个为0
                for (int j = 0; j < numParts - 1; j++)
                {
                    startPos += multiPolyline.GetPolyline(j).PointCount;    // 计算第j+1条子线段的起始点索引
                    bw.Write(startPos);
                }
                //  写入所有点坐标信息
                for (int j = 0; j < numParts; j++)
                {
                    for (int k = 0; k < multiPolyline.GetPolyline(j).PointCount; k++)
                    {
                        bw.Write(((WEPolyline)multiPolyline.GetPolyline(j)).Points[k].X);
                        bw.Write(((WEPolyline)multiPolyline.GetPolyline(j)).Points[k].Y);
                    }
                }
            }
        }

        /// <summary>
        /// 保存多边形要素至shp文件
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="layer"></param>
        private static void SaveShpPolygon(BinaryWriter bw, WEVectorLayer layer)
        {
            int recordNum = layer.GeometryCount;
            for (int i = 0; i < recordNum; i++)
            {
                WEMultiPolygon multiPolygon = (WEMultiPolygon)layer.GetFeature(i).Geometries;
                bw.Write(OnChangeByteOrder(i + 1)); // 写入记录号
                bw.Write(OnChangeByteOrder(28 + 20 * multiPolygon.PointCount));      // 写入坐标记录长度
                bw.Write(5);    // 写入几何类型，多边形要素
                // 写入MBR数据
                bw.Write(multiPolygon.MBR.MinX);
                bw.Write(multiPolygon.MBR.MinY);
                bw.Write(multiPolygon.MBR.MaxX);
                bw.Write(multiPolygon.MBR.MaxY);

                Int32 numParts = multiPolygon.Polygons.Count();
                bw.Write(numParts);     // 写入子环个数
                Int32 numPoints = multiPolygon.PointCount;
                bw.Write(numPoints);    // 写入总的点个数
                // 写入各子环起始点索引信息
                Int32 startPos = 0;
                bw.Write(startPos); // 写入每个子环的起始点在所有点中的索引，第一个为0
                for (int j = 0; j < numParts - 1; j++)
                {
                    startPos += multiPolygon.GetPolygon(j).PointCount;    // 计算第j+1条子环的起始点索引
                    bw.Write(startPos);
                }
                //  写入所有点坐标信息，每条子环需要写入终止点坐标（与起始点相同）
                for (int j = 0; j < numParts; j++)
                {
                    for (int k = 0; k < multiPolygon.GetPolygon(j).PointCount - 1; k++) // PointCount加上了1，但是Points数组没有加上最后一个点，在属性定义改变时，这里也要改变
                    {
                        bw.Write(((WEPolygon)multiPolygon.GetPolygon(j)).Points[k].X);
                        bw.Write(((WEPolygon)multiPolygon.GetPolygon(j)).Points[k].Y);
                    }
                    bw.Write(((WEPolygon)multiPolygon.GetPolygon(j)).Points[0].X);
                    bw.Write(((WEPolygon)multiPolygon.GetPolygon(j)).Points[0].Y);
                }
            }
        }
        /*
        private static void SaveShpMultiPoint(BinaryWriter bw, string filename)
        {
            
        }
        */

        #endregion
    }
}
