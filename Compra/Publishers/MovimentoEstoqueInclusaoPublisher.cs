using Compra.Models;
using Compra.Repositories;
using Infraestrutura.Messager;
using Infraestrutura.Publishers;
using RabbitMQ.Client;

namespace Compra.Publishers
{
    public class MovimentoEstoqueInclusaoPublisher : BasicPublisher<MovimentoEstoqueInclusaoEvento>
    {
        private readonly EventoRepository _eventoRepository;

        public MovimentoEstoqueInclusaoPublisher(IMessageBroker messageBroker,
                                                 EventoRepository eventoRepository) : base(messageBroker)
        {
            exchange = "MovimentoEstoqueInclusao";

            _eventoRepository = eventoRepository;
        }

        public override void CriarQueue()
        {
            _channel.ExchangeDeclare(exchange, ExchangeType.Direct, false, false);
            _channel.QueueDeclare("MovimentoEstoqueInclusao", false, false, false);
            _channel.QueueBind("MovimentoEstoqueInclusao", exchange, "");
        }

        public override void RegistrarEvento(string message)
        {
            Evento _evento = new Evento()
            {
                Message = message,
                Exchange = "MovimentoEstoqueInclusao"
            };

            _eventoRepository.Incluir(_evento);
        }
    }
}