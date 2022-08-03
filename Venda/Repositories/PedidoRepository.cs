using Dapper;
using Infraestrutura.Database;
using Venda.Models;

namespace Venda.Repositories
{
    public class PedidoRepository
    {
        private readonly IDatabase _database;
        private readonly ConnectionManager _connectionManager;

        public PedidoRepository(IDatabase database,
                                ConnectionManager connectionManager)
        {
            _database = database;
            _connectionManager = connectionManager;
        }

        public int Incluir(Pedido _compra)
        {

            var connection = _connectionManager.GetConnection();

            int id = connection.Execute(@"INSERT INTO Pedido (IdProduto,
                                                                  Quantidade) 
                                              VALUES (@IdProduto,
                                                      @Quantidade);
                                              SELECT last_insert_rowid()",
                                         param: _compra);

            _connectionManager.CloseIfNotPersistent();

            return id;

        }

        public void Alterar(Pedido _compra)
        {

            var connection = _connectionManager.GetConnection();

            connection.Execute(@"UPDATE Pedido
                                     SET IdPedido   = @IdPedido,
                                         IdProduto  = @IdProduto,
                                         QUantidade = @Quantidade
                                     WHERE IdPedido = @IdPedido",
                                param: _compra);

            _connectionManager.CloseIfNotPersistent();

        }

        public void Excluir(int IdPedido)
        {

            var connection = _connectionManager.GetConnection();

            connection.Execute(@"DELETE FROM Pedido WHERE IdPedido = @IdPedido",
                                param: new { IdPedido });

            _connectionManager.CloseIfNotPersistent();

        }

        public Pedido Selecionar(int IdPedido)
        {

            var connection = _connectionManager.GetConnection();

            Pedido _compra = connection.QueryFirstOrDefault<Pedido>(@"SELECT *
                                                                      FROM Pedido
                                                                      WHERE IdPedido = @IdPedido",
                                                                   param: new { IdPedido });


            _connectionManager.CloseIfNotPersistent();

            return _compra;

        }

        public IEnumerable<Pedido> SelecionarTodos()
        {
            var connection = _connectionManager.GetConnection();

            var _lstPedido = connection.Query<Pedido>(@"SELECT *
                                                        FROM Pedido");

            _connectionManager.CloseIfNotPersistent();

            return _lstPedido;
        }

    }
}