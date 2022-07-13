using Compra.Models;
using Dapper;
using Infraestrutura.Database;

namespace Compra.Repositories
{
    public class EventoRepository
    {
        private readonly IDatabase _database;

        public EventoRepository(IDatabase database)
        {
            _database = database;

        }

        public int Incluir(Evento _evento)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                int id = connection.QueryFirstOrDefault<int>(@"INSERT INTO Evento (Message,
                                                                                   Exchange,
                                                                                   Tipo,
                                                                                   Operacao) 
                                              VALUES (@Message,
                                                      @Exchange,
                                                      @Tipo,
                                                      @Operacao);
                                              SELECT last_insert_rowid()",
                                             param: _evento);

                return id;

            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public void Excluir(int IdEvento)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                connection.Execute(@"DELETE FROM Evento WHERE IdEvento = @IdEvento",
                                    param: new { IdEvento });

            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public Evento Selecionar(int IdEvento)
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                Evento _evento = connection.QueryFirstOrDefault<Evento>(@"SELECT *
                                                                          FROM Evento
                                                                          WHERE IdEvento = @IdEvento",
                                                                         param: new { IdEvento });

                return _evento;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public IEnumerable<Evento> SelecionarTodos()
        {
            var connection = _database.GetConnection();

            connection.Open();

            try
            {
                var _lstEvento = connection.Query<Evento>(@"SELECT *
                                                            FROM Evento");

                return _lstEvento;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

    }
}