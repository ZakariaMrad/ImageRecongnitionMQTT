using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace ImageRecognitionMQTT.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly ImageContext _context;
        public ImagesController(ImageContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult PostMarker([FromBody] ImageModel imageModel)
        {
            if (imageModel == null || imageModel.AsBase64 == null)
            {
                return BadRequest("Invalid image model.");
            }

            if (imageModel.Name == null)
            {
                imageModel.Name = "Image-" + Guid.NewGuid().ToString() + "." + Base64Helper.GetFileExtensionFromBase64(imageModel.AsBase64);
            }

            imageModel.Path = Path.Combine("wwwroot", "images", imageModel.Name);
            Base64Helper.SaveFileFromBase64(imageModel.AsBase64, imageModel.Path);

            imageModel.AsBase64 = null;

            _context.Images.Add(imageModel);
            _context.SaveChanges();
            return Ok(imageModel);
        }

        [HttpGet]
        public IActionResult GetMarkers()
        {
            var latest = Request.Query["latest"].ToArray().Length != 0;
            var images = _context.Images.ToList();

            if (latest)
            {
                images = images.OrderByDescending(i => i.Id).Take(1).ToList();

            }
            return Ok(images);
        }

        [HttpGet("{id}")]
        public IActionResult GetMarker(int id)
        {
            var image = _context.Images.Find(id);
            if (image == null)
            {
                return NotFound();
            }
            return Ok(image);
        }
    }
}
