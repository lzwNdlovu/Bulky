using BulkyWeb.Data;
using BulkyWeb.Models;
using BulkyWeb.Repository.IRepository;

namespace BulkyWeb.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Remove(Product obj)
        {
            _db.Remove(obj);
        }



        //public void save()
        //{
        //    throw new NotImplementedException();
        //}

        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
