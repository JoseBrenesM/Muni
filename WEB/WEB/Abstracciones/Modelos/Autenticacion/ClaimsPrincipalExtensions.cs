using System.Security.Claims;

namespace Abstracciones.Modelos.Autenticacion
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetIdUsuario(this ClaimsPrincipal user)
        {
            var recuperaId = user.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

            return new Guid(recuperaId);
        }
    }
}