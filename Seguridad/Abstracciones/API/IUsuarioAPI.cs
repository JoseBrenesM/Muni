using Abstracciones.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.API
{
    public interface IUsuarioAPI
    {
        Task<IActionResult> PostAsync([FromBody] UsuarioPost usuario);
        Task<IActionResult> ObtenerUsuario([FromBody] Usuario usuario);
        Task<IActionResult> ActualizarHashUsuario(Guid idPersona, string nuevoHash);

    }
}
