using API.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

GET/api/tarefasommi
app.MapGet("api/tarefas", ([FromServices] AppDataContext ctx) => 
{
    var tarefas = ctx.tarefas.include (t => t.status).toList();
    results.Ok(tarefa): results.NotFound();
});




app.Run();
