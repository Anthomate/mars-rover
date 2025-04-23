using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    
    if (!context.Messages.Any())
    {
        context.Messages.Add(new Message { Text = "Hello World from PostgreSQL!", CreatedAt = DateTime.UtcNow });
        context.SaveChanges();
    }
}

app.Run();