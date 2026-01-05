using AutoMapper;
using FoodieHub.API.Context;
using FoodieHub.API.Dtos.ContactDtos;
using FoodieHub.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodieHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public ContactsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await _context.Contacts.ToListAsync();
            var mappedContacts = _mapper.Map<List<ResultContactDto>>(contacts);
            return Ok(mappedContacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto contactDto)
        {
            var contact = _mapper.Map<Contact>(contactDto);
            await _context.Contacts.AddAsync(contact);
            _context.SaveChanges();
            return Ok("Contact created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(UpdateContactDto contactDto)
        {
            var contact = await _context.Contacts.FindAsync(contactDto.ContactID);
            if (contact == null)
            {
                return NotFound();
            }

            _mapper.Map(contactDto, contact);
            _context.SaveChanges();
            return Ok("Contact updated successfully");
        }


    }
}
