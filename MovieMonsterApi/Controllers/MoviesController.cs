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
    public class MoviesController : ControllerBase
    {
        private readonly IEFBaseAsyncRepository<Movie,int> _contextMovie;
        private readonly IEFBaseAsyncRepository<Actor, int> _contextActor;
        private readonly IEFBaseAsyncRepository<Category, int> _contextCategory;
        private readonly IEFBaseAsyncRepository<Cinema, int> _contextCinema;

        public MoviesController(IEFBaseAsyncRepository<Movie, int> contextMovie, IEFBaseAsyncRepository<Actor, int> contextActor, IEFBaseAsyncRepository<Category, int> contextCategory, IEFBaseAsyncRepository<Cinema, int> contextCinema)
        {
            _contextMovie = contextMovie;
            _contextActor = contextActor;
            _contextCategory = contextCategory;
            _contextCinema = contextCinema;
        }



        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Movie>>> GetMovies()
        {
            return Ok(await _contextMovie.GetAllAsync());
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var actor = await _contextMovie.GetByIdAsync(id, x => x.Categories, x => x.Cinemas, x => x.Actors);

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

            await _contextMovie.UpdateAsync(actor);

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieCreateDTO movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var categories = await _contextCategory.GetAllAsync();
            var cinemas = await _contextCinema.GetAllAsync();
            var actors = await _contextActor.GetAllAsync();


            var movie = new Movie()
            {
                Name = movieDto.Name,
                Description = movieDto.Description,
                Price = movieDto.Price,
                StartDate = movieDto.StartDate,
                EndDate = movieDto.EndDate,

                Actors = actors.Where(x => movieDto.Actors.Contains(x.Id)).ToList(),
                Cinemas = cinemas.Where(x => movieDto.Cinemas.Contains(x.Id)).ToList(),
                Categories = categories.Where(x => movieDto.Categories.Contains(x.Id)).ToList()
            };

            await _contextMovie.CreateAsync(movie);

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        /// <summary>
        /// Test summary for delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns code="204">Testing No Content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _contextMovie.DeleteAsync(id);

            return NoContent();
        }
    }
}
