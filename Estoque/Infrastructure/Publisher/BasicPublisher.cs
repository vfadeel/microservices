using System.Text;
using System.Text.Json;
using Estoque.Infrastructure;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Estoque.Publishers
{
    public abstract class BasicPublisher<T>
    {
        protected readonly IConnection _connection;
        protected readonly IModel _channel;
        protected string? exchange { get; set; }

        public BasicPublisher(IMessageBroker messageBroker)
        {
            _connection = messageBroker.GetConnection();
            _channel = _connection.CreateModel();
        }

        public void Publicar(T model)
        {
            CriarQueue();

            string mensagem = JsonSerializer.Serialize(model);

            var body = Encoding.UTF8.GetBytes(mensagem);

            _channel.BasicPublish(exchange: exchange,
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
        }

        public abstract void CriarQueue();

    }
}