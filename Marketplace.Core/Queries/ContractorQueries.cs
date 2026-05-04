using MediatR;
using Marketplace.Core.Dtos;

namespace Marketplace.Core.Queries;

public record GetContractorQuery(Guid Id) : IRequest<ContractorDto>;
public record GetAllContractorsQuery(int Page = 1, int PageSize = 10) : IRequest<IEnumerable<ContractorDto>>;
public record SearchContractorsByNameQuery(string Name) : IRequest<IEnumerable<ContractorDto>>;
