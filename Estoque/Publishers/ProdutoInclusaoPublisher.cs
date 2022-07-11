using Infraestrutura.Database;
using Estoque.Models;
using Estoque.Publishers;
using RabbitMQ.Client;

namespace Estoque.Publishers
{
    public class ProdutoInclusaoPublisher : BasicPublisher<Produto>
    {
        public ProdutoInclusaoPublisher(IMessageBroker messageBroker) : base(messageBroker)
        {
            base.exchange = "ProdutoInclusao";
        }

        public override void CriarQueue()
        {
            base._channel.ExchangeDeclare(exchange: base.exchange, type: ExchangeType.Fanout);
        }
    }
}