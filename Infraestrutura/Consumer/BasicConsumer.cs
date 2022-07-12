using System.Text;
using Infraestrutura.Messager;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infraestrutura.Consumers
{
    public abstract class BasicConsumer : IHostedService
    {
        protected readonly IConnection _connection;
        protected readonly IModel _channel;

        public BasicConsumer(IServiceScopeFactory serviceScopeFactory)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                IMessageBroker messageBroker = scope.ServiceProvider.GetRequiredService<IMessageBroker>();

                _connection = messageBroker.GetConnection();
                _channel = _connection.CreateModel();
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            var queueNome = CriarQueue();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {

                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    ProcessarMensagem(message);

                    _channel.BasicAck(ea.DeliveryTag, false);

                }catch
                {
                    _channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            _channel.BasicConsume(queue: queueNome,
                                  autoAck: false,
                                  consumer: consumer);

            return Task.CompletedTask;
        }

        public abstract string CriarQueue();
        public abstract void ProcessarMensagem(string Mensagem);

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}