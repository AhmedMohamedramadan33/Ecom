using System.Linq.Expressions;

namespace Ecom.Core.Interfaces
{
    public interface IGenericRepositories<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] include);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] include);
        Task AddAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);

    }
}
