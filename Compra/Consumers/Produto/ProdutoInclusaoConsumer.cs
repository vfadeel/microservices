using System.Text;
using System.Text.Json;
using Compra.Models;
using Compra.Repositories;
using Infraestrutura.Consumers;
using RabbitMQ.Client;

namespace Compra.Consumers
{
    public class ProdutoInclusaoConsumer : BasicConsumer
    {
        private const string exchange = "ProdutoInclusao";
        private const string queue = "ProdutoInclusaoCompra";
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
            _channel.ExchangeDeclare(exchange, ExchangeType.Fanout, false, false);
            _channel.QueueDeclare(queue, false, false, false);
            _channel.QueueBind(queue, exchange, "");

            return queue;
        }

        public override void ProcessarMensagem(string Mensagem)
        {
            ProdutoInclusaoEvento? _produtoInclusaoEvento = JsonSerializer.Deserialize<ProdutoInclusaoEvento>(Mensagem);

            _produtoRepository.Incluir(_produtoInclusaoEvento.Produto);

            _eventoRepository.Incluir(new Evento()
            {
                Message = JsonSerializer.Serialize(_produtoInclusaoEvento.Produto),
                Exchange = exchange,
                Tipo = "Consumer",
                Operacao = "Inclusao"
            });
        }
    }
}