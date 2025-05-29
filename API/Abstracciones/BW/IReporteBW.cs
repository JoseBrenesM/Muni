using Abstracciones.Modelos;

namespace Abstracciones.BW
{
    public interface IReporteBW
    {
        Task<List<HorarioDetalle>> ObtenerReporteHorario(FiltroReporteHorario filtro);
    }
}
