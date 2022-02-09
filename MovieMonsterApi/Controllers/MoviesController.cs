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
    public class MoviesController : ControllerBase
    {
        private readonly IEFBaseAsyncRepository<Movie,int> _context;

        public MoviesController(IEFBaseAsyncRepository<Movie, int> context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Movie>>> GetMovies()
        {
            return Ok(await _context.GetAllAsync());
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var actor = await _context.GetByIdAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            return actor;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie actor)
        {
            if (id != actor.Id)
            {
                return BadRequest();
            }

            await _context.UpdateAsync(actor);

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie actor)
        {
            await _context.CreateAsync(actor);

            return CreatedAtAction("GetMovie", new { id = actor.Id }, actor);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _context.DeleteAsync(id);

            return NoContent();
        }
    }
}
