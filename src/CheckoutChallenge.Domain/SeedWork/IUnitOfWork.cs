using System;
using System.Threading;
using System.Threading.Tasks;

namespace CheckoutChallenge.Domain.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {        
        Task<Guid> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
