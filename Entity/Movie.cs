using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicData.Entity;


public class Movie
{
    //[Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
    // public int MovieId { get; set; }

    [DisplayName("Başlık")]
    [Required(ErrorMessage = "Film başlığı eklemelisiniz.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Film başlığı 5 ile 10 karakter arasında olmalı.")]
    public string MovieTitle { get; set; }


    [Required(ErrorMessage = "Film açıklaması eklemelisiniz.")]
    public string MovieDescription { get; set; }

    public string? MovieUrl { get; set; }
    public int MovieId { get; set; }

    // Navigation property: bir film birden fazla oyuncuya sahip olabilir
    public ICollection<Cast> Casts { get; set; } = new List<Cast>();

    // Navigation property: bir film birden fazla ekip üyesine sahip olabilir
    public ICollection<Crew> Crews { get; set; } = new List<Crew>();
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();

}