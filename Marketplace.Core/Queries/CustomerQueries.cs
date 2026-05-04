using MediatR;
using Marketplace.Core.Dtos;

namespace Marketplace.Core.Queries;

public record GetCustomerQuery(Guid Id) : IRequest<CustomerDto>;
public record GetAllCustomersQuery(int Page = 1, int PageSize = 10) : IRequest<IEnumerable<CustomerDto>>;
public record SearchCustomersByLastNameQuery(string LastName) : IRequest<IEnumerable<CustomerDto>>;
