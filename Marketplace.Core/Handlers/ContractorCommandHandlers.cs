using MediatR;
using Marketplace.Core.Commands;
using Marketplace.Core.Dtos;
using Marketplace.Core.Entities;
using Marketplace.Core.Services;

namespace Marketplace.Core.Handlers;

public class CreateContractorCommandHandler : IRequestHandler<CreateContractorCommand, ContractorDto>
{
    private readonly ContractorService _service;
    private readonly IAuditService _audit;

    public CreateContractorCommandHandler(ContractorService service, IAuditService audit) => (_service, _audit) = (service, audit);

    public async Task<ContractorDto> Handle(CreateContractorCommand request, CancellationToken cancellationToken)
    {
        var contractor = new Contractor { Name = request.Name, Rating = request.Rating };
        await _service.AddAsync(contractor);
        await _audit.LogAsync(nameof(Contractor), contractor.Id, "Create", "system", contractor);
        return new ContractorDto { Id = contractor.Id, Name = contractor.Name, Rating = contractor.Rating };
    }
}

public class UpdateContractorCommandHandler : IRequestHandler<UpdateContractorCommand, ContractorDto>
{
    private readonly ContractorService _service;
    private readonly IAuditService _audit;

    public UpdateContractorCommandHandler(ContractorService service, IAuditService audit) => (_service, _audit) = (service, audit);

    public async Task<ContractorDto> Handle(UpdateContractorCommand request, CancellationToken cancellationToken)
    {
        var contractor = await _service.GetByIdAsync(request.Id);
        contractor.Name = request.Name;
        contractor.Rating = request.Rating;
        await _service.UpdateAsync(contractor);
        await _audit.LogAsync(nameof(Contractor), contractor.Id, "Update", "system", contractor);
        return new ContractorDto { Id = contractor.Id, Name = contractor.Name, Rating = contractor.Rating };
    }
}

public class DeleteContractorCommandHandler : IRequestHandler<DeleteContractorCommand, bool>
{
    private readonly ContractorService _service;
    private readonly IAuditService _audit;

    public DeleteContractorCommandHandler(ContractorService service, IAuditService audit) => (_service, _audit) = (service, audit);

    public async Task<bool> Handle(DeleteContractorCommand request, CancellationToken cancellationToken)
    {
        var contractor = await _service.GetByIdAsync(request.Id);
        await _service.DeleteAsync(request.Id);
        await _audit.LogAsync(nameof(Contractor), request.Id, "Delete", "system", contractor);
        return true;
    }
}
