using FoodieHub.API.Context;
using FoodieHub.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FoodieHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefsController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public ChefsController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetChef(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid chef ID.");
            }
            return Ok(_apiContext.Chefs.Find(id));
        }

        [HttpGet]
        public IActionResult GetChefs()
        {
            var values = _apiContext.Chefs.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult AddChef(Chef chef)
        {
            if (chef == null)
            {
                return BadRequest("Chef cannot be null.");
            }
            _apiContext.Chefs.Add(chef);
            _apiContext.SaveChanges();
            return Ok(chef);
        }

        [HttpDelete]
        public IActionResult DeleteChef(int id)
        {
            var chef = _apiContext.Chefs.Find(id);
            if (chef == null)
            {
                return NotFound("Chef not found.");
            }
            _apiContext.Chefs.Remove(chef);
            _apiContext.SaveChanges();
            return Ok("Chef deleted successfully.");
        }

        [HttpPut]
        public IActionResult UpdateChef(Chef chef)
        {
            var existingChef = _apiContext.Chefs.Find(chef.ChefID);
            if (existingChef == null)
            {
                return NotFound("Chef not found.");
            }
            existingChef.NameSurname = chef.NameSurname;
            existingChef.Title = chef.Title;
            existingChef.Description = chef.Description;
            existingChef.ImageUrl = chef.ImageUrl;
            _apiContext.SaveChanges();
            return Ok(existingChef);
        }
    }
}
