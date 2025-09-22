using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Models;
//using TaskAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppContextDb>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// Repo pattern 
//builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users & Tasks API v1");
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
