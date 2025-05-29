using Abstracciones.DA;
using Abstracciones.Modelos;
using Dapper;
using Helpers;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class HorarioDA : IHorarioDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public HorarioDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<Guid> MarcarEntrada(Guid idPersona)
        {
            string sql = "MarcarEntrada";
            await _sqlConnection.ExecuteAsync(sql, new { IdPersona = idPersona }, commandType: System.Data.CommandType.StoredProcedure);
            return idPersona;
        }

        public async Task<Guid> MarcarInicioAlmuerzo(Guid idPersona)
        {
            string sql = "MarcarInicioAlmuerzo";
            await _sqlConnection.ExecuteAsync(sql, new { IdPersona = idPersona }, commandType: System.Data.CommandType.StoredProcedure);
            return idPersona;
        }

        public async Task<Guid> MarcarFinAlmuerzo(Guid idPersona)
        {
            string sql = "MarcarRegresoAlmuerzo";
            await _sqlConnection.ExecuteAsync(sql, new { IdPersona = idPersona }, commandType: System.Data.CommandType.StoredProcedure);
            return idPersona;
        }

        public async Task<Guid> MarcarSalida(Guid idPersona)
        {
            string sql = "MarcarFinalizacionJornada";
            await _sqlConnection.ExecuteAsync(sql, new { IdPersona = idPersona }, commandType: System.Data.CommandType.StoredProcedure);
            return idPersona;
        }

        public async Task<Horario> ObtenerHorarioActual(Guid idPersona)
        {
            string sql = "GetEstado";
            var resultado = await _sqlConnection.QueryFirstOrDefaultAsync<Horario>(sql, new { IdPersona = idPersona }, commandType: System.Data.CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<IEnumerable<HorarioDetalle>> ObtenerHorarios()
        {
            string sql = "GetHorarios";
            var resultado = await _sqlConnection.QueryAsync<HorarioDetalle>(sql, commandType: System.Data.CommandType.StoredProcedure);
            return resultado;
        }
        public async Task<IEnumerable<Horario>> ObtenerHorariosPorPersona(Guid idPersona)
        {
            string sql = "GetHorariosPorPersona";

            var resultado = await _sqlConnection.QueryAsync<Horario>(sql, new { IdPersona = idPersona }, commandType: System.Data.CommandType.StoredProcedure);

            return resultado;
        }


    }
}
