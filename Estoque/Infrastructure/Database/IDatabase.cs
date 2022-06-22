

using System.Data;

namespace Estoque.Infrastructure
{
    public interface IDatabase
    {
       IDbConnection GetConnection();
    }
}