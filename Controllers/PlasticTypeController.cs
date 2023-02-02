﻿using DascoPlasticRecyclingApp.Interfaces;
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

        public PlasticTypeController(AppDbContext context) { 
            _context= context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PlasticType>))]
        public IActionResult GetPlasticTypes() { 
            //var plasticTypes = _plasticTypeRepository.GetPlasticTypes();
            var plasticTypes = _context.PlasticTypes.OrderBy(p => p.Id).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(plasticTypes);
        }

        [HttpGet("{id}")]
        public IActionResult GetPlasticType(int id)
        {
            if (!_context.PlasticTypes.Any(p => p.Id == id))
                return NotFound();

            var plasticType = _context.PlasticTypes.Where(p => p.Id == id).FirstOrDefault();
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(plasticType);
        }
    }
}