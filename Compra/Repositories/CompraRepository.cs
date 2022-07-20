using Dapper;
using Infraestrutura.Database;

namespace Compra.Repositories
{
    public class CompraRepository
    {
        private readonly ConnectionManager _connectionManager;

        public CompraRepository(ConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public int Incluir(Compra.Models.Compra _compra)
        {
            var connection = _connectionManager.GetConnection();

            int id = connection.Execute(@"INSERT INTO Compra (IdProduto,
                                                                  Quantidade) 
                                              VALUES (@IdProduto,
                                                      @Quantidade)",
                                         param: _compra);

            _connectionManager.CloseIfNotPersistent();

            return id;
        }

        public void Alterar(Compra.Models.Compra _compra)
        {
            var connection = _connectionManager.GetConnection();

            connection.Execute(@"UPDATE Compra
                                     SET IdCompra   = @IdCompra,
                                         IdProduto  = @IdProduto,
                                         QUantidade = @Quantidade
                                     WHERE IdCompra = @IdCompra",
                                param: _compra);

            _connectionManager.CloseIfNotPersistent();
        }

        public void Excluir(int IdCompra)
        {
            var connection = _connectionManager.GetConnection();

            connection.Execute(@"DELETE FROM Compra WHERE IdCompra = @IdCompra",
                                param: new { IdCompra });


            _connectionManager.CloseIfNotPersistent();
        }

        public Compra.Models.Compra Selecionar(int IdCompra)
        {
            var connection = _connectionManager.GetConnection();

            Compra.Models.Compra _compra = connection.QueryFirstOrDefault<Compra.Models.Compra>(@"SELECT *
                                                                                                      FROM Compra
                                                                                                      WHERE IdCompra = @IdCompra",
                                                                                                   param: new { IdCompra });


            _connectionManager.CloseIfNotPersistent();

            return _compra;

        }

        public IEnumerable<Compra.Models.Compra> SelecionarTodos()
        {
            var connection = _connectionManager.GetConnection();

            var _lstCompra = connection.Query<Compra.Models.Compra>(@"SELECT *
                                                                      FROM Compra");

            _connectionManager.CloseIfNotPersistent();

            return _lstCompra;
        }

    }
}