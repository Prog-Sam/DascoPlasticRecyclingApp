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
    public class UserAccountController : Controller
    {
        //private readonly IUserAccountRepository _userAccountRepository;
        //public UserAccountController(IUserAccountRepository userAccountRepository)
        //{
        //    //_userAccountRepository= userAccountRepository;
        //}

        private AppDbContext _context;
        private readonly IMapper _mapper;

        public UserAccountController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserAccount>))]
        public IActionResult GetUserAccounts()
        {
            //var userAccounts = _userAccountRepository.GetUserAccounts();
            var userAccounts = _mapper.Map<List<CUserAccountDto>>(_context.UserAccounts.Include(c => c.User).OrderBy(p => p.Id).ToList());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userAccounts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CUserAccountDto))]
        [ProducesResponseType(404)]
        public IActionResult GetUserAccount(int id)
        {
            if (!_context.UserAccounts.Any(p => p.Id == id))
                return NotFound();

            var userAccount = _mapper.Map<CUserAccountDto>(_context.UserAccounts.Include(c => c.User).Where(p => p.Id == id).FirstOrDefault());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userAccount);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UserAccountDto))]
        [ProducesResponseType(400)]
        public IActionResult CreateUserAccount([FromBody] UserAccountDto userAccountDto)
        {
            if (userAccountDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

             UserAccount Accessor = _context.UserAccounts.Where(u => u.Id == userAccountDto.accessorId).FirstOrDefault();

            if (Accessor == null)
                return BadRequest(ModelState);

            if(!Accessor.Admin)
            {
                ModelState.AddModelError("", "Only admin accounts can post here");
                return StatusCode(405,ModelState);
            }    

            var userAccount = new UserAccount
            {
                Id = userAccountDto.Id,
                Username = userAccountDto.Username,
                Password = userAccountDto.Password,
                Admin = userAccountDto.Admin,
            };

            //var userAccount = _mapper.Map<UserAccount>(userAccountDto);
            userAccount.User = _context.Users.Where(u => u.Id == userAccountDto.UserId).FirstOrDefault();

            _context.Add(userAccount);
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
        public IActionResult UpdateUserAccount(int id, [FromBody] UserAccountDto userAccountDto)
        {
            if (userAccountDto == null)
                return BadRequest(ModelState);

            if (id != userAccountDto.Id)
                return BadRequest(ModelState);

            if (!_context.UserAccounts.Any(p => p.Id == id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            UserAccount Accessor = _context.UserAccounts.Where(u => u.Id == userAccountDto.accessorId).FirstOrDefault();

            if (Accessor == null)
                return BadRequest(ModelState);

            if (!Accessor.Admin)
            {
                ModelState.AddModelError("", "Only admin accounts can update here");
                return StatusCode(405, ModelState);
            }

            var userAccountInDb = _context.UserAccounts.Where(p => p.Id == id).FirstOrDefault();

            userAccountInDb.Id = userAccountDto.Id;
            userAccountInDb.Username = userAccountDto.Username;
            userAccountInDb.Admin = userAccountDto.Admin;
            userAccountInDb.User = _context.Users.Where(u => u.Id == userAccountDto.UserId).FirstOrDefault();

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
        public IActionResult DeleteUserAccount(int id)
        {
            if (!_context.UserAccounts.Any(p => p.Id == id))
                return NotFound();

            var userAccountInDb = _context.UserAccounts.Where(p => p.Id == id).FirstOrDefault();
            if(userAccountInDb.Admin == true)
            {
                ModelState.AddModelError("", "Admin accounts cannot be deleted.");
                return StatusCode(405, ModelState);
            }


            _context.Remove(userAccountInDb);

            var deleted = _context.SaveChanges() > 0;
            if (!deleted)
            {
                return StatusCode(500, "Something went wrong while deleting");
            }

            return NoContent();
        }

    }
}
