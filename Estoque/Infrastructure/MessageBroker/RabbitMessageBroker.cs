using RabbitMQ.Client;

namespace Estoque.Infrastructure
{
    public class RabbitMessageBroker : IMessageBroker
    {
        private readonly IConfiguration _configuration;

        public RabbitMessageBroker(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConnection GetConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();

            connectionFactory.HostName = _configuration.GetSection("MessageBroker:Host").Value;
            connectionFactory.Port =  Convert.ToInt32(_configuration.GetSection("MessageBroker:Port").Value);
            connectionFactory.UserName = _configuration.GetSection("MessageBroker:Username").Value;
            connectionFactory.Password = _configuration.GetSection("MessageBroker:Password").Value;

            return connectionFactory.CreateConnection();
        }
    }
}