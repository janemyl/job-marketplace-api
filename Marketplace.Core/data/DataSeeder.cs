using Marketplace.Core.Entities;

namespace Marketplace.Core.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (context.Customers.Any()) return;

        var customers = new[]
        {
            new Customer { Id = Guid.NewGuid(), FirstName = "John", LastName = "Smith" },
            new Customer { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe" },
            new Customer { Id = Guid.NewGuid(), FirstName = "Michael", LastName = "Johnson" }
        };

        var contractors = new[]
        {
            new Contractor { Id = Guid.NewGuid(), Name = "BuildCorp", Rating = 4.5 },
            new Contractor { Id = Guid.NewGuid(), Name = "DevSolutions", Rating = 4.8 },
            new Contractor { Id = Guid.NewGuid(), Name = "TechExperts", Rating = 4.2 }
        };

        await context.Customers.AddRangeAsync(customers);
        await context.Contractors.AddRangeAsync(contractors);
        await context.SaveChangesAsync();

        var job = new Job
        {
            Id = Guid.NewGuid(),
            CustomerId = customers[0].Id,
            StartDate = DateTime.UtcNow.AddDays(1),
            DueDate = DateTime.UtcNow.AddDays(30),
            Budget = 5000,
            Description = "Build a modern web application"
        };

        await context.Jobs.AddAsync(job);
        await context.SaveChangesAsync();
    }
}
