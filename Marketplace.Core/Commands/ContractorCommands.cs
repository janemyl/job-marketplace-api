using MediatR;
using Marketplace.Core.Dtos;

namespace Marketplace.Core.Commands;

/// <summary>Command to create a new contractor.</summary>
public record CreateContractorCommand(string? Name, double Rating) : IRequest<ContractorDto>;
/// <summary>Command to update an existing contractor.</summary>
public record UpdateContractorCommand(Guid Id, string? Name, double Rating) : IRequest<ContractorDto>;
/// <summary>Command to delete a contractor.</summary>
public record DeleteContractorCommand(Guid Id) : IRequest<bool>;
