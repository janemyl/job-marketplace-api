using System.Threading.Tasks;

namespace Marketplace.Core.Services;

public interface IAuditService
{
    Task LogAsync(string entityType, Guid? entityId, string action, string userId, object changes);
}
