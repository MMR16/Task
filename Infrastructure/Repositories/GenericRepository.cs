using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().AddAsync(entity);
        }
        public void Delete(T entity)
        {
           _context.Set<T>().Remove(entity);
        }
        public virtual async Task<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
         return await  _context.Set<T>().SingleOrDefaultAsync(expression);
        }
        public async Task<T> GetByIdAsync(int id)
        {
          return await  _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e=>e.Id==id);
        }
        public async Task<List<T>> ListAllAsync()
        {
           return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
