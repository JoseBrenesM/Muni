using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.API
{
    public interface IHorarioAPI
    {
        [HttpPost]
        Task<IActionResult> MarcarEntrada(Guid idPersona);

        [HttpPost]
        Task<IActionResult> MarcarInicioAlmuerzo(Guid idPersona);

        [HttpPost]
        Task<IActionResult> MarcarFinAlmuerzo(Guid idPersona);

        [HttpPost]
        Task<IActionResult> MarcarSalida(Guid idPersona);

        [HttpGet]
        Task<IActionResult> ObtenerHorarioActual(Guid idPersona);
        [HttpGet]
        Task<IActionResult> ObtenerHorariosPorPersona(Guid idPersona);

        [HttpGet]
        Task<IActionResult> ObtenerHorarios();
    }
}
