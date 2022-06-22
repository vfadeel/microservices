

using System.Data;

namespace Venda.Infrastructure
{
    public interface IDatabase
    {
       IDbConnection GetConnection();
    }
}