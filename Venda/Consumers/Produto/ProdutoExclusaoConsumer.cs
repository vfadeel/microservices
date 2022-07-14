using System.Text.Json;
using Infraestrutura.Consumers;
using RabbitMQ.Client;
using Venda.Models;
using Venda.Repositories;

public class ProdutoExclusaoConsumer : BasicConsumer
{
    private const string exchange = "ProdutoExclusao";
    private const string queue = "ProdutoExclusaoVenda";
    private readonly ProdutoRepository _produtoRepository;
    private readonly EventoRepository _eventoRepository;

    public ProdutoExclusaoConsumer(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
    {
        using (var scope = serviceScopeFactory.CreateScope())
        {
            _produtoRepository = scope.ServiceProvider.GetRequiredService<ProdutoRepository>();
            _eventoRepository = scope.ServiceProvider.GetRequiredService<EventoRepository>();
        }
    }

    public override string CriarQueue()
    {
        _channel.ExchangeDeclare(exchange, ExchangeType.Fanout, false, false);
        _channel.QueueDeclare(queue, false, false, false);
        _channel.QueueBind(queue, exchange, "");

        return queue;
    }

    public override void ProcessarMensagem(string Mensagem)
    {
        ProdutoExclusaoEvento? _produtoExclusaoEvento = JsonSerializer.Deserialize<ProdutoExclusaoEvento>(Mensagem);

        _produtoRepository.Excluir(_produtoExclusaoEvento.IdProduto);

        _eventoRepository.Incluir(new Evento()
        {
            Message = JsonSerializer.Serialize(_produtoExclusaoEvento.IdProduto),
            Exchange = exchange,
            Tipo = "Consumer",
            Operacao = "Exclusao"
        });
    }
}