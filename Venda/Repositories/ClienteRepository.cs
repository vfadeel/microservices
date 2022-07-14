using Dapper;
using Infraestrutura.Database;
using Venda.Models;

namespace Venda.Repositories
{
    public class ClienteRepository
    {
        private readonly IDatabase _database;

        public ClienteRepository(IDatabase database)
        {
            _database = database;

        }

        public int Incluir(Cliente _cliente)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                int id = connection.Execute(@"INSERT INTO Cliente (Nome,
                                                                   Endereco) 
                                              VALUES (@Nome,
                                                      @Endereco);
                                              SELECT last_insert_rowid()",
                                             param: _cliente);

                return id;

            }
            catch 
            {
                connection.Close();
                throw;
            }
        }

        public void Alterar(Cliente _cliente)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                connection.Execute(@"UPDATE Cliente
                                     SET IdCliente = @IdCliente,
                                         Nome         = @Nome,
                                         Endereco     = @Endereco
                                     WHERE IdCliente = @IdCliente",
                                    param: _cliente);

            }
            catch 
            {
                connection.Close();
                throw;
            }
        }

        public void Excluir(int IdCliente)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                connection.Execute(@"DELETE FROM Cliente WHERE IdCliente = @IdCliente",
                                    param: new { IdCliente });

            }
            catch 
            {
                connection.Close();
                throw;
            }
        }

        public Cliente Selecionar(int IdCliente)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                Cliente _cliente = connection.QueryFirstOrDefault<Cliente>(@"SELECT *
                                                                             FROM Cliente
                                                                             WHERE IdCliente = @IdCliente",
                                                                              param: new { IdCliente });

                return _cliente;
            }
            catch 
            {
                connection.Close();
                throw;
            }
        }

        public IEnumerable<Cliente> SelecionarTodos()
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                var _lstCliente = connection.Query<Cliente>(@"SELECT *
                                                              FROM Cliente");

                return _lstCliente;
            }
            catch 
            {
                connection.Close();
                throw;
            }
        }

    }
}