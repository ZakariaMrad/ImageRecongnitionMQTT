using Microsoft.AspNetCore.Mvc;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.Aruco;
using System.Drawing;
using Emgu.CV.Structure;

namespace ImageRecognitionMQTT.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MarkersController : ControllerBase
    {
        private readonly ImageContext _context;
        private readonly MarkerDetectionService _markerDetectionService;

        public MarkersController(ImageContext context, MarkerDetectionService markerDetectionService)
        {
            _context = context;
            _markerDetectionService = markerDetectionService;

        }

        [HttpGet("{id}")]
        public IActionResult GetMarker(int id)
        {
            var image = _context.Images.Find(id);
            if (image == null)
            {
                return NotFound();
            }
            var markers = _markerDetectionService.DetectMarkers(image);


            return Ok(markers);
        }
    }
}
