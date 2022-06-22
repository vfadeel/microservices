

using System.Data;
using RabbitMQ.Client;

namespace Compra.Infrastructure
{
    public interface IMessageBroker
    {
       IConnection  GetConnection();
    }
}