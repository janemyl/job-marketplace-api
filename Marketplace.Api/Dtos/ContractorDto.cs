using System.ComponentModel.DataAnnotations;

namespace Marketplace.Api.Dtos;

/// <summary>Data transfer object for contractor information.</summary>
public class ContractorDto
{
    /// <summary>Gets or sets the contractor ID.</summary>
    public Guid Id { get; set; }
    /// <summary>Gets or sets the contractor's name.</summary>
    public string? Name { get; set; }
    /// <summary>Gets or sets the contractor's rating.</summary>
    public double Rating { get; set; }
}

/// <summary>Data transfer object for creating a new contractor.</summary>
public class CreateContractorDto
{
    /// <summary>Gets or sets the contractor's name.</summary>
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string? Name { get; set; }

    /// <summary>Gets or sets the contractor's rating.</summary>
    [Range(0, 5)]
    public double Rating { get; set; }
}

/// <summary>Data transfer object for updating an existing contractor.</summary>
public class UpdateContractorDto
{
    /// <summary>Gets or sets the contractor's name.</summary>
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string? Name { get; set; }

    /// <summary>Gets or sets the contractor's rating.</summary>
    [Range(0, 5)]
    public double Rating { get; set; }
}
