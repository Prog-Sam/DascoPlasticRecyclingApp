using AutoMapper;
using DascoPlasticRecyclingApp.Dto;
using DascoPlasticRecyclingApp.Interfaces;
using DascoPlasticRecyclingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DascoPlasticRecyclingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlasticTypeController : Controller
    {
        //private readonly IPlasticTypeRepository _plasticTypeRepository;
        //public PlasticTypeController(IPlasticTypeRepository plasticTypeRepository)
        //{
        //    //_plasticTypeRepository= plasticTypeRepository;
        //}

        private AppDbContext _context;
        private readonly IMapper _mapper;

        public PlasticTypeController(AppDbContext context, IMapper mapper) { 
            _context= context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PlasticType>))]
        public IActionResult GetPlasticTypes() {
            //var plasticTypes = _plasticTypeRepository.GetPlasticTypes();
            var plasticTypes = _mapper.Map<List<PlasticTypeDto>>(_context.PlasticTypes.OrderBy(p => p.Id).ToList());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(plasticTypes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PlasticTypeDto))]
        [ProducesResponseType(404)]
        public IActionResult GetPlasticType(int id)
        {
            if (!_context.PlasticTypes.Any(p => p.Id == id))
                return NotFound();

            var plasticType = _mapper.Map<PlasticTypeDto>(_context.PlasticTypes.Where(p => p.Id == id).FirstOrDefault());
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(plasticType);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PlasticTypeDto))]
        [ProducesResponseType(400)]
        public IActionResult CreatePlasticType([FromBody] PlasticTypeDto plasticTypeDto)
        {
            if(plasticTypeDto == null)
                return BadRequest(ModelState);


            var plasticTypeInDb = _context.PlasticTypes.Where(p => p.Name == plasticTypeDto.Name).FirstOrDefault();
            if(plasticTypeInDb != null)
            {
                ModelState.AddModelError("", "Plastic Type already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var plasticType = _mapper.Map<PlasticType>(plasticTypeDto);

            _context.Add(plasticType);
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
        public IActionResult UpdatePlasticType(int id, [FromBody] PlasticTypeDto plasticTypeDto)
        {
            if (plasticTypeDto == null)
                return BadRequest(ModelState);

            if (id != plasticTypeDto.Id)
                return BadRequest(ModelState);

            if (!_context.PlasticTypes.Any(p => p.Id == id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var plasticTypeInDb = _context.PlasticTypes.Where(p => p.Id == id).FirstOrDefault();
            if (plasticTypeInDb.Name != plasticTypeDto.Name &&
                _context.PlasticTypes.Any(p => p.Name == plasticTypeDto.Name))
            {
                ModelState.AddModelError("", "Plastic Type already exists");
                return StatusCode(422, ModelState);
            }

            _mapper.Map(plasticTypeDto, plasticTypeInDb);

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
        public IActionResult DeletePlasticType(int id)
        {
            if (!_context.PlasticTypes.Any(p => p.Id == id))
                return NotFound();

            var plasticTypeInDb = _context.PlasticTypes.Where(p => p.Id == id).FirstOrDefault();
            _context.Remove(plasticTypeInDb);

            var deleted = _context.SaveChanges() > 0;
            if (!deleted)
            {
                return StatusCode(500, "Something went wrong while deleting");
            }

            return NoContent();
        }

    }
}
