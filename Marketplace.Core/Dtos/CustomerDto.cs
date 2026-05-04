using System.ComponentModel.DataAnnotations;

namespace Marketplace.Core.Dtos;

/// <summary>Data transfer object for customer information.</summary>
public class CustomerDto
{
    /// <summary>Gets or sets the customer ID.</summary>
    public Guid Id { get; set; }
    /// <summary>Gets or sets the customer's first name.</summary>
    public string? FirstName { get; set; }
    /// <summary>Gets or sets the customer's last name.</summary>
    public string? LastName { get; set; }
}

/// <summary>Data transfer object for creating a new customer.</summary>
public class CreateCustomerDto
{
    /// <summary>Gets or sets the customer's first name.</summary>
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string? FirstName { get; set; }

    /// <summary>Gets or sets the customer's last name.</summary>
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string? LastName { get; set; }
}

/// <summary>Data transfer object for updating an existing customer.</summary>
public class UpdateCustomerDto
{
    /// <summary>Gets or sets the customer's first name.</summary>
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string? FirstName { get; set; }

    /// <summary>Gets or sets the customer's last name.</summary>
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string? LastName { get; set; }
}
