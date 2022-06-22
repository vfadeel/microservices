using Dapper;
using Compra.Infrastructure;

namespace Compra.Repositories
{
    public class CompraRepository
    {
        private readonly IDatabase _database;

        public CompraRepository(IDatabase database)
        {
            _database = database;

        }

        public int Incluir(Compra.Models.Compra _compra)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                int id = connection.Execute(@"INSERT INTO Compra (IdProduto,
                                                                  Quantidade) 
                                              VALUES (@IdProduto,
                                                      @Quantidade)",
                                             param: _compra);

                return id;

            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public void Alterar(Compra.Models.Compra _compra)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                connection.Execute(@"UPDATE Compra
                                     SET IdCompra   = @IdCompra,
                                         IdProduto  = @IdProduto,
                                         QUantidade = @Quantidade
                                     WHERE IdCompra = @IdCompra",
                                    param: _compra);

            }
            catch 
            {
                connection.Close();
                throw;
            }
        }

        public void Excluir(int IdCompra)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                connection.Execute(@"DELETE FROM Compra WHERE IdCompra = @IdCompra",
                                    param: new { IdCompra });

            }
            catch 
            {
                connection.Close();
                throw;
            }
        }

        public Compra.Models.Compra Selecionar(int IdCompra)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                Compra.Models.Compra _compra = connection.QueryFirstOrDefault<Compra.Models.Compra>(@"SELECT *
                                                                                                      FROM Compra
                                                                                                      WHERE IdCompra = @IdCompra",
                                                                                                       param: new { IdCompra });

                return _compra;
            }
            catch 
            {
                connection.Close();
                throw;
            }
        }

        public IEnumerable<Compra.Models.Compra> SelecionarTodos()
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                var _lstCompra = connection.Query<Compra.Models.Compra>(@"SELECT *
                                                                          FROM Compra");

                return _lstCompra;
            }
            catch 
            {
                connection.Close();
                throw;
            }
        }

    }
}