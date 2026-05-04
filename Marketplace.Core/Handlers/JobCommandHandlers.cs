using MediatR;
using Marketplace.Core.Commands;
using Marketplace.Core.Dtos;
using Marketplace.Core.Entities;
using Marketplace.Core.Services;

namespace Marketplace.Core.Handlers;

public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, JobDto>
{
    private readonly JobService _service;
    private readonly IAuditService _audit;

    public CreateJobCommandHandler(JobService service, IAuditService audit) => (_service, _audit) = (service, audit);

    public async Task<JobDto> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var job = new Job
        {
            CustomerId = request.CustomerId,
            StartDate = request.StartDate,
            DueDate = request.DueDate,
            Budget = request.Budget,
            Description = request.Description
        };
        await _service.AddAsync(job);
        await _audit.LogAsync(nameof(Job), job.Id, "Create", "system", job);
        return new JobDto
        {
            Id = job.Id,
            StartDate = job.StartDate,
            DueDate = job.DueDate,
            Budget = job.Budget,
            Description = job.Description,
            AcceptedJobOfferId = job.AcceptedJobOfferId
        };
    }
}

public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, JobDto>
{
    private readonly JobService _service;
    private readonly IAuditService _audit;

    public UpdateJobCommandHandler(JobService service, IAuditService audit) => (_service, _audit) = (service, audit);

    public async Task<JobDto> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _service.GetByIdAsync(request.Id);
        job.StartDate = request.StartDate;
        job.DueDate = request.DueDate;
        job.Budget = request.Budget;
        job.Description = request.Description;
        await _service.UpdateAsync(job);
        await _audit.LogAsync(nameof(Job), job.Id, "Update", "system", job);
        return new JobDto
        {
            Id = job.Id,
            StartDate = job.StartDate,
            DueDate = job.DueDate,
            Budget = job.Budget,
            Description = job.Description,
            AcceptedJobOfferId = job.AcceptedJobOfferId
        };
    }
}

public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, bool>
{
    private readonly JobService _service;
    private readonly IAuditService _audit;

    public DeleteJobCommandHandler(JobService service, IAuditService audit) => (_service, _audit) = (service, audit);

    public async Task<bool> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _service.GetByIdAsync(request.Id);
        await _service.DeleteAsync(request.Id);
        await _audit.LogAsync(nameof(Job), request.Id, "Delete", "system", job);
        return true;
    }
}
