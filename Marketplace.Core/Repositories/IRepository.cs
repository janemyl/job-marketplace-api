namespace Marketplace.Core.Repositories;

/// <summary>Generic repository interface for data access operations.</summary>
/// <typeparam name="T">The entity type.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>Gets an entity by its ID asynchronously.</summary>
    /// <param name="id">The entity ID.</param>
    /// <returns>The entity if found; otherwise null.</returns>
    Task<T?> GetByIdAsync(Guid id);
    
    /// <summary>Gets all entities asynchronously.</summary>
    /// <returns>A collection of all entities.</returns>
    Task<IEnumerable<T>> GetAllAsync();
    
    /// <summary>Adds a new entity asynchronously.</summary>
    /// <param name="entity">The entity to add.</param>
    Task AddAsync(T entity);
    
    /// <summary>Updates an existing entity asynchronously.</summary>
    /// <param name="entity">The entity to update.</param>
    Task UpdateAsync(T entity);
    
    /// <summary>Deletes an entity by its ID asynchronously.</summary>
    /// <param name="id">The entity ID.</param>
    Task DeleteAsync(Guid id);
    
    /// <summary>Saves changes to the database asynchronously.</summary>
    Task SaveAsync();
}
