using FoodieHub.API.Context;
using FoodieHub.API.Dtos.ContactDtos;
using FoodieHub.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FoodieHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApiContext _context;

        public ContactsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetContacts()
        {
            var contacts = _context.Contacts.ToList();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public IActionResult GetContact(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public IActionResult CreateContact(CreateContactDto contactDto)
        {
            Contact contact = new Contact
            {
                Address = contactDto.Address,
                Email = contactDto.Email,
                MapLocation = contactDto.MapLocation,
                Phone = contactDto.Phone,
                OpenHours = contactDto.OpenHours
            };
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return Ok("Contact created successfully");

        }

        [HttpPut]
        public IActionResult UpdateContact(UpdateContactDto contactDto)
        {
            var contact = _context.Contacts.Find(contactDto.ContactID);
            if (contact == null)
            {
                return NotFound();
            }
            contact.Address = contactDto.Address;
            contact.Email = contactDto.Email;
            contact.MapLocation = contactDto.MapLocation;
            contact.Phone = contactDto.Phone;
            contact.OpenHours = contactDto.OpenHours;
            _context.Contacts.Update(contact);
            _context.SaveChanges();
            return Ok("Contact updated successfully");
        }


    }
}
