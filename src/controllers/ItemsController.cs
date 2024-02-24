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
    public class ItemsController : ControllerBase
    {
        private readonly ImageRecognitionContext _context;

        public ItemsController(ImageRecognitionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetItems()
        {
            var beams = _context.GetItems();
            if (beams == null)
            {
                return NotFound("No beams found.");
            }
            return Ok(beams);
        }

        [HttpGet("{id}")]
        public IActionResult GetItem(string id)
        {
            var (error,item) = _context.GetItemById(id);
            if (error != null)
            {
                return NotFound(error);
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateItem([FromBody] ItemModel itemModel)
        {
            if (itemModel.Name == "")
            {
                return BadRequest("Name is required.");
            }
            // Select a marker value that
            // is not already in use
            var markerValue = "0";
            while (_context.Items.Any(i => i.MarkerValue == markerValue))
            {
                markerValue = (int.Parse(markerValue) + 1).ToString();
            }

            var IdItem = Guid.NewGuid().ToString();

            var item = new ItemModel{
                IdItem = IdItem,
                Name = itemModel.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                MarkerValue = markerValue,
                MarkerValueBase64 = ProcessHelper.DrawArucoMarkerAsBase64(markerValue),
                Href = $"{Constants.ITEMS_URL}/{IdItem}"
            };
           

            var (error, data) = _context.CreateItem(item);
            if (error != null)
            {
                return BadRequest(error);
            }
            return Ok(data);
        }
    }
}
