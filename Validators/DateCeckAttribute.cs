using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Validators
{
    public class DateCeckAttribute : ValidationAttribute

    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            
        }
    }
}
