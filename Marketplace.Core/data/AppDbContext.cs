using Marketplace.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Core.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Contractor> Contractors { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<JobOffer> JobOffers { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.LastName);

        modelBuilder.Entity<Contractor>()
            .HasIndex(c => c.Name);

        modelBuilder.Entity<Job>()
            .HasOne<JobOffer>()
            .WithMany()
            .HasForeignKey(j => j.AcceptedJobOfferId)
            .IsRequired(false);
    }
}