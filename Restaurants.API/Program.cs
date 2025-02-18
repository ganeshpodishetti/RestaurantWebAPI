using Restaurants.API.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Exception Handler
builder.Services.AddScoped<ExceptionHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, _) =>
    {
        document.Info = new()
        {
            Title = "Restaurants Catalog API",
            Version = "v1",
            Description = "Modern API for managing restaurant catalogs.",
            // Contact = new ()
            // {
            //     Email = "admin@admin.com",
            //     Name = "Admin",
            //     Url = new ("https://api.example.com/support")
            // }
        };
        return Task.CompletedTask;
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

// app builder
var app = builder.Build();

// creating scope for seeding data into tables if tables has no data.
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.SeedAsync();

// Exception Handling Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();

// Serilog middleware
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Scalar API middleware config
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Restaurant API")
            .WithTheme(ScalarTheme.Mars)
            .WithDarkMode(true)
            .WithSidebar(true)
            .DefaultHttpClient = new (ScalarTarget.Http, ScalarClient.Http11);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();