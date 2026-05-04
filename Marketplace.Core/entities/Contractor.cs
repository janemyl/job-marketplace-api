namespace Marketplace.Core.Entities;

/// <summary>Entity representing a contractor in the marketplace.</summary>
public class Contractor
{
    /// <summary>Gets or sets the contractor ID.</summary>
    public Guid Id { get; set; }
    /// <summary>Gets or sets the contractor's name.</summary>
    public string? Name { get; set; }
    /// <summary>Gets or sets the contractor's rating.</summary>
    public double Rating { get; set; }
    /// <summary>Gets or sets the collection of job offers made by the contractor.</summary>
    public ICollection<JobOffer> JobOffers { get; set; } = new List<JobOffer>();
}