using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Companys.Domain.Entities;
using Companys.Domain.Common;

namespace Companys.Infrastructure.Persistence
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {

        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<ExchangeType> ExchangeType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
            .HasOne<ExchangeType>(e => e.ExType)
            .WithMany(em => em.Comp)
            .HasForeignKey(em => em.ExchangeTypeId);

            modelBuilder.Entity<ExchangeType>()
                .Property(e => e.StockExchange)
                .HasMaxLength(5);

            modelBuilder.Entity<ExchangeType>(entity =>
            {
                entity.Property(e => e.StockExchange)
                    .IsRequired()
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Ceo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Website)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TurnOver)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasPrecision(19, 2)
                    .HasColumnType("money");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Company>().HasIndex(s => s.Code).IsUnique();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.CreatedBy = "SUGAN";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = "SUGAN";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
