using MediatR;
using Marketplace.Core.Dtos;

namespace Marketplace.Core.Queries;

public record GetJobOfferQuery(Guid Id) : IRequest<JobOfferDto>;
public record GetAllJobOffersQuery(int Page = 1, int PageSize = 10) : IRequest<IEnumerable<JobOfferDto>>;

