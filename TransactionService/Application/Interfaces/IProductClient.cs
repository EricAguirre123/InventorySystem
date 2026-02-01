using System.Threading.Tasks;
using TransactionService.Application.DTOs;


namespace TransactionService.Application.Interfaces
{
    public interface IProductClient
    {
        Task<ProductDto?> GetByIdAsync(int productId);
        //Task<int> GetStockAsync(int productId);
        Task UpdateStockAsync(int productId, int newStock);


    }
}
