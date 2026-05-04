using Marketplace.Core.Data;
using Marketplace.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Core.Repositories;

public interface IJobRepository : IRepository<Job>
{
    Task<IEnumerable<Job>> GetByCustomerIdAsync(Guid customerId);
}

public class JobRepository : Repository<Job>, IJobRepository
{
    public JobRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Job>> GetByCustomerIdAsync(Guid customerId) =>
        await Context.Jobs
            .Where(j => j.CustomerId == customerId)
            .ToListAsync();
}
