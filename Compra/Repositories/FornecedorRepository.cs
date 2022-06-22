using Dapper;
using Compra.Infrastructure;
using Compra.Models;

namespace Compra.Repositories
{
    public class FornecedorRepository
    {
        private readonly IDatabase _database;

        public FornecedorRepository(IDatabase database)
        {
            _database = database;

        }

        public int Incluir(Fornecedor _fornecedor)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                int id = connection.Execute(@"INSERT INTO Fornecedor (Nome,
                                                                      Endereco) 
                                              VALUES (@Nome,
                                                      @Endereco)",
                                             param: _fornecedor);

                return id;

            }
            catch 
            {
                connection.Close();
                throw;
            }
        }

        public void Alterar(Fornecedor _fornecedor)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                connection.Execute(@"UPDATE Fornecedor
                                     SET IdFornecedor = @IdFornecedor,
                                         Nome         = @Nome,
                                         Endereco     = @Endereco
                                     WHERE IdFornecedor = @IdFornecedor",
                                    param: _fornecedor);

            }
            catch 
            {
                connection.Close();
                throw;
            }
        }

        public void Excluir(int IdFornecedor)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                connection.Execute(@"DELETE FROM Fornecedor WHERE IdFornecedor = @IdFornecedor",
                                    param: new { IdFornecedor });

            }
            catch 
            {
                connection.Close();
                throw;
            }
        }

        public Fornecedor Selecionar(int IdFornecedor)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                Fornecedor _fornecedor = connection.QueryFirstOrDefault<Fornecedor>(@"SELECT *
                                                                                      FROM Fornecedor
                                                                                      WHERE IdFornecedor = @IdFornecedor",
                                                                                       param: new { IdFornecedor });

                return _fornecedor;
            }
            catch 
            {
                connection.Close();
                throw;
            }
        }

        public IEnumerable<Fornecedor> SelecionarTodos()
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                var _lstFornecedor = connection.Query<Fornecedor>(@"SELECT *
                                                                    FROM Fornecedor");

                return _lstFornecedor;
            }
            catch 
            {
                connection.Close();
                throw;
            }
        }

    }
}