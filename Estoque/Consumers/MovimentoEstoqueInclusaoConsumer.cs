using System.Text.Json;
using Infraestrutura.Consumers;
using RabbitMQ.Client;
using Estoque.Models;
using Estoque.Repositories;

public class MovimentoEstoqueInclusaoConsumer : BasicConsumer
{
    private const string queue = "MovimentoEstoqueInclusao";
    private readonly MovimentoEstoqueRepository _movimentoEstoqueRepository;
    private readonly EventoRepository _eventoRepository;

    public MovimentoEstoqueInclusaoConsumer(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
    {
        using (var scope = serviceScopeFactory.CreateScope())
        {
            _movimentoEstoqueRepository = scope.ServiceProvider.GetRequiredService<MovimentoEstoqueRepository>();
            _eventoRepository = scope.ServiceProvider.GetRequiredService<EventoRepository>();
        }
    }

    public override string CriarQueue()
    {

        _channel.ExchangeDeclare("MovimentoEstoqueInclusao", ExchangeType.Direct, false, false);
        _channel.QueueDeclare("MovimentoEstoqueInclusao", false, false, false);
        _channel.QueueBind("MovimentoEstoqueInclusao", "MovimentoEstoqueInclusao", "");

        return queue;
    }

    public override void ProcessarMensagem(string Mensagem)
    {
        MovimentoEstoqueInclusaoEvento? _movimentoEstoqueInclusaoEvento = JsonSerializer.Deserialize<MovimentoEstoqueInclusaoEvento>(Mensagem);

        _movimentoEstoqueRepository.Incluir(new MovimentoEstoque()
        {
            IdProduto = _movimentoEstoqueInclusaoEvento.IdProduto,
            Quantidade = _movimentoEstoqueInclusaoEvento.Quantidade,
            Tipo = _movimentoEstoqueInclusaoEvento.Tipo
        });

        _eventoRepository.Incluir(new Evento()
        {
            Message = JsonSerializer.Serialize(_movimentoEstoqueInclusaoEvento),
            Exchange = queue,
            Tipo = "Consumer",
            Operacao = "Alteracao"
        });
    }
}