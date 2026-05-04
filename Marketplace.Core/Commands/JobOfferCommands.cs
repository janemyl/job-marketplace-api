using MediatR;
using Marketplace.Core.Dtos;

namespace Marketplace.Core.Commands;

/// <summary>Command to create a new job offer.</summary>
public record CreateJobOfferCommand(Guid JobId, Guid ContractorId, decimal Price) : IRequest<JobOfferDto>;
/// <summary>Command to accept a job offer.</summary>
public record AcceptJobOfferCommand(Guid JobOfferId) : IRequest<JobOfferDto>;
/// <summary>Command to delete a job offer.</summary>
public record DeleteJobOfferCommand(Guid Id) : IRequest<bool>;
