using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interface
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        Task<bool> Delete(object id);
        Task<bool> Delete(T entity);
        Task<bool> Delete(Expression<Func<T, bool>> where);
        Task<T> Get(object id);
        Task<T> Get(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetAll();
        Task<int> Count(Expression<Func<T, bool>> where);
        Task<int> Count();

        IQueryable<T> Table { get; }

        IQueryable<T> TableNoTracking { get; }
    }
}
