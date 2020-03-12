using System;
using System.Linq;
using System.Threading.Tasks;
using CheckoutChallenge.Domain.PaymentAggregate;
using CheckoutChallenge.Domain.PaymentAggregate.Services;
using CheckoutChallenge.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace CheckoutChallenge.Infrastructure.Data
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDbContext _context;
        public IUnitOfWork UnitOfWork { get => _context; }

        public PaymentRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public Payment Add(Payment payment)
        {
            return _context.Payments.Add(payment).Entity;
        }

        public async Task<Payment> FindByIdAsync(Guid id)
        {
            return await _context.Payments.SingleOrDefaultAsync(p => p.Id == id).ConfigureAwait(false);
        }
    }
}