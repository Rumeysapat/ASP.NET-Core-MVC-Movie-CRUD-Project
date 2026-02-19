using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DynamicData.Models;

public class Film
{

    [DisplayName("Başlık")]
    [Required(ErrorMessage = "Film başlığı eklemelisiniz.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Film başlığı 5 ile 10 karakter arasında olmalı.")]
    public string MovieTitle { get; set; }
    public string MovieDirektor { get; set; }

    [Required(ErrorMessage = "Film açıklaması eklemelisiniz.")]
    public string MovieDescription { get; set; }
    [Required]
    public string MovieUrl { get; set; }
    public int MovieId { get; set; }
    [Required]
    public int? GenreId { get; set; }
}