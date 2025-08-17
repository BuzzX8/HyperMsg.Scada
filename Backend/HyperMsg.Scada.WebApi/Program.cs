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
    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Description = "HyperMsg Scada Web API",
        Title = "HyperMsg Scada WebApi",
        Version = "v2"
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
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "swagger/v2/swagger.json";
        options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "HyperMsg.Scada.WebApi v2");
        
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
