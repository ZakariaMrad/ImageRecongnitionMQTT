using System.Drawing;
using Emgu.CV;
using Emgu.CV.Aruco;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Microsoft.AspNetCore.Mvc;

namespace ImageRecognitionMQTT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BeamsController : ControllerBase
    {
        private readonly ImageRecognitionContext _context;

        public BeamsController(ImageRecognitionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBeams()
        {
            var beams = _context.GetBeams();
            if (beams == null)
            {
                return NotFound("No beams found.");
            }
            return Ok(beams);
        }

        [HttpGet("{id}")]
        public IActionResult GetBeam(string id)
        {
            var (error, beam) = _context.GetBeamByIdOrName(id);
            if (error != null)
            {
                return NotFound(error);
            }
            return Ok(beam);
        }

        [HttpPost]
        public IActionResult CreateBeam([FromBody] BeamModel beamModel)
        {
            if (beamModel.Name == "")
            {
                return BadRequest("Name is required.");
            }

            var markerValue = "0";
            while (_context.Beams.Any(b => b.MarkerValue == markerValue))
            {
                markerValue = (int.Parse(markerValue) + 1).ToString();
            }
            var IdBeam = Guid.NewGuid().ToString();

            var beam = new BeamModel
            {
                IdBeam = IdBeam,
                MarkerValue = markerValue,
                MarkerValueBase64 = ProcessHelper.DrawBeamArucoMarkerAsBase64(markerValue),
                Name = beamModel.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Items = [],
                CanBeSaved = true,
                Href = $"{Constants.BEAMS_URL}/{IdBeam}"
            };

            var (error, data) = _context.AddBeam(beam);
            if (error != null)
            {
                return BadRequest(error);
            }
            return Ok(data);
        }
    }
}
