using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.DA
{
    public interface IHorarioDA
    {
        Task<Guid> MarcarEntrada(Guid idPersona);
        Task<Guid> MarcarInicioAlmuerzo(Guid idPersona);
        Task<Guid> MarcarFinAlmuerzo(Guid idPersona);
        Task<Guid> MarcarSalida(Guid idPersona);
        Task<Horario> ObtenerHorarioActual(Guid idPersona);
        Task<IEnumerable<HorarioDetalle>> ObtenerHorarios();
        Task<IEnumerable<Horario>> ObtenerHorariosPorPersona(Guid idPersona);

    }
}
