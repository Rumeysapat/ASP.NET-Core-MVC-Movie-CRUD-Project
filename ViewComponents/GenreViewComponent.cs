
using DynamicData.Data;
using DynamicData.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DynamicData.ViewComponents;

public class GenreViewComponent : ViewComponent
{
  private readonly MovieContext _context;
  public GenreViewComponent(MovieContext context)
  {
    _context = context;
  }

  public IViewComponentResult Invoke()
  {

    ViewBag.SelectedGenre = RouteData.Values["id"];
    // Null kontrolü ekliyoruz
    // var genres = GenreRepository.Genres ?? new List<Genre>();
    var genres = _context.Genres.ToList();
    return View(genres);
  }
}
