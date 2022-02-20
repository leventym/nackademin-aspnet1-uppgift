using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieMonsterApi.Data;
using MovieMonsterApi.Entities;
using MovieMonsterApi.Models;
using MovieMonsterApi.Repositories;

namespace MovieMonsterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IEFBaseAsyncRepository<Actor,int> _contextActor;
        private readonly IEFBaseAsyncRepository<Movie, int> _contextMovie;

        public ActorsController(IEFBaseAsyncRepository<Actor, int> contextActor, IEFBaseAsyncRepository<Movie, int> contextMovie)
        {
            _contextActor = contextActor;
            _contextMovie = contextMovie;
        }

        // GET: api/Actors
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Actor>>> GetActors()
        {
            return Ok(await _contextActor.GetAllAsync());
        }

        // GET: api/Actors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Actor>> GetActor(int id)
        {
            var actor = await _contextActor.GetByIdAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            return actor;
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, Actor actor)
        {
            if (id != actor.Id)
            {
                return BadRequest();
            }

            await _contextActor.UpdateAsync(actor);

            return NoContent();
        }


        // POST: api/Actors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Actor>> PostActor(ActorCreateDto actorDto)
        {
            var movies = await _contextMovie.GetAllAsync();

            var actor = new Actor()
            {
                FirstName = actorDto.FirstName,
                LastName = actorDto.LastName,

                Movies = movies.Where(x => actorDto.Movies.Contains(x.Id)).ToList(),
            };

            await _contextActor.CreateAsync(actor);

            return CreatedAtAction("GetActor", new { id = actor.Id }, actor);
        }


        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            await _contextActor.DeleteAsync(id);

            return NoContent();
        }
    }
}
