using AutoMapper;
using FluentValidation;
using FoodieHub.API.Context;
using FoodieHub.API.Dtos.ChefDtos;
using FoodieHub.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FoodieHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefsController : ControllerBase
    {
        private readonly ApiContext _apiContext;
        private readonly IMapper _mapper;
        private readonly IValidator<Chef> _chefValidator;

        public ChefsController(ApiContext apiContext, IMapper mapper, IValidator<Chef> chefValidator)
        {
            _apiContext = apiContext;
            _mapper = mapper;
            _chefValidator = chefValidator;
        }

        [HttpGet]
        public IActionResult GetChefs()
        {
            var values = _apiContext.Chefs.ToList();
            var mappedValues = _mapper.Map<List<ResultChefDto>>(values);
            return Ok(mappedValues);
        }

        [HttpGet("{id}")]
        public IActionResult GetChef(int id)
        {
            var chef = _apiContext.Chefs.Find(id);
            if (chef == null)
            {
                return NotFound("Chef not found.");
            }

            var mappedChef = _mapper.Map<GetByIdChefDto>(chef);
            return Ok(mappedChef);
        }

        [HttpPost]
        public IActionResult AddChef(CreateChefDto chefDto)
        {
            var addedChef = _mapper.Map<Chef>(chefDto);
            var validationResult = _chefValidator.Validate(addedChef);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            _apiContext.Chefs.Add(addedChef);
            _apiContext.SaveChanges();
            return Ok(addedChef);
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
        public IActionResult UpdateChef(UpdateChefDto chefDto)
        {
            var existingChef = _apiContext.Chefs.Find(chefDto.ChefID);
            if (existingChef == null)
            {
                return NotFound("Chef not found.");
            }

            var updatedChef = _mapper.Map<Chef>(chefDto);

            var validationResult = _chefValidator.Validate(updatedChef);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            _mapper.Map(chefDto, existingChef);
            _apiContext.SaveChanges();
            return Ok(existingChef);
        }
    }
}
