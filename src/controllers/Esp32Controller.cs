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
        public IActionResult CreateEsp32([FromBody] Esp32Model esp32Model)
        {
            if (esp32Model==null || esp32Model.MacAddress == "")
            {
                return BadRequest("MacAddress is required.");
            }
            var IdEsp32 = Guid.NewGuid().ToString();
            var esp32 = new Esp32Model
            {
                IdEsp32 = IdEsp32,
                MacAddress = esp32Model.MacAddress,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Token = Guid.NewGuid().ToString(),
                Href = $"{Constants.ESP32S_URL}/{IdEsp32}"
            };
            var (error, data) = _context.AddEsp32(esp32);
            if (error != null)
            {
                return BadRequest(error);
            }
            return Ok(data);
        }
    }
}
