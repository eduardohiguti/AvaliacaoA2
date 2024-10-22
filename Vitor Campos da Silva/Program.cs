using Microsoft.AspNetCore.Mvc;
using Vitor_Campos_da_Silva.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();

app.MapPost("/funcionario/cadastrar", async ([FromServices] AppDbContext context, [FromBody] Funcionario funcionario) =>
{
    context.Funcionarios.Add(funcionario);
    await context.SaveChangesAsync();
    return Results.Created($"/funcionario/{funcionario.Cpf}", funcionario);
});

app.MapGet("/funcionario/listar", async ([FromServices] AppDbContext context) =>
{
    var funcionarios = await context.Funcionarios.ToListAsync();
    return Results.Ok(funcionarios);
});

app.MapPost("/folha/cadastrar", async ([FromServices] AppDbContext context, [FromBody] Folha folha) =>
{
    context.Folhas.Add(folha);
    await context.SaveChangesAsync();
    return Results.Created($"/folha/{folha.FuncionarioId}", folha);
});

app.MapGet("/folha/listar", async ([FromServices] AppDbContext context) =>
{
    var folhas = await context.Folhas.ToListAsync();
    return Results.Ok(folhas);
});

app.MapGet("/folhar/buscar/{cpf}/{mes}/{ano}", async ([FromServices] AppDbContext context, string cpf, int mes, int ano) =>
{
    var funcionario = await context.Funcionarios.FirstOrDefaultAsync(f => f.Cpf == cpf);
    
    var folha = await context.Folhas
        .FirstOrDefaultAsync(f => f.FuncionarioId == funcionario.Id && f.Mes == mes && f.Ano == ano);

    return folha == null ? Results.NotFound() : Results.Ok(folha);
});

app.Run();