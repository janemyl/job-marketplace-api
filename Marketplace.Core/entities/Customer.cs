namespace Marketplace.Core.Entities;

/// <summary>Entity representing a customer in the marketplace.</summary>
public class Customer
{
    /// <summary>Gets or sets the customer ID.</summary>
    public Guid Id { get; set; }
    /// <summary>Gets or sets the customer's first name.</summary>
    public string? FirstName { get; set; }
    /// <summary>Gets or sets the customer's last name.</summary>
    public string? LastName { get; set; }
    /// <summary>Gets or sets the collection of jobs posted by the customer.</summary>
    public ICollection<Job> Jobs { get; set; } = new List<Job>();
}