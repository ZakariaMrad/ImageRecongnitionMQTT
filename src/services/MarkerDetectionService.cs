using System.Drawing;
using Emgu.CV;
using Emgu.CV.Aruco;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

public class MarkerDetectionService
{
    private (VectorOfInt, VectorOfVectorOfPointF, Mat?) DetectMarkers(ImageModel image)
    {

        Mat matImage = CvInvoke.Imread(image.Path, ImreadModes.Color);

        return DetectMarkers(matImage);
    }
    private (VectorOfInt, VectorOfVectorOfPointF, Mat?) DetectMarkers(Mat image)
    {

        Dictionary dictionary = new Dictionary(Dictionary.PredefinedDictionaryName.Dict7X7_1000);
        DetectorParameters parameters = DetectorParameters.GetDefault();
        VectorOfInt ids = new VectorOfInt();
        VectorOfVectorOfPointF corners = new VectorOfVectorOfPointF();

        ArucoInvoke.DetectMarkers(image, dictionary, corners, ids, parameters);
        return (ids, corners, image);
    }

    public List<MarkerModel> GetDetectedMarkerAsModel(ImageModel image)
    {
        var (ids, corners, _) = DetectMarkers(image);
        List<MarkerModel> MarkerList = [];
        for (int i = 0; i < ids.Size; i++)
        {
            MarkerModel marker = new MarkerModel(ids[i], corners[i]);
            MarkerList.Add(marker);

        }
        return MarkerList;
    }
    public void WriteMarkersOnImage(ImageModel image)
    {
        var (ids, corners, matImage) = DetectMarkers(image);
        Console.WriteLine("Detected " + ids.Size + " markers");

        // Draw the detected markers on the image in blue
        ArucoInvoke.DrawDetectedMarkers(matImage, corners, ids, new MCvScalar(5, 13, 163));

        // Save the image
        CvInvoke.Imwrite(image.Path, matImage);

        Console.WriteLine("Markers written on image with path: " + image.Path);
    }

    internal void WriteMarkerOnImage(ImageModel image, MCvScalar mCvScalar)
    {
        var (ids, corners, matImage) = DetectMarkers(image);
        Console.WriteLine("Detected " + ids.Size + " markers");

        // For each marker, draw a circle on the image in the position of the marker
        for (int i = 0; i < ids.Size; i++)
        {
            var marker = new MarkerModel(ids[i], corners[i]);
            var center = marker.Position;
            CvInvoke.Circle(matImage, center.ToPoint(), 5, mCvScalar, 2);
        }

        // Save the image
        CvInvoke.Imwrite(image.Path, matImage);

        Console.WriteLine("Markers written on image with path: " + image.Path);
    }

    internal List<MarkerModel> GetDetectedMarkerAsModel(Mat image)
    {
        var (ids, corners, _) = DetectMarkers(image);
        List<MarkerModel> MarkerList = [];
        for (int i = 0; i < ids.Size; i++)
        {
            MarkerModel marker = new MarkerModel(ids[i], corners[i]);
            MarkerList.Add(marker);

        }
        return MarkerList;
    }

    internal Mat DrawMarkersOnImage(Mat image, List<MarkerModel> markers)
    {
        for (int i = 0; i < markers.Count; i++)
        {
            var marker = markers[i];
            var center = marker.Position;
            CvInvoke.Circle(image, center.ToPoint(), 20, new MCvScalar(5, 13, 163), 2);
        }
        return image;
    }
}