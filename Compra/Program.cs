using Compra.Consumers;
using Compra.Repositories;
using Infraestrutura.Database;
using Infraestrutura.Messager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDatabase, SQLiteDatabase>();
builder.Services.AddScoped<IMessageBroker, RabbitMessageBroker>();
builder.Services.AddScoped<FornecedorRepository>();
builder.Services.AddScoped<CompraRepository>();
builder.Services.AddScoped<ProdutoRepository>();

builder.Services.AddHostedService<ProdutoInclusaoConsumer>();
builder.Services.AddHostedService<ProdutoAlteracaoConsumer>();
builder.Services.AddHostedService<ProdutoExclusaoConsumer>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
