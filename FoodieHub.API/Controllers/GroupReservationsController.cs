using AutoMapper;
using FoodieHub.API.Context;
using FoodieHub.API.Dtos.GroupReservationDtos;
using FoodieHub.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodieHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupReservationsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public GroupReservationsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupReservations()
        {
            var groupReservations = await _context.GroupReservations.ToListAsync();
            var mappedGroupReservations = _mapper.Map<List<ResultGroupReservationDto>>(groupReservations);
            return Ok(mappedGroupReservations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupReservation(int id)
        {
            var groupReservation = await _context.GroupReservations.FindAsync(id);
            if (groupReservation == null)
            {
                return NotFound();
            }
            var mappedGroupReservation = _mapper.Map<GetGroupReservationDto>(groupReservation);
            return Ok(mappedGroupReservation);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroupReservation(CreateGroupReservationDto createDto)
        {
            var groupReservation = _mapper.Map<GroupReservation>(createDto);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.GroupReservations.Add(groupReservation);
            await _context.SaveChangesAsync();
            return Ok("Group reservation created successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroupReservation(int id, UpdateGroupReservationDto updateDto)
        {
            var groupReservation = await _context.GroupReservations.FindAsync(id);
            if (groupReservation == null)
            {
                return NotFound();
            }
            _mapper.Map(updateDto, groupReservation);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.GroupReservations.Update(groupReservation);
            await _context.SaveChangesAsync();
            return Ok("Group reservation updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupReservation(int id)
        {
            var groupReservation = await _context.GroupReservations.FindAsync(id);
            if (groupReservation == null)
            {
                return NotFound();
            }
            _context.GroupReservations.Remove(groupReservation);
            await _context.SaveChangesAsync();
            return Ok("Group reservation deleted successfully.");
        }
    }
}