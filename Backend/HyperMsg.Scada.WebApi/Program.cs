using HyperMsg.Messaging;
using HyperMsg.Scada.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

AddServices(builder.Services, builder.Configuration);

var app = builder.Build();

ConfigureApplication(app);

app.Run();

static void AddServices(IServiceCollection services, IConfiguration configuration)
{
    var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    //var connectionString = configuration.GetConnectionString("LocalMsSql") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    services.AddDbContext<UserContext>(options => options.UseSqlite());
    services.AddIdentityApiEndpoints<IdentityUser>()
        .AddEntityFrameworkStores<UserContext>();

    // Add services to the container.
    services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddMessagingContext();

    services.AddDataComponent();
    services.AddDataAccessRepositories(options =>
    {
        options.UseSqlServer(connectionString);
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