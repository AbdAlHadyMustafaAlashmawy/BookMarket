using BookMarket.Models.Enums;
using BookMarket.Models.MyValidationAttributes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMarket.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Display(Name= "Name of the Book")]
        [BookNameValidation]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a non-negative value.")]
        public decimal Cost { get; set; }
        public BookCategory Kind { get; set; }
        [DataType(DataType.Url)]
        public string? PictureUrl { get; set; }
        public string Description { get; set; }
        public int WriterId { get; set; }
        public int ProducerId { get; set; }
        public int SellingTimes { get; set; }
        [ValidateNever]
        public Producer Producer { get; set; }
        [ValidateNever]
        public Writer Writer { get; set; }
    }
}
