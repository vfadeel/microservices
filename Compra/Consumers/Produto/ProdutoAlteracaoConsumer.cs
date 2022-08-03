using System.Text;
using System.Text.Json;
using Compra.Models;
using Compra.Repositories;
using Infraestrutura.Consumers;
using RabbitMQ.Client;

namespace Compra.Consumers
{
    public class ProdutoAlteracaoConsumer : BasicConsumer
    {
        private const string exchange = "ProdutoAlteracao";
        private const string queue = "ProdutoAlteracaoCompra";
        private readonly ProdutoRepository _produtoRepository;
        private readonly EventoRepository _eventoRepository;

        public ProdutoAlteracaoConsumer(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
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
            ProdutoAlteracaoEvento? _produtoAlteracaoEvento = JsonSerializer.Deserialize<ProdutoAlteracaoEvento>(Mensagem);

            _produtoRepository.Alterar(_produtoAlteracaoEvento.Produto);

            _eventoRepository.Incluir(new Evento(){
                Message = JsonSerializer.Serialize(_produtoAlteracaoEvento.Produto),
                Exchange = exchange,
                Tipo = "Consumer",
                Operacao = "Alteracao"
            });
        }
    }
}