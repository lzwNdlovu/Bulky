using BulkyWeb.Data;
using BulkyWeb.Models;
using BulkyWeb.Repository.IRepository;

namespace BulkyWeb.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Remove(Category obj)
        {
            _db.Remove(obj);
        }



        //public void save()
        //{
        //    throw new NotImplementedException();
        //}

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
