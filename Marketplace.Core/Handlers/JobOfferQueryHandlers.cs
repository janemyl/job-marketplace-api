using MediatR;
using Marketplace.Core.Dtos;
using Marketplace.Core.Queries;
using Marketplace.Core.Services;

namespace Marketplace.Core.Handlers;

public class GetJobOfferQueryHandler : IRequestHandler<GetJobOfferQuery, JobOfferDto>
{
    private readonly JobOfferService _service;

    public GetJobOfferQueryHandler(JobOfferService service) => _service = service;

    public async Task<JobOfferDto> Handle(GetJobOfferQuery request, CancellationToken cancellationToken)
    {
        var jobOffer = await _service.GetByIdAsync(request.Id);
        return new JobOfferDto
        {
            Id = jobOffer.Id,
            JobId = jobOffer.JobId,
            ContractorId = jobOffer.ContractorId,
            Price = jobOffer.Price
        };
    }
}

public class GetAllJobOffersQueryHandler : IRequestHandler<GetAllJobOffersQuery, IEnumerable<JobOfferDto>>
{
    private readonly JobOfferService _service;

    public GetAllJobOffersQueryHandler(JobOfferService service) => _service = service;

    public async Task<IEnumerable<JobOfferDto>> Handle(GetAllJobOffersQuery request, CancellationToken cancellationToken)
    {
        var jobOffers = await _service.GetAllAsync();
        return jobOffers.Skip((request.Page - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .Select(jo => new JobOfferDto
                        {
                            Id = jo.Id,
                            JobId = jo.JobId,
                            ContractorId = jo.ContractorId,
                            Price = jo.Price
                        });
    }
}

