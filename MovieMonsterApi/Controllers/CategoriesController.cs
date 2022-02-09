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
    public class CategoriesController : ControllerBase
    {
        private readonly IEFBaseAsyncRepository<Category,int> _context;

        public CategoriesController(IEFBaseAsyncRepository<Category, int> context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories()
        {
            return Ok(await _context.GetAllAsync());
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var actor = await _context.GetByIdAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            return actor;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category actor)
        {
            if (id != actor.Id)
            {
                return BadRequest();
            }

            await _context.UpdateAsync(actor);

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category actor)
        {
            await _context.CreateAsync(actor);

            return CreatedAtAction("GetCategory", new { id = actor.Id }, actor);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _context.DeleteAsync(id);

            return NoContent();
        }
    }
}
