using MediatR;
using Marketplace.Core.Commands;
using Marketplace.Core.Dtos;
using Marketplace.Core.Entities;
using Marketplace.Core.Services;

namespace Marketplace.Core.Handlers;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
{
    private readonly CustomerService _service;
    private readonly IAuditService _audit;

    public CreateCustomerCommandHandler(CustomerService service, IAuditService audit) => (_service, _audit) = (service, audit);

    public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer { FirstName = request.FirstName, LastName = request.LastName };
        await _service.AddAsync(customer);
        await _audit.LogAsync(nameof(Customer), customer.Id, "Create", "system", customer);
        return new CustomerDto { Id = customer.Id, FirstName = customer.FirstName, LastName = customer.LastName };
    }
}

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
{
    private readonly CustomerService _service;
    private readonly IAuditService _audit;

    public UpdateCustomerCommandHandler(CustomerService service, IAuditService audit) => (_service, _audit) = (service, audit);

    public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _service.GetByIdAsync(request.Id);
        customer.FirstName = request.FirstName;
        customer.LastName = request.LastName;
        await _service.UpdateAsync(customer);
        await _audit.LogAsync(nameof(Customer), customer.Id, "Update", "system", customer);
        return new CustomerDto { Id = customer.Id, FirstName = customer.FirstName, LastName = customer.LastName };
    }
}

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
{
    private readonly CustomerService _service;
    private readonly IAuditService _audit;

    public DeleteCustomerCommandHandler(CustomerService service, IAuditService audit) => (_service, _audit) = (service, audit);

    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _service.GetByIdAsync(request.Id);
        await _service.DeleteAsync(request.Id);
        await _audit.LogAsync(nameof(Customer), request.Id, "Delete", "system", customer);
        return true;
    }
}
