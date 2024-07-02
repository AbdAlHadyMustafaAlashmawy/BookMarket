namespace BookMarket.Models.DTOs
{
    public class GroupedOrderByIdDTO
    {
        public int BookId { get; set; }
        public int Count { get; set; }
        public List<Bag> Orders { get; set; }
    }
}
