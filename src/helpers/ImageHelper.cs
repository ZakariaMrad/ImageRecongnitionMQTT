using System.Drawing;
using System.IO;
using System.Threading.Tasks.Dataflow;
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

    public static Mat DrawMarkers(Mat mat, string path)
    {
        var markers = MarkerDetectionHelper.GetMarkersAsModel(mat);
        foreach (var marker in markers)
        {
            CvInvoke.Circle(mat, marker.Position.ToPoint(), 1, new MCvScalar(5, 13, 163), 1);
            //write id under the marker
            CvInvoke.PutText(mat, marker.IdMarker.ToString(), new Point(marker.Position.X -25, marker.Position.Y + 50), FontFace.HersheySimplex, 0.5, new MCvScalar(5, 13, 163), 1);
        }
        SaveImage(mat, path);
        return mat;
    }

    public static Mat DrawBeamMarkers(Mat mat, string path)
    {
        var markers = MarkerDetectionHelper.GetBeamMarkersAsModel(mat);
        foreach (var marker in markers)
        {
            CvInvoke.Circle(mat, marker.Position.ToPoint(), 1, new MCvScalar(21, 163, 5), 1);
            CvInvoke.PutText(mat, marker.IdMarker.ToString(), new Point(marker.Position.X -25, marker.Position.Y + 50), FontFace.HersheySimplex, 0.5, new MCvScalar(5, 13, 163), 1);

        }

        SaveImage(mat, path);
        return mat;
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
