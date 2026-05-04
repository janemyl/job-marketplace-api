using Marketplace.Core.Entities;
using Marketplace.Core.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Marketplace.Core.Services;

public class CustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly IMemoryCache _cache;
    private const string AllCustomersKey = "all_customers";
    private const string CustomerSearchKeyPrefix = "customer_search_";
    private readonly MemoryCacheEntryOptions _cacheOptions = new() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) };

    public CustomerService(ICustomerRepository repository, IMemoryCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<Customer> GetByIdAsync(Guid id)
    {
        var customer = await _repository.GetByIdAsync(id);
        if (customer == null)
            throw new ArgumentException($"Customer with ID {id} not found.");
        return customer;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        if (_cache.TryGetValue(AllCustomersKey, out IEnumerable<Customer>? cached))
            return cached!;

        var customers = await _repository.GetAllAsync();
        _cache.Set(AllCustomersKey, customers, _cacheOptions);
        return customers;
    }

    public async Task<IEnumerable<Customer>> SearchByLastNameAsync(string lastName)
    {
        var cacheKey = $"{CustomerSearchKeyPrefix}{lastName.ToLower()}";
        if (_cache.TryGetValue(cacheKey, out IEnumerable<Customer>? cached))
            return cached!;

        var results = await _repository.SearchByLastNameAsync(lastName);
        _cache.Set(cacheKey, results, _cacheOptions);
        return results;
    }

    public async Task AddAsync(Customer customer)
    {
        if (string.IsNullOrWhiteSpace(customer.FirstName))
            throw new ArgumentException("First Name is required.");
        if (string.IsNullOrWhiteSpace(customer.LastName))
            throw new ArgumentException("Last Name is required.");

        customer.Id = Guid.NewGuid();
        await _repository.AddAsync(customer);
    }

    public async Task UpdateAsync(Customer customer)
    {
        if (string.IsNullOrWhiteSpace(customer.FirstName))
            throw new ArgumentException("First Name is required.");
        if (string.IsNullOrWhiteSpace(customer.LastName))
            throw new ArgumentException("Last Name is required.");

        await _repository.UpdateAsync(customer);
    }

    public async Task DeleteAsync(Guid id) =>
        await _repository.DeleteAsync(id);
}
