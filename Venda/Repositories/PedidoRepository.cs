using Dapper;
using Infraestrutura.Database;
using Venda.Models;

namespace Venda.Repositories
{
    public class PedidoRepository
    {
        private readonly IDatabase _database;

        public PedidoRepository(IDatabase database)
        {
            _database = database;

        }

        public int Incluir(Pedido _compra)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                int id = connection.Execute(@"INSERT INTO Pedido (IdProduto,
                                                                  Quantidade) 
                                              VALUES (@IdProduto,
                                                      @Quantidade);
                                              SELECT last_insert_rowid()",
                                             param: _compra);

                return id;

            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public void Alterar(Pedido _compra)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                connection.Execute(@"UPDATE Pedido
                                     SET IdPedido   = @IdPedido,
                                         IdProduto  = @IdProduto,
                                         QUantidade = @Quantidade
                                     WHERE IdPedido = @IdPedido",
                                    param: _compra);

            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public void Excluir(int IdPedido)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                connection.Execute(@"DELETE FROM Pedido WHERE IdPedido = @IdPedido",
                                    param: new { IdPedido });

            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public Pedido Selecionar(int IdPedido)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                Pedido _compra = connection.QueryFirstOrDefault<Pedido>(@"SELECT *
                                                                                                      FROM Pedido
                                                                                                      WHERE IdPedido = @IdPedido",
                                                                                                       param: new { IdPedido });

                return _compra;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public IEnumerable<Pedido> SelecionarTodos()
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                var _lstPedido = connection.Query<Pedido>(@"SELECT *
                                                                          FROM Pedido");

                return _lstPedido;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

    }
}