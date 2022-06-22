using Estoque.Infrastructure;
using Estoque.Models;
using Estoque.Publishers;
using RabbitMQ.Client;

namespace Estoque.Publishers
{
    public class ProdutoExclusaoPublisher : BasicPublisher<int>
    {
        public ProdutoExclusaoPublisher(IMessageBroker messageBroker) : base(messageBroker)
        {
            base.exchange = "ProdutoExclusao";
        }

        public override void CriarQueue()
        {
            base._channel.ExchangeDeclare(exchange: base.exchange, type: ExchangeType.Fanout);
        }
    }
}