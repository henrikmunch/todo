using api;
using efscaffold;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var appOptions = builder.Services.AddAppOptions(builder.Configuration);

builder.Services.AddDbContext<MyDbContext>(conf =>
{
    conf.UseNpgsql(appOptions.DbConnectionString);
});

var app = builder.Build();

app.MapGet("/", ([FromServices] IOptionsMonitor<AppOptions> optionsMonitor, [FromServices] MyDbContext context) =>
{
    var Todo = new efscaffold.Entities.Todo()
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
