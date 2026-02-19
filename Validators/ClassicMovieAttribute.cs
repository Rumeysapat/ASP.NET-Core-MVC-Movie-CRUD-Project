using System.ComponentModel.DataAnnotations;
using DynamicData.Models;

namespace DynamicData.Validators;

public class ClassicMovieAttribute : ValidationAttribute
{
    public ClassicMovieAttribute(int year)
    {
        Year = year;
    }
    public int Year { get; set; }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var movie = (AdminCreateMovieViewModel)validationContext.ObjectInstance;
        var ReleaseYear = ((DateTime)value).Year;

        if (movie.IsClassic && ReleaseYear > Year)
        {
            return new ValidationResult($"Klasik film için {Year} ve öncesi değer girmelisiniz.");

        }
        return ValidationResult.Success;
    }
}