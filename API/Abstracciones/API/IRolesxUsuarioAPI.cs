using Abstracciones.Modelos.Roles;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.API
{
    public interface IRolesxUsuarioAPI
    {
        public Task<IActionResult> ObtenerTodos(Guid IdPersona);
        public Task<IActionResult> Editar([FromBody] RolesxUsuarioPost roles);
        public Task<IActionResult> Eliminar([FromRoute] Guid IdPersona);
    }
}
