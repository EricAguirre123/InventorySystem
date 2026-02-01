using System.Net.Http;
using System.Net.Http.Json;
using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;

namespace ProductService.Application.Services
{
    public class ProductClient : IProductClient
    {
        private readonly HttpClient _httpClient;

        public ProductClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDto?> GetByIdAsync(int productId)
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>(
                $"/api/Products/{productId}"
            );
        }

        public async Task UpdateStockAsync(int productId, int newStock)
        {
            var dto = new UpdateStockDto
            {
                Stock = newStock
            };

            var response = await _httpClient.PutAsJsonAsync(
                $"/api/Products/{productId}/stock",
                dto
            );

            response.EnsureSuccessStatusCode();
        }
    }
}
