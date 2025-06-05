using API.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();



//GET/api/tarefas
app.MapGet("/api/tarefas", ([FromServices] AppDataContext ctx) => 
{
    var tarefas = ctx.tarefas.include (t => t.status).toList();
    Results.Ok(tarefa): Results.NotFound();
});

//GET/api/tarefas/{id}
app.MapGet("/api/tarefas/{id}",([FromRoute]string id, [FromServices] AppDataContext ctx)=>
{
    var tarefa = ctx.tarefa.find(id);
    return tarefa == null ?
    Results.NotFound();
    Results.Ok(tarefa);
});

//POST/api/tarefa
app.MapPost("/api/tarefas", ([FromBody]Tarefa tarefa.[FromServices]AppDataContext ctx )=>
{
    var status = ctx.status.Find(tarefa.status id);
    if (status == null)return
    Results.NotFound();
    tarefa.status =status;
    ctx.tarefas.add(tarefas);
    ctx.SaveChanges();
    return result.Created($"/api/tarefas/{tarefa.id}".tarefa);

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
app.MapDelete("/api/tarefas/{id}", ([FromRoute]string id, [FromServices]AppDataContext ctx)=>
{
    var tarefa = ctx.tarefas.Find(id);
    if (tarefa == null) return Results.NotFound();
    ctx.tarefas.remove(tarefa);
    ctx.SaveChanges();
    return Results.Ok(tarefa);

});

app.Run();

