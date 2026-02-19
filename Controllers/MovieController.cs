using DynamicData.Data;
using DynamicData.Entity;
using DynamicData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class MovieController : Controller
{

    private readonly MovieContext _context;

    public MovieController(MovieContext context)
    {
        _context = context;

    }
    public IActionResult Index()
    {

        return View();
    }

    public IActionResult List(int? id, string q)
    {
        var movies = _context.Movies
                             .Include(m => m.Genres)
                             .AsQueryable();

        if (id != null)
        {
            movies = movies
                .Where(m => m.Genres.Any(g => g.GenreId == id));
        }

        if (!string.IsNullOrEmpty(q))
        {
            movies = movies
                .Where(i =>
                    i.MovieTitle.ToLower().Contains(q.ToLower()) ||
                    i.MovieDescription.ToLower().Contains(q.ToLower()));
        }

        var model = new MovieViewModel
        {
            Movies = movies.ToList()
        };

        ViewBag.Genres = new SelectList(_context.Genres, "GenreId", "Name");
        return View("Movie", model);
    }









    public IActionResult Details(int id)
    {
        //return View(MovieRepository.GetById(id));
        var movie = _context.Movies.Find(id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }







}
