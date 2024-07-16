using BookMarket.Models;
using BookMarket.Models.Helpers;
using BookMarket.Repos.Repo_Interfaces;

namespace BookMarket.Repos
{
 
    public class BagsReposatory : IBagsReposatory
    {
        private readonly AppDbContext _context;

        public BagsReposatory(AppDbContext context)
        {
            _context = context;
        }

        public List<Bag> GetAll()
        {
           return _context.Bag.ToList();
        }

        public Bag GetById(int id)
        {
            return _context.Bag.FirstOrDefault(x=>x.Id==id);
        }

        public void Insert(Bag model)
        {
            _context.Bag.Add(model);
            _context.SaveChanges();
        }

        public void Remove(Bag model)
        {
            _context.Remove(model);
            _context.SaveChanges();
        }
        public void Update(Bag model)
        {
            _context.Update(model);
            _context.SaveChanges();
        }

    }
}
