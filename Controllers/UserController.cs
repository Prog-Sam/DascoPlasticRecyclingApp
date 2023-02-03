using AutoMapper;
using DascoPlasticRecyclingApp.Dto;
using DascoPlasticRecyclingApp.Interfaces;
using DascoPlasticRecyclingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DascoPlasticRecyclingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        //private readonly IUserRepository _userRepository;
        //public UserController(IUserRepository userRepository)
        //{
        //    //_userRepository= userRepository;
        //}

        private AppDbContext _context;
        private readonly IMapper _mapper;

        public UserController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            //var users = _userRepository.GetUsers();
            var users = _mapper.Map<List<UserDto>>(_context.Users.OrderBy(p => p.Id).ToList());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(404)]
        public IActionResult GetUser(int id)
        {
            if (!_context.Users.Any(p => p.Id == id))
                return NotFound();

            var user = _mapper.Map<UserDto>(_context.Users.Where(p => p.Id == id).FirstOrDefault());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<User>(userDto);

            _context.Add(user);
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
        public IActionResult UpdateUser(int id, [FromBody] UserDto userDto)
        {
            if (userDto == null)
                return BadRequest(ModelState);

            if (id != userDto.Id)
                return BadRequest(ModelState);

            if (!_context.Users.Any(p => p.Id == id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userInDb = _context.Users.Where(p => p.Id == id).FirstOrDefault();

            _mapper.Map(userDto, userInDb);

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
        public IActionResult DeleteUser(int id)
        {
            if (!_context.Users.Any(p => p.Id == id))
                return NotFound();

            var userInDb = _context.Users.Where(p => p.Id == id).FirstOrDefault();
            _context.Remove(userInDb);

            var deleted = _context.SaveChanges() > 0;
            if (!deleted)
            {
                return StatusCode(500, "Something went wrong while deleting");
            }

            return NoContent();
        }

    }
}
