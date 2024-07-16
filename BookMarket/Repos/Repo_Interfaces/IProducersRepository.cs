using BookMarket.Models.Helpers;
using BookMarket.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMarket.Repos.Repo_Interfaces
{
    public interface IProducersRepository:GenaricReposatory<Producer>
    {
        public List<Producer> GetAll();
        public Task<List<Producer>> GetAllAsync();
        public void Insert(Producer producer);
        public Producer GetById(int id);
        public  Task<Producer> GetByIdAsync(int id);
        public void Update(Producer producer);
        public int GetIdByBookId(int BookId);
        public void Remove(Producer producer);
    }
}
