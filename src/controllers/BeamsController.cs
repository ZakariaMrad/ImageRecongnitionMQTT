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

        public BeamsController()
        {
        }

        [HttpPost]
        public IActionResult CreateBeam([FromBody] BeamModel beamModel)
        {
            return Ok();
        }
    }
}
