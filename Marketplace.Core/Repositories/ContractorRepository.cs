using Marketplace.Core.Data;
using Marketplace.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Core.Repositories;

public interface IContractorRepository : IRepository<Contractor>
{
    Task<IEnumerable<Contractor>> SearchByNameAsync(string name);
}

public class ContractorRepository : Repository<Contractor>, IContractorRepository
{
    public ContractorRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Contractor>> SearchByNameAsync(string name) =>
        await Context.Contractors
            .Where(c => (c.Name ?? "").ToLower().StartsWith(name.ToLower()))
            .ToListAsync();
}
