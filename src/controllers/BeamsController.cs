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
            var (error,beam) = _context.GetBeamByIdOrName(id);
            if (error != null)
            {
                return NotFound(error);
            }
            return Ok(beam);
        }

        [HttpPost]
        public IActionResult CreateBeam([FromBody] BeamModel beamModel)
        {
            if (beamModel.IdBeam == "" || beamModel.Name == "")
            {
                return BadRequest("IdBeam and Name are required.");
            }

            var beam = new BeamModel
            {
                IdBeam = beamModel.IdBeam,
                Name = beamModel.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Items = [],
                Corners = []
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
