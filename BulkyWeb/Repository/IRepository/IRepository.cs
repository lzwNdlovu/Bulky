using System.Linq.Expressions;

namespace BulkyWeb.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Category
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);
        void Add(T Entity);
        //void Update(T Entity);
        void REmove(T Entity);
        void RemoveRange(IEnumerable<T> Entity);

    }
}
