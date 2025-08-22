using HyperMsg.Messaging;
using HyperMsg.Scada.DataAccess;
using HyperMsg.Scada.WebApi;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMessagingContext();

builder.Services.AddDataComponent();
builder.Services.AddDataAccessRepositories(options => options.UseSqlServer(
            @"Server=(localdb)\mssqllocaldb;Database=HyperMsg.Scada;ConnectRetryCount=0"));

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
