

using System.Data;
using RabbitMQ.Client;

namespace Estoque.Infrastructure
{
    public interface IMessageBroker
    {
       IConnection  GetConnection();
    }
}