using HyperMsg.Messaging;
using HyperMsg.Scada.DataAccess;
using HyperMsg.Scada.Shared.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

AddServices(builder.Services, builder.Configuration);

var app = builder.Build();

ConfigureApplication(app);

app.Run();

static void AddServices(IServiceCollection services, IConfiguration configuration)
{
    var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    // Add services to the container.
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddMessagingContext();

    services.AddDataComponent();
    services.AddDataAccessRepositories(options =>
    {
        options.UseSqlite(connectionString);
    });

    // Authentication - validate JWTs issued by the Identity app
    var identityAuthority = configuration["Identity:Authority"];
    var identityAudience = configuration["Identity:Audience"];

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        // If you configure Authority (Identity app URL) the middleware will fetch signing keys via discovery.
        if (!string.IsNullOrEmpty(identityAuthority))
        {
            options.Authority = identityAuthority;      // e.g. "https://localhost:5001"
            options.Audience = identityAudience;        // e.g. "hypermsg-api"
            options.RequireHttpsMetadata = !string.Equals(configuration["Identity:AllowInsecureHttp"], "true", StringComparison.OrdinalIgnoreCase);
        }
        else
        {
            // Fallback: symmetric key from configuration (use only for dev/test)
            var key = configuration["Jwt:Key"] ?? throw new InvalidOperationException("No Identity:Authority or Jwt:Key configured.");
            options.RequireHttpsMetadata = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidateLifetime = true
            };
        }

        // Optional: helpful for debugging token issues in development
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = ctx =>
            {
                var logger = ctx.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("JwtAuth");
                logger.LogError(ctx.Exception, "Authentication failed");
                return Task.CompletedTask;
            }
        };
    });

    // Authorization policies (existing)
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
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    // IMPORTANT: Authentication before Authorization
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}