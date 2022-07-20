using Estoque.Publishers;
using Estoque.Repositories;
using Infraestrutura.Database;
using Infraestrutura.Messager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ConnectionManager>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<IDatabase, SQLiteDatabase>();
builder.Services.AddScoped<IMessageBroker, RabbitMessageBroker>();
builder.Services.AddScoped<EventoRepository>();
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<MovimentoEstoqueRepository>();
builder.Services.AddScoped<ProdutoInclusaoPublisher>();
builder.Services.AddScoped<ProdutoAlteracaoPublisher>();
builder.Services.AddScoped<ProdutoExclusaoPublisher>();

builder.Services.AddHostedService<MovimentoEstoqueInclusaoConsumer>();

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
