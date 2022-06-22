

using System.Data;

namespace Compra.Infrastructure
{
    public interface IDatabase
    {
       IDbConnection GetConnection();
    }
}