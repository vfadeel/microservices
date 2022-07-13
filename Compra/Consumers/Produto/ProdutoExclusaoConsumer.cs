using System.Text;
using System.Text.Json;
using Compra.Models;
using Compra.Repositories;
using Infraestrutura.Consumers;
using RabbitMQ.Client;

namespace Compra.Consumers
{
    public class ProdutoExclusaoConsumer : BasicConsumer
    {
        private const string exchange = "ProdutoExclusao";
        private const string queue = "ProdutoExclusaoCompra";
        private readonly ProdutoRepository _produtoRepository;
        private readonly EventoRepository _eventoRepository;

        public ProdutoExclusaoConsumer(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                _produtoRepository = scope.ServiceProvider.GetRequiredService<ProdutoRepository>();
                _eventoRepository = scope.ServiceProvider.GetRequiredService<EventoRepository>();
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

            _eventoRepository.Incluir(new Evento()
            {
                Message = Mensagem,
                Exchange = exchange,
                Tipo = "Consumer",
                Operacao = "Exclusao"
            });
        }
    }
}