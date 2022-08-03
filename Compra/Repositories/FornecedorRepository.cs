using Dapper;
using Compra.Models;
using Infraestrutura.Database;

namespace Compra.Repositories
{
    public class FornecedorRepository
    {
        private readonly ConnectionManager _connectionManager;

        public FornecedorRepository(ConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public int Incluir(Fornecedor _fornecedor)
        {
            var connection = _connectionManager.GetConnection();

            int id = connection.Execute(@"INSERT INTO Fornecedor (Nome,
                                                                      Endereco) 
                                              VALUES (@Nome,
                                                      @Endereco)",
                                         param: _fornecedor);

            _connectionManager.CloseIfNotPersistent();

            return id;

        }

        public void Alterar(Fornecedor _fornecedor)
        {
            var connection = _connectionManager.GetConnection();

            connection.Execute(@"UPDATE Fornecedor
                                     SET IdFornecedor = @IdFornecedor,
                                         Nome         = @Nome,
                                         Endereco     = @Endereco
                                     WHERE IdFornecedor = @IdFornecedor",
                                param: _fornecedor);

            _connectionManager.CloseIfNotPersistent();

        }

        public void Excluir(int IdFornecedor)
        {
            var connection = _connectionManager.GetConnection();

            connection.Execute(@"DELETE FROM Fornecedor WHERE IdFornecedor = @IdFornecedor",
                                param: new { IdFornecedor });


            _connectionManager.CloseIfNotPersistent();

        }

        public Fornecedor Selecionar(int IdFornecedor)
        {

            var connection = _connectionManager.GetConnection();

            Fornecedor _fornecedor = connection.QueryFirstOrDefault<Fornecedor>(@"SELECT *
                                                                                      FROM Fornecedor
                                                                                      WHERE IdFornecedor = @IdFornecedor",
                                                                                   param: new { IdFornecedor });


            _connectionManager.CloseIfNotPersistent();


            return _fornecedor;

        }

        public IEnumerable<Fornecedor> SelecionarTodos()
        {
            var connection = _connectionManager.GetConnection();

            var _lstFornecedor = connection.Query<Fornecedor>(@"SELECT *
                                                                    FROM Fornecedor");


            _connectionManager.CloseIfNotPersistent();

            return _lstFornecedor;
        }

    }
}