using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Entidades
{
    public class Usuario
    {
        public Guid IdPersona { get; set; }
        public string? Corrreo { get; set; }
        public string? Hash { get; set; }
        public Boolean? Activo { get; set; }
    }
}
