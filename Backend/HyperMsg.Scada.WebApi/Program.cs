using HyperMsg.Messaging;
using HyperMsg.Scada.DataAccess;
using HyperMsg.Scada.Shared.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

AddServices(builder.Services, builder.Configuration);

var app = builder.Build();

ConfigureApplication(app);

await SeedTestAdminUser(app);

app.Run();

static void AddServices(IServiceCollection services, IConfiguration configuration)
{
    var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    //services.AddDbContext<UserContext>(options => options.UseSqlite(connectionString));
    //services.AddIdentityApiEndpoints<IdentityUser>()
    //    .AddEntityFrameworkStores<UserContext>();

    // Add services to the container.
    services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddMessagingContext();

    services.AddDataComponent();
    services.AddDataAccessRepositories(options =>
    {
        options.UseSqlite(connectionString);
    });

    services.AddAuthorization(options =>
    {
        foreach (var permission in Permissions.AllUsers)
        {
            options.AddPolicy(permission, policy => policy.RequireClaim(Permissions.ClaimType, permission));
        }
    });
}

static void ConfigureApplication(WebApplication app)
{
    // Configure the HTTP request pipeline.5
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapIdentityApi<IdentityUser>();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    //app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}

static async Task SeedTestAdminUser(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        using var scope = app.Services.CreateScope();

        var services = scope.ServiceProvider;
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        string adminEmail = "admin@mail.com";
        string adminPassword = "Admin123!"; // for dev/test only!
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(adminUser, adminPassword);

            if (createResult.Succeeded)
            {
                // 3. Add high-level claims for flexibility
                await userManager.AddClaimAsync(adminUser, Permissions.Claim(Permissions.Users.View));
                await userManager.AddClaimAsync(adminUser, Permissions.Claim(Permissions.Users.Create));
            }
        }
    }
}