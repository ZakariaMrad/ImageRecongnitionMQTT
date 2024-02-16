using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Collections.Generic;
using Emgu.CV.Util;

public static class GeometryHelper
{

     public static bool IsPointInCorners(MarkerModel marker, List<MarkerModel> corners, Mat mat, string path)
    {
        List<Point> polygonPoints = GetConvexHullPoints(corners);
        return IsPointInPolygon(marker.Position.ToPoint(), polygonPoints, mat, path);
    }

    private static List<Point> GetConvexHullPoints(List<MarkerModel> corners)
    {
        List<Point> points = corners.Select(c => c.Position.ToPoint()).ToList();
        return ConvexHull.FindConvexHull(points);
    }

    private static bool IsPointInPolygon(Point testPoint, List<Point> polygonPoints, Mat mat, string path)
    {
        using (GraphicsPath polygon = new GraphicsPath())
        {
            polygon.AddPolygon(polygonPoints.ToArray());
            //draw the polygon on the image
            for (int i = 0; i < polygonPoints.Count; i++)
            {
                //CvInvoke.Line(mat, polygonPoints[i], polygonPoints[(i + 1) % polygonPoints.Count], new MCvScalar(5, 13, 163), 1);
            }
            ImageHelper.SaveImage(mat, path);
            return polygon.IsVisible(testPoint);
        }
    }

}