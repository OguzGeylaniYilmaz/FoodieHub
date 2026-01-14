using FoodieHub.API.Context;
using Microsoft.AspNetCore.Mvc;

namespace FoodieHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly ApiContext _context;

        public StatisticsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet("ProductCount")]
        public IActionResult GetProductCount()
        {
            var productCount = _context.Products.Count();
            return Ok(productCount);
        }

        [HttpGet("ReservationCount")]
        public IActionResult GetReservationCount()
        {
            var reservationCount = _context.Reservations.Count();
            return Ok(reservationCount);
        }

        [HttpGet("ChefCount")]
        public IActionResult GetChefCount()
        {
            var chefCount = _context.Chefs.Count();
            return Ok(chefCount);
        }

        [HttpGet("CategoryCount")]
        public IActionResult GetTotalCustomerCount()
        {
            var categoryCount = _context.Categories.Count();
            return Ok(categoryCount);
        }
    }
}
