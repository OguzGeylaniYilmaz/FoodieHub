using AutoMapper;
using FluentValidation;
using FoodieHub.API.Context;
using FoodieHub.API.Dtos.ServiceDtos;
using FoodieHub.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FoodieHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<Service> _serviceValidator;

        public ServicesController(ApiContext context, IMapper mapper, IValidator<Service> serviceValidator)
        {
            _context = context;
            _mapper = mapper;
            _serviceValidator = serviceValidator;
        }

        [HttpGet]
        public IActionResult GetServices()
        {
            var services = _context.Services.ToList();
            var serviceDtos = _mapper.Map<List<ResultServiceDto>>(services);
            return Ok(serviceDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetService(int id)
        {
            var service = _context.Services.Find(id);
            if (service == null)
            {
                return NotFound();
            }
            var serviceDto = _mapper.Map<GetByIdServiceDto>(service);
            return Ok(serviceDto);
        }

        [HttpDelete]
        public IActionResult DeleteService(int id)
        {
            var service = _context.Services.Find(id);
            if (service == null)
            {
                return NotFound();
            }
            _context.Services.Remove(service);
            _context.SaveChanges();
            return Ok("Service deletes successfuly");
        }

        [HttpPost]
        public IActionResult CreateService(CreateServiceDto createService)
        {
            var service = _mapper.Map<Service>(createService);
            var validationResult = _serviceValidator.Validate(service);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            }
            _context.Services.Add(service);
            _context.SaveChanges();
            return Ok("Service created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateService(int id, UpdateServiceDto updateService)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound();

            var validationResult = _serviceValidator.Validate(service);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            }
            _mapper.Map(updateService, service);
            await _context.SaveChangesAsync();
            return Ok("Service updated successfully");
        }
    }
}