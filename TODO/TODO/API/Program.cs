using API.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

GET/api/tarefas
app.MapGet("api/tarefas", ([FromServices] AppDataContext ctx) => 
{
    var tarefas = ctx.tarefas.include (t => t.status).toList();
    results.Ok(tarefa): results.NotFound();
});

GET/api/tarefas/{id}
app.MapGet("api/tarefas/{id}",([FromRoute]string id, [FromServices] AppDataContext ctx)=>
{
    var tarefa = ctx.tarefa.find(id);
    return tarefa == null ?
    results.NotFound();
    results.Ok(tarefa);
});


app.Run();
