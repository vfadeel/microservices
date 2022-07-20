using System.Data;

namespace Infraestrutura.Database;

public class UnitOfWork
{
    private readonly ConnectionManager _connectionManager;
    private readonly IServiceProvider _services;
    private IDbTransaction _transaction;

    public UnitOfWork(ConnectionManager connectionManager,
                      IServiceProvider services)
    {
        _connectionManager = connectionManager;
        _services = services;
    }

    public void Start()
    {
        IDbConnection connection = _connectionManager.GetPersistentConnection();

        connection.Open();

        _transaction = connection.BeginTransaction();

    }

    public void Commit()
    {
        _transaction.Commit();
        _connectionManager.Close();
    }

    public void Rollback()
    {
        _transaction.Rollback();
        _connectionManager.Close();
    }

}