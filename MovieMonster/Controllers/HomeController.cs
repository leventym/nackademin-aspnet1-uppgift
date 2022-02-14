using Microsoft.AspNetCore.Mvc;
using MovieMonster.Models;
using System.Diagnostics;

namespace MovieMonster.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;


        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}