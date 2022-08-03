using Dapper;
using Infraestrutura.Database;
using Estoque.Models;
using System.Data;

namespace Estoque.Repositories
{
    public class EventoRepository
    {
        private ConnectionManager _connectionManager;

        public EventoRepository(ConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public int Incluir(Evento _evento)
        {

            var connection = _connectionManager.GetConnection();

            int id = connection.QueryFirstOrDefault<int>(@"INSERT INTO Evento (Message,
                                                                  Exchange) 
                                                           VALUES (@Message,
                                                                   @Exchange);
                                                           SELECT last_insert_rowid()",
                                                      param: _evento);

            _connectionManager.CloseIfNotPersistent();

            return id;
        }

        public void Excluir(int IdEvento)
        {

            var connection = _connectionManager.GetConnection();

            connection.Execute(@"DELETE FROM Evento WHERE IdEvento = @IdEvento",
                                param: new { IdEvento });

            _connectionManager.CloseIfNotPersistent();

        }

        public Evento Selecionar(int IdEvento)
        {

            var connection = _connectionManager.GetConnection();

            Evento _evento = connection.QueryFirstOrDefault<Evento>(@"SELECT *
                                                                          FROM Evento
                                                                          WHERE IdEvento = @IdEvento",
                                                                     param: new { IdEvento });

            _connectionManager.CloseIfNotPersistent();

            return _evento;

        }

        public IEnumerable<Evento> SelecionarTodos()
        {
            var connection = _connectionManager.GetConnection();

            var _lstEvento = connection.Query<Evento>(@"SELECT *
                                                            FROM Evento");

            _connectionManager.CloseIfNotPersistent();

            return _lstEvento;
        }
    }
}