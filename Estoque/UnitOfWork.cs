using Estoque.Repositories;
using Infraestrutura.Database;

namespace Estoque.Database
{
    public class UnitOfWork
    {
        private readonly IDatabase _database;
        private readonly IServiceProvider _services;

        public UnitOfWork(IDatabase database,
                          IServiceProvider services)
        {
            _database = database;
            _services = services;

        }

        public T? GetRepository<T>()
        {
            return _services.GetService<T>();
        }

    }
}