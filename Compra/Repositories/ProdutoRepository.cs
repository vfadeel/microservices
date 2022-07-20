using Dapper;
using Compra.Models;
using Infraestrutura.Database;

namespace Compra.Repositories
{
    public class ProdutoRepository
    {
        private readonly ConnectionManager _connectionManager;

        public ProdutoRepository(ConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public int Incluir(Produto _produto)
        {
            var connection = _connectionManager.GetConnection();

            int id = connection.Execute(@"INSERT INTO Produto (IdProduto,
                                                                   Nome, 
                                                                   Preco) 
                                              VALUES (@IdProduto,
                                                      @Nome, 
                                                      @Preco)",
                                         param: _produto);

            _connectionManager.CloseIfNotPersistent();

            return id;

        }

        public void Alterar(Produto _produto)
        {

            var connection = _connectionManager.GetConnection();

            connection.Execute(@"UPDATE Produto
                                     SET Nome  = @Nome,
                                         Preco = @Preco
                                     WHERE IdProduto = @IdProduto",
                                param: _produto);


            _connectionManager.CloseIfNotPersistent();
        }

        public void Excluir(int IdProduto)
        {

            var connection = _connectionManager.GetConnection();

            connection.Execute(@"DELETE FROM Produto WHERE IdProduto = @IdProduto",
                                param: new { IdProduto });


            _connectionManager.CloseIfNotPersistent();
        }

        public Produto Selecionar(int IdProduto)
        {

            var connection = _connectionManager.GetConnection();

            Produto _produto = connection.QueryFirstOrDefault<Produto>(@"SELECT *
                                                                             FROM Produto
                                                                             WHERE IdProduto = @IdProduto",
                                                                          param: new { IdProduto });

            _connectionManager.CloseIfNotPersistent();

            return _produto;
        }

        public IEnumerable<Produto> SelecionarTodos()
        {
            var connection = _connectionManager.GetConnection();

            var _lstProduto = connection.Query<Produto>(@"SELECT *
                                                              FROM Produto");

            _connectionManager.CloseIfNotPersistent();

            return _lstProduto;
        }

    }
}