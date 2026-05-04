using MediatR;
using Marketplace.Core.Dtos;
using Marketplace.Core.Queries;
using Marketplace.Core.Services;

namespace Marketplace.Core.Handlers;

public class GetContractorQueryHandler : IRequestHandler<GetContractorQuery, ContractorDto>
{
    private readonly ContractorService _service;

    public GetContractorQueryHandler(ContractorService service) => _service = service;

    public async Task<ContractorDto> Handle(GetContractorQuery request, CancellationToken cancellationToken)
    {
        var contractor = await _service.GetByIdAsync(request.Id);
        return new ContractorDto { Id = contractor.Id, Name = contractor.Name, Rating = contractor.Rating };
    }
}

public class GetAllContractorsQueryHandler : IRequestHandler<GetAllContractorsQuery, IEnumerable<ContractorDto>>
{
    private readonly ContractorService _service;

    public GetAllContractorsQueryHandler(ContractorService service) => _service = service;

    public async Task<IEnumerable<ContractorDto>> Handle(GetAllContractorsQuery request, CancellationToken cancellationToken)
    {
        var contractors = await _service.GetAllAsync();
        return contractors.Skip((request.Page - 1) * request.PageSize)
                          .Take(request.PageSize)
                          .Select(c => new ContractorDto { Id = c.Id, Name = c.Name, Rating = c.Rating });
    }
}

public class SearchContractorsByNameQueryHandler : IRequestHandler<SearchContractorsByNameQuery, IEnumerable<ContractorDto>>
{
    private readonly ContractorService _service;

    public SearchContractorsByNameQueryHandler(ContractorService service) => _service = service;

    public async Task<IEnumerable<ContractorDto>> Handle(SearchContractorsByNameQuery request, CancellationToken cancellationToken)
    {
        var contractors = await _service.SearchByNameAsync(request.Name);
        return contractors.Select(c => new ContractorDto { Id = c.Id, Name = c.Name, Rating = c.Rating });
    }
}
