using System.Drawing;
using System.Text.Json.Serialization;
using System.Text.Json;
using Emgu.CV;
using Emgu.CV.Aruco;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using ImageRecognitionMQTT.Enums;
using System.Drawing.Drawing2D;



public class BeamDetectionService
{
    private readonly ImageRecognitionContext _context;

    public BeamDetectionService(ImageRecognitionContext context)
    {
        _context = context;
    }

    public void HandleBeams(Mat mat, string path)
    {
        var markers = MarkerDetectionHelper.GetMarkersAsModel(mat);
        var beamMarkers = MarkerDetectionHelper.GetBeamMarkersAsModel(mat);
        var beams = new List<BeamModel>();
        if (!FindBeam(beamMarkers, out beams))
        {
            Console.WriteLine("No beams found in the image");
            return;
        }

        

        beams.ForEach(beam =>
        {
            Console.WriteLine("Updating beam # " + beam.IdBeam);
            updateBeam(beam, beamMarkers, markers, mat, path);

        });



    }

    private void updateBeam(BeamModel beam, List<MarkerModel> beamMarkers, List<MarkerModel> markers, Mat mat, string path)
    {
        beam.UpdatedAt = DateTime.Now;
        var cornerMarkers = beamMarkers.Where(marker => marker.IdMarker == beam.IdBeam).ToList();
        if (cornerMarkers.Count != 4)
        {
            Console.WriteLine("Beam # " + beam.IdBeam + " has more or less than 4 markers");
            return;
        }

        beam = addItems(beam, cornerMarkers, markers, mat, path);
    }

    private BeamModel addItems(BeamModel beam, List<MarkerModel> cornerMarkers, List<MarkerModel> markers, Mat mat, string path)
    {
        markers.ForEach(marker =>
        {
            if (GeometryHelper.IsPointInCorners(marker, cornerMarkers, mat, path))
            {
                Console.WriteLine("Marker # " + marker.IdMarker + " is inside beam # " + beam.IdBeam);
            }
            else
            {
                // Console.WriteLine("Marker # " + marker.IdMarker + " is outside beam # " + beam.IdBeam);
            }
        });

        return beam;
    }

    private bool FindBeam(List<MarkerModel> markers, out List<BeamModel> beams)
    {
        beams = _context.GetBeams();
        if (beams == null)
        {
            return false;
        }
        beams = beams.Where(beam => markers.Any(marker => marker.IdMarker == beam.IdBeam)).ToList();
        return beams.Count > 0;
    }

}