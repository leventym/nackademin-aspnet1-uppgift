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

namespace MovieMonster.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly HttpClient _httpClient;

        public CategoriesController(ILogger<CategoriesController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7175");
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Categories");
            response.EnsureSuccessStatusCode();
            var categories = await response.Content.ReadFromJsonAsync<IEnumerable<Category>>();
            return View(categories);
        }

        public async Task<IActionResult> MoviesByCategories()
        {
            var response = await _httpClient.GetAsync("api/Categories");
            response.EnsureSuccessStatusCode();
            var categories = await response.Content.ReadFromJsonAsync<IEnumerable<Category>>();
            return View(categories);
        }

        //    // GET: Categories/Details/5
        //    public async Task<IActionResult> Details(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var category = await _context.Category
        //            .FirstOrDefaultAsync(m => m.Id == id);
        //        if (category == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(category);
        //    }

        //    // GET: Categories/Create
        //    public IActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: Categories/Create
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _context.Add(category);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(category);
        //    }

        //    // GET: Categories/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var category = await _context.Category.FindAsync(id);
        //        if (category == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(category);
        //    }

        //    // POST: Categories/Edit/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        //    {
        //        if (id != category.Id)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(category);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!CategoryExists(category.Id))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(category);
        //    }

        //    // GET: Categories/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var category = await _context.Category
        //            .FirstOrDefaultAsync(m => m.Id == id);
        //        if (category == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(category);
        //    }

        //    // POST: Categories/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var category = await _context.Category.FindAsync(id);
        //        _context.Category.Remove(category);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool CategoryExists(int id)
        //    {
        //        return _context.Category.Any(e => e.Id == id);
        //    }
        //}
    }
}
