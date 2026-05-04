using MediatR;
using Marketplace.Core.Dtos;

namespace Marketplace.Core.Queries;

public record GetJobQuery(Guid Id) : IRequest<JobDto>;
public record GetAllJobsQuery(int Page = 1, int PageSize = 10) : IRequest<IEnumerable<JobDto>>;

