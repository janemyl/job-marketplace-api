using Marketplace.Core.Entities;
using Marketplace.Core.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Marketplace.Core.Services;

public class ContractorService
{
    private readonly IContractorRepository _repository;
    private readonly IMemoryCache _cache;
    private const string AllContractorsKey = "all_contractors";
    private readonly MemoryCacheEntryOptions _cacheOptions = new() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) };

    public ContractorService(IContractorRepository repository, IMemoryCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<Contractor> GetByIdAsync(Guid id)
    {
        var contractor = await _repository.GetByIdAsync(id);
        if (contractor == null)
            throw new ArgumentException($"Contractor with ID {id} not found.");
        return contractor;
    }

    public async Task<IEnumerable<Contractor>> GetAllAsync()
    {
        if (_cache.TryGetValue(AllContractorsKey, out IEnumerable<Contractor>? cached))
            return cached!;

        var contractors = await _repository.GetAllAsync();
        _cache.Set(AllContractorsKey, contractors, _cacheOptions);
        return contractors;
    }

    public async Task<IEnumerable<Contractor>> SearchByNameAsync(string name)
    {
        var cacheKey = $"contractor_search_{name.ToLower()}";
        if (_cache.TryGetValue(cacheKey, out IEnumerable<Contractor>? cached))
            return cached!;

        var results = await _repository.SearchByNameAsync(name);
        _cache.Set(cacheKey, results, _cacheOptions);
        return results;
    }

    public async Task AddAsync(Contractor contractor)
    {
        if (string.IsNullOrWhiteSpace(contractor.Name))
            throw new ArgumentException("Name is required.");
        if (contractor.Rating < 0 || contractor.Rating > 5)
            throw new ArgumentException("Rating must be between 0 and 5.");

        contractor.Id = Guid.NewGuid();
        await _repository.AddAsync(contractor);
    }

    public async Task UpdateAsync(Contractor contractor)
    {
        if (string.IsNullOrWhiteSpace(contractor.Name))
            throw new ArgumentException("Name is required.");
        if (contractor.Rating < 0 || contractor.Rating > 5)
            throw new ArgumentException("Rating must be between 0 and 5.");

        await _repository.UpdateAsync(contractor);
    }

    public async Task DeleteAsync(Guid id) =>
        await _repository.DeleteAsync(id);
}
