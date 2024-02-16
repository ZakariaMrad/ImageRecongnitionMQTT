using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

public static class ConvexHull
{
    // Cross product (p1-p0)x(p2-p0)
    private static double CrossProduct(Point p0, Point p1, Point p2)
    {
        return (p1.X - p0.X) * (p2.Y - p0.Y) - (p2.X - p0.X) * (p1.Y - p0.Y);
    }

    // Graham's Scan algorithm
    public static List<Point> FindConvexHull(List<Point> points)
    {
        if (points.Count < 3)
            throw new ArgumentException("There should be at least 3 points to find a convex hull.");

        List<Point> hull = new List<Point>();

        // Sort points by x-coordinate
        points.Sort((p1, p2) =>
        {
            if (p1.X != p2.X)
                return p1.X.CompareTo(p2.X);
            return p1.Y.CompareTo(p2.Y);
        });

        // Lower hull
        foreach (var p in points)
        {
            while (hull.Count >= 2 && CrossProduct(hull[hull.Count - 2], hull[hull.Count - 1], p) <= 0)
                hull.RemoveAt(hull.Count - 1);
            hull.Add(p);
        }

        // Upper hull
        int lowerHullCount = hull.Count;
        for (int i = points.Count - 2; i >= 0; i--)
        {
            var p = points[i];
            while (hull.Count > lowerHullCount && CrossProduct(hull[hull.Count - 2], hull[hull.Count - 1], p) <= 0)
                hull.RemoveAt(hull.Count - 1);
            hull.Add(p);
        }

        // Remove the last point (same as the first one)
        hull.RemoveAt(hull.Count - 1);

        return hull;
    }
}
