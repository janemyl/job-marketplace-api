using MediatR;
using Marketplace.Core.Commands;
using Marketplace.Core.Dtos;
using Marketplace.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers;

/// <summary>Manages job offer operations in the marketplace.</summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Job Offers")]
public class JobOffersController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>Initializes a new instance of the <see cref="JobOffersController"/> class.</summary>
    /// <param name="mediator">The MediatR mediator instance.</param>
    public JobOffersController(IMediator mediator) => _mediator = mediator;

    /// <summary>Get a job offer by ID.</summary>
    /// <param name="id">Job Offer ID</param>
    /// <returns>Job Offer details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JobOfferDto>> GetById(Guid id) =>
        Ok(await _mediator.Send(new GetJobOfferQuery(id)));

    /// <summary>Get all job offers with pagination.</summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="pageSize">Page size (default: 10)</param>
    /// <returns>Paginated list of job offers</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<JobOfferDto>>> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10) =>
        Ok(await _mediator.Send(new GetAllJobOffersQuery(page, pageSize)));

    /// <summary>Create a new job offer.</summary>
    /// <param name="dto">Job Offer creation details</param>
    /// <returns>Created job offer</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JobOfferDto>> Create([FromBody] CreateJobOfferDto dto)
    {
        var result = await _mediator.Send(new CreateJobOfferCommand(dto.JobId, dto.ContractorId, dto.Price));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>Delete a job offer.</summary>
    /// <param name="id">Job Offer ID</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteJobOfferCommand(id));
        return NoContent();
    }
}
