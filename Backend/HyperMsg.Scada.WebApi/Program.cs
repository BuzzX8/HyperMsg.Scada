using HyperMsg.Messaging;
using HyperMsg.Scada.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;



var builder = WebApplication.CreateBuilder(args);

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var connectionString = builder.Configuration.GetConnectionString("LocalMsSql") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMessagingContext();

builder.Services.AddDataComponent();
builder.Services.AddDataAccessRepositories(options =>
{
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

var deviceContext = app.Services.GetRequiredService<IDbContextFactory<DeviceContext>>();

ApplyMigrations(deviceContext);

// Configure the HTTP request pipeline.5
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();

static void ApplyMigrations(IDbContextFactory<DeviceContext> deviceContextFactory)
{
    using var context = deviceContextFactory.CreateDbContext();    

    var pendingMigrations = context.Database.GetPendingMigrations().ToList();
}