using DevelopmentFast.Infra.ProjectTest.DomainTest.Entities;
using DevelopmentFast.Infra.ProjectTest.DomainTest.Repositories;
using DevelopmentFast.Infra.ProjectTest.InfraTest.DataContext;
using DevelopmentFast.Infra.ProjectTest.InfraTest.Repositories;
using DevelopmentFast.Repository.Domain.Interfaces.Repository;
using DevelopmentFast.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using StackExchange.Redis.Profiling;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<Context>(opt => {
    opt.UseInMemoryDatabase("Database");
    });



builder.Services.AddSingleton<IDistributedCache, RedisCache>();
builder.Services.AddStackExchangeRedisCache(x => x.Configuration = "localhost:6379");
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IBaseRedisRepositoryDF, BaseRedisRepositoryDF>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //builder.Services.AddDistributedMemoryCache();

    using (var serviceScope = app.Services.CreateScope())
    {
        var db = serviceScope.ServiceProvider.GetRequiredService<Context>();        
        if (db.Database.EnsureCreated())
        {
            var list = new List<Courses>();
            var student = new Student("Abner");
            for (var i = 0; i < 1000; i++)
            {
                list.Add(new Courses("Cursos " + (i + 1)));
            }
            student.Courses = list;
            db.Student.Add(student);
            db.SaveChanges();
        }
    }
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
