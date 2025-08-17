using HyperMsg.Messaging;
using HyperMsg.Scada.DataAccess;
using HyperMsg.Scada.WebApi;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("HyperMsg Scada Web API", new OpenApiInfo
    {
        Title = "HyperMsg Scada API",
        Version = "v3",
        Description = "Web API for HyperMsg Scada"
    });
});
builder.Services.AddMessagingContext();

builder.Services.AddDataAccessRepositories();
builder.Services.AddDataComponent();
builder.Services.AddDbContext<DeviceContext>();
builder.Services.AddDbContext<DeviceTypeContext>();

var app = builder.Build();

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
