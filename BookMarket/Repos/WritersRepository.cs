using BookMarket.Models;
using BookMarket.Models.Helpers;

namespace BookMarket.Repos
{
    public class WritersRepository
    {
        private readonly AppDbContext context;

        public WritersRepository()
        {
            context = new AppDbContext(Helper._configurationPub);
        }
        public List<Writer> GetAll()
        {
            return context.Writers.ToList();
        }
    }
}
