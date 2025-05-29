using Abstracciones.Modelos.Roles;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.API
{
    public interface IRolesAPI
    {
        public Task<IActionResult> ObtenerTodos();
        public Task<IActionResult> Agregar([FromBody] RolesPost rol);
        public Task<IActionResult> Editar([FromBody] RolesPost rol);
        public Task<IActionResult> Eliminar([FromRoute] int Id);
    }
}