#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieMonster.Data;
using MovieMonster.Models;
using MovieMonster.Models.Forms;

namespace MovieMonster.Controllers
{
    public class MoviesController : Controller
    {
        private readonly HttpClient _httpClient;

        public MoviesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7175");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Movies");
            response.EnsureSuccessStatusCode();
            var movies = await response.Content.ReadFromJsonAsync<IEnumerable<Movie>>();
            return View(movies);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync($"api/Movies/{id}");
            
            if (response == null)
            {
                return NotFound();
            }

            response.EnsureSuccessStatusCode();
            var movie = await response.Content.ReadFromJsonAsync<Movie>();
            return View(movie);
        }

        // GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            var response = await _httpClient.GetAsync("api/Categories");
            if (response == null)
            {
                return NotFound();
            }
            response.EnsureSuccessStatusCode();
            var categories = await response.Content.ReadFromJsonAsync<IEnumerable<Category>>();

            var responseCinema = await _httpClient.GetAsync("api/Cinemas");
            if (responseCinema == null)
            {
                return NotFound();
            }
            responseCinema.EnsureSuccessStatusCode();
            var cinemas = await responseCinema.Content.ReadFromJsonAsync<IEnumerable<Cinema>>();

            var responseActors = await _httpClient.GetAsync("api/Actors");
            if (responseActors == null)
            {
                return NotFound();
            }
            responseActors.EnsureSuccessStatusCode();
            var actors = await responseActors.Content.ReadFromJsonAsync<IEnumerable<Actor>>();


            ViewData["CategoryName"] = new SelectList(categories, "Id", "Name");
            ViewData["ActorName"] = new SelectList(actors, "Id", "FullName");
            ViewData["CinemaName"] = new SelectList(cinemas, "Id", "Name");

            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieCreateForm formData)
        {
            if (ModelState.IsValid)
            {
                var responseCategories = await _httpClient.GetAsync("api/Categories");
                if (responseCategories == null)
                {
                    return NotFound();
                }
                responseCategories.EnsureSuccessStatusCode();
                var categories = await responseCategories.Content.ReadFromJsonAsync<IEnumerable<Category>>();

                var responseCinema = await _httpClient.GetAsync("api/Cinemas");
                if (responseCinema == null)
                {
                    return NotFound();
                }
                responseCinema.EnsureSuccessStatusCode();
                var cinemas = await responseCinema.Content.ReadFromJsonAsync<IEnumerable<Cinema>>();

                var responseActors = await _httpClient.GetAsync("api/Actors");
                if (responseActors == null)
                {
                    return NotFound();
                }
                responseActors.EnsureSuccessStatusCode();
                var actors = await responseActors.Content.ReadFromJsonAsync<IEnumerable<Actor>>();

                var movie = new Movie()
                {
                    Name = formData.Name,
                    Description = formData.Description,
                    Price = formData.Price,
                    StartDate = formData.StartDate,
                    EndDate = formData.EndDate,
                    Actors = actors.Where(x => formData.Actors.Contains(x.Id)).ToList(),
                    Cinemas = cinemas.Where(x => formData.Cinemas.Contains(x.Id)).ToList(),
                    Categories = categories.Where(x => formData.Categories.Contains(x.Id)).ToList()
                };

                var response = await _httpClient.PostAsJsonAsync("api/Movies", movie);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        //// GET: Movies/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var movie = await _context.Movie.FindAsync(id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(movie);
        //}

        //// POST: Movies/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,StartDate,EndDate")] Movie movie)
        //{
        //    if (id != movie.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(movie);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MovieExists(movie.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(movie);
        //}

        //// GET: Movies/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var movie = await _context.Movie
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(movie);
        //}

        //// POST: Movies/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var movie = await _context.Movie.FindAsync(id);
        //    _context.Movie.Remove(movie);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool MovieExists(int id)
        //{
        //    return _context.Movie.Any(e => e.Id == id);
        //}
    }
}
