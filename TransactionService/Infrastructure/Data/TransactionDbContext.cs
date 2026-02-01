using Microsoft.EntityFrameworkCore;
using TransactionService.Domain.Entities;

namespace TransactionService.Infrastructure.Data
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
            : base(options)
        {
        }

        public DbSet<Transaction> Transactions => Set<Transaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.TransactionDate)
                      .HasColumnType("datetime2");

                entity.Property(e => e.CreatedAt)
                      .HasColumnType("datetime2");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
