using System.Text;
using System.Text.Json;
using Compra.Infrastructure;
using Compra.Models;
using Compra.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Compra.Consumers
{
    public class ProdutoInclusaoConsumer : BasicConsumer
    {
        private const string exchange = "ProdutoInclusao";
        private const string queue = "ProdutoInclusaoCompra";
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoInclusaoConsumer(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                _produtoRepository = scope.ServiceProvider.GetRequiredService<ProdutoRepository>();
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
            Produto? _produto = JsonSerializer.Deserialize<Produto>(Mensagem);

            _produtoRepository.Incluir(_produto);
        }
    }
}