using System.ComponentModel.DataAnnotations;

namespace Marketplace.Api.Dtos;

/// <summary>Data transfer object for job information.</summary>
public class JobDto
{
    /// <summary>Gets or sets the job ID.</summary>
    public Guid Id { get; set; }
    /// <summary>Gets or sets the job start date.</summary>
    public DateTime StartDate { get; set; }
    /// <summary>Gets or sets the job due date.</summary>
    public DateTime DueDate { get; set; }
    /// <summary>Gets or sets the job budget.</summary>
    public decimal Budget { get; set; }
    /// <summary>Gets or sets the job description.</summary>
    public string? Description { get; set; }
    /// <summary>Gets or sets the accepted job offer ID.</summary>
    public Guid? AcceptedJobOfferId { get; set; }
}

/// <summary>Data transfer object for creating a new job.</summary>
public class CreateJobDto
{
    /// <summary>Gets or sets the customer ID.</summary>
    [Required]
    public Guid CustomerId { get; set; }

    /// <summary>Gets or sets the job start date.</summary>
    [Required]
    public DateTime StartDate { get; set; }

    /// <summary>Gets or sets the job due date.</summary>
    [Required]
    public DateTime DueDate { get; set; }

    /// <summary>Gets or sets the job budget.</summary>
    [Range(0.01, double.MaxValue)]
    public decimal Budget { get; set; }

    /// <summary>Gets or sets the job description.</summary>
    [Required]
    [StringLength(500, MinimumLength = 10)]
    public string? Description { get; set; }
}

/// <summary>Data transfer object for updating an existing job.</summary>
public class UpdateJobDto
{
    /// <summary>Gets or sets the job start date.</summary>
    [Required]
    public DateTime StartDate { get; set; }

    /// <summary>Gets or sets the job due date.</summary>
    [Required]
    public DateTime DueDate { get; set; }

    /// <summary>Gets or sets the job budget.</summary>
    [Range(0.01, double.MaxValue)]
    public decimal Budget { get; set; }

    /// <summary>Gets or sets the job description.</summary>
    [Required]
    [StringLength(500, MinimumLength = 10)]
    public string? Description { get; set; }
}
