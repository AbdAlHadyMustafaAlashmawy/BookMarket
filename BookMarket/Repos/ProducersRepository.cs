using BookMarket.Models;
using BookMarket.Models.Helpers;
using BookMarket.Repos.Repo_Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookMarket.Repos
{
    public class ProducersRepository:IProducersRepository
    {

        private readonly AppDbContext context;
        public ProducersRepository(AppDbContext _context)
        {
            context = _context;
        }
        public List<Producer> GetAll()
        {
            return context.Producers.ToList();
        }
        public async Task<List<Producer>> GetAllAsync()
        {
            return await context.Producers.ToListAsync();
        }
        public void Insert(Producer producer)
        {
            context.Producers.Add(producer);
            context.SaveChanges();
        }
        public Producer GetById(int id)
        {
            return context.Producers.FirstOrDefault(x=> x.Id == id);
        }
        public async Task<Producer> GetByIdAsync(int id)
        {
            return await context.Producers.FirstOrDefaultAsync(x => x.Id == id);
        }
        public void Update(Producer producer)
        {
            context.Producers.Update(producer);
            context.SaveChanges();
        }
        public int GetIdByBookId(int BookId)
        {
            return context.Books.FirstOrDefault(x => x.Id == BookId).ProducerId;
        }
        public void Remove(Producer producer)
        {
            context.Producers.Remove(producer);
            context.SaveChanges();
        }
    }
}
