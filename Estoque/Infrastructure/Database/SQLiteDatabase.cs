using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace Estoque.Infrastructure
{
    public class SQLiteDatabase : IDatabase
    {
        private readonly IConfiguration _configuration;

        public SQLiteDatabase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection()
        {
            string _dbFile = ObterArquivo();
            
            return new SQLiteConnection($"Data Source={_dbFile}; Version=3;");
        }

        private string ObterArquivo()
        {
            return _configuration.GetSection("DbFile").Value;
        }
        
    }
}