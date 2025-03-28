using Ecom.Core.DTO;
using Ecom.Core.Entities;

namespace Ecom.Core.Interfaces
{
    public interface IProductRepository : IGenericRepositories<Product>
    {
        Task<bool> AddAsync(AddProductDto productDto);
        Task<bool> UpdateAsync(int id, UpdateProductDto productDto);
        Task<bool> DeleteAsync(int id);

    }
}
