using AutoMapper;
using FluentValidation;
using FoodieHub.API.Context;
using FoodieHub.API.Dtos.EventDtos;
using FoodieHub.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FoodieHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<Event> _validator;

        public EventsController(ApiContext context, IMapper mapper, IValidator<Event> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            var events = _context.Events.ToList();
            var mappedEvents = _mapper.Map<List<ResultEventDto>>(events);
            return Ok(mappedEvents);
        }

        [HttpGet("{id}")]
        public IActionResult GetEventById(int id)
        {
            var eventEntity = _context.Events.Find(id);
            if (eventEntity == null)
            {
                return NotFound();
            }
            var mappedEvent = _mapper.Map<GetByIdEventDto>(eventEntity);
            return Ok(mappedEvent);
        }

        [HttpPost]
        public IActionResult CreateEvent(CreateEventDto createEvent)
        {
            var eventEntity = _mapper.Map<Event>(createEvent);
            var validationResult = _validator.Validate(eventEntity);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            _context.Events.Add(eventEntity);
            _context.SaveChanges();
            return Ok("Event created successfully");
        }

        [HttpPut]
        public IActionResult UpdateEvent(UpdateEventDto updateEvent)
        {
            var eventEntity = _context.Events.Find(updateEvent.EventID);
            if (eventEntity == null)
            {
                return NotFound("Event not found");
            }

            var validationResult = _validator.Validate(eventEntity);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            _mapper.Map(updateEvent, eventEntity);
            _context.SaveChanges();
            return Ok("Event updated successfully");
        }

        [HttpDelete]
        public IActionResult DeleteEvent(int id)
        {
            var eventEntity = _context.Events.Find(id);
            if (eventEntity == null)
            {
                return NotFound();
            }
            _context.Events.Remove(eventEntity);
            _context.SaveChanges();
            return Ok("Event deleted successfully");
        }
    }
}
