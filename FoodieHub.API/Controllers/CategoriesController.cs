using AutoMapper;
using FoodieHub.API.Context;
using FoodieHub.API.Dtos.CategoryDtos;
using FoodieHub.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FoodieHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiContext _apiContext;
        private readonly IMapper _mapper;

        public CategoriesController(ApiContext apiContext, IMapper mapper)
        {
            _apiContext = apiContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _apiContext.Categories.ToList();
            var mappedCategories = _mapper.Map<List<ResultCategoryDto>>(categories);
            return Ok(mappedCategories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _apiContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            var mappedCategory = _mapper.Map<GetByIdCategoryDto>(category);
            return Ok(mappedCategory);
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
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
        public IActionResult UpdateCategory(UpdateCategoryDto updateCategory)
        {
            var category = _mapper.Map<Category>(updateCategory);
            _apiContext.Categories.Update(category);
            _apiContext.SaveChanges();
            return Ok("Category updated successfully.");
        }


    }
}
