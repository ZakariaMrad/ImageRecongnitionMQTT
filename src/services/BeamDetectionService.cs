using System.Drawing;
using Emgu.CV;
using Emgu.CV.Aruco;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using ImageRecognitionMQTT.Enums;
using MediaToolkit.Util;


public class BeamDetectionService
{
    private readonly ImageRecognitionContext _context;

    public BeamDetectionService(ImageRecognitionContext context)
    {
        _context = context;
    }

    public void HandleBeams(Mat mat)
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
            updateBeam(beam, beamMarkers,  markers);

        });



    }

    private void updateBeam(BeamModel beam, List<MarkerModel> beamMarkers, List<MarkerModel> markers)
    {
        beam.UpdatedAt = DateTime.Now;
        var cornerMarkers = beamMarkers.Where(marker => marker.IdMarker == beam.IdBeam).Select(marker => marker.ToCorner()).ToList();
        if (cornerMarkers.Count != 4) {
            Console.WriteLine("Beam # " + beam.IdBeam + " has more or less than 4 markers");
            _context.UpdateBeam(beam);
            return;
        }
        Console.WriteLine("Beam # " + beam.IdBeam + " has 4 markers");
        beam.Corners.AddRange(cornerMarkers);
        beam = addItems(beam, markers);
        _context.UpdateBeam(beam);

    }

    private BeamModel addItems(BeamModel beam, List<MarkerModel> markers)
    {
        markers.ForEach(marker =>
        {
            if (GeometryHelper.IsPointInCorners(marker, beam.Corners))
            {
                Console.WriteLine("Marker # " + marker.IdMarker + " is inside beam # " + beam.IdBeam);
            } else {
                Console.WriteLine("Marker # " + marker.IdMarker + " is outside beam # " + beam.IdBeam);
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