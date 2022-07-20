using Dapper;
using Infraestrutura.Database;
using Venda.Models;

namespace Venda.Repositories
{
    public class ClienteRepository
    {
        private readonly ConnectionManager _connectionManager;

        public ClienteRepository(ConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public int Incluir(Cliente _cliente)
        {
            var connection = _connectionManager.GetConnection();

            int id = connection.Execute(@"INSERT INTO Cliente (Nome,
                                                                   Endereco) 
                                              VALUES (@Nome,
                                                      @Endereco);
                                              SELECT last_insert_rowid()",
                                         param: _cliente);

            _connectionManager.CloseIfNotPersistent();

            return id;

        }

        public void Alterar(Cliente _cliente)
        {

            var connection = _connectionManager.GetConnection();

            connection.Execute(@"UPDATE Cliente
                                     SET IdCliente = @IdCliente,
                                         Nome         = @Nome,
                                         Endereco     = @Endereco
                                     WHERE IdCliente = @IdCliente",
                                param: _cliente);

            _connectionManager.CloseIfNotPersistent();
        }

        public void Excluir(int IdCliente)
        {

            var connection = _connectionManager.GetConnection();

            connection.Execute(@"DELETE FROM Cliente WHERE IdCliente = @IdCliente",
                                param: new { IdCliente });


            _connectionManager.CloseIfNotPersistent();
        }

        public Cliente Selecionar(int IdCliente)
        {

            var connection = _connectionManager.GetConnection();

            Cliente _cliente = connection.QueryFirstOrDefault<Cliente>(@"SELECT *
                                                                             FROM Cliente
                                                                             WHERE IdCliente = @IdCliente",
                                                                          param: new { IdCliente });


            _connectionManager.CloseIfNotPersistent();

            return _cliente;

        }

        public IEnumerable<Cliente> SelecionarTodos()
        {
            var connection = _connectionManager.GetConnection();

            var _lstCliente = connection.Query<Cliente>(@"SELECT *
                                                          FROM Cliente");


            _connectionManager.CloseIfNotPersistent();

            return _lstCliente;

        }

    }
}