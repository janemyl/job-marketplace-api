using Marketplace.Core.Data;
using Marketplace.Core.Entities;
using System.Text.Json;

namespace Marketplace.Core.Services;

public class AuditService : IAuditService
{
    private readonly AppDbContext _db;

    public AuditService(AppDbContext db) => _db = db;

    public async Task LogAsync(string entityType, Guid? entityId, string action, string userId, object changes)
    {
        var entry = new AuditLog
        {
            Id = Guid.NewGuid(),
            EntityType = entityType,
            EntityId = entityId,
            Action = action,
            UserId = userId ?? "system",
            Timestamp = DateTime.UtcNow,
            ChangesJson = JsonSerializer.Serialize(changes)
        };
        _db.AuditLogs.Add(entry);
        await _db.SaveChangesAsync();
    }
}
