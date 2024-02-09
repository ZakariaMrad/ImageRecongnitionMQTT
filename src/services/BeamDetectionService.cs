// using System.Drawing;
// using Emgu.CV;
// using Emgu.CV.Aruco;
// using Emgu.CV.CvEnum;
// using Emgu.CV.Structure;
// using Emgu.CV.Util;
// using ImageRecognitionMQTT.Enums;


// public class BeamDetectionService
// {

//     public BeamModel? DetectBeam(List<MarkerModel> markerModels, int Id)
//     {
//         try {
//             //Keep only the markers that have the id of the beam
//             var beamMarkers = markerModels.Where(m => m.Id == Id).ToList();

//             //If there are no markers with the id of the beam, return null
//             if (beamMarkers.Count == 0) return null;

//             var beam = new BeamModel(Id, beamMarkers.Select(m => m.Position).ToList());
//             return beam;
//         } catch (System.Exception e) {
//             Console.WriteLine(e);
//             return null;
//         }
//     }

//     public void WriteBeamOnImage(ImageModel image, BeamModel beam)
//     {
//         Mat matImage = CvInvoke.Imread(image.Path, ImreadModes.Color);
//         //draw a lign between a marker and the next one
//         CvInvoke.Line(matImage, beam.Markers[(int)BeamMarkers.TopLeft].ToPoint(), beam.Markers[(int)BeamMarkers.TopRight].ToPoint(), new MCvScalar(255, 0, 0), 2); // Red
//         CvInvoke.Line(matImage, beam.Markers[(int)BeamMarkers.TopRight].ToPoint(), beam.Markers[(int)BeamMarkers.BottomRight].ToPoint(), new MCvScalar(0, 255, 0), 2); // Green
//         CvInvoke.Line(matImage, beam.Markers[(int)BeamMarkers.BottomRight].ToPoint(), beam.Markers[(int)BeamMarkers.BottomLeft].ToPoint(), new MCvScalar(0, 0, 255), 2); // Blue
//         CvInvoke.Line(matImage, beam.Markers[(int)BeamMarkers.BottomLeft].ToPoint(), beam.Markers[(int)BeamMarkers.TopLeft].ToPoint(), new MCvScalar(255, 255, 0), 2); // Yellow

//         // Save the image
//         CvInvoke.Imwrite(image.Path, matImage);

//         Console.WriteLine("Beam written on image with path: " + image.Path);
//     }

//     public Mat DrawBeamOnImage(Mat matImage, BeamModel? beam)
//     {
//         if (beam == null) return matImage;
//         //draw a lign between a marker and the next one
//         CvInvoke.Line(matImage, beam.Markers[(int)BeamMarkers.TopLeft].ToPoint(), beam.Markers[(int)BeamMarkers.TopRight].ToPoint(), new MCvScalar(255, 0, 0), 2); // Red
//         CvInvoke.Line(matImage, beam.Markers[(int)BeamMarkers.TopRight].ToPoint(), beam.Markers[(int)BeamMarkers.BottomRight].ToPoint(), new MCvScalar(0, 255, 0), 2); // Green
//         CvInvoke.Line(matImage, beam.Markers[(int)BeamMarkers.BottomRight].ToPoint(), beam.Markers[(int)BeamMarkers.BottomLeft].ToPoint(), new MCvScalar(0, 0, 255), 2); // Blue
//         CvInvoke.Line(matImage, beam.Markers[(int)BeamMarkers.BottomLeft].ToPoint(), beam.Markers[(int)BeamMarkers.TopLeft].ToPoint(), new MCvScalar(255, 255, 0), 2); // Yellow

//         return matImage;
//     }
// }