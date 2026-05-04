using System.Text.Json;

namespace Marketplace.Core.Entities;

/// <summary>Entity representing an audit log entry for tracking entity changes.</summary>
public class AuditLog
{
    /// <summary>Gets or sets the audit log entry ID.</summary>
    public Guid Id { get; set; }
    /// <summary>Gets or sets the type of entity being audited.</summary>
    public string? EntityType { get; set; }
    /// <summary>Gets or sets the ID of the entity being audited.</summary>
    public Guid? EntityId { get; set; }
    /// <summary>Gets or sets the action performed (Create, Update, Delete).</summary>
    public string? Action { get; set; }
    /// <summary>Gets or sets the user ID who performed the action.</summary>
    public string? UserId { get; set; }
    /// <summary>Gets or sets the timestamp of when the action occurred.</summary>
    public DateTime Timestamp { get; set; }
    /// <summary>Gets or sets the JSON representation of the changes made.</summary>
    public string? ChangesJson { get; set; }
}
