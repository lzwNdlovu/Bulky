using Bulky.DataAccess.Data;
using Bulky.Models.Models;
using BulkyWeb.Repository.IRepository;

namespace BulkyWeb.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

       



        //public void save()
        //{
        //    throw new NotImplementedException();
        //}

       
    }
}
