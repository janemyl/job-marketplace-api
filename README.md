# Marketplace API

A .NET 10.0 ASP.NET Core REST API for a services marketplace platform with customer job posting, contractor bidding, and job offer management.

## Architecture

### Layers

1. **Marketplace.Core** - Business logic, data access, CQRS pattern
   - Entities: Customer, Contractor, Job, JobOffer
   - Services: Validation, caching (5-min TTL)
   - Repository Pattern: Generic + Specialized repositories
   - CQRS: Commands (Create/Update/Delete), Queries (Read with pagination)
   - DTOs: Data transfer objects for API contracts

2. **Marketplace.Api** - HTTP endpoints, middleware, dependency injection
   - Controllers: REST endpoints for all 4 entities
   - Middleware: Global error handling
   - Dependency Injection: Service registration in Program.cs
   - Swagger: OpenAPI documentation

3. **Marketplace.Testing** - Unit tests with Moq
   - Repository mocking tests

### Technology Stack

- **Runtime**: .NET 10.0
- **Language**: C# 10+
- **Database**: PostgreSQL 15 (docker-compose)
- **ORM**: Entity Framework Core 10.0.7 with Npgsql provider
- **Patterns**: Repository, Dependency Injection, CQRS, Service Layer
- **Caching**: IMemoryCache (5-minute TTL, auto-invalidation)
- **Command Dispatch**: MediatR (command/query pattern)
- **Audit Logging**: Automatic tracking of Create/Update/Delete operations
- **Documentation**: Swagger/OpenAPI with XML documentation
- **Testing**: xUnit, Moq

## Getting Started

### Prerequisites

- .NET 10.0 SDK
- Docker & Docker Compose
- PostgreSQL 15 (or use docker-compose)

### Implementation Phases (Completed)

✅ **Phase 5**: IMemoryCache with 5-minute TTL and auto-invalidation on mutations
✅ **Phase 6**: Pagination with page/pageSize parameters, filtering, and sorting support
✅ **Phase 7**: Audit Logging system tracking all Create/Update/Delete operations
✅ **Phase 8**: CQRS pattern with MediatR dispatcher for all entities
✅ **Phase 9**: Swagger/OpenAPI with XML documentation and endpoint grouping

### Setup

1. **Start PostgreSQL**
   ```bash
   docker-compose up -d
   ```

2. **Build**
   ```bash
   dotnet build
   ```

3. **Run migrations**
   ```bash
   dotnet run --project Marketplace.Api
   ```
   (Migrations run automatically on startup)

4. **Access API**
   - API: `http://localhost:5181`
   - Swagger: `http://localhost:5181/swagger`

### Test

```bash
dotnet test Marketplace.Testing
```

## Project Structure

```
Marketplace.Api/
├── Controllers/          # REST endpoints (GET, POST, PUT, DELETE)
├── Middleware/          # Global error handling
├── Program.cs           # Dependency injection & middleware setup
├── appsettings.json     # Configuration
└── Properties/
    └── launchSettings.json

Marketplace.Core/
├── Entities/            # Domain models (Customer, Contractor, Job, JobOffer)
├── Data/                # DbContext, Migrations
├── Repositories/        # Generic & Specialized data access
├── Services/            # Business logic, validation, caching
├── Commands/            # CQRS write operations
├── Queries/             # CQRS read operations
├── Handlers/            # Command & query handlers
├── Dtos/                # Data transfer objects
└── Repositories/

Marketplace.Testing/
└── Tests/               # Unit tests with Moq
```

## API Endpoints

### Customers
- `GET /api/customers` - Get all (paginated)
- `GET /api/customers/{id}` - Get by ID
- `GET /api/customers/search?lastName=...` - Search by last name
- `POST /api/customers` - Create
- `PUT /api/customers/{id}` - Update
- `DELETE /api/customers/{id}` - Delete

### Contractors
- `GET /api/contractors` - Get all (paginated)
- `GET /api/contractors/{id}` - Get by ID
- `GET /api/contractors/search?name=...` - Search by name
- `POST /api/contractors` - Create
- `PUT /api/contractors/{id}` - Update
- `DELETE /api/contractors/{id}` - Delete

### Jobs
- `GET /api/jobs` - Get all (paginated)
- `GET /api/jobs/{id}` - Get by ID
- `GET /api/jobs/customer/{customerId}` - Get by customer
- `POST /api/jobs` - Create
- `PUT /api/jobs/{id}` - Update
- `DELETE /api/jobs/{id}` - Delete

### Job Offers
- `GET /api/joboffers` - Get all (paginated)
- `GET /api/joboffers/{id}` - Get by ID
- `GET /api/joboffers/job/{jobId}` - Get by job
- `POST /api/joboffers` - Create
- `PUT /api/joboffers/{id}` - Update
- `DELETE /api/joboffers/{id}` - Delete

## Design Patterns

### Repository Pattern
- `IRepository<T>` - Generic repository with CRUD operations
- Specialized repositories extend with domain-specific queries
- All methods async with `ToListAsync()` for efficiency

### Service Layer
- Business logic separation
- Input validation
- Caching orchestration
- Transaction management

### CQRS (Command Query Responsibility Segregation)
- Commands: Mutating operations (Create, Update, Delete)
- Queries: Read operations with optional pagination
- MediatR: Dispatches commands/queries to handlers
- Enables caching at query level, separate read/write logic

### Dependency Injection
- Constructor injection via IServiceProvider
- Scoped services for request lifetime
- Automatic handler discovery with MediatR

### Caching
- 5-minute TTL for read operations
- Auto-invalidation on mutations
- Reduces database load for repeated queries

## Configuration

### Connection String
**appsettings.json** or **appsettings.Development.json**:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=MarketplaceDb;Username=postgres;Password=Password123!"
  }
}
```

### Database
- **Host**: localhost
- **Port**: 5432
- **Database**: MarketplaceDb
- **User**: postgres
- **Password**: Password123!

## Error Handling

Global middleware catches exceptions:
- `ArgumentException` → HTTP 400 (Bad Request)
- All others → HTTP 500 (Internal Server Error)

Logged to console with full details.

## Database

### Entities
1. **Customer**
   - Fields: Id, FirstName, LastName, CreatedAt
   - Index: LastName (for search optimization)

2. **Contractor**
   - Fields: Id, Name, Rating, CreatedAt
   - Index: Name (for search optimization)

3. **Job**
   - Fields: Id, CustomerId, StartDate, DueDate, Budget, Description, AcceptedJobOfferId, CreatedAt
   - Foreign Key: CustomerId → Customer.Id

4. **JobOffer**
   - Fields: Id, JobId, ContractorId, Price, CreatedAt
   - Foreign Keys: JobId → Job.Id, ContractorId → Contractor.Id

5. **AuditLog**
   - Fields: Id, EntityType, EntityId, Action, UserId, Timestamp, ChangesJson
   - Purpose: Track all Create/Update/Delete operations for compliance and debugging

### Migrations
Automatic on startup via EF Core. Manual migration:
```bash
dotnet ef migrations add MigrationName --project Marketplace.Core
dotnet ef database update --project Marketplace.Core
```

## Performance Considerations

1. **B-Tree Indices** on CustomerRepository.LastName, ContractorRepository.Name for O(log N) search
2. **Caching** with 5-minute TTL reduces database load
3. **Async/Await** prevents thread starvation on I/O
4. **Pagination** limits result sets to prevent memory bloat
5. **Connection Pooling** via Npgsql for efficient database connections

## Future Enhancements

- Advanced sorting and filtering
- Authentication & Authorization
- Rate limiting
- Distributed caching (Redis)
- Message queue (RabbitMQ) for async job processing
- UI Dashboard (React, Vue, Blazor)
