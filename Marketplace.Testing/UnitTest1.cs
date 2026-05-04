using Marketplace.Core.Entities;
using Marketplace.Core.Repositories;
using Marketplace.Core.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace Marketplace.Testing;

public class CustomerServiceTests
{
    [Fact]
    public async Task SearchByLastName_WithValidPrefix_ReturnsMatches()
    {
        var mockRepo = new Mock<ICustomerRepository>();
        var testData = new[]
        {
            new Customer { Id = Guid.NewGuid(), FirstName = "John", LastName = "Smith" },
            new Customer { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe" },
            new Customer { Id = Guid.NewGuid(), FirstName = "Michael", LastName = "Smithson" }
        };

        mockRepo.Setup(r => r.SearchByLastNameAsync("Smi"))
            .ReturnsAsync(testData.Where(c => (c.LastName ?? "").StartsWith("Smi", StringComparison.OrdinalIgnoreCase)).ToList());

        var cache = new MemoryCache(new MemoryCacheOptions());
        var service = new CustomerService(mockRepo.Object, cache);
        var result = (await service.SearchByLastNameAsync("Smi")).ToList();

        Assert.Equal(2, result.Count);
        Assert.All(result, c => Assert.StartsWith("Smi", c.LastName));
    }

    [Fact]
    public async Task GetByIdAsync_WithNonexistentId_ThrowsArgumentException()
    {
        var mockRepo = new Mock<ICustomerRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Customer)null!);

        var cache = new MemoryCache(new MemoryCacheOptions());
        var service = new CustomerService(mockRepo.Object, cache);
        var testId = Guid.NewGuid();

        await Assert.ThrowsAsync<ArgumentException>(() => service.GetByIdAsync(testId));
    }

    [Fact]
    public async Task AddAsync_WithInvalidFirstName_ThrowsArgumentException()
    {
        var mockRepo = new Mock<ICustomerRepository>();
        var cache = new MemoryCache(new MemoryCacheOptions());
        var service = new CustomerService(mockRepo.Object, cache);
        var customer = new Customer { FirstName = "", LastName = "Doe" };

        await Assert.ThrowsAsync<ArgumentException>(() => service.AddAsync(customer));
    }
}
