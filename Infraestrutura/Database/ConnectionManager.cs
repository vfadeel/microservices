using System.Data;

namespace Infraestrutura.Database
{
    public class ConnectionManager
    {
        private readonly IDatabase _database;
        private IDbConnection? _connection { get; set; }
        private bool IsPersistent { get; set; }

        public ConnectionManager(IDatabase database)
        {
            _database = database;
            IsPersistent = false;
        }

        public IDbConnection GetConnection()
        {
            if (_connection == null || _connection.State == ConnectionState.Closed)
                _connection = _database.GetConnection();

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

        public void Close()
        {
            _connection.Close();
        }

    }
}