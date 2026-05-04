using MediatR;
using Marketplace.Core.Dtos;
using Marketplace.Core.Queries;
using Marketplace.Core.Services;

namespace Marketplace.Core.Handlers;

public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
{
    private readonly CustomerService _service;

    public GetCustomerQueryHandler(CustomerService service) => _service = service;

    public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await _service.GetByIdAsync(request.Id);
        return new CustomerDto { Id = customer.Id, FirstName = customer.FirstName, LastName = customer.LastName };
    }
}

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
{
    private readonly CustomerService _service;

    public GetAllCustomersQueryHandler(CustomerService service) => _service = service;

    public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _service.GetAllAsync();
        return customers.Skip((request.Page - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .Select(c => new CustomerDto { Id = c.Id, FirstName = c.FirstName, LastName = c.LastName });
    }
}

public class SearchCustomersByLastNameQueryHandler : IRequestHandler<SearchCustomersByLastNameQuery, IEnumerable<CustomerDto>>
{
    private readonly CustomerService _service;

    public SearchCustomersByLastNameQueryHandler(CustomerService service) => _service = service;

    public async Task<IEnumerable<CustomerDto>> Handle(SearchCustomersByLastNameQuery request, CancellationToken cancellationToken)
    {
        var customers = await _service.SearchByLastNameAsync(request.LastName);
        return customers.Select(c => new CustomerDto { Id = c.Id, FirstName = c.FirstName, LastName = c.LastName });
    }
}
