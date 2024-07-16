using Bulky.Models.Models;

namespace BulkyWeb.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
        //void save();

        void Remove(Category obj);
    }
}
