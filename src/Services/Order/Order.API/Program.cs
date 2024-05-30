using Order.API;
using Order.Application;
using Order.Infrastructure;
using Order.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services
    .AddApplicationServices()
    .AddInfrastructureService(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

//Configure the HTTP request pipeline.
app.UseApiServices();


if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
     
}


app.Run();
