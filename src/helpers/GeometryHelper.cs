using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;

public static class GeometryHelper
{

    public static bool IsPointInCorners(MarkerModel marker, List<BeamCornerModel> corners)
    {
        Point[] polygonPoints = corners.Select(c => c.Position.ToPoint()).ToArray();
        return IsPointInPolygon(marker.Position.ToPoint(), polygonPoints);
    }


    private static bool IsPointInPolygon(Point testPoint, Point[] polygonPoints)
    {
        // Create a GraphicsPath and add the polygon
        GraphicsPath polygonPath = new GraphicsPath();
        polygonPath.AddPolygon(polygonPoints);

        // Check if the test point is contained within the polygon
        bool isInside = polygonPath.IsVisible(testPoint);
        return isInside;
    }


}