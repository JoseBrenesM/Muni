using Abstracciones.API;
using Abstracciones.BW;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    
    [ApiController]
    public class UsuarioController : ControllerBase, IUsuarioAPI
    {
        private IUsuarioBW _usuarioBW;
        public UsuarioController(IUsuarioBW usuariobw)
        {
            _usuarioBW = usuariobw;
        }
        [Route("api/[controller]")]
        [HttpPost]
        public async Task<IActionResult> Agregar(Usuario usuario)
        {
            return Ok(await _usuarioBW.Agregar(usuario));
        }
        [Route("api/[controller]")]
        [HttpPut]

        public async Task<IActionResult> Editar(Usuario usuario)
        {
            return Ok(await _usuarioBW.Editar(usuario));
        }
        [Route("api/[controller]")]
        [HttpDelete]
        public async Task<IActionResult> Eliminar(Guid Id)
        {
            return Ok(await _usuarioBW.Eliminar(Id));
        }
        [Route("api/[controller]/getOne")]
        [HttpGet ]
        public async Task<IActionResult> Obtener(Guid Id)
        {
            return Ok(await _usuarioBW.Obtener(Id));
        }
        [Route("api/[controller]")]
        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            return Ok(await _usuarioBW.Obtener());
        }
    }
}
