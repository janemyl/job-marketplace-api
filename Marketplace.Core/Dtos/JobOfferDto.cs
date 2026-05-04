using System.ComponentModel.DataAnnotations;

namespace Marketplace.Core.Dtos;

/// <summary>Data transfer object for job offer information.</summary>
public class JobOfferDto
{
    /// <summary>Gets or sets the job offer ID.</summary>
    public Guid Id { get; set; }
    /// <summary>Gets or sets the job ID.</summary>
    public Guid JobId { get; set; }
    /// <summary>Gets or sets the contractor ID.</summary>
    public Guid ContractorId { get; set; }
    /// <summary>Gets or sets the offered price.</summary>
    public decimal Price { get; set; }
}

/// <summary>Data transfer object for creating a new job offer.</summary>
public class CreateJobOfferDto
{
    /// <summary>Gets or sets the job ID.</summary>
    [Required]
    public Guid JobId { get; set; }

    /// <summary>Gets or sets the contractor ID.</summary>
    [Required]
    public Guid ContractorId { get; set; }

    /// <summary>Gets or sets the offered price.</summary>
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
}

/// <summary>Data transfer object for updating an existing job offer.</summary>
public class UpdateJobOfferDto
{
    /// <summary>Gets or sets the offered price.</summary>
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
}
