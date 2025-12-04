using AutoMapper;
using FoodieHub.API.Context;
using FoodieHub.API.Dtos.MessageDtos;
using FoodieHub.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FoodieHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _apiContext;

        public MessagesController(IMapper mapper, ApiContext apiContext)
        {
            _mapper = mapper;
            _apiContext = apiContext;
        }

        [HttpGet]
        public IActionResult GetMessages()
        {
            var messages = _apiContext.Messages.ToList();
            return Ok(_mapper.Map<List<ResultMessageDto>>(messages));
        }

        [HttpGet("{id}")]
        public IActionResult GetMessageById(int id)
        {
            var message = _apiContext.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetByIdMessageDto>(message));
        }

        [HttpPost]
        public IActionResult CreateMessage(CreateMessageDto messageDto)
        {
            var message = _mapper.Map<Message>(messageDto);
            _apiContext.Messages.Add(message);
            _apiContext.SaveChanges();
            return Ok("Message created successfully");
        }

        [HttpDelete]
        public IActionResult DeleteMessage(int id)
        {
            var message = _apiContext.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }
            _apiContext.Messages.Remove(message);
            _apiContext.SaveChanges();
            return Ok("Message deleted successfully");
        }

        [HttpPut]
        public IActionResult UpdateMessage(UpdateMessageDto messageDto)
        {
            var message = _apiContext.Messages.FirstOrDefault(m => m.MessageID == messageDto.MessageID);
            if (message == null)
            {
                return NotFound();
            }
            _mapper.Map(messageDto, message);
            _apiContext.SaveChanges();
            return Ok("Message updated successfully");
        }

        [HttpGet("GetReadMessages")]
        public IActionResult GetUnreadMessages()
        {
            var readMessages = _apiContext.Messages.Where(m => !m.IsRead).ToList();
            return Ok(_mapper.Map<List<ResultMessageDto>>(readMessages));
        }
    }
}
