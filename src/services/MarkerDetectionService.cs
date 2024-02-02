using Emgu.CV;
using Emgu.CV.Aruco;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

public class MarkerDetectionService
{
    public List<MarkerModel> DetectMarkers(ImageModel image)
    {

        Mat matImage = CvInvoke.Imread(image.Path, ImreadModes.Color);
        CvInvoke.Imshow("Original Image", matImage);
        CvInvoke.WaitKey(0);
        CvInvoke.CvtColor(matImage, matImage, ColorConversion.Bgr2Gray);

        Dictionary dictionary = new Dictionary(Dictionary.PredefinedDictionaryName.Dict7X7_1000);
        DetectorParameters parameters = DetectorParameters.GetDefault();
        VectorOfInt ids = new VectorOfInt();
        VectorOfVectorOfPointF corners = new VectorOfVectorOfPointF();

        ArucoInvoke.DetectMarkers(matImage, dictionary, corners, ids, parameters);
        List<MarkerModel> MarkerList = [];
        for (int i = 0; i < ids.Size; i++)
        {
            MarkerModel marker = new MarkerModel(ids[i], corners[i]);
            MarkerList.Add(marker);

        }
        return MarkerList;
    }

    public BeamModel? DetectBeam(List<MarkerModel> markerModels, int Id)
    {
        //Keep only the markers that have the id of the beam

        var beamMarkers = markerModels.Where(m => m.Id == Id).ToList();

        //If there are no markers with the id of the beam, return null
        if (beamMarkers.Count == 0) return null;

        //Calculate the corners of the beam



        return new BeamModel();
    }
}