using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class Horario
    {
        [Required]
        public Guid IdPersona { get; set; }

        public DateTime? HoraEntrada { get; set; }
        public DateTime? HoraSalida { get; set; }
        public DateTime? HoraInicioAlmuerzo { get; set; }
        public DateTime? HoraFinAlmuerzo { get; set; }

        public int? IdEstado { get; set; }
    }
}
