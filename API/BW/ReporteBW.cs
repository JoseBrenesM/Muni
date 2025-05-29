using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Modelos;

namespace BW
{
    public class ReporteBW : IReporteBW
    {
        private readonly IReporteDA _reporteDA;

        public ReporteBW(IReporteDA reporteDA)
        {
            _reporteDA = reporteDA;
        }

        public async Task<List<HorarioDetalle>> ObtenerReporteHorario(FiltroReporteHorario filtro)
        {
            return await _reporteDA.ObtenerReporteHorario(filtro);
        }
    }
}
