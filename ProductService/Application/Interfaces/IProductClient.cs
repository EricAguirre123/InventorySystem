using System.Threading.Tasks;
using ProductService.Application.DTOs;

namespace ProductService.Application.Interfaces
{
    public interface IProductClient
    {
        Task<ProductDto?> GetByIdAsync(int productId);
        Task UpdateStockAsync(int productId, int newStock);
    }
}
