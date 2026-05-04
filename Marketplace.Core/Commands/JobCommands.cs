using MediatR;
using Marketplace.Core.Dtos;

namespace Marketplace.Core.Commands;

/// <summary>Command to create a new job.</summary>
public record CreateJobCommand(Guid CustomerId, DateTime StartDate, DateTime DueDate, decimal Budget, string? Description) : IRequest<JobDto>;
/// <summary>Command to update an existing job.</summary>
public record UpdateJobCommand(Guid Id, DateTime StartDate, DateTime DueDate, decimal Budget, string? Description) : IRequest<JobDto>;
/// <summary>Command to delete a job.</summary>
public record DeleteJobCommand(Guid Id) : IRequest<bool>;
