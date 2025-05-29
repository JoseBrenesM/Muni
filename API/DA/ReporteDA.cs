using Abstracciones.DA;
using Abstracciones.Modelos;
using Dapper;
using System.Data;

using Microsoft.Data.SqlClient;
namespace DA
{
    public class ReporteDA : IReporteDA
    {
        private readonly IRepositorioDapper _repositorioDapper;

        public ReporteDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
        }

        public async Task<List<HorarioDetalle>> ObtenerReporteHorario(FiltroReporteHorario filtro)
        {
            // Usar el repositorio para obtener la conexión
            using var conexion = _repositorioDapper.ObtenerRepositorioDapper();
            var parametros = new DynamicParameters();

            // Agregar parámetros de filtro
            parametros.Add("@FechaInicio", filtro.FechaInicio);
            parametros.Add("@FechaFin", filtro.FechaFin);
            parametros.Add("@IdsPersona", string.Join(",", filtro.PersonasIds));

            var resultado = await conexion.QueryAsync<HorarioDetalle>(
                "GetHorariosFiltrados",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return resultado.ToList();
        }
    }
}

