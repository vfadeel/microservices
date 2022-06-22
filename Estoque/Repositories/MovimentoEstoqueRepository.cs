using Dapper;
using Estoque.Infrastructure;
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
                int id = connection.Execute(@"INSERT INTO MovimentoEstoque (IdProduto,
                                                                            Quantidade,
                                                                            Tipo) 
                                              VALUES (@IdProduto,
                                                      @Quantidade,
                                                      @Tipo)",
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
            catch (Exception e)
            {
                connection.Close();
                throw e;
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
            catch (Exception e)
            {
                connection.Close();
                throw e;
            }
        }

    }
}