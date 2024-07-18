using Microsoft.EntityFrameworkCore;
using System.Domain;
using System.Domain.Entities;
using System.Domain.Specification;
using System.Repository.Data;

namespace System.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly SystemContext _dbcontext;

        public GenericRepository(SystemContext dbcontext) // Ask CLR to inject object from DbContext [Implecitly]
        {
            _dbcontext = dbcontext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
           => await _dbcontext.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id)
            => await _dbcontext.Set<T>().FindAsync(id);

        public async Task<int> AddAsync(T entity)
        {
            await _dbcontext.Set<T>().AddAsync(entity);
            return await _dbcontext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
            return await _dbcontext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsyncWithSpec(ISpecifiction<T> specifiction)
            => await SpecifictionEvalutor<T>.GetQuery(_dbcontext.Set<T>(), specifiction).ToListAsync();

        public async Task<T> GetByIdAsyncWithSpec(ISpecifiction<T> specifiction)
            => await SpecifictionEvalutor<T>.GetQuery(_dbcontext.Set<T>(), specifiction).FirstOrDefaultAsync();

        public async Task<int> UpdateAsync(T entity)
        {
            _dbcontext.Set<T>().Update(entity);
            return await _dbcontext.SaveChangesAsync();
        }


    }
}
