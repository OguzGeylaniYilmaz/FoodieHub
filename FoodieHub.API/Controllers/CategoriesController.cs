using FoodieHub.API.Context;
using FoodieHub.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FoodieHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public CategoriesController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _apiContext.Categories.ToList();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _apiContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }
            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if (category == null)
            {
                return BadRequest("Category cannot be null.");
            }
            _apiContext.Categories.Add(category);
            _apiContext.SaveChanges();
            return Ok(category);
        }

        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var category = _apiContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }
            _apiContext.Categories.Remove(category);
            _apiContext.SaveChanges();
            return Ok("Category deleted successfully.");
        }

        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            var existingCategory = _apiContext.Categories.Find(category.CategoryID);
            if (existingCategory == null)
            {
                return NotFound("Category not found.");
            }
            existingCategory.CategoryName = category.CategoryName;
            _apiContext.SaveChanges();
            return Ok(existingCategory);
        }


    }
}
