using Estoque.Infrastructure;
using Estoque.Models;
using Estoque.Publishers;
using RabbitMQ.Client;

namespace Estoque.Publishers
{
    public class ProdutoAlteracaoPublisher : BasicPublisher<Produto>
    {
        public ProdutoAlteracaoPublisher(IMessageBroker messageBroker) : base(messageBroker)
        {
            base.exchange = "ProdutoAlteracao";
        }

        public override void CriarQueue()
        {
            base._channel.ExchangeDeclare(exchange: base.exchange, type: ExchangeType.Fanout);
        }
    }
}