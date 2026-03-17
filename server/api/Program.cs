using Microsoft.EntityFrameworkCore;
using efscaffold;
using efscaffold.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDbContext>(conf =>
{
    conf.UseNpgsql("");
});

var app = builder.Build();

app.MapGet("/", (MyDbContext context) =>
{
    var Todo = new Todo()
    {
        Id = Guid.NewGuid().ToString(),
        Title = "Title",
        Description = "Description",
        Isdone = false,
        Priority = 5,
    };

    context.Todos.Add(Todo);
    context.SaveChanges();

    var objects = context.Todos.ToList();

    return objects;
});

app.Run();
