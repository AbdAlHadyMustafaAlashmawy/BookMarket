using BookMarket.Models.Helpers;
using System.ComponentModel.DataAnnotations;

namespace BookMarket.Models.MyValidationAttributes
{
    public class UniqueProducerNameAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var name = value?.ToString();
            var producer = (Producer)validationContext.ObjectInstance;
            if (producer.Name == "") return ValidationResult.Success;
            using (var context = new AppDbContext(Helper._configurationPub))
            {
                if (!context.Producers.Where(x => x.Name == producer.Name).Any())
                {
                    return ValidationResult.Success;
                }
                else
                {
                    if (producer.Name?.Trim() != "" && producer.Name?.Trim() != null)
                    {
                        return new ValidationResult("Producer Name Must be Unique");
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
                }
            }
        }
    }
}
