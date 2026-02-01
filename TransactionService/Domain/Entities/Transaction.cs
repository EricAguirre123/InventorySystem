using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactionService.Domain.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public DateTime TransactionDate { get; set; }

        public string TransactionType { get; set; } = null!;

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public string? Detail { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
