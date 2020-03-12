using System;
using System.Threading;
using System.Threading.Tasks;
using CheckoutChallenge.Domain.PaymentAggregate;
using CheckoutChallenge.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace CheckoutChallenge.Infrastructure.Data
{
    public class PaymentDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Payment> Payments { get; set; }

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>().OwnsOne(o => o.CardNumber);
            modelBuilder.Entity<Payment>().OwnsOne(o => o.PaymentStatus);
            modelBuilder.Entity<Payment>().OwnsOne(o => o.Currency);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return true;
        }
    }
}