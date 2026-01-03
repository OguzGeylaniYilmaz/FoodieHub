using AutoMapper;
using FoodieHub.API.Context;
using FoodieHub.API.Dtos.GalleryDtos;
using FoodieHub.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodieHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleriesController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public GalleriesController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetGalleries()
        {
            var galleries = await _context.Galleries.ToListAsync();
            var mappedGalleries = _mapper.Map<List<ResultGalleryDto>>(galleries);
            return Ok(mappedGalleries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGallery(int id)
        {
            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return NotFound();
            }
            var mappedGallery = _mapper.Map<GetGalleryByIdDto>(gallery);
            return Ok(mappedGallery);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGallery([FromBody] CreateGalleryDto createGalleryDto)
        {
            var gallery = _mapper.Map<Gallery>(createGalleryDto);

            if (gallery == null)
            {
                return BadRequest();
            }

            _context.Galleries.Add(gallery);
            await _context.SaveChangesAsync();
            return Ok("Gallery created successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGallery(int id)
        {
            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return NotFound();
            }
            _context.Galleries.Remove(gallery);
            await _context.SaveChangesAsync();
            return Ok("Gallery deleted successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGallery(int id, [FromBody] UpdateGalleryDto updateGalleryDto)
        {
            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return NotFound();
            }
            _mapper.Map(updateGalleryDto, gallery);
            _context.Galleries.Update(gallery);
            await _context.SaveChangesAsync();
            return Ok("Gallery updated successfully.");
        }
    }
}