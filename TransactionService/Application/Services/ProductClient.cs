using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TransactionService.Application.DTOs;
using TransactionService.Application.Interfaces;

namespace TransactionService.Application.Services
{
    public class ProductClient : IProductClient
    {
        private readonly HttpClient _httpClient;

        public ProductClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // ✅ IMPLEMENTACIÓN NUEVA
        public async Task<ProductDto?> GetByIdAsync(int productId)
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>(
                $"/api/products/{productId}"
            );
        }

        public async Task<bool> UpdateStockAsync(int productId, int newStock)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"/api/products/{productId}/stock",
                new { stock = newStock }
            );

            // 👇 ProductService devuelve 204 NoContent
            return response.IsSuccessStatusCode;

        }


        public async Task<int> GetStockAsync(int productId)
        {
            var response = await _httpClient.GetAsync($"/api/products/{productId}");

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Producto no encontrado");

            var product = await response.Content.ReadFromJsonAsync<ProductDto>();
            return product.Stock;
        }

        Task<DTOs.ProductDto?> IProductClient.GetByIdAsync(int productId)
        {
            throw new NotImplementedException();
        }

        Task IProductClient.UpdateStockAsync(int productId, int newStock)
        {
            throw new NotImplementedException();
        }
    }

    public class ProductDto
    {
        public int Stock { get; set; }
    }
}
