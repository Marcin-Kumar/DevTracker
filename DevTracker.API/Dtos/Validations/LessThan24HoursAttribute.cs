using System.ComponentModel.DataAnnotations;

namespace DevTracker.API.Dtos.Validations;

public class LessThan24HoursAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is TimeSpan ts && ts >= TimeSpan.FromHours(24))
        {
            return new ValidationResult("The duration must be less than 24 hours.");
        }
        return ValidationResult.Success;
    }
}