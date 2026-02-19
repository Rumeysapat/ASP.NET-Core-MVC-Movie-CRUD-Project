using System.ComponentModel.DataAnnotations;

namespace DynamicData.Validators;

public class BirthDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null)
            return true; // Required ayrı kontrol edilir

        if (value is not DateTime datetime)
            return false;

        return datetime <= DateTime.Today;
    }


}