using BookMarket.Models.Helpers;
using System.ComponentModel.DataAnnotations;

namespace BookMarket.Models.MyValidationAttributes
{
    public class BookNameValidationAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var BookValidator = (Book) validationContext.ObjectInstance;
            if(BookValidator.Name.Length<3 || BookValidator.Name.Length > 25)
            {
                return new ValidationResult("Book Name must be between 3 and 25 character");
            }
            else
            {
                using (var context = new AppDbContext(Helper._configurationPub))
                {
                    if (!context.Books.Where(x => x.Name == BookValidator.Name.Trim()).Any())
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        if (BookValidator.Name != null && BookValidator.Name != "")
                        {
                            return new ValidationResult("This name is already used");
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
}
