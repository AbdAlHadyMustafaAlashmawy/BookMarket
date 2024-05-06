using BookMarket.Models.MyValidationAttributes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMarket.Models
{
    [Table("Accounts")]
    public class Account
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [RegularExpression(@"[A-Za-z0-9]{3,25}", ErrorMessage = "The UserName should be between 6 and 25 characters and contain only letters and numbers")]
        [UniqueAccName]
        public string UserName { get; set; }
        public string? Uploaded_Photo_URl { get; set; }
        [DataType(DataType.Password)]
        [RegularExpression(@"[A-Za-z0-9]{3,25}", ErrorMessage = "The password should be between 6 and 25 characters and contain only letters and numbers")]
        public string? Password { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-.\s]?([0-9]{3})[-.\s]?([0-9]{4})$", ErrorMessage = "Invalid phone number format. Please enter a valid phone number like '123-456-7890' or '(123) 456-7890'.")]
        public string? PhoneNumber { get; set; }

        [RegularExpression(@"\w*@gmail\.com",ErrorMessage ="Invalid gmail")]
        public string? Gmail { get; set; }
        [PastBirthdate]
        public DateTime Birthday { get; set;}
        public bool? Admin { get; set; }
        [ValidateNever]

        public string? facebookAcct { get; set; }

    }
}
