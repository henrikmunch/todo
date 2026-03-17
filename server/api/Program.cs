using Microsoft.EntityFrameworkCore;
using efscaffold;
using efscaffold.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDbContext>(conf =>
{
    conf.UseNpgsql("Server=ep-nameless-bonus-aha0bgj9-pooler.c-3.us-east-1.aws.neon.tech;DB=neondb;UID=neondb_owner;PWD=npg_KB7xHRfntOF4;SslMode=require");
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
