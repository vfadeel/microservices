using System.Text;
using System.Text.Json;
using Compra.Infrastructure;
using Compra.Models;
using Compra.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Compra.Consumers
{
    public class ProdutoExclusaoConsumer : BasicConsumer
    {
        private const string exchange = "ProdutoExclusao";
        private const string queue = "ProdutoExclusaoCompra";
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoExclusaoConsumer(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
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
            int IdProduto = JsonSerializer.Deserialize<int>(Mensagem);

            _produtoRepository.Excluir(IdProduto);
        }
    }
}