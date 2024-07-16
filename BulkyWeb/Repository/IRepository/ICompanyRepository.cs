using Bulky.Models.Models;

namespace BulkyWeb.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Update(Company obj);
        //void save();

        void Remove(Company obj);
    }
}
