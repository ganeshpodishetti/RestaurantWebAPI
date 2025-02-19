using Restaurants.API.Extensions;
using Restaurants.API.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// app builder
var app = builder.Build();

// creating scope for seeding data into tables if tables has no data.
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
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
            .WithDefaultHttpClient(ScalarTarget.Http, ScalarClient.Http11)
            // .WithHttpBearerAuthentication(bearer =>
            // {
            //     bearer.Token = app.Configuration["Bearer:Token"];
            // });
            .Authentication = new ScalarAuthenticationOptions
            {
                PreferredSecurityScheme = "Bearer"
            };
    });
}

app.UseHttpsRedirection();

app.UseRouting();

// app.UseAuthentication(); invoked by calling MapIdentityApi<User>
app.MapGroup("api/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();