using Infraestrutura.Database;
using Infraestrutura.Messager;
using Venda.Publishers;
using Venda.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<ProdutoAlteracaoConsumer>();
builder.Services.AddHostedService<ProdutoInclusaoConsumer>();
builder.Services.AddHostedService<ProdutoExclusaoConsumer>();
builder.Services.AddScoped<IMessageBroker, RabbitMessageBroker>();
builder.Services.AddScoped<IDatabase, SQLiteDatabase>();
builder.Services.AddScoped<EventoRepository>();
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<PedidoRepository>();
builder.Services.AddScoped<ProdutoRepository>();

builder.Services.AddScoped<MovimentoEstoqueInclusaoPublisher>();

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
