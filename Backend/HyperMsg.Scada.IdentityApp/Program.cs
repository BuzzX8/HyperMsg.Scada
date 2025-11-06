using HyperMsg.Scada.IdentityApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var services = builder.Services;

services.AddControllers();
services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(connectionString);
    options.UseOpenIddict();
});

services.AddOpenIddict()
    // Register the OpenIddict core components.
    .AddCore(options =>
    {
        // Configure OpenIddict to use the Entity Framework Core stores and models.
        // Note: call ReplaceDefaultEntities() to replace the default entities.
        options.UseEntityFrameworkCore()
               .UseDbContext<ApplicationDbContext>();
    });

services.AddOpenIddict()
    // Register the OpenIddict server components.
    .AddServer(options =>
    {
        // Enable the token endpoint.
        options.SetTokenEndpointUris("connect/token");

        // Enable the client credentials flow.
        options.AllowClientCredentialsFlow();

        // Register the signing and encryption credentials.
        options.AddDevelopmentEncryptionCertificate()
               .AddDevelopmentSigningCertificate();

        // Register the ASP.NET Core host and configure the ASP.NET Core options.
        options.UseAspNetCore()
               .EnableTokenEndpointPassthrough();
    });

services.AddHostedService<Worker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapDefaultControllerRoute();

app.UseHttpsRedirection();

await ApplyMigrationsIfNeeded(app);

app.Run();

static async Task ApplyMigrationsIfNeeded(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("DbMigration");

    try
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
        var pending = await db.Database.GetPendingMigrationsAsync();

        if (pending.Any())
        {
            logger.LogInformation("Applying {Count} pending EF Core migrations...", pending.Count());
            await db.Database.MigrateAsync();
            logger.LogInformation("Migrations applied.");
        }
        else
        {
            logger.LogInformation("No pending EF Core migrations.");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while applying EF Core migrations.");
        throw;
    }
}