using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMarket.Models
{
    [Table("Bag")]
    public class Bag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int AccountId { get; set; }
        public double cost { get; set; }
        public DateTime Buying_Date { get; set; } = DateTime.Now;
        public List<Book> Books { get; set; }
        public Account Account { get; set; }
    }
}
