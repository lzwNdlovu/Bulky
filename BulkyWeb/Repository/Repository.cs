using BulkyWeb.Data;
using BulkyWeb.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            // _db.Categories == dbSet
            _db.Products.Include(u=> u.Category).Include(u=> u.CategoryID);
        }
        public void Add(T Entity)
        {
            dbSet.Add(Entity);
        }

        //public T Get(Expression<Func<T, bool>> filter)
        //{
        //    IQueryable<T> query = dbSet;
        //    query = query.Where(filter);
        //    if (!string.IsNullOrEmpty(IncludeProperties))
        //    {
        //        foreach (var includeProp in IncludeProperties
        //            .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            query = query.Include(includeProp);
        //        }
        //    }
        //    return query.FirstOrDefault();
        //}

        public T Get(Expression<Func<T, bool>> filter, string? IncludeProperties = null)
        {

            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(IncludeProperties))
            {
                foreach (var includeProp in IncludeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        //Category,CoverType
        public IEnumerable<T> GetAll(string? IncludeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(IncludeProperties))
            {
                foreach (var includeProp in IncludeProperties
                    .Split (new char[] {','},StringSplitOptions.RemoveEmptyEntries)) 
                { 
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public void REmove(T Entity)
        {
            dbSet.Remove(Entity);
        }

        public void RemoveRange(IEnumerable<T> Entity)
        {
            dbSet.RemoveRange(Entity);
        }
    }
}
