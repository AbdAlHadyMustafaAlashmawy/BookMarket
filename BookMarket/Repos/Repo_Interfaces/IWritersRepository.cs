using BookMarket.Models.Helpers;
using BookMarket.Models;

namespace BookMarket.Repos.Repo_Interfaces
{
    public interface IWritersRepository:GenaricReposatory<Writer>
    {
        public List<Writer> GetAll();

        public Writer GetById(int id);

        public void Insert(Writer model);

        public void Remove(Writer model);

        public void Update(Writer model);
    }
}
