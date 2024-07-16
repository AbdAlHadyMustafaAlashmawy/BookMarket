using BookMarket.Models;

namespace BookMarket.Repos
{
    public interface GenaricReposatory<T> where T : class
    {
        ///////////////////////////
        public List<T> GetAll();

        // public async Task<List<T>> GetAllAsync() { };
        public void Insert(T model);
        public T GetById(int id);
        // public async Task<Producer> GetByIdAsync(int id);
        public void Update(T model);
        //public int GetIdByBookId(int BookId);
        public void Remove(T model);
    }
    ///////////////////////////
}