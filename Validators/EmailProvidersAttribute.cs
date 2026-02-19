using System.ComponentModel.DataAnnotations;

namespace DynamicData.Validators;

public class EmailProvidersAttribute : ValidationAttribute
{

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {

        string email = "";
        if (value != null)
        {
            email = value.ToString();


        }

        if (!email.EndsWith("@gmail.com") || !email.EndsWith("@hotmail.com"))
        {
            return new ValidationResult("hatalı eposta sunucusu.");

        }
        return ValidationResult.Success;
    }
}