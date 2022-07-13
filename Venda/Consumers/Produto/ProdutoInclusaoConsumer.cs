using Infraestrutura.Consumers;
using Venda.Repositories;

public class ProdutoInclusaoConsumer : BasicConsumer
{

    private readonly ProdutoRepository _produtoRepository;
    private readonly EventoRepository _eventoRepository;

    public ProdutoInclusaoConsumer(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
    {
        using (var scope = serviceScopeFactory.CreateScope())
        {
            _produtoRepository = scope.ServiceProvider.GetRequiredService<ProdutoRepository>();
            _eventoRepository = scope.ServiceProvider.GetRequiredService<EventoRepository>();
        }
    }

    public override string CriarQueue()
    {
        base._channel.
    }

    public override void ProcessarMensagem(string Mensagem)
    {
        throw new NotImplementedException();
    }
}