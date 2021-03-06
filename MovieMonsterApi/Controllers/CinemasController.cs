using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieMonsterApi.Data;
using MovieMonsterApi.Entities;
using MovieMonsterApi.Repositories;

namespace MovieMonsterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemasController : ControllerBase
    {
        private readonly IEFBaseAsyncRepository<Cinema,int> _context;

        public CinemasController(IEFBaseAsyncRepository<Cinema, int> context)
        {
            _context = context;
        }

        // GET: api/Cinemas
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Cinema>>> GetCinemas()
        {
            return Ok(await _context.GetAllAsync());
        }

        // GET: api/Cinemas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cinema>> GetCinema(int id)
        {
            var actor = await _context.GetByIdAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            return actor;
        }

        // PUT: api/Cinemas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCinema(int id, Cinema actor)
        {
            if (id != actor.Id)
            {
                return BadRequest();
            }

            await _context.UpdateAsync(actor);

            return NoContent();
        }

        // POST: api/Cinemas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cinema>> PostCinema(Cinema actor)
        {
            await _context.CreateAsync(actor);

            return CreatedAtAction("GetCinema", new { id = actor.Id }, actor);
        }

        // DELETE: api/Cinemas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCinema(int id)
        {
            await _context.DeleteAsync(id);

            return NoContent();
        }
    }
}
