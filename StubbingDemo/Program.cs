using Microsoft.EntityFrameworkCore;
using StubbingDemo.Database.Models;
using StubbingDemo.Repositories;
using StubbingDemo.Services;
var builder = WebApplication.CreateBuilder(args);
// Add middleware services
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Configuration
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables();
builder.Services.AddDbContext<NorthwindContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Northwind")));
builder.Services.AddScoped<IShipperService, ShipperService>();
builder.Services.AddScoped<IShipperRepository, ShipperRepository>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();