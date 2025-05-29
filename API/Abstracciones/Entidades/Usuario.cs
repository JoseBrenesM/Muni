using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Entidades
{
    public class Usuario
    {
        public Guid IdPersona { get; set; }
        public byte[] Hash { get; set; }
    }
}
