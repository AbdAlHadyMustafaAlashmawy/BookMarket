using BookMarket.Models.Helpers;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.ComponentModel.DataAnnotations;

namespace BookMarket.Models.MyValidationAttributes
{
    public class UniqueAccNameAttribute:ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var name = value?.ToString();
            var Acc = (Account) validationContext.ObjectInstance;
            if(Acc.UserName == "")return ValidationResult.Success;
            using (var context = new AppDbContext(Helper._configurationPub))
            {
                if (!context.Accounts.Where(x => x.UserName == Acc.UserName).Any())
                {
                    return ValidationResult.Success;
                }
                else
                {
                    if (Acc.UserName != ""&&Acc.UserName!=null)
                    {
                        return new ValidationResult("Username Must be Unique");
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
