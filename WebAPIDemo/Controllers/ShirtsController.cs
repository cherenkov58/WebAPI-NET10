using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Filters;
using WebAPIDemo.Filters.ActionFilters;
using WebAPIDemo.Filters.ExceptionFilters;
using WebAPIDemo.Models;
using WebAPIDemo.Models.Repositories;

namespace WebAPIDemo.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        private List<Shirt> shirts = new List<Shirt>()
        {
            new Shirt { ShirtId = 1, Brand = "My Brand", Color = "Blue", Gender = "Men", Price = 30, Size = 10 },
            new Shirt { ShirtId = 2, Brand = "My Brand", Color = "Black", Gender = "Men", Price = 35, Size = 12 },
            new Shirt { ShirtId = 3, Brand = "Your Brand", Color = "Pink", Gender = "Women", Price = 28, Size = 8 },
            new Shirt { ShirtId = 4, Brand = "Your Brand", Color = "Yellow", Gender = "Women", Price = 30, Size = 9 }
        };

        [HttpGet]
        public IActionResult GetShirts()
        {
            return Ok(ShirtRepository.GetShirts());
        }

		/*	[HttpGet("{id}")]
	    public string GetShirtsBiId(int id,[FromHeader(Name = "Color")] string color)
	    {
		    return $"Reading shirt: {id}, color: {color}";
	    }
	    */

		[HttpGet("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult GetShirtById(int id)
        {
			/*if (id <= 0) 
			return BadRequest();

		    var shirt= ShirtRepository.GetShirtById(id);
		    if (shirt == null)
			    return NotFound();*/

			return Ok(ShirtRepository.GetShirtById(id));
        }

        [HttpPost]
        [Shirt_ValidateCreateShirtFilter]
        public IActionResult CreateShirt([FromBody] Shirt shirt)
        {
            ShirtRepository.AddShirt(shirt);

            return CreatedAtAction(nameof(GetShirtById),
                new { id = shirt.ShirtId },
                shirt);
        }

        [HttpPut("{id}")]
        [Shirt_ValidateShirtIdFilter]
        [Shirt_ValidateUpdateShirtFilter]
        [Shirt_HandleUpdateExceptionsFilter]
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {
            ShirtRepository.UpdateShirt(shirt);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShirt(int id)
        {            
            return Ok($"Deleting shirt: {id}");
        }
    }
}
