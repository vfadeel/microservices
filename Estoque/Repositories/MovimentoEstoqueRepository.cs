using Dapper;
using Infraestrutura.Database;
using Estoque.Models;

namespace Estoque.Repositories
{
    public class MovimentoEstoqueRepository
    {
        private readonly ConnectionManager _connectionManager;

        public MovimentoEstoqueRepository(ConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public int Incluir(MovimentoEstoque _movimentoEstoque)
        {
            var connection = _connectionManager.GetConnection();

            int id = connection.QueryFirstOrDefault<int>(@"INSERT INTO MovimentoEstoque (IdProduto,
                                                                            Quantidade,
                                                                            Tipo) 
                                              VALUES (@IdProduto,
                                                      @Quantidade,
                                                      @Tipo);
                                              SELECT last_insert_rowid()",
                                         param: _movimentoEstoque);

            _connectionManager.CloseIfNotPersistent();

            return id;
        }

        public void Alterar(MovimentoEstoque _movimentoEstoque)
        {
            var connection = _connectionManager.GetConnection();

            connection.Execute(@"UPDATE MovimentoEstoque
                                     SET IdProduto  = @IdProduto,
                                         Quantidade = @Quantidade,
                                         Tipo       = @Tipo
                                     WHERE IdMovimentoEstoque = @IdMovimentoEstoque",
                                param: _movimentoEstoque);


            _connectionManager.CloseIfNotPersistent();
        }

        public void Excluir(int IdMovimentoEstoque)
        {
            var connection = _connectionManager.GetConnection();

            connection.Execute(@"DELETE FROM MovimentoEstoque WHERE IdMovimentoEstoque = @IdMovimentoEstoque",
                                param: new { IdMovimentoEstoque });

            _connectionManager.CloseIfNotPersistent();
        }

        public MovimentoEstoque Selecionar(int IdMovimentoEstoque)
        {
            var connection = _connectionManager.GetConnection();

            MovimentoEstoque _movimentoEstoque = connection.QueryFirstOrDefault<MovimentoEstoque>(@"SELECT *
                                                                                                    FROM MovimentoEstoque
                                                                                                    WHERE IdMovimentoEstoque = @IdMovimentoEstoque",
                                                                                                 param: new { IdMovimentoEstoque });

            _connectionManager.CloseIfNotPersistent();

            return _movimentoEstoque;

        }

        public IEnumerable<MovimentoEstoque> SelecionarTodos()
        {
            var connection = _connectionManager.GetConnection();

            var _lstMovimentoEstoque = connection.Query<MovimentoEstoque>(@"SELECT *
                                                                            FROM MovimentoEstoque");


            _connectionManager.CloseIfNotPersistent();

            return _lstMovimentoEstoque;
        }

    }
}