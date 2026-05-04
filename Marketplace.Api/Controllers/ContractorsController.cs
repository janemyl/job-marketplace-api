using MediatR;
using Marketplace.Core.Commands;
using Marketplace.Core.Dtos;
using Marketplace.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers;

/// <summary>Manages contractor operations in the marketplace.</summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Contractors")]
public class ContractorsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>Initializes a new instance of the <see cref="ContractorsController"/> class.</summary>
    /// <param name="mediator">The MediatR mediator instance.</param>
    public ContractorsController(IMediator mediator) => _mediator = mediator;

    /// <summary>Get a contractor by ID.</summary>
    /// <param name="id">Contractor ID</param>
    /// <returns>Contractor details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContractorDto>> GetById(Guid id) =>
        Ok(await _mediator.Send(new GetContractorQuery(id)));

    /// <summary>Get all contractors with pagination.</summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="pageSize">Page size (default: 10)</param>
    /// <returns>Paginated list of contractors</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ContractorDto>>> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10) =>
        Ok(await _mediator.Send(new GetAllContractorsQuery(page, pageSize)));

    /// <summary>Search contractors by name.</summary>
    /// <param name="name">Name to search for</param>
    /// <returns>Contractors matching the search criteria</returns>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ContractorDto>>> SearchByName([FromQuery] string name) =>
        Ok(await _mediator.Send(new SearchContractorsByNameQuery(name)));

    /// <summary>Create a new contractor.</summary>
    /// <param name="dto">Contractor creation details</param>
    /// <returns>Created contractor</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContractorDto>> Create([FromBody] CreateContractorDto dto)
    {
        var result = await _mediator.Send(new CreateContractorCommand(dto.Name, dto.Rating));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>Update an existing contractor.</summary>
    /// <param name="id">Contractor ID</param>
    /// <param name="dto">Updated contractor details</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateContractorDto dto)
    {
        await _mediator.Send(new UpdateContractorCommand(id, dto.Name, dto.Rating));
        return NoContent();
    }

    /// <summary>Delete a contractor.</summary>
    /// <param name="id">Contractor ID</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteContractorCommand(id));
        return NoContent();
    }
}
