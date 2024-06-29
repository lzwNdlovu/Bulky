using BulkyWeb.Data;
using BulkyWeb.Repository.IRepository;

namespace BulkyWeb.Repository
{
    public class UnitOFWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }

        public UnitOFWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            //Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
