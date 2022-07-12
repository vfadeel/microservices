using Estoque.Models;
using Estoque.Repositories;
using Infraestrutura.Messager;
using Infraestrutura.Publishers;
using RabbitMQ.Client;

namespace Estoque.Publishers
{
    public class ProdutoExclusaoPublisher : BasicPublisher<int>
    {
        private readonly EventoRepository _eventoRepository;

        public ProdutoExclusaoPublisher(IMessageBroker messageBroker,
                                        EventoRepository eventoRepository) : base(messageBroker)
        {
            base.exchange = "ProdutoExclusao";
            
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