using Microsoft.AspNetCore.Mvc;
namespace ImageRecognitionMQTT.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class Esp32sController : ControllerBase
    {
        private readonly ImageRecognitionContext _context;
        public Esp32sController( ImageRecognitionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetEsp32s()
        {
            return Ok(_context.Esp32s);
        }

        [HttpGet("{id}")]
        public IActionResult GetEsp32sByIdOrMacAddress(string id)
        {
            var (error, data) = _context.GetEsp32ByIdOrMacAddress(id);
            if (error != null)
            {
                return NotFound(error);
            }
            return Ok(data);
        }

        [HttpPost]
        public IActionResult CreateEsp32([FromBody] Esp32Model esp32)
        {
            if (esp32==null || esp32.MacAddress == "")
            {
                return BadRequest("MacAddress is required.");
            }
            esp32.CreatedAt = DateTime.Now;
            esp32.UpdatedAt = DateTime.Now;
            esp32.Token = Guid.NewGuid().ToString();
            esp32.IdEsp32 = Guid.NewGuid().ToString();
            var (error, data) = _context.AddEsp32(esp32);
            if (error != null)
            {
                return BadRequest(error);
            }
            return Ok(esp32);
        }
    }
}
