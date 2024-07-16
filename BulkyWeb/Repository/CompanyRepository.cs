using Bulky.DataAccess.Data;
using Bulky.Models.Models;
using BulkyWeb.Repository.IRepository;

namespace BulkyWeb.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Remove(Company obj)
        {
            _db.Remove(obj);
        }



        //public void save()
        //{
        //    throw new NotImplementedException();
        //}

        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}
