using Abstracciones.API;
using Abstracciones.BW;
using Abstracciones.Modelos.Persona;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase, IPersonaAPI
    {
        private IPersonaBW _personaBW;

        public PersonaController(IPersonaBW personaBW)
        {
            _personaBW = personaBW;
        }
        [HttpPut("Activar/{Id}")]
        public async Task<IActionResult> Activar([FromRoute] Guid Id)
        {
            return Ok(await _personaBW.Activar(Id));
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] PersonaPost persona)
        {
            var resultado = await _personaBW.Agregar(persona);

            if (!resultado)
                return NoContent();

            return CreatedAtAction(nameof(ObtenerTodos), null);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Persona persona)
        {
            var existePersona = await _personaBW.Obtener(persona.Id);

            if (existePersona == null)
                return NotFound();

            return Ok(await _personaBW.Editar(persona));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid Id)
        {
            return  Ok(await _personaBW.Eliminar(Id));

            
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid Id)
        {
            return Ok(await _personaBW.Obtener(Id));
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Ok(await _personaBW.ObtenerTodos());
        }
    }
}