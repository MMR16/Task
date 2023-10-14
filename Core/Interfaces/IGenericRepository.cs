using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> ListAllAsync();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> FindByCondition(Expression<Func<T, bool>> expression);
    }
}
