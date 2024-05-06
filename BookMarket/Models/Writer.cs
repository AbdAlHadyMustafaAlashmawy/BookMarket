using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMarket.Models
{
    public class Writer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [Display(Name="Writer Name")]
        [RegularExpression("[A-Za-zأ-ي]{3,25}", ErrorMessage = "The user name should be characters(Capital or small letters) without spaces and between 3 and 25 charcters")]
        public string Name { get; set; }
        [Display(Name= "Description")]
        public string Description { get; set; }
        [Display(Name= "Religion")]
        public string Religion { get; set; }
        [Display(Name= "BirthDay")]
        public DateTime BirthDay { get; set; }
        [Display(Name="IsDead")]
        public bool DeadOrAlive { get; set; }
        [Display(Name= "Profile Picture Url")]
        [DataType(DataType.Url)]
        public string ProfilePictureUrl { get; set; }
        public List<Book> My_Books { get; set; }

    }
}
