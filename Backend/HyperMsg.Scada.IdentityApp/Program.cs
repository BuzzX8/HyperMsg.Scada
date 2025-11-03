using HyperMsg.Scada.IdentityApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var services = builder.Services;

services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));
services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapIdentityApi<IdentityUser>();
}

app.UseAuthentication();
app.UseHttpsRedirection();

await ApplyMigrationsIfNeeded(app);
await SeedTestAdminUser(app);

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

static async Task SeedTestAdminUser(WebApplication app)
{
    if (!app.Environment.IsDevelopment())
    {
        return;
    }

    using var scope = app.Services.CreateScope();

    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    string adminEmail = "admin@mail.com";
    string adminPassword = "Admin123!"; // for dev/test only!
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        adminUser = new()
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var createResult = await userManager.CreateAsync(adminUser, adminPassword);

        if (!createResult.Succeeded)
        {
            throw new Exception("Failed to create admin user: " + string.Join(", ", createResult.Errors.Select(e => e.Description)));
        }
    }
}