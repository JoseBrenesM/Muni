using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.API
{
    public interface IAutenticacionAPI
    {
        Task<IActionResult> PostAsync([FromBody] Login login);
    }
}
