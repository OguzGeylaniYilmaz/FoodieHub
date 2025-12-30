using AutoMapper;
using FoodieHub.API.Context;
using FoodieHub.API.Dtos.ReservationDtos;
using Microsoft.AspNetCore.Mvc;

namespace FoodieHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public ReservationsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetReservations()
        {
            var reservations = _context.Reservations.ToList();
            var mappedReservations = _mapper.Map<List<ResultReservationDto>>(reservations);
            return Ok(mappedReservations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound("Reservation not found");
            }
            var mappedReservation = _mapper.Map<GetReservationByIdDto>(reservation);
            return Ok(mappedReservation);
        }

        [HttpPost]
        public IActionResult CreateReservation(CreateReservationDto createReservationDto)
        {
            var reservation = _mapper.Map<Entities.Reservation>(createReservationDto);
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return Ok("Reservation added successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound("Reservation not found");
            }
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return Ok("Reservation deleted successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReservation(int id, UpdateReservationDto updateReservationDto)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound("Reservation not found");
            }
            _mapper.Map(updateReservationDto, reservation);
            await _context.SaveChangesAsync();
            return Ok("Reservation updated successfully");
        }
    }
}
