using Abstracciones.API;
using Abstracciones.BW;
using Abstracciones.Modelos.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Authorize(Roles = "1")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioxRolesController : ControllerBase, IRolesxUsuarioAPI
    {
        private IRolesxUsuarioBW _rolesxUsuarioBW;

        public UsuarioxRolesController(IRolesxUsuarioBW rolesxUsuarioBW)
        {
            _rolesxUsuarioBW = rolesxUsuarioBW;
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] RolesxUsuarioPost roles)
        {
            return Ok(await _rolesxUsuarioBW.Editar(roles));
        }

        [HttpDelete("{IdPersona}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid IdPersona)
        {
            try
            {
                await _rolesxUsuarioBW.Eliminar(IdPersona);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{IdPersona}")]
        public async Task<IActionResult> ObtenerTodos(Guid IdPersona)
        {
            return Ok(await _rolesxUsuarioBW.ObtenerTodos(IdPersona));
        }
    }
}