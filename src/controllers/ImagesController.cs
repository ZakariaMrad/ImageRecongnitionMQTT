using System.IO.Abstractions;
using Emgu.CV;
using MediaToolkit;
using MediaToolkit.Model;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace ImageRecognitionMQTT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly ImageRecognitionContext _context;

        public ImagesController(ImageRecognitionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Getimages()
        {
            var latest = Request.Query["latest"].ToArray().Length != 0;
            var base64 = Request.Query["base64"].ToArray().Length != 0;
            if (latest)
            {
                var image = _context.GetLatestImage();
                if (image == null)
                {
                    return NotFound("No images found.");
                }
                return Ok(base64? ImageHelper.GetBase64(image) : image);
            }
            var images = _context.GetImages();
            return Ok(base64? ImageHelper.GetBase64(images) : images);
        }

        [HttpPost]
        public IActionResult createImage([FromBody] ImageModel? imageModel)
        {
            if (imageModel == null || imageModel.AsBase64 == null || imageModel.TakenBy == "")
            {
                return BadRequest("Base64 and TakenBy are required.");
            }

            string IdImage = Guid.NewGuid().ToString();
            string path = Path.Combine("wwwroot", "images", IdImage + ".jpg");
            var image = new ImageModel
            {
                IdImage = IdImage,
                Path = path,
                CreatedAt = DateTime.Now,
                TakenBy = imageModel.TakenBy,
                DeleteAt = DateTime.Now.AddSeconds(30)
            };

            Mat mat = Base64Helper.ToMat(imageModel.AsBase64);
            ImageHelper.SaveImage(mat, path);

            Task.Run(() =>
            {
                ImageHelper.DrawMarkers(mat, path);
            });

            _context.AddImage(image);
            _context.SaveChanges();
            return Ok(image);
        }
    }
}
