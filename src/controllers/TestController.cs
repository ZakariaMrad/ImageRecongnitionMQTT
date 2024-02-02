using Microsoft.AspNetCore.Mvc;

namespace ImageRecognitionMQTT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTest()
        {
            // TODO: Implement your GET logic here
            var test = new TestModel
            {
                Id = 1,
                Text = "Test",
                Description = "This is a test"
            };
            return Ok(test);
        }

        [HttpPost("post")]
        public IActionResult PostTest()
        {
            // TODO: Implement your POST logic here
            return Ok("POST Test");
        }
    }
}
