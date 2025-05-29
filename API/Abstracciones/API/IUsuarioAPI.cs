using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
namespace Abstracciones.API
{
    public interface IUsuarioAPI
    {
        [HttpGet]
        public Task<IActionResult> Obtener(Guid Id);
        [HttpGet]
        public Task<IActionResult> Obtener();
        [HttpPost]
        public Task<IActionResult> Agregar(Usuario usuario);
        [HttpPut]
        public Task<IActionResult> Editar(Usuario usuario);
        [HttpDelete]
        public Task<IActionResult> Eliminar(Guid Id);
    }
}
