using MediatR;
using Marketplace.Core.Dtos;
using Marketplace.Core.Queries;
using Marketplace.Core.Services;

namespace Marketplace.Core.Handlers;

public class GetJobQueryHandler : IRequestHandler<GetJobQuery, JobDto>
{
    private readonly JobService _service;

    public GetJobQueryHandler(JobService service) => _service = service;

    public async Task<JobDto> Handle(GetJobQuery request, CancellationToken cancellationToken)
    {
        var job = await _service.GetByIdAsync(request.Id);
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

public class GetAllJobsQueryHandler : IRequestHandler<GetAllJobsQuery, IEnumerable<JobDto>>
{
    private readonly JobService _service;

    public GetAllJobsQueryHandler(JobService service) => _service = service;

    public async Task<IEnumerable<JobDto>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
    {
        var jobs = await _service.GetAllAsync();
        return jobs.Skip((request.Page - 1) * request.PageSize)
                   .Take(request.PageSize)
                   .Select(j => new JobDto
                   {
                       Id = j.Id,
                       StartDate = j.StartDate,
                       DueDate = j.DueDate,
                       Budget = j.Budget,
                       Description = j.Description,
                       AcceptedJobOfferId = j.AcceptedJobOfferId
                   });
    }
}

