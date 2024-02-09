using System.Drawing;
using System.IO;
using Emgu.CV;
using Emgu.CV.Aruco;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

public static class ImageHelper
{
    public static string SaveImage(Mat image, string path)
    {
        CvInvoke.Imwrite(path, image);
        return path;
    }

    public static void DeleteImages(List<string> list)
    {
        foreach (var filePath in list)
        {
            try
            {
                File.Delete(filePath);
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }
        }
    }

    public static Mat ReadImage(string path)
    {
        return CvInvoke.Imread(path, ImreadModes.Color);
    }

    public static void DrawMarkers(Mat mat, string path)
    {
        var markers = MarkerDetectionHelper.GetMarkersAsModel(mat);
        foreach (var marker in markers)
        {
            CvInvoke.Circle(mat, marker.Position.ToPoint(), 30, new MCvScalar(5, 13, 163), 10);
        }
        SaveImage(mat, path);
    }
    public static void DrawBeamMarkers(Mat mat, string path)
    {
        var markers = MarkerDetectionHelper.GetBeamMarkersAsModel(mat);
        foreach (var marker in markers)
        {
            CvInvoke.Circle(mat, marker.Position.ToPoint(), 30, new MCvScalar(21, 163, 5), 10);
        }
        SaveImage(mat, path);
    }

    public static ImageModel GetBase64(ImageModel image)
    {
        var imageMat = ReadImage(image.Path);
        var base64 = Base64Helper.MatToBase64(imageMat);
        image.AsBase64 = base64;
        return image;
    }

    public static List<ImageModel> GetBase64(List<ImageModel> images)
    {
        List<ImageModel> base64Images = new List<ImageModel>();
        foreach (var image in images)
        {
            base64Images.Add(GetBase64(image));
        }
        return base64Images;
    }
}
