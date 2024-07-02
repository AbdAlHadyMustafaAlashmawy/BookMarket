using BookMarket.Models.MyValidationAttributes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        public byte[]? Image { get; set; }
        [NotMapped]
        [JsonIgnore]
        public IFormFile? client_file { get; set; }

        [NotMapped]
        public string Imagesrc
        {
            get
            {
                if (Image == null)
                {
                    return string.Empty;
                }

                try
                {
                    if (!ValidateImageFormat(Image))
                    {
                        throw new ArgumentException("Invalid image format. Supported formats: JPG, JPEG, PNG");
                    }

                    string base64String = Convert.ToBase64String(Image);
                    string mimeType = GetImageMimeType(Image);

                    return $"data:{mimeType};base64,{base64String}";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error converting image to base64: {ex.Message}");
                    return string.Empty;
                }
            }
        }

        private bool ValidateImageFormat(byte[] imageData)
        {
            if (imageData.Length < 3)
            {
                return false;
            }

            return (imageData[0] == 0xFF && imageData[1] == 0xD8 && imageData[2] == 0xFF)
                   || (imageData[0] == 0x89 && imageData[1] == 0x50 && imageData[2] == 0x4E && imageData[3] == 0x47);
        }

        private string GetImageMimeType(byte[] imageData)
        {
            if (imageData.Length < 3)
            {
                return "application/octet-stream";
            }

            return (imageData[0] == 0xFF && imageData[1] == 0xD8 && imageData[2] == 0xFF) ? "image/jpeg"
                   : (imageData[0] == 0x89 && imageData[1] == 0x50 && imageData[2] == 0x4E && imageData[3] == 0x47) ? "image/png"
                   : "application/octet-stream";
        }

    }
}
