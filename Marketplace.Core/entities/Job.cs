namespace Marketplace.Core.Entities;

/// <summary>Entity representing a job posting in the marketplace.</summary>
public class Job
{
    /// <summary>Gets or sets the job ID.</summary>
    public Guid Id { get; set; }
    /// <summary>Gets or sets the customer ID who posted the job.</summary>
    public Guid CustomerId { get; set; }
    /// <summary>Gets or sets the customer who posted the job.</summary>
    public Customer? Customer { get; set; }
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
    /// <summary>Gets or sets the collection of job offers for this job.</summary>
    public ICollection<JobOffer> JobOffers { get; set; } = new List<JobOffer>();
}