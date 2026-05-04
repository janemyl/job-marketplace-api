using Marketplace.Core.Data;
using Marketplace.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Core.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<IEnumerable<Customer>> SearchByLastNameAsync(string lastName);
}

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Customer>> SearchByLastNameAsync(string lastName) =>
        await Context.Customers
            .Where(c => (c.LastName ?? "").ToLower().StartsWith(lastName.ToLower()))
            .ToListAsync();
}
