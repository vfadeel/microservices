using System.Data;
using Microsoft.Extensions.Configuration;

namespace Infraestrutura.Database
{
    public interface IDatabase
    {
       IDbConnection GetConnection();

    }
}