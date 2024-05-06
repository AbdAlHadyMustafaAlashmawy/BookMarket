using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BookMarket.Models.MyValidationAttributes;

namespace BookMarket.Models.MetaDataModels
{
    public class ProducerMetaData
    {
        public int Id { get; set; }
        [Remote("IsProducerNameUnique", "ValidationController", ErrorMessage = "Producer name must be unique")]
        [ProducerNameValidation]
        public string? Name { get; set; }
        public string? LogoUrl { get; set; }
        public string? Description { get; set; }
        public List<Book>? Books { get; set; }
    }
}
