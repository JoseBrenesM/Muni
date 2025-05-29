using Abstracciones.Modelos.Persona;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.API
{
    public interface IPersonaAPI
    {
        public Task<IActionResult> Obtener([FromRoute] Guid Id);
        public Task<IActionResult> ObtenerTodos();
        public Task<IActionResult> Agregar([FromBody] PersonaPost persona);
        public Task<IActionResult> Editar([FromBody] Persona persona);
        public Task<IActionResult> Eliminar([FromRoute] Guid Id);
        public Task<IActionResult> Activar([FromRoute] Guid Id);
    }
}