using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Modelos;

namespace BW
{
    public class HorarioBW : IHorarioBW
    {
        private readonly IHorarioDA _horarioDA;

        public HorarioBW(IHorarioDA horarioDA)
        {
            _horarioDA = horarioDA;
        }

        public async Task<Guid> MarcarEntrada(Guid idPersona)
        {
            return await _horarioDA.MarcarEntrada(idPersona);
        }

        public async Task<Guid> MarcarInicioAlmuerzo(Guid idPersona)
        {
            return await _horarioDA.MarcarInicioAlmuerzo(idPersona);
        }

        public async Task<Guid> MarcarFinAlmuerzo(Guid idPersona)
        {
            return await _horarioDA.MarcarFinAlmuerzo(idPersona);
        }

        public async Task<Guid> MarcarSalida(Guid idPersona)
        {
            return await _horarioDA.MarcarSalida(idPersona);
        }

        public async Task<Horario> ObtenerHorarioActual(Guid idPersona)
        {
            return await _horarioDA.ObtenerHorarioActual(idPersona);
        }

        public async Task<IEnumerable<HorarioDetalle>> ObtenerHorarios()
        {
            return await _horarioDA.ObtenerHorarios();
        }
        public async Task<IEnumerable<Horario>> ObtenerHorariosPorPersona(Guid idPersona)
        {
            return await _horarioDA.ObtenerHorariosPorPersona(idPersona);
        }

    }
}
