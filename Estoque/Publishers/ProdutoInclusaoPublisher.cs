using Estoque.Models;
using RabbitMQ.Client;
using Infraestrutura.Publishers;
using Infraestrutura.Messager;
using Estoque.Repositories;

namespace Estoque.Publishers
{
    public class ProdutoInclusaoPublisher : BasicPublisher<ProdutoInclusaoEvento>
    {
        private readonly EventoRepository _eventoRepository;

        public ProdutoInclusaoPublisher(IMessageBroker messageBroker,
                                        EventoRepository eventoRepository) : base(messageBroker)
        {
            base.exchange = "ProdutoInclusao";
            
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