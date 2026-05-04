using MediatR;
using Marketplace.Core.Commands;
using Marketplace.Core.Dtos;
using Marketplace.Core.Entities;
using Marketplace.Core.Services;

namespace Marketplace.Core.Handlers;

public class CreateJobOfferCommandHandler : IRequestHandler<CreateJobOfferCommand, JobOfferDto>
{
    private readonly JobOfferService _service;
    private readonly IAuditService _audit;

    public CreateJobOfferCommandHandler(JobOfferService service, IAuditService audit) => (_service, _audit) = (service, audit);

    public async Task<JobOfferDto> Handle(CreateJobOfferCommand request, CancellationToken cancellationToken)
    {
        var jobOffer = new JobOffer
        {
            JobId = request.JobId,
            ContractorId = request.ContractorId,
            Price = request.Price
        };
        await _service.AddAsync(jobOffer);
        await _audit.LogAsync(nameof(JobOffer), jobOffer.Id, "Create", "system", jobOffer);
        return new JobOfferDto
        {
            Id = jobOffer.Id,
            JobId = jobOffer.JobId,
            ContractorId = jobOffer.ContractorId,
            Price = jobOffer.Price
        };
    }
}

public class DeleteJobOfferCommandHandler : IRequestHandler<DeleteJobOfferCommand, bool>
{
    private readonly JobOfferService _service;
    private readonly IAuditService _audit;

    public DeleteJobOfferCommandHandler(JobOfferService service, IAuditService audit) => (_service, _audit) = (service, audit);

    public async Task<bool> Handle(DeleteJobOfferCommand request, CancellationToken cancellationToken)
    {
        var jobOffer = await _service.GetByIdAsync(request.Id);
        await _service.DeleteAsync(request.Id);
        await _audit.LogAsync(nameof(JobOffer), request.Id, "Delete", "system", jobOffer);
        return true;
    }
}

