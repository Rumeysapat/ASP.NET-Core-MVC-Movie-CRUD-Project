using DynamicData.Entity;
using DynamicData.Validators;
using System.ComponentModel.DataAnnotations;

namespace DynamicData.Models;

public class AdminMoviesViewModel
{

    public List<AdminMovieViewModel> Movies { get; set; }
    public List<AdminMovieGenreViewModel> Genres { get; set; }


}


public class AdminMovieViewModel
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public List<AdminMovieGenreViewModel> Genres { get; set; }


}
public class AdminMovieGenreViewModel
{
    public int GenreId { get; set; }
    public string Name { get; set; }
}
public class AdminEditMovieViewModel

{
    public int MovieId { get; set; }
    [Display(Name = "Film Adı")]
    [Required(ErrorMessage = "Film adı girmelisiniz.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Film adı için 3 ile 50 karakter arasında girmelisiniz.")]
    public string Title { get; set; }

    [Display(Name = "Film Acıklama")]
    [Required(ErrorMessage = "Film Açıklaması girmelisiniz.")]
    [StringLength(3000, MinimumLength = 10, ErrorMessage = "Film açıklaması için 10 ile 3000 karakter arasında olmalıdır.")]
    public string MovieDescription { get; set; }
    public string ImageUrl { get; set; }
    public List<int> SelectedGenreIds { get; set; } = new();





}


public class AdminCreateMovieViewModel
{

    [Display(Name = "Film Adı")]
    [Required(ErrorMessage = "Film adı girmelisiniz.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Film adı için 3 ile 50 karakter arasında girmelisiniz.")]
    public string Title { get; set; }

    [Display(Name = "Film Acıklama")]
    [Required(ErrorMessage = "Film Açıklaması girmelisiniz.")]
    [StringLength(3000, MinimumLength = 10, ErrorMessage = "Film açıklaması için 10 ile 3000 karakter arasında olmalıdır.")]
    public string MovieDescription { get; set; }
    [MinLength(1, ErrorMessage = "En az bir tür seçmelisiniz.")]
    public List<int> SelectedGenreIds { get; set; } = new();
    public bool IsClassic { get; set; }
    [ClassicMovie(1950)]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; } = DateTime.Now;

}

