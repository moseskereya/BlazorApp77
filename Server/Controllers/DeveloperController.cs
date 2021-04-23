using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApps.Server.Data;
using MyApps.Shared.Models;

namespace MyApps.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public DeveloperController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var devs = await _context.Develoers.ToListAsync();
            return Ok(devs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var devs = await _context.Develoers.FirstOrDefaultAsync(a => a.Id == id);
            return Ok(devs);
        }

        public async Task<IActionResult> Post(Developer developer)
        {
            _context.Add(developer);
            await _context.SaveChangesAsync();
            return Ok(developer.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Developer developer)
        {
            _context.Entry(developer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dev = new Developer { Id = id };
            _context.Remove(dev);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
