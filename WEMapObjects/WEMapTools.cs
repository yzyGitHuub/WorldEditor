using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WEMapObjects
{
    public static class WEMapTools
    {
        public static WEStyle DefaultStyle = new WEStyle(1, 1, Color.Black, Color.Red, Color.Yellow, 5, 2);

        public static WERectangle DisplayMBR = new WERectangle(-180, 180, -90, 90);

        public static int DisplayWidth = 0, DisplayHeight = 0;

        public static long GeometryCount = 0;

        public static double Tolerance = 0.1;

        private static List<Color> surfaceColorTable = new List<Color>
        { Color.Tomato,Color.LightCoral,Color.Orange,Color.LightSalmon,Color.Bisque,
            Color.LemonChiffon,Color.LightGreen,Color.PaleGreen,Color.LightSkyBlue,
            Color.LightCyan,Color.Lavender,Color.Plum};     //默认颜色表
        
        /// <summary>
        /// 获得两点间距离
        /// </summary>
        /// <param name="Point1"></param>
        /// <param name="Point2"></param>
        /// <returns></returns>
        public static double GetDistance(WEPoint Point1,WEPoint Point2)
        {
            return Math.Sqrt((Point1.X - Point2.X) * (Point1.X - Point2.X) +
                (Point1.Y - Point2.Y) * (Point1.Y - Point2.Y));
        }
        
        /// <summary>
        /// 判断两点在容限内是否重合
        /// </summary>
        /// <param name="Point1"></param>
        /// <param name="Point2"></param>
        /// <returns></returns>
        public static bool IsPointOnPoint(WEPoint Point1, WEPoint Point2)
        {
            double Dis = GetDistance(Point1, Point2);
            if (Dis <= Tolerance)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 判断点是否选中多点
        /// </summary>
        /// <param name="point"></param>
        /// <param name="multiPoint"></param>
        /// <returns></returns>
        public static bool IsPointOnMultiPoint(WEPoint point,WEMultiPoint multiPoint)
        {
            int sPointCount = multiPoint.PointCount;
            for(int i=0;i<sPointCount;i++)
            {
                if (IsPointOnPoint(point, (WEPoint)multiPoint.GetPoint(i)))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 获取一个点到一条线段的最短距离
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polyline"></param>
        /// <returns></returns>
        public static double GetDistanceFromPointToSegment(WEPoint point,WEPoint startPoint,WEPoint endPoint)
        {
            double length = GetDistance(startPoint, endPoint);
            if (startPoint.Equals(endPoint))        //若线段首尾点重合
                return length;
            else
            {
                double dot = (point.X - startPoint.X) * (endPoint.X - startPoint.X) + 
                    (point.Y - startPoint.Y) * (endPoint.Y - startPoint.Y);     //计算点积
                double d = dot / (length * length);
                if (d < 0)      //最近点取线段起点
                    return GetDistance(point, startPoint);
                else if (d > 1) //最近点取线段终点
                    return GetDistance(point, endPoint);
                else
                {
                    double Xd = startPoint.X + d * (endPoint.X - startPoint.X);
                    double Yd = startPoint.Y + d * (endPoint.Y - startPoint.Y);
                    WEPoint D = new WEPoint(Xd, Yd);    //最近点取垂足点
                    return GetDistance(point, D);
                }
            }
        }

        /// <summary>
        /// 判断点在容限内是否在线上
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polyline"></param>
        /// <returns></returns>
        public static bool IsPointOnPolyline(WEPoint point,WEPolyline polyline)
        {
            WERectangle box = polyline.MBR;
            if (!IsPointInBox(point, box))      //若点不在线外包矩形内，无需继续计算
                return false;
            int sPointCount = polyline.PointCount;
            for(int i=0;i<sPointCount-1;i++)
            {
                WEPoint sPoint = polyline.Points[i];
                WEPoint ePoint = polyline.Points[i + 1];
                double thisDis = GetDistanceFromPointToSegment(point, sPoint, ePoint);
                if (thisDis < Tolerance)    //点到一段线段的距离小于容限即可
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 判断点是否选中多线
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polyline"></param>
        /// <returns></returns>
        public static bool IsPointOnMultiPolyline(WEPoint point, WEMultiPolyline multipolyline)
        {
            WERectangle box = multipolyline.MBR;
            if (!IsPointInBox(point, box))
                return false;
            int sPolylineCount = multipolyline.Polylines.Count();
            for(int i=0;i<sPolylineCount;i++)
            {
                if (IsPointOnPolyline(point, (WEPolyline)multipolyline.Polylines[i]))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 判断点是否位于矩形盒内
        /// </summary>
        /// <param name="point"></param>
        /// <param name="rect"></param>
        public static bool IsPointInBox(WEPoint point, WERectangle box, double tolerance=0.1)
        {
            //点完全在矩形盒内时
            if (point.X >= box.MinX && point.X <= box.MaxX && point.Y >= box.MinY && point.Y <= box.MaxY)
                return true;            
            if(tolerance>0)
            {
                //点在一定容限内位于矩形盒边界上
                WEPoint LeftTop = new WEPoint(box.MinX, box.MaxY);
                WEPoint LeftBottom = new WEPoint(box.MinX, box.MinY);
                WEPoint RightTop = new WEPoint(box.MaxX, box.MaxY);
                WEPoint RightBottom = new WEPoint(box.MaxX, box.MinY);
                if (GetDistanceFromPointToSegment(point, LeftTop, LeftBottom) < Tolerance)  //左边界
                    return true;
                if (GetDistanceFromPointToSegment(point, RightTop, RightBottom) < Tolerance)  //右边界
                    return true;
                if (GetDistanceFromPointToSegment(point, LeftTop, RightTop) < Tolerance)  //上边界
                    return true;
                if (GetDistanceFromPointToSegment(point, LeftBottom, RightBottom) < Tolerance)  //下边界
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 判断多点是否部分或完全位于矩形盒内
        /// </summary>
        /// <param name="multiPoint"></param>
        /// <param name="box"></param>
        /// <returns></returns>
        public static bool IsMultiPointPartiallyWithinBox(WEMultiPoint multiPoint,WERectangle box)
        {
            WERectangle pointsBox = multiPoint.MBR;
            if (!IsTwoBoxesIntersect(pointsBox, box))
                return false;
            int sPointCount = multiPoint.PointCount;
            for(int i=0;i<sPointCount;i++)
            {
                if (IsPointInBox((WEPoint)multiPoint.GetPoint(i), box))     //某一个点位于矩形盒内即可
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 判断一条水平向右的射线与指定线段是否相交
        /// </summary>
        /// <returns></returns>
        public static bool IsRayIntersectsSegment(WEPoint point,WEPoint startPoint,WEPoint endPoint)
        {
            if (startPoint.Y == endPoint.Y)     //排除射线与线段重合或平行的情况
                return false;
            if (startPoint.Y > point.Y && endPoint.Y > point.Y)     //线段完全位于射线上方
                return false;
            if (startPoint.Y < point.Y && endPoint.Y < point.Y)      //线段完全位于射线下方
                return false;
            if (startPoint.Y == point.Y && endPoint.Y > point.Y)    //交点为下端点
                return false;
            if (endPoint.Y == point.Y && startPoint.Y > point.Y)    //交点为下端点
                return false;
            if (startPoint.X < point.X && endPoint.X < point.X)     //线段完全在射线左侧
                return false;
            double x = endPoint.X - (endPoint.X - startPoint.X) * (endPoint.Y - point.Y) 
                / (endPoint.Y - startPoint.Y);                      //计算交点横坐标
            if (x < point.X)        //交点在射线起点左侧
                return false;
            return true;
        }

        /// <summary>
        /// 判断点是否在简单多边形内
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static bool IsPointInPolygon(WEPoint point,WEPolygon polygon, double tolerance=0.1)
        {
            WERectangle box = polygon.MBR;
            if (!IsPointInBox(point, box))
                return false;
            int sPointCount = polygon.PointCount;
            int sIntersectionCount = 0;         //射线与边的交点数
            WEPoint startPoint, endPoint;       //线段起点、终点
            for(int i=0;i<sPointCount-1;i++)
            {
                startPoint = polygon.Points[i];
                endPoint = polygon.Points[i + 1];
                if (IsRayIntersectsSegment(point, startPoint, endPoint))
                    sIntersectionCount++;
            }
            startPoint = polygon.Points[sPointCount - 1];
            endPoint = polygon.Points[0];
            if(IsRayIntersectsSegment(point, startPoint, endPoint))     //最后一条边
                sIntersectionCount++;
            if (sIntersectionCount % 2 == 1)        //奇数个交点
                return true;
            if(tolerance>0)
            {
                //对每条边判断点是否在一定容限内位于边上
                for (int i = 0; i < sPointCount - 1; i++)
                {
                    startPoint = polygon.Points[i];
                    endPoint = polygon.Points[i + 1];
                    if (GetDistanceFromPointToSegment(point, startPoint, endPoint) <= Tolerance)
                        return true;
                }
                startPoint = polygon.Points[sPointCount - 1];
                endPoint = polygon.Points[0];
                if (GetDistanceFromPointToSegment(point, startPoint, endPoint) < Tolerance)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 判断点是否在复合多边形内
        /// </summary>
        /// <param name="point"></param>
        /// <param name="multiPolygon"></param>
        /// <returns></returns>
        public static bool IsPointInMultiPolygon(WEPoint point,WEMultiPolygon multiPolygon)
        {
            WERectangle box = multiPolygon.MBR;
            if (!IsPointInBox(point, box))
                return false;
            int sPolygonCount = multiPolygon.Polygons.Count();
            int sIntersectionCount = 0;         //射线与边的交点数
            for(int j=0;j<sPolygonCount;j++)    //遍历每个简单多边形求射线交点个数
            {
                WEPolygon thisPolygon = (WEPolygon)multiPolygon.GetPolygon(j);
                if (thisPolygon.PointCount < 3)
                    continue;
                int sPointCount = thisPolygon.PointCount;
                WEPoint startPoint, endPoint;       //线段起点、终点
                for (int i = 0; i < sPointCount - 1; i++)
                {
                    startPoint = thisPolygon.Points[i];
                    endPoint = thisPolygon.Points[i + 1];
                    if (IsRayIntersectsSegment(point, startPoint, endPoint))
                        sIntersectionCount++;
                }
                startPoint = thisPolygon.Points[sPointCount - 1];
                endPoint = thisPolygon.Points[0];
                if (IsRayIntersectsSegment(point, startPoint, endPoint))     //最后一条边
                    sIntersectionCount++;
            }
            if (sIntersectionCount % 2 == 1)        //奇数个交点
                return true;
            return false;
        }
        
        /// <summary>
        /// 判断两个矩形盒是否相交
        /// </summary>
        /// <param name="box1"></param>
        /// <param name="box2"></param>
        /// <returns></returns>
        public static bool IsTwoBoxesIntersect(WERectangle box1,WERectangle box2)
        {
            double MaxX1, MinX1, MaxY1, MinY1;
            double MaxX2, MinX2, MaxY2, MinY2;
            MaxX1 = box1.MaxX;
            MinX1 = box1.MinX;
            MaxY1 = box1.MaxY;
            MinY1 = box1.MinY;
            MaxX2 = box2.MaxX;
            MinX2 = box2.MinX;
            MaxY2 = box2.MaxY;
            MinY2 = box2.MinY;
            if (MinY1 > MaxY2 || MinY2 > MaxY1 || MinX1 > MaxX2 || MinX2 > MaxX1)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断两条线段是否相交
        /// </summary>
        /// <returns></returns>
        public static bool IsTwoSegmentsIntersect(WEPoint startPoint1,WEPoint endPoint1, WEPoint startPoint2, WEPoint endPoint2)
        {
            //使用向量叉积的方法
            double cross1 = (endPoint1.X - startPoint2.X) * (endPoint2.Y - startPoint2.Y) - (endPoint1.Y - startPoint2.Y) * (endPoint2.X - startPoint2.X);
            double cross2 = (startPoint1.X - startPoint2.X) * (endPoint2.Y - startPoint2.Y) - (startPoint1.Y - startPoint2.Y) * (endPoint2.X - startPoint2.X);
            double dot = cross1 * cross2;
            if (dot < 0)
                return true;
            return false;
        }

        /// <summary>
        /// 判断一条线段是否和矩形盒相交
        /// </summary>
        /// <returns></returns>
        public static bool IsSegmentIntersectsBox(WEPoint startPoint,WEPoint endPoint,WERectangle box)
        {
            if (startPoint.X < box.MinX && endPoint.X < box.MinX)    //线段在矩形盒左边的外侧
                return false;
            if (startPoint.X > box.MaxX && endPoint.X > box.MaxX)    //线段在矩形盒右边的外侧
                return false;
            if (startPoint.Y < box.MinY && endPoint.Y < box.MinY)    //线段在矩形盒下边的外侧
                return false;
            if (startPoint.Y > box.MaxY && endPoint.Y > box.MaxY)    //线段在矩形盒上边的外侧
                return false;
            WEPoint LeftTop = new WEPoint(box.MinX, box.MaxY);
            WEPoint LeftBottom = new WEPoint(box.MinX, box.MinY);
            WEPoint RightTop = new WEPoint(box.MaxX, box.MaxY);
            WEPoint RightBottom = new WEPoint(box.MaxX, box.MinY);
            if (IsTwoSegmentsIntersect(startPoint, endPoint, LeftBottom, LeftTop))  //线段与矩形盒左边界求交
                return true;
            if (IsTwoSegmentsIntersect(startPoint, endPoint, RightBottom, RightTop))  //线段与矩形盒右边界求交
                return true;
            if (IsTwoSegmentsIntersect(startPoint, endPoint, RightTop, LeftTop))  //线段与矩形盒上边界求交
                return true;
            if (IsTwoSegmentsIntersect(startPoint, endPoint, LeftBottom, RightBottom))  //线段与矩形盒下边界求交
                return true;
            return false;
        }

        /// <summary>
        /// 判断简单折线是否部分或者完全位于矩形盒内
        /// </summary>
        /// <returns></returns>
        public static bool IsPolylinePartiallyWithinBox(WEPolyline polyline,WERectangle box)
        {
            WERectangle lineBox = polyline.MBR;
            if (!IsTwoBoxesIntersect(lineBox, box))     //两外包矩形不相交
                return false;
            int sPointCount = polyline.PointCount;
            for(int i=0;i<sPointCount;i++)              //对折线上每个点判断是否在矩形盒内
            {
                if (IsPointInBox(polyline.Points[i], box))
                    return true;
            }
            for(int j=0;j<sPointCount-1;j++)
            {
                if (IsSegmentIntersectsBox(polyline.Points[j], polyline.Points[j + 1], box))    //对折线上每一段与矩形盒求交
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 判断复合折线是否部分或者完全位于矩形盒内
        /// </summary>
        /// <returns></returns>
        public static bool IsMultiPolylinePartiallyWithinBox(WEMultiPolyline multiPolyline,WERectangle box)
        {
            WERectangle multiPolylineBox = multiPolyline.MBR;
            if (!IsTwoBoxesIntersect(multiPolylineBox, box))
                return false;
            int sPolylineCount = multiPolyline.Polylines.Count();
            for(int i=0;i<sPolylineCount;i++)
            {
                if (IsPolylinePartiallyWithinBox((WEPolyline)multiPolyline.Polylines[i], box))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 判断简单多边形是否部分或者完全位于矩形盒内
        /// </summary>
        /// <returns></returns>
        public static bool IsPolygonPartiallyWithinBox(WEPolygon polygon,WERectangle box)
        {
            WERectangle polygonBox = polygon.MBR;
            if (!IsTwoBoxesIntersect(polygonBox, box))      //矩形盒不相交
                return false;
            int sPointCount = polygon.PointCount;
            for(int i=0;i<sPointCount;i++)                  //多边形是否有顶点位于矩形盒内
            {
                if (IsPointInBox(polygon.Points[i], box))
                    return true;
            }
            WEPoint LeftTop = new WEPoint(box.MinX, box.MaxY);
            WEPoint LeftBottom = new WEPoint(box.MinX, box.MinY);
            WEPoint RightTop = new WEPoint(box.MaxX, box.MaxY);
            WEPoint RightBottom = new WEPoint(box.MaxX, box.MinY);
            if (IsPointInPolygon(LeftTop, polygon) || IsPointInPolygon(LeftBottom, polygon)
                || IsPointInPolygon(RightTop, polygon) || IsPointInPolygon(RightBottom, polygon))   //矩形盒是否有顶点位于多边形内
                return true;
            //简单多边形与矩形盒的边是否有交点
            WEPoint startPoint, endPoint;
            for(int i = 0; i < sPointCount-1; i++)
            {
                startPoint = polygon.Points[i];
                endPoint = polygon.Points[i + 1];
                if (IsSegmentIntersectsBox(startPoint, endPoint, box))
                    return true;
            }
            startPoint = polygon.Points[sPointCount - 1];           //最后一条边
            endPoint = polygon.Points[0];
            if (IsSegmentIntersectsBox(startPoint, endPoint, box))
                return true;
            return false;
        }

        /// <summary>
        /// 判断复合多边形是否部分或者完全位于矩形盒内
        /// </summary>
        /// <returns></returns>
        public static bool IsMultiPolygonPartiallyWithinBox(WEMultiPolygon multiPolygon,WERectangle box)
        {
            WERectangle multipolygonBox = multiPolygon.MBR;         //矩形盒不相交
            if (!IsTwoBoxesIntersect(multipolygonBox, box))
                return false;
            int sPolygonCount = multiPolygon.Polygons.Count();
            for (int j = 0; j < sPolygonCount; j++)    //遍历每个简单多边形判断是否有顶点位于矩形盒内
            {
                WEPolygon thisPolygon = (WEPolygon)multiPolygon.GetPolygon(j);
                int sPointCount = thisPolygon.PointCount;
                for (int i = 0; i < sPointCount; i++)
                {
                    if (IsPointInBox(thisPolygon.Points[i], box))
                        return true;
                }
            }
            WEPoint LeftTop = new WEPoint(box.MinX, box.MaxY);
            WEPoint LeftBottom = new WEPoint(box.MinX, box.MinY);
            WEPoint RightTop = new WEPoint(box.MaxX, box.MaxY);
            WEPoint RightBottom = new WEPoint(box.MaxX, box.MinY);
            //判断矩形盒是否有顶点位于复合多边形内
            if (IsPointInMultiPolygon(LeftTop, multiPolygon) || IsPointInMultiPolygon(LeftBottom, multiPolygon) ||
                IsPointInMultiPolygon(RightBottom, multiPolygon) || IsPointInMultiPolygon(RightTop, multiPolygon))
                return true;
            for (int j = 0; j < sPolygonCount; j++)    //遍历每个简单多边形判断是否有边与矩形盒相交
            {
                WEPolygon thisPolygon = (WEPolygon)multiPolygon.GetPolygon(j);
                int sPointCount = thisPolygon.PointCount;
                WEPoint startPoint, endPoint;
                for (int i = 0; i < sPointCount-1; i++)
                {
                    startPoint = thisPolygon.Points[i];
                    endPoint = thisPolygon.Points[i + 1];
                    if (IsSegmentIntersectsBox(startPoint, endPoint, box))
                        return true;
                }
                startPoint = thisPolygon.Points[sPointCount - 1];
                endPoint = thisPolygon.Points[0];
                if (IsSegmentIntersectsBox(startPoint, endPoint, box))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 获取图层默认样式
        /// 点图层：颜色-seagreen，边界色-black，边界宽-2；
        /// 线图层：颜色-DarkSlateGray，边界色-DarkSlateGray，线宽-5
        /// 面图层：颜色-从颜色表中随机选取，边界色-black，边界宽-2
        /// </summary>
        /// <param name="mapType"></param>
        /// <returns></returns>
        public static WEStyle GetDefaultRender(FeatureType mapType)
        {
            WEStyle DefaultStyle = new WEStyle();
            DefaultStyle.SymbolMethod = 1;      //默认样式使用唯一值渲染
            if(mapType==FeatureType.WEPoint|| mapType == FeatureType.WEMultiPoint|| mapType == FeatureType.WEEntityPoint)
            {
                DefaultStyle.BoundaryColor = Color.Black;
                DefaultStyle.FromColor = Color.SeaGreen;
                DefaultStyle.ToColor = DefaultStyle.FromColor;
                DefaultStyle.BoundaryWidth = 2;
            }
            if(mapType==FeatureType.WEPolygon|| mapType == FeatureType.WEMultiPolygon|| mapType == FeatureType.WEEntityPolygon)
            {
                //面的颜色从颜铯表中随机选取
                Random r = new Random();
                DefaultStyle.FromColor = surfaceColorTable[r.Next(0, surfaceColorTable.Count())];
                DefaultStyle.ToColor = DefaultStyle.FromColor;
                DefaultStyle.BoundaryColor = Color.Black;
                DefaultStyle.BoundaryWidth = 2;
            }
            if(mapType==FeatureType.WEPolyline|| mapType == FeatureType.WEMultiPolyline|| mapType == FeatureType.WEEntityPolyline)
            {
                DefaultStyle.BoundaryColor = Color.DarkSlateGray;
                DefaultStyle.FromColor = DefaultStyle.ToColor = Color.DarkSlateGray;
                DefaultStyle.BoundaryWidth = 5;
            }
            return DefaultStyle;
        }

        /// <summary>
        /// 将地图坐标转换为屏幕坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static PointF FromMapPoint(WEPoint point)
        {
            PointF sPoint = new PointF();
            sPoint.X = (float)((point.X - DisplayMBR.MinX) * DisplayWidth / DisplayMBR.Width);
            sPoint.Y = (float)((DisplayMBR.MaxY - point.Y) * DisplayHeight / DisplayMBR.Height);
            return sPoint;
        }

        /// <summary>
        /// 将屏幕坐标转换为地图坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static WEPoint ToMapPoint(PointF point)
        {
            WEPoint sPoint = new WEPoint();
            sPoint.X = DisplayMBR.MinX + point.X * DisplayMBR.Width / DisplayWidth;
            sPoint.Y = DisplayMBR.MaxY - point.Y * DisplayMBR.Height / DisplayHeight;
            return sPoint;
        }


        /*
         /// <summary>
        /// 判断线是否完全位于矩形盒内
        /// </summary>
        /// <param name="polyline"></param>
        /// <param name="box"></param>
        /// <returns></returns>
        public static bool IsPolylineCompleteInBox(WEPolyline polyline,WERectangle box)
        {
            WERectangle lineBox = polyline.MBR;
            if (!IsTwoBoxesIntersect(lineBox, box))     //若线的外包矩形与矩形盒不相交，无需继续计算
                return false;
            int sPointCount = polyline.PointCount;
            for (int i = 0; i < sPointCount; i++)
            {
                if (IsPointInBox(polyline.Points[i], box) == false)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 判断多边形是否完全位于矩形盒内
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="box"></param>
        /// <returns></returns>
        public static bool IsPolygonCompleteInBox(WEPolygon polygon, WERectangle box)
        {
            int sPointCount = polygon.PointCount;
            for (int i = 0; i < sPointCount; i++)
            {
                if (IsPointInBox(polygon.Points[i], box) == false)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 判断复合多边形是否位于矩形盒内（有一个完全位于矩形盒内即可）
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="box"></param>
        /// <returns></returns>
        public static bool IsMultiPolygonInBox(WEMultiPolygon multipolygon, WERectangle box)
        {
            int sPolygonCount = multipolygon.PolygonCount;
            for(int i=0;i<sPolygonCount;i++)
            {
                if (IsPolygonCompleteInBox((WEPolygon)multipolygon.polygons[i], box))
                    return true;
            }
            return false;
        }*/

    }
}
