using Dapper;
using Infraestrutura.Database;
using Estoque.Models;

namespace Estoque.Repositories
{
    public class ProdutoRepository
    {
        private readonly IDatabase _database;

        public ProdutoRepository(IDatabase database)
        {
            _database = database;

        }

        public int Incluir(Produto _produto)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                int id = connection.QueryFirstOrDefault<int>(@"INSERT INTO Produto (Nome, 
                                                                               Preco) 
                                                               VALUES (@Nome, 
                                                                       @Preco);
                                                               SELECT last_insert_rowid()",
                                             param: _produto);

                return id;

            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public void Alterar(Produto _produto)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                connection.Execute(@"UPDATE Produto
                                     SET Nome  = @Nome,
                                         Preco = @Preco
                                     WHERE IdProduto = @IdProduto",
                                    param: _produto);

            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public void Excluir(int IdProduto)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                connection.Execute(@"DELETE FROM Produto WHERE IdProduto = @IdProduto",
                                    param: new { IdProduto });

            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public Produto Selecionar(int IdProduto)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                Produto _produto = connection.QueryFirstOrDefault<Produto>(@"SELECT *
                                                                             FROM Produto
                                                                             WHERE IdProduto = @IdProduto",
                                                                              param: new { IdProduto });

                return _produto;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public IEnumerable<Produto> SelecionarTodos()
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                var _lstProduto = connection.Query<Produto>(@"SELECT *
                                                              FROM Produto");

                return _lstProduto;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

    }
}