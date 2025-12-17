using AutoMapper;
using FoodieHub.API.Context;
using FoodieHub.API.Dtos.AboutDtos;
using FoodieHub.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodieHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly ApiContext _apiContext;
        private readonly IMapper _mapper;

        public AboutsController(ApiContext apiContext, IMapper mapper)
        {
            _apiContext = apiContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAbouts()
        {
            var abouts = _apiContext.Abouts.ToList();
            var aboutDtos = _mapper.Map<List<ResultAboutDto>>(abouts);
            return Ok(aboutDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetAboutById(int id)
        {
            var about = _apiContext.Abouts.Find(id);
            if (about == null)
            {
                return NotFound();
            }
            var aboutDto = _mapper.Map<GetAboutByIdDto>(about);
            return Ok(aboutDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            var about = await _apiContext.Abouts.FindAsync(id);
            if (about == null)
            {
                return NotFound();
            }
            _apiContext.Abouts.Remove(about);
            _apiContext.SaveChanges();
            return Ok("About deleted successfully");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            var about = _mapper.Map<About>(createAboutDto);
            await _apiContext.Abouts.AddAsync(about);
            await _apiContext.SaveChangesAsync();
            return Ok("About created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAbout(int id, UpdateAboutDto updateAboutDto)
        {
            var about = await _apiContext.Abouts.FirstOrDefaultAsync(x => x.AboutID == id);
            if (about == null)
            {
                return NotFound($"About with id {id} not found.");
            }
            var result = _mapper.Map(updateAboutDto, about);
            await _apiContext.SaveChangesAsync();
            return Ok(result);
        }
    }
}