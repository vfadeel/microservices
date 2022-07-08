

using System.Data;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;

namespace Infraestrutura.Messager
{
    public interface IMessageBroker
    {
       IConnection  GetConnection();
    }
}