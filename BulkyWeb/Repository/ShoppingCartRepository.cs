using Bulky.DataAccess.Data;
using Bulky.Models.Models;
using BulkyWeb.Repository.IRepository;

namespace BulkyWeb.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Remove(ShoppingCart obj)
        {
            _db.Remove(obj);
        }



        //public void save()
        //{
        //    throw new NotImplementedException();
        //}

        public void Update(ShoppingCart obj)
        {
            _db.ShoppingCarts.Update(obj);
        }
    }
}
