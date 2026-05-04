using Marketplace.Api.Dtos;
using Marketplace.Core.Commands;
using Marketplace.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers;

/// <summary>Manages customer operations in the marketplace.</summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Customers")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>Initializes a new instance of the <see cref="CustomersController"/> class.</summary>
    /// <param name="mediator">The MediatR mediator instance.</param>
    public CustomersController(IMediator mediator) => _mediator = mediator;

    /// <summary>Get a customer by ID.</summary>
    /// <param name="id">Customer ID</param>
    /// <returns>Customer details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetCustomerQuery(id));
        return Ok(result);
    }

    /// <summary>Get all customers with pagination.</summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="pageSize">Page size (default: 10)</param>
    /// <returns>Paginated list of customers</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllCustomersQuery(page, pageSize));
        return Ok(result);
    }

    /// <summary>Search customers by last name.</summary>
    /// <param name="lastName">Last name to search for</param>
    /// <returns>Customers matching the search criteria</returns>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> SearchByLastName([FromQuery] string lastName)
    {
        var result = await _mediator.Send(new SearchCustomersByLastNameQuery(lastName));
        return Ok(result);
    }

    /// <summary>Create a new customer.</summary>
    /// <param name="dto">Customer creation details</param>
    /// <returns>Created customer</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerDto>> Create([FromBody] CreateCustomerDto dto)
    {
        var result = await _mediator.Send(new CreateCustomerCommand(dto.FirstName, dto.LastName));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>Update an existing customer.</summary>
    /// <param name="id">Customer ID</param>
    /// <param name="dto">Updated customer details</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerDto dto)
    {
        await _mediator.Send(new UpdateCustomerCommand(id, dto.FirstName, dto.LastName));
        return NoContent();
    }

    /// <summary>Delete a customer.</summary>
    /// <param name="id">Customer ID</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteCustomerCommand(id));
        return NoContent();
    }
}
