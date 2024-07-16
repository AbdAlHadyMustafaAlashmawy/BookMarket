using BookMarket.Models;

namespace BookMarket.Repos.Repo_Interfaces
{
    public interface IBagsReposatory:GenaricReposatory<Bag>
    {
        public List<Bag> GetAll();

        public Bag GetById(int id);

        public void Insert(Bag model);

        public void Remove(Bag model);
        public void Update(Bag model);

    }
}
