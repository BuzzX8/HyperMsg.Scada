using HyperMsg.Scada.IdentityApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

AddApplicationServices(configuration, builder.Services, builder.Environment);

var app = builder.Build();

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

app.Run();

static void AddApplicationServices(IConfiguration configuration, IServiceCollection services, IWebHostEnvironment env)
{
    services.AddControllers();

    // Swagger registration
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "HyperMsg.Scada.IdentityApp",
            Version = "v1"
        });
    });

    services.AddDbContext<ApplicationDbContext>(options =>
    {
        var dbFileName = "hypermsg_identity.db";
        var dbPath = Path.Combine(AppContext.BaseDirectory, dbFileName);
        options.UseSqlite($"Data Source={dbPath}");
        options.UseOpenIddict();
    });

    services.AddOpenIddict()
        .AddCore(options =>
        {
            options.UseEntityFrameworkCore()
                   .UseDbContext<ApplicationDbContext>();
        });

    services.AddOpenIddict()
        .AddServer(options =>
        {
            options.SetTokenEndpointUris("connect/token");
            options.AllowClientCredentialsFlow();
            options.AddDevelopmentEncryptionCertificate()
                   .AddDevelopmentSigningCertificate();

            // Allow non-HTTPS only in Development for local testing.
            options.UseAspNetCore()
                   .EnableTokenEndpointPassthrough();

            if (env.IsDevelopment())
            {
                options.UseAspNetCore()
                       .DisableTransportSecurityRequirement();
            }
        });

    services.AddHostedService<BootstrapWorker>();
}