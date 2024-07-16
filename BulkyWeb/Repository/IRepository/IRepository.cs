using System.Linq.Expressions;

namespace BulkyWeb.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Category
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? IncludeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? IncludeProperties = null, bool tracked = false);
        void Add(T Entity);
        //void Update(T Entity);
        void REmove(T Entity);
        void RemoveRange(IEnumerable<T> Entity);

    }
}
