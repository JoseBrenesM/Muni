using Abstracciones.Modelos;

namespace Abstracciones.DA
{
    public interface IReporteDA
    {
        Task<List<HorarioDetalle>> ObtenerReporteHorario(FiltroReporteHorario filtro);
    }
}
