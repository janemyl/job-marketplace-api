using Marketplace.Core.Entities;

namespace Marketplace.Core.Entities;

/// <summary>Entity representing a job offer from a contractor in the marketplace.</summary>
public class JobOffer
{
    /// <summary>Gets or sets the job offer ID.</summary>
    public Guid Id { get; set; }
    
    /// <summary>Gets or sets the job ID.</summary>
    public Guid JobId { get; set; }
    /// <summary>Gets or sets the job.</summary>
    public Job? Job { get; set; } 

    /// <summary>Gets or sets the contractor ID.</summary>
    public Guid ContractorId { get; set; }
    /// <summary>Gets or sets the contractor.</summary>
    public Contractor? Contractor { get; set; } 

    /// <summary>Gets or sets the offered price.</summary>
    public decimal Price { get; set; }
}