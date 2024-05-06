using BookMarket.Models.Helpers;
using System.ComponentModel.DataAnnotations;

namespace BookMarket.Models.MyValidationAttributes
{
    public class UniqueAccEmailAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var name = value?.ToString();
            var Acc = (Account)validationContext.ObjectInstance;
            if (Acc.UserName == "") return ValidationResult.Success;
            using (var context = new AppDbContext(Helper._configurationPub))
            {
                if (context.Accounts.Where(x => x.UserName == Acc.UserName).Any())
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Username Must be Unique");
                }
            }
        }
    }
}
