using MediatR;
using Marketplace.Core.Dtos;

namespace Marketplace.Core.Commands;

/// <summary>Command to create a new customer.</summary>
public record CreateCustomerCommand(string? FirstName, string? LastName) : IRequest<CustomerDto>;
/// <summary>Command to update an existing customer.</summary>
public record UpdateCustomerCommand(Guid Id, string? FirstName, string? LastName) : IRequest<CustomerDto>;
/// <summary>Command to delete a customer.</summary>
public record DeleteCustomerCommand(Guid Id) : IRequest<bool>;
