using AutoMapper;
using DascoPlasticRecyclingApp.Dto;
using DascoPlasticRecyclingApp.Interfaces;
using DascoPlasticRecyclingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DascoPlasticRecyclingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        //private readonly IContactRepository _contactRepository;
        //public ContactController(IContactRepository contactRepository)
        //{
        //    //_contactRepository= contactRepository;
        //}

        private AppDbContext _context;
        private readonly IMapper _mapper;

        public ContactController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Contact>))]
        public IActionResult GetContacts()
        {
            //var contacts = _contactRepository.GetContacts();
            var contacts = _mapper.Map<List<CContactDto>>(_context.Contacts.Include(c => c.ContactType).Include(c => c.User).OrderBy(p => p.Id).ToList());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(contacts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CContactDto))]
        [ProducesResponseType(404)]
        public IActionResult GetContact(int id)
        {
            if (!_context.Contacts.Any(p => p.Id == id))
                return NotFound();

            var contact = _mapper.Map<CContactDto>(_context.Contacts.Include(c => c.ContactType).Include(c => c.User).Where(p => p.Id == id).FirstOrDefault());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(contact);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ContactDto))]
        [ProducesResponseType(400)]
        public IActionResult CreateContact([FromBody] ContactDto contactDto)
        {
            if (contactDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var newContact = contactDto;
            //newContact.User = _context.Users.Where(u => u.Id == contactDto.User.Id).FirstOrDefault();
            //newContact.

            var contact = new Contact {
                Id = contactDto.Id,
                Value = contactDto.Value
            };

            //var contact = _mapper.Map<Contact>(contactDto);
            contact.User = _context.Users.Where(u => u.Id == contactDto.UserId).FirstOrDefault();
            contact.ContactType = _context.ContactTypes.Where(c => c.Id == contactDto.ContactTypeId).FirstOrDefault();

            _context.Add(contact);
            var saved = _context.SaveChanges() > 0;
            if (!saved)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully saved Created");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        public IActionResult UpdateContact(int id, [FromBody] ContactDto contactDto)
        {
            if (contactDto == null)
                return BadRequest(ModelState);

            if (id != contactDto.Id)
                return BadRequest(ModelState);

            if (!_context.Contacts.Any(p => p.Id == id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contactInDb = _context.Contacts.Where(p => p.Id == id).FirstOrDefault();

            contactInDb.Id = contactDto.Id;
            contactInDb.Value = contactDto.Value;
            contactInDb.User = _context.Users.Where(u => u.Id == contactDto.UserId).FirstOrDefault();
            contactInDb.ContactType = _context.ContactTypes.Where(c => c.Id == contactDto.ContactTypeId).FirstOrDefault();

            var updated = _context.SaveChanges() > 0;
            if (!updated)
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteContact(int id)
        {
            if (!_context.Contacts.Any(p => p.Id == id))
                return NotFound();

            var contactInDb = _context.Contacts.Where(p => p.Id == id).FirstOrDefault();
            _context.Remove(contactInDb);

            var deleted = _context.SaveChanges() > 0;
            if (!deleted)
            {
                return StatusCode(500, "Something went wrong while deleting");
            }

            return NoContent();
        }

    }
}
