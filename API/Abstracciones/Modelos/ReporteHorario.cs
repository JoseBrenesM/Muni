using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class ReporteHorario
    {
        public Guid IdPersona { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public TimeSpan? HoraEntrada { get; set; }
        public TimeSpan? HoraSalida { get; set; }
        public TimeSpan? HoraInicioAlmuerzo { get; set; }
        public TimeSpan? HoraFinAlmuerzo { get; set; }
        public string EstadoDescripcion { get; set; } = string.Empty;
        public int IdEstado { get; set; }
    }

}
