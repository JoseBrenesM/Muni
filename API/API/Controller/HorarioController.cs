
using Abstracciones.API;
using Abstracciones.BW;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorarioController : ControllerBase, IHorarioAPI
    {
        private readonly IHorarioBW _horarioBW;

        public HorarioController(IHorarioBW horarioBW)
        {
            _horarioBW = horarioBW;
        }

        [HttpPost("marcar-entrada")]
        public async Task<IActionResult> MarcarEntrada([FromQuery] Guid idPersona)
        {
            return Ok(await _horarioBW.MarcarEntrada(idPersona));
        }

        [HttpPost("marcar-inicio-almuerzo")]
        public async Task<IActionResult> MarcarInicioAlmuerzo([FromQuery] Guid idPersona)
        {
            return Ok(await _horarioBW.MarcarInicioAlmuerzo(idPersona));
        }

        [HttpPost("marcar-fin-almuerzo")]
        public async Task<IActionResult> MarcarFinAlmuerzo([FromQuery] Guid idPersona)
        {
            return Ok(await _horarioBW.MarcarFinAlmuerzo(idPersona));
        }

        [HttpPost("marcar-salida")]
        public async Task<IActionResult> MarcarSalida([FromQuery] Guid idPersona)
        {
            return Ok(await _horarioBW.MarcarSalida(idPersona));
        }

        [HttpGet("actual")]
        public async Task<IActionResult> ObtenerHorarioActual([FromQuery] Guid idPersona)
        {
            var resultado = await _horarioBW.ObtenerHorarioActual(idPersona);
            if (resultado == null)
                return NotFound("No se encontró horario actual para esta persona.");
            return Ok(resultado);
        }

        [HttpGet("todos")]
        public async Task<IActionResult> ObtenerHorarios()
        {
            var resultado = await _horarioBW.ObtenerHorarios();
            return Ok(resultado);
        }
        [HttpGet("{idPersona}/horarios")]
        public async Task<IActionResult> ObtenerHorariosPorPersona(Guid idPersona)
        {
            var horarios = await _horarioBW.ObtenerHorariosPorPersona(idPersona);

            if (horarios == null || !horarios.Any())
                return NotFound();

            return Ok(horarios);
        }
    }
}
