using BookMarket.Models;

namespace BookMarket.Repos.Repo_Interfaces
{
    public interface IBooksReposatory:GenaricReposatory<Book>
    {
        public int GetWriterId(int id);
        public Task<List<Book>> GetWholeAsync();
        public List<Book> GetWhole();
    }
}
