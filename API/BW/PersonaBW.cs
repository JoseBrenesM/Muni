using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Modelos.Persona;

namespace BW
{
    public class PersonaBW : IPersonaBW
    {
        private IPersonaDA _personaDA;

        public PersonaBW(IPersonaDA personaDA)
        {
            _personaDA = personaDA;
        }

        public async Task<bool> Activar(Guid Id)
        {
           return await _personaDA.Activar(Id);
        }

        public async Task<bool> Agregar(PersonaPost persona)
        {
            return await _personaDA.Agregar(persona);
        }

        public async Task<bool> Editar(Persona persona)
        {
            return await _personaDA.Editar(persona);
        }

        public Task<bool> Eliminar(Guid Id)
        {
            return _personaDA.Eliminar(Id);
        }

        public async Task<Persona> Obtener(Guid Id)
        {
            return await _personaDA.Obtener(Id);
        }

        public Task<IEnumerable<Persona>> ObtenerTodos()
        {
            return _personaDA.ObtenerTodos();
        }
    }
}