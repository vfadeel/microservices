using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using Microsoft.Extensions.Configuration;

namespace Infraestrutura.Database
{
    public class SQLiteDatabase : IDatabase
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _connection;
        private bool IsPersistent;

        public SQLiteDatabase(IConfiguration configuration)
        {
            _configuration = configuration;
            IsPersistent = false;
        }

        public IDbConnection GetConnection()
        {
            string _dbFile = ObterArquivo();

            _connection = new SQLiteConnection($"Data Source={_dbFile}; Version=3;");

            return _connection;
        }

        public IDbConnection GetPersistentConnection()
        {
            IsPersistent = true;

            return GetConnection();
        }

        public void CloseIfNotPersistent()
        {
            if (!IsPersistent)
                _connection.Close();
        }

        private string ObterArquivo()
        {
            return _configuration.GetSection("DbFile").Value;
        }

    }
}