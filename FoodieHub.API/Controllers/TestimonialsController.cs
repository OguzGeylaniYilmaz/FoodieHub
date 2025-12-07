using AutoMapper;
using FluentValidation;
using FoodieHub.API.Context;
using FoodieHub.API.Dtos.TestimonialDtos;
using FoodieHub.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FoodieHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<Testimonial> _validator;

        public TestimonialsController(ApiContext context, IMapper mapper, IValidator<Testimonial> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult GetTestimonials()
        {
            var testimonials = _context.Testimonials.ToList();
            var mappedTestimonials = _mapper.Map<List<ResultTestimonialDto>>(testimonials);
            return Ok(mappedTestimonials);
        }

        [HttpGet("{id}")]
        public IActionResult GetTestimonialById(int id)
        {
            var testimonial = _context.Testimonials.Find(id);
            if (testimonial == null)
            {
                return NotFound();
            }
            var mappedTestimonial = _mapper.Map<GetByIdTestimonialDto>(testimonial);
            return Ok(mappedTestimonial);
        }

        [HttpDelete]
        public IActionResult DeleteTestimonial(int id)
        {
            var testimonial = _context.Testimonials.Find(id);
            if (testimonial == null)
            {
                return NotFound();
            }
            _context.Testimonials.Remove(testimonial);
            _context.SaveChanges();
            return Ok("Testimonial deleted successfuly");
        }

        [HttpPost]
        public IActionResult CreateTestimonial(CreateTestimonialDto createTestimonial)
        {
            var testimonial = _mapper.Map<Testimonial>(createTestimonial);
            var validationResult = _validator.Validate(testimonial);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            _context.Testimonials.Add(testimonial);
            _context.SaveChanges();
            return Ok("Testimonial created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTestimonial(int id, UpdateTestimonialDto testimonialDto)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);

            if (testimonial == null)
                return NotFound("Record not found");

            _mapper.Map(testimonialDto, testimonial);

            await _context.SaveChangesAsync();

            return Ok("Testimonial updated successfully");
        }
    }
}
