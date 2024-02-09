using Emgu.CV;
using Emgu.CV.Aruco;
using Emgu.CV.Structure;
using Emgu.CV.Util;

public static class MarkerDetectionHelper
{
    public static List<MarkerModel> GetMarkersAsModel(Mat mat)
    {
        List<MarkerModel> MarkerList = GetMarkersAsModel(mat, Dictionary.PredefinedDictionaryName.Dict4X4_1000);
        MarkerList.AddRange(GetMarkersAsModel(mat, Dictionary.PredefinedDictionaryName.Dict5X5_1000));
        MarkerList.AddRange(GetMarkersAsModel(mat, Dictionary.PredefinedDictionaryName.Dict6X6_1000));
        return MarkerList;
    }

    public static List<MarkerModel> GetBeamMarkersAsModel(Mat mat)
    {
        List<MarkerModel> MarkerList = GetMarkersAsModel(mat, Dictionary.PredefinedDictionaryName.Dict7X7_1000);
        return MarkerList;
    }

    private static List<MarkerModel> MarkersToModel(VectorOfInt ids, VectorOfVectorOfPointF corners)
    {
        List<MarkerModel> MarkerList = new List<MarkerModel>();
        for (int i = 0; i < ids.Size; i++)
        {
            MarkerModel marker = new MarkerModel(ids[i], corners[i]);
            MarkerList.Add(marker);
        }
        return MarkerList;
    }

    public static List<MarkerModel> GetMarkersAsModel(Mat mat, Dictionary.PredefinedDictionaryName predefinedDictionaryName)
    {
        Dictionary dictionary = new Dictionary(predefinedDictionaryName);
        DetectorParameters parameters = DetectorParameters.GetDefault();
        VectorOfInt ids = new VectorOfInt();
        VectorOfVectorOfPointF corners = new VectorOfVectorOfPointF();

        ArucoInvoke.DetectMarkers(mat, dictionary, corners, ids, parameters);
        return MarkersToModel(ids, corners);
    }

}
