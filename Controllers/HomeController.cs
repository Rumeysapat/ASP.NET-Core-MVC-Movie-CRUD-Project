using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DynamicData.Models;
using DynamicData.Entity;
using DynamicData.Data;

namespace DynamicData.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MovieContext _context;

    public HomeController(ILogger<HomeController> logger, MovieContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var model = new HomePageViewModel
        {
            //PopularMovies = MovieRepository.Movies
            PopularMovies = _context.Movies.ToList()
        };


        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
