using Microsoft.EntityFrameworkCore;
using TelefonApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Database connection
var connectionString = builder.Configuration.GetConnectionString("MyDatabase");
builder.Services.AddDbContext<DbTelefonContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
