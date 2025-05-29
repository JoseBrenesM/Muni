using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Abstracciones.Modelos.Autenticacion
{
    public class AuthHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var tokenClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token");
            var token = tokenClaim?.Value;

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}