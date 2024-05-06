using System.ComponentModel.DataAnnotations;

namespace BookMarket.Models.MyValidationAttributes
{
    public class PastBirthdateAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is DateTime date)
            {
                if (date < DateTime.Now)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Birthdate must be in past");
        }
    }
}
