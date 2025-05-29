using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Entidades
{
    public class HorarioDetalle
    {
        public Guid IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? HoraEntrada { get; set; }
        public DateTime? HoraInicioAlmuerzo { get; set; }
        public DateTime? HoraFinAlmuerzo { get; set; }
        public DateTime? HoraSalida { get; set; }
        public string Estado { get; set; }
    }

}
