using AutoMapper;
using DascoPlasticRecyclingApp.Dto;
using DascoPlasticRecyclingApp.Interfaces;
using DascoPlasticRecyclingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DascoPlasticRecyclingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactTypeController : Controller
    {
        //private readonly IContactTypeRepository _contactTypeRepository;
        //public ContactTypeController(IContactTypeRepository contactTypeRepository)
        //{
        //    //_contactTypeRepository= contactTypeRepository;
        //}

        private AppDbContext _context;
        private readonly IMapper _mapper;

        public ContactTypeController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContactType>))]
        public IActionResult GetContactTypes()
        {
            //var contactTypes = _contactTypeRepository.GetContactTypes();
            var contactTypes = _mapper.Map<List<ContactTypeDto>>(_context.ContactTypes.OrderBy(p => p.Id).ToList());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(contactTypes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ContactTypeDto))]
        [ProducesResponseType(404)]
        public IActionResult GetContactType(int id)
        {
            if (!_context.ContactTypes.Any(p => p.Id == id))
                return NotFound();

            var contactType = _mapper.Map<ContactTypeDto>(_context.ContactTypes.Where(p => p.Id == id).FirstOrDefault());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(contactType);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ContactTypeDto))]
        [ProducesResponseType(400)]
        public IActionResult CreateContactType([FromBody] ContactTypeDto contactTypeDto)
        {
            if (contactTypeDto == null)
                return BadRequest(ModelState);


            var contactTypeInDb = _context.ContactTypes.Where(p => p.Name == contactTypeDto.Name).FirstOrDefault();
            if (contactTypeInDb != null)
            {
                ModelState.AddModelError("", "Contact Type already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contactType = _mapper.Map<ContactType>(contactTypeDto);

            _context.Add(contactType);
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
        public IActionResult UpdateContactType(int id, [FromBody] ContactTypeDto contactTypeDto)
        {
            if (contactTypeDto == null)
                return BadRequest(ModelState);

            if(id != contactTypeDto.Id) 
                return BadRequest(ModelState);

            if (!_context.ContactTypes.Any(p => p.Id == id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contactTypeInDb = _context.ContactTypes.Where(p => p.Id == id).FirstOrDefault();
            if (contactTypeInDb.Name != contactTypeDto.Name &&
                _context.ContactTypes.Any(p => p.Name == contactTypeDto.Name))
            {
                ModelState.AddModelError("", "Contact Type already exists");
                return StatusCode(422, ModelState);
            }

            _mapper.Map(contactTypeDto, contactTypeInDb);

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
        public IActionResult DeleteContactType(int id)
        {
            if (!_context.ContactTypes.Any(p => p.Id == id))
                return NotFound();

            var contactTypeInDb = _context.ContactTypes.Where(p => p.Id == id).FirstOrDefault();
            _context.Remove(contactTypeInDb);

            var deleted = _context.SaveChanges() > 0;
            if (!deleted)
            {
                return StatusCode(500, "Something went wrong while deleting");
            }

            return NoContent();
        }

    }
}
