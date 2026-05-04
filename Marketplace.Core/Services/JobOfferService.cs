using Marketplace.Core.Entities;
using Marketplace.Core.Repositories;

namespace Marketplace.Core.Services;

public class JobOfferService
{
    private readonly IRepository<JobOffer> _repository;
    private readonly IRepository<Job> _jobRepository;

    public JobOfferService(
        IRepository<JobOffer> repository, 
        IRepository<Job> jobRepository)
    {
        _repository = repository;
        _jobRepository = jobRepository;
    }

    public async Task<JobOffer> GetByIdAsync(Guid id)
    {
        var offer = await _repository.GetByIdAsync(id);
        if (offer == null)
            throw new ArgumentException($"Job Offer with ID {id} not found.");
        return offer;
    }

    public async Task<IEnumerable<JobOffer>> GetAllAsync() =>
        await _repository.GetAllAsync();

    public async Task AddAsync(JobOffer offer)
    {
        var job = await _jobRepository.GetByIdAsync(offer.JobId);
        if (job == null)
            throw new ArgumentException("Job does not exist.");
        if (job.AcceptedJobOfferId.HasValue)
            throw new ArgumentException("This job has already been accepted.");
        if (offer.Price <= 0)
            throw new ArgumentException("Price must be greater than 0.");

        offer.Id = Guid.NewGuid();
        await _repository.AddAsync(offer);
    }

    public async Task UpdateAsync(JobOffer offer)
    {
        if (offer.Price <= 0)
            throw new ArgumentException("Price must be greater than 0.");

        await _repository.UpdateAsync(offer);
    }

    public async Task DeleteAsync(Guid id) =>
        await _repository.DeleteAsync(id);
}
