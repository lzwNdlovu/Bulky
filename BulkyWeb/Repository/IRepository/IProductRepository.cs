using BulkyWeb.Models;

namespace BulkyWeb.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
        //void save();

        void Remove(Product obj);
    }
}
