using Dapper;
using Infraestrutura.Database;
using Estoque.Models;

namespace Estoque.Repositories
{
    public class MovimentoEstoqueRepository
    {
        private readonly IDatabase _database;

        public MovimentoEstoqueRepository(IDatabase database)
        {
            _database = database;

        }

        public int Incluir(MovimentoEstoque _movimentoEstoque)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                int id = connection.QueryFirstOrDefault<int>(@"INSERT INTO MovimentoEstoque (IdProduto,
                                                                            Quantidade,
                                                                            Tipo) 
                                              VALUES (@IdProduto,
                                                      @Quantidade,
                                                      @Tipo);
                                              SELECT last_insert_rowid()",
                                             param: _movimentoEstoque);

                return id;

            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public void Alterar(MovimentoEstoque _movimentoEstoque)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                connection.Execute(@"UPDATE MovimentoEstoque
                                     SET IdProduto  = @IdProduto,
                                         Quantidade = @Quantidade,
                                         Tipo       = @Tipo
                                     WHERE IdMovimentoEstoque = @IdMovimentoEstoque",
                                    param: _movimentoEstoque);

            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public void Excluir(int IdMovimentoEstoque)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                connection.Execute(@"DELETE FROM MovimentoEstoque WHERE IdMovimentoEstoque = @IdMovimentoEstoque",
                                    param: new { IdMovimentoEstoque });

            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public MovimentoEstoque Selecionar(int IdMovimentoEstoque)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                MovimentoEstoque _movimentoEstoque = connection.QueryFirstOrDefault<MovimentoEstoque>(@"SELECT *
                                                                                                        FROM MovimentoEstoque
                                                                                                        WHERE IdMovimentoEstoque = @IdMovimentoEstoque",
                                                                                                         param: new { IdMovimentoEstoque });

                return _movimentoEstoque;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public IEnumerable<MovimentoEstoque> SelecionarTodos()
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                var _lstMovimentoEstoque = connection.Query<MovimentoEstoque>(@"SELECT *
                                                                                FROM MovimentoEstoque");

                return _lstMovimentoEstoque;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

    }
}