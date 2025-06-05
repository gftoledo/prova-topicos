using API.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

// GET /api/status
app.MapGet("/api/status", ([FromServices] AppDataContext ctx) =>
{
    var status = ctx.Status.ToList();
    return status.Any() ? Results.Ok(status) : Results.NotFound();
});

// POST /api/status
app.MapPost("/api/status", ([FromBody] Status status, [FromServices] AppDataContext ctx) =>
{
    ctx.Status.Add(status);
    ctx.SaveChanges();
    return Results.Created($"/api/status/{status.Id}", status);
});

// PUT /api/status/{id}
app.MapPut("/api/status/{id}", ([FromRoute] int id, [FromBody] Status statusAlterado, [FromServices] AppDataContext ctx) =>
{
    var status = ctx.Status.Find(id);
    if (status == null) return Results.NotFound();

    status.Nome = statusAlterado.Nome;
    ctx.Status.Update(status);
    ctx.SaveChanges();
    return Results.Ok(status);
});



// GET /api/tarefas
app.MapGet("/api/tarefas", ([FromServices] AppDataContext ctx) =>
{
    var tarefas = ctx.Tarefas.Include(p => p.Status).ToList();
    return tarefas.Any() ? Results.Ok(tarefas) : Results.NotFound();
});

// GET /api/tarefas/{id}
app.MapGet("/api/tarefas/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    var tarefa = ctx.Tarefas.Find(id);
    return tarefa == null ? Results.NotFound() : Results.Ok(tarefa);
});

//POST/api/tarefa
app.MapPost("/api/tarefas", ([FromBody] Tarefa tarefa, [FromServices] AppDataContext ctx) =>
{
    var status = ctx.Status.Find(tarefa.StatusId);
    if (status == null) return Results.NotFound();

    tarefa.Status = status;
    ctx.Tarefas.Add(tarefa);
    ctx.SaveChanges();
    return Results.Created($"/api/tarefas/{tarefa.Id}", tarefa);
});

//PUT/api/tarefas/{id}
app.MapPut("/api/tarefas/{id}",([FromRoute]string id, [FromBody]Tarefa tarefaAlterado, [FromServices] AppDataContext ctx)=>
{
var tarefa = ctx.Tarefas.Find(id);
    if (tarefa == null) return Results.NotFound();

    var status = ctx.Status.Find(tarefaAlterado.StatusId);
    if (status == null) return Results.NotFound();

    tarefa.Titulo = tarefaAlterado.Titulo;
    tarefa.DataVencimento = tarefaAlterado.DataVencimento;
    tarefa.Status = status;

    ctx.Tarefas.Update(tarefa);
    ctx.SaveChanges();
    return Results.Ok(tarefa);

});

//DELETE/api/tarefa/{id}
app.MapDelete("/api/tarefas/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    var tarefa = ctx.Tarefas.Find(id);
    if (tarefa == null) return Results.NotFound();

    ctx.Tarefas.Remove(tarefa);
    ctx.SaveChanges();
    return Results.Ok(tarefa);
});

app.Run();

