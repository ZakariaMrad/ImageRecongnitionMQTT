using Emgu.CV;
using Emgu.CV.Aruco;
using Emgu.CV.Structure;

public static class MarkerHelper
{
    public static string CreateMarker(string markerValue)
    {
        var marker = ProcessHelper.DrawArucoMarkerAsBase64(markerValue);
        return marker;
    }
}
