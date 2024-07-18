using System.Domain.Specification;

namespace System.Domain.Entities
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);

        Task<IReadOnlyList<T>> GetAllAsyncWithSpec(ISpecifiction<T> specifiction);
        Task<T> GetByIdAsyncWithSpec(ISpecifiction<T> specifiction);


    }
}
