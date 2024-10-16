using Application.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal readonly AppContext _appcontext;
        public Repository(AppContext appContext) 
        {
            _appcontext = appContext;
        }

        public async Task DeleteAsync(T entity)
        {
            _appcontext.Set<T>().Remove(entity);
            await _appcontext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
          return  await _appcontext.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _appcontext.Set<T>().AddAsync(entity);
            await _appcontext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _appcontext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _appcontext.Entry(entity).State = EntityState.Modified;
            await _appcontext.SaveChangesAsync();
        }
    }
}
