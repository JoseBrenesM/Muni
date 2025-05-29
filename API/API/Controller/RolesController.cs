using Abstracciones.API;
using Abstracciones.BW;
using Abstracciones.Modelos.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase, IRolesAPI
    {
        private IRolesBW _rolesBW;

        public RolesController(IRolesBW rolesBW)
        {
            _rolesBW = rolesBW;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] RolesPost rol)
        {
            var resultado = await _rolesBW.Agregar(rol);

            return CreatedAtAction(nameof(ObtenerTodos), new { Id = resultado });
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] RolesPost rol)
        {
            return Ok(await _rolesBW.Editar(rol));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int Id)
        {
            try
            {
                await _rolesBW.Eliminar(Id);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Ok(await _rolesBW.ObtenerTodos());
        }
    }
}
