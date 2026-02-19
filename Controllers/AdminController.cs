using System.ComponentModel;
using System.Security.Principal;
using System.Threading.Tasks;
using DynamicData.Data;
using DynamicData.Entity;
using DynamicData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace DynamicData.Controllers;

public class AdminController : Controller
{
    private readonly MovieContext _context;

    public AdminController(MovieContext context)
    {
        _context = context;
    }



    public IActionResult Index()
    {
        return View();
    }

    public IActionResult MovieList()
    {
        var model = new AdminMoviesViewModel
        {
            Movies = _context.Movies
                .Include(m => m.Genres)
                .Select(m => new AdminMovieViewModel
                {
                    MovieId = m.MovieId,
                    Title = m.MovieTitle,
                    ImageUrl = m.MovieUrl,
                    Genres = m.Genres.Select(g => new AdminMovieGenreViewModel
                    {
                        GenreId = g.GenreId,
                        Name = g.Name
                    }).ToList()
                })
                .ToList()
        };

        return View(model);
    }


    public IActionResult MovieUpdate(int? id)
    {
        if (id == null)
        {
            return NotFound();

        }

        var entity = _context.Movies.Select(m => new AdminEditMovieViewModel
        {
            MovieId = m.MovieId,
            Title = m.MovieTitle,
            MovieDescription = m.MovieDescription,
            ImageUrl = m.MovieUrl,
            SelectedGenreIds = m.Genres.Select(g => g.GenreId).ToList()

        }).FirstOrDefault(m => m.MovieId == id);

        ViewBag.Genres = _context.Genres.ToList();

        if (entity == null)
        {
            return NotFound();

        }
        return View(entity);


    }


    [HttpPost]
    public async Task<IActionResult> MovieUpdate(AdminEditMovieViewModel model, IFormFile file)
    {
        // 1️⃣ Genre seçilmiş mi
        if (model.SelectedGenreIds == null || !model.SelectedGenreIds.Any())
        {
            ModelState.AddModelError(
                nameof(model.SelectedGenreIds),
                "En az bir tür seçmelisiniz"
            );
        }

        // 2️⃣ Model valid mi
        if (!ModelState.IsValid)
        {
            ViewBag.Genres = _context.Genres.ToList();
            return View(model);
        }

        // 3️⃣ Entity çek
        var entity = _context.Movies
            .Include(m => m.Genres)
            .FirstOrDefault(m => m.MovieId == model.MovieId);

        if (entity == null)
        {
            return NotFound();
        }

        // 4️⃣ Alanları güncelle
        entity.MovieTitle = model.Title;
        entity.MovieDescription = model.MovieDescription;

        // 5️⃣ Dosya varsa
        if (file != null)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/img/",
                fileName
            );

            entity.MovieUrl = fileName;

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
        }


        // 6️⃣ Genre güncelle (DOĞRU SATIR)
        entity.Genres = _context.Genres
            .Where(g => model.SelectedGenreIds.Contains(g.GenreId))
            .ToList();
        foreach (var item in ModelState)
        {
            foreach (var error in item.Value.Errors)
            {
                Console.WriteLine($"{item.Key} => {error.ErrorMessage}");
            }
        }


        _context.SaveChanges();

        return RedirectToAction("MovieList");
    }





    public IActionResult GenreList()
    {
        return View(new AdminGenresViewModel
        {
            Genres = _context.Genres.Select(g => new AdminGenreViewModel
            {
                GenreId = g.GenreId,
                Name = g.Name,
                Count = g.Movies.Count
            }).ToList()


        });

    }



    public IActionResult GenreUpdate(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = _context.Genres.Select(g => new AdminGenreEditViewModel
        {
            GenreId = g.GenreId,
            Name = g.Name,
            Movies = g.Movies.Select(m => new AdminEditMovieViewModel
            {
                MovieId = m.MovieId,
                Title = m.MovieTitle,
                ImageUrl = m.MovieUrl
            })
            .ToList()
        })
            .FirstOrDefault(g => g.GenreId == id); ;

        if (entity == null)

        {
            return NotFound();

        }
        return View(entity);


    }

    [HttpPost]
    public IActionResult GenreUpdate(AdminGenreEditViewModel adminGenreEditViewModel, int[] MovieIds)
    {


        var entity = _context.Genres.Include("Movies").FirstOrDefault(m => m.GenreId == adminGenreEditViewModel.GenreId);
        if (entity == null)
        {
            return NotFound();

        }
        entity.Name = adminGenreEditViewModel.Name;

        foreach (var id in MovieIds)
        {
            var movie = entity.Movies.FirstOrDefault(m => m.MovieId == id);
            if (movie != null)
            {
                entity.Movies.Remove(movie);
            }
        }

        _context.SaveChanges();

        return RedirectToAction("GenreList");

    }



    public IActionResult GenreDelete(int genreId)
    {
        var entity = _context.Genres.Find(genreId);

        if (entity != null)
        {
            _context.Genres.Remove(entity);
            _context.SaveChanges();
        }

        return RedirectToAction("GenreList");

    }
    public IActionResult MovieDelete(int movieId)
    {
        var entity = _context.Movies.Find(movieId);

        if (entity != null)
        {
            _context.Movies.Remove(entity);
            _context.SaveChanges();
        }

        return RedirectToAction("MovieList");

    }



    [HttpGet]
    public IActionResult MovieCreate()
    {
        ViewBag.Genres = _context.Genres.ToList();
        return View(new AdminCreateMovieViewModel());
    }

    [HttpPost]
    public IActionResult MovieCreate(AdminCreateMovieViewModel model)
    {
        if (model.Title != null && model.Title.Contains('@'))
        {
            ModelState.AddModelError("", "Film başlığı @ içeremez");

        }
        if (!model.SelectedGenreIds.Any())
        {
            ModelState.AddModelError(nameof(model.SelectedGenreIds),
                "Film için en az bir tür seçmelisiniz");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Genres = _context.Genres.ToList();
            return View(model);
        }

        var movie = new Movie
        {
            MovieTitle = model.Title,
            MovieDescription = model.MovieDescription,
            MovieUrl = "1.png"
        };

        if (model.SelectedGenreIds.Any())
        {
            var genres = _context.Genres
                .Where(g => model.SelectedGenreIds.Contains(g.GenreId))
                .ToList();

            foreach (var genre in genres)
            {
                movie.Genres.Add(genre);
            }

            _context.Movies.Add(movie);
            _context.SaveChanges();
            return RedirectToAction("MovieList", "Admin");
        }
        ViewBag.Genres = _context.Genres.ToList();

        return View(model);
    }



    [HttpGet]
    public IActionResult GenreCreate()
    {
        var model = new GenreCreateViewModel
        {
            Movies = _context.Movies.ToList()
        };

        return View(model);
    }
    [HttpPost]
    public IActionResult GenreCreate(GenreCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Movies = _context.Movies.ToList();
            return View(model);
        }

        var genre = new DynamicData.Entity.Genre
        {
            Name = model.Name,
            Movies = new List<Movie>()
        };

        var selectedMovies = _context.Movies
            .Where(m => model.SelectedMovieIds.Contains(m.MovieId))
            .ToList();

        foreach (var movie in selectedMovies)
        {
            genre.Movies.Add(movie);
        }

        _context.Genres.Add(genre);

        _context.SaveChanges();

        return RedirectToAction("GenreList");
    }


}