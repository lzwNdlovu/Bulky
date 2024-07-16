using Bulky.Models.Models;

namespace BulkyWeb.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart obj);
        //void save();

        void Remove(ShoppingCart obj);
    }
}
