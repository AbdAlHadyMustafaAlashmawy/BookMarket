using BookMarket.Models;
using BookMarket.Models.Helpers;
using BookMarket.Repos.Repo_Interfaces;

namespace BookMarket.Repos
{
    public class WritersRepository:IWritersRepository
    {
        private readonly AppDbContext context;

        public WritersRepository(AppDbContext _context)
        {
            context = _context;
        }
        public List<Writer> GetAll()
        {
            return context.Writers.ToList();
        }

        public Writer GetById(int id)
        {
            return context.Writers.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Writer model)
        {
            context.Writers.Add(model);
            context.SaveChanges();
        }

        public void Remove(Writer model)
        {
            context.Remove(model);
            context.SaveChanges();
        }

        public void Update(Writer model)
        {
            context.Writers.Update(model);
            context.SaveChanges();
        }
    }
}
