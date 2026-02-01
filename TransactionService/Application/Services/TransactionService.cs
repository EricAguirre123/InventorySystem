using TransactionService.Application.Interfaces;
using TransactionService.Domain.Entities;
using TransactionService.Infrastructure.Data;

namespace TransactionService.Application.Services
{
    public class TransactionService
    {
        private readonly TransactionDbContext _context;
        private readonly IProductClient _productClient;

        public TransactionService(
            TransactionDbContext context,
            IProductClient productClient)
        {
            _context = context;
            _productClient = productClient;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return _context.Transactions.ToList();
        }

        public async Task<IEnumerable<Transaction>> GetFilteredAsync(
            DateTime? from,
            DateTime? to,
            string? type,
            int? productId
        )
        {
            var query = _context.Transactions.AsQueryable();

            if (from.HasValue)
                query = query.Where(t => t.TransactionDate >= from.Value);

            if (to.HasValue)
                query = query.Where(t => t.TransactionDate <= to.Value);

            if (!string.IsNullOrEmpty(type))
                query = query.Where(t => t.TransactionType == type);

            if (productId.HasValue)
                query = query.Where(t => t.ProductId == productId.Value);

            return query.ToList();
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            // 1️⃣ Obtener producto completo
            var product = await _productClient.GetByIdAsync(transaction.ProductId);

            if (product == null)
                throw new Exception("Producto no existe");

            // 2️⃣ Validar stock si es venta
            if (transaction.TransactionType == "Venta")
            {
                if (product.Stock < transaction.Quantity)
                    throw new Exception("Stock insuficiente");

                await _productClient.UpdateStockAsync(
                    transaction.ProductId,
                    product.Stock - transaction.Quantity
                );
            }

            // 3️⃣ Ajustar stock si es compra
            if (transaction.TransactionType == "Compra")
            {
                await _productClient.UpdateStockAsync(
                    transaction.ProductId,
                    product.Stock + transaction.Quantity
                );
            }

            // 4️⃣ Calcular totales y fechas
            transaction.TotalPrice =
                transaction.Quantity * transaction.UnitPrice;

            transaction.TransactionDate = DateTime.UtcNow;
            transaction.CreatedAt = DateTime.UtcNow;

            // 5️⃣ Guardar transacción
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }
    }
}
