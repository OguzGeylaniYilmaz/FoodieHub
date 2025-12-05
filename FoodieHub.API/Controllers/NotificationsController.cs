using AutoMapper;
using FoodieHub.API.Context;
using FoodieHub.API.Dtos.NotificationDtos;
using FoodieHub.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FoodieHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public NotificationsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetNotifications()
        {
            var notifications = _context.Notifications.ToList();
            var notificationDtos = _mapper.Map<List<ResultNotificationDto>>(notifications);
            return Ok(notificationDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetNotification(int id)
        {
            var notification = _context.Notifications.Find(id);
            if (notification == null)
            {
                return NotFound();
            }
            var notificationDto = _mapper.Map<GetByIdNotificationDto>(notification);
            return Ok(notificationDto);
        }

        [HttpPost]
        public IActionResult CreateNotfication(CreateNotificationDto notificationDto)
        {
            var notification = _mapper.Map<Notification>(notificationDto);
            _context.Notifications.Add(notification);
            _context.SaveChanges();
            return Ok("Notification created successfully.");
        }

        [HttpPut]
        public IActionResult UpdateNotification(UpdateNotificationDto notificationDto)
        {
            var notification = _context.Notifications.Find(notificationDto.NotificationID);
            if (notification == null)
            {
                return NotFound();
            }
            _mapper.Map(notificationDto, notification);
            _context.SaveChanges();
            return Ok("Notification updated successfully.");
        }

        [HttpDelete]
        public IActionResult DeleteNotification(int id)
        {
            var notification = _context.Notifications.Find(id);
            if (notification == null)
            {
                return NotFound();
            }
            _context.Notifications.Remove(notification);
            _context.SaveChanges();
            return Ok("Notification deleted successfully.");
        }
    }
}
