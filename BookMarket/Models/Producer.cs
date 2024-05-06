using BookMarket.Models.MetaDataModels;
using BookMarket.Models.MyValidationAttributes;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMarket.Models
{
    [ModelMetadataType(typeof(ProducerMetaData))]

    public class Producer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Remote("UniqueProducerName", "Validation",ErrorMessage ="This Name is already used in database")]
        [ProducerNameValidation]
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
        public List<Book>? Books { get; set; }

    }
}
