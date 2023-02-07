﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DascoPlasticRecyclingApp.Dto;
using DascoPlasticRecyclingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DascoPlasticRecyclingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        public IActionResult LoginDto(LoginDto loginDto)
        {
            var userAccount = _context.UserAccounts.Where(u => u.Username == loginDto.Username).FirstOrDefault();
            if (userAccount == null)
                return Unauthorized(new { message = "Username or password is incorrect" });
            if (userAccount.Password != loginDto.Password)
                return Unauthorized(new { message = "Username or password is incorrect" });

            // Create JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Your-Secure-Key-String");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()),
                    new Claim(ClaimTypes.Name, userAccount.Username),
                    new Claim(ClaimTypes.Role, userAccount.Admin ? "Admin" : "User")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(tokenString);
        }
    }
}
