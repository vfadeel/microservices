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

        public ProdutoAlteracaoConsumer(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
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

            _produtoRepository.Alterar(_produto);
        }
    }
}