using Marketplace.Api.Middleware;
using Marketplace.Core.Data;
using Marketplace.Core.Repositories;
using Marketplace.Core.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
var xmlFile = Path.Combine(AppContext.BaseDirectory, "Marketplace.Api.xml");
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Marketplace API", Version = "v1", Description = "API for managing the job marketplace platform" });
    if (File.Exists(xmlFile))
        options.IncludeXmlComments(xmlFile);
});
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IContractorRepository, ContractorRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ContractorService>();
builder.Services.AddScoped<JobService>();
builder.Services.AddScoped<JobOfferService>();
builder.Services.AddScoped<IAuditService, AuditService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        await DataSeeder.SeedAsync(context);
    }
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
