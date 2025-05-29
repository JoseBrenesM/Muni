using Microsoft.AspNetCore.Mvc;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracciones.API
{
    public interface IReporteAPI
    {
        [HttpGet("descargar")]
        Task<IActionResult> DescargarReporteHorarios([FromQuery] FiltroReporteHorario filtro);
    }
}
