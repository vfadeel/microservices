using Estoque.Models;
using RabbitMQ.Client;
using Infraestrutura.Messager;
using Infraestrutura.Publishers;
using Estoque.Repositories;
using System.Text.Json;

namespace Estoque.Publishers
{
    public class ProdutoAlteracaoPublisher : BasicPublisher<Produto>
    {
        private readonly EventoRepository _eventoRepository;

        public ProdutoAlteracaoPublisher(IMessageBroker messageBroker,
                                         EventoRepository eventoRepository) : base(messageBroker)
        {
            base.exchange = "ProdutoAlteracao";

            _eventoRepository = eventoRepository;
        }

        public override void CriarQueue()
        {
            base._channel.ExchangeDeclare(exchange: base.exchange, type: ExchangeType.Fanout);
        }

        public override void RegistrarEvento(string message)
        {
            Evento _evento = new Evento()
            {
                Message = message,
                Exchange = base.exchange
            };

            _eventoRepository.Incluir(_evento);
        }
    }
}