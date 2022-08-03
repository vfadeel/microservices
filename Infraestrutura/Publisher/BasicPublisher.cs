using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Configuration;
using Infraestrutura.Messager;

namespace Infraestrutura.Publishers
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

            this.RegistrarEvento(mensagem);

            var body = Encoding.UTF8.GetBytes(mensagem);

            _channel.BasicPublish(exchange: exchange,
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
        }

        public abstract void RegistrarEvento(string message);

        public abstract void CriarQueue();

    }
}