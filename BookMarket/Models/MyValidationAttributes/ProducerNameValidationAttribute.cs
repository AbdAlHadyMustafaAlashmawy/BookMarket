using BookMarket.Models.Helpers;
using System.ComponentModel.DataAnnotations;

namespace BookMarket.Models.MyValidationAttributes
{
    public class ProducerNameValidationAttribute:ValidationAttribute
    {
            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                var name = value?.ToString();
                var Acc = (Producer)validationContext.ObjectInstance;
                if (Acc.Name == "") return ValidationResult.Success;
                using (var context = new AppDbContext(Helper._configurationPub))
                {
                    if (!context.Producers.Where(x => x.Name == Acc.Name).Any())
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        if (Acc.Name != "" && Acc.Name != null)
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

