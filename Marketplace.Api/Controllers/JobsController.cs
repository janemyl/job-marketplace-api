using MediatR;
using Marketplace.Core.Commands;
using Marketplace.Core.Dtos;
using Marketplace.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers;

/// <summary>Manages job operations in the marketplace.</summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Jobs")]
public class JobsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>Initializes a new instance of the <see cref="JobsController"/> class.</summary>
    /// <param name="mediator">The MediatR mediator instance.</param>
    public JobsController(IMediator mediator) => _mediator = mediator;

    /// <summary>Get a job by ID.</summary>
    /// <param name="id">Job ID</param>
    /// <returns>Job details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JobDto>> GetById(Guid id) =>
        Ok(await _mediator.Send(new GetJobQuery(id)));

    /// <summary>Get all jobs with pagination.</summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="pageSize">Page size (default: 10)</param>
    /// <returns>Paginated list of jobs</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<JobDto>>> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10) =>
        Ok(await _mediator.Send(new GetAllJobsQuery(page, pageSize)));

    /// <summary>Create a new job.</summary>
    /// <param name="dto">Job creation details</param>
    /// <returns>Created job</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JobDto>> Create([FromBody] CreateJobDto dto)
    {
        var result = await _mediator.Send(
            new CreateJobCommand(dto.CustomerId, dto.StartDate, dto.DueDate, dto.Budget, dto.Description));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>Update an existing job.</summary>
    /// <param name="id">Job ID</param>
    /// <param name="dto">Updated job details</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateJobDto dto)
    {
        await _mediator.Send(new UpdateJobCommand(id, dto.StartDate, dto.DueDate, dto.Budget, dto.Description));
        return NoContent();
    }

    /// <summary>Delete a job.</summary>
    /// <param name="id">Job ID</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteJobCommand(id));
        return NoContent();
    }
}
