using AutoMapper;
using DascoPlasticRecyclingApp.Dto;
using DascoPlasticRecyclingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DascoPlasticRecyclingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult LoginDto(LoginDto loginDto)
        {
            var userAccount = _context.UserAccounts.Where(u => u.Username == loginDto.Username).FirstOrDefault();
            if (userAccount == null)
                return Unauthorized(new { message = "Username or password is incorrect" });
            if (userAccount.Password != loginDto.Password)
                return Unauthorized(new { message = "Username or password is incorrect" });

            return Ok(userAccount.Admin ? "Admin Login Successful" : "Login Successful");
        }

    }
}


