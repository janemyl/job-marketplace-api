using Marketplace.Core.Entities;
using Marketplace.Core.Repositories;

namespace Marketplace.Core.Services;

public class JobService
{
    private readonly IRepository<Job> _repository;

    public JobService(IRepository<Job> repository) =>
        _repository = repository;

    public async Task<Job> GetByIdAsync(Guid id)
    {
        var job = await _repository.GetByIdAsync(id);
        if (job == null)
            throw new ArgumentException($"Job with ID {id} not found.");
        return job;
    }

    public async Task<IEnumerable<Job>> GetAllAsync() =>
        await _repository.GetAllAsync();

    public async Task AddAsync(Job job)
    {
        if (job.CustomerId == Guid.Empty)
            throw new ArgumentException("CustomerId is required");
        if (job.Budget <= 0)
            throw new ArgumentException("Budget must be greater than 0.");
        if (job.DueDate <= job.StartDate)
            throw new ArgumentException("Due Date must be after Start Date.");
        if (string.IsNullOrWhiteSpace(job.Description))
            throw new ArgumentException("Description is required.");

        job.Id = Guid.NewGuid();
        await _repository.AddAsync(job);
    }

    public async Task UpdateAsync(Job job)
    {
        if (job.Budget <= 0)
            throw new ArgumentException("Budget must be greater than 0.");
        if (job.DueDate <= job.StartDate)
            throw new ArgumentException("Due Date must be after Start Date.");
        if (string.IsNullOrWhiteSpace(job.Description))
            throw new ArgumentException("Description is required.");

        await _repository.UpdateAsync(job);
    }

    public async Task DeleteAsync(Guid id) =>
        await _repository.DeleteAsync(id);
}
