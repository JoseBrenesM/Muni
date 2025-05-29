using Abstracciones.Modelos.Autenticacion;
using BC;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace WEB.Pages.Cuenta
{
    public class LoginModel : PageModel
    {
        [BindProperty] public Login loginInfo { get; set; } = default;

        /*[BindProperty]*/
        public Token token { get; set; }

        public Configuracion _configuracion { get; set; }

        public LoginModel(Configuracion configuracion)
        {
            _configuracion = configuracion;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var hash = Password.GenerarHash(loginInfo.Password);

                loginInfo.Hash = Password.ObtenerHash(hash);

                string endPoint = _configuracion.ObtenerEndPoint("Login");

                var cliente = new HttpClient();

                var request = new LoginRequest
                {
                    Correo = loginInfo.Correo,
                    Hash = loginInfo.Hash
                };

                var respuesta = await cliente.PostAsJsonAsync<LoginRequest>(string.Format(endPoint), request);

                if (!respuesta.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("loginInfo", "Ocurrio un error en el servidor. Intentalo más tarde.");
                    return Page();
                }

                token = JsonConvert.DeserializeObject<Token>(respuesta.Content.ReadAsStringAsync().Result);

                if (token != null && token.ValidacionExitosa)
                {
                    TempData["LoginExitoso"] = true;
                    JwtSecurityToken? tokens = leerInformacionToken();

                    var perfiles = tokens.Claims.Where(c => c.Type == ClaimTypes.Role);

                    await AgregarClaims(tokens, perfiles);

                    return RedirectToPage("/Index");
                }
                else
                {
                    TempData["ErrorLogin"] = token?.MensajeError ?? "Error desconocido al intentar iniciar sesión.";
                }
            }

            return Page();
        }

        private JwtSecurityToken? leerInformacionToken()
        {
            var handler = new JwtSecurityTokenHandler();

            var jsonToken = handler.ReadToken(token.AccessToken);

            var tokens = jsonToken as JwtSecurityToken;

            return tokens;
        }

        private async Task AgregarClaims(JwtSecurityToken? tokens, IEnumerable<Claim> perfiles)
        {
            var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, tokens.Claims.First(c=>c.Type=="usuario").Value),
            new Claim("usuario", tokens.Claims.First(c=>c.Type=="usuario").Value),
            new Claim(ClaimTypes.Email, loginInfo.Correo),
            new Claim("Token", token.AccessToken)
        };

            var idUsuarioClaim = tokens.Claims.FirstOrDefault(c => c.Type == "IdUsuario");
            var idUsuario = User.FindFirst("IdUsuario")?.Value;
            if (idUsuarioClaim != null)
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, idUsuarioClaim.Value));
                claims.Add(new Claim("IdUsuario", idUsuarioClaim.Value));
                
            }

            var rolUsuarioClaim = tokens.Claims.FirstOrDefault(c => c.Type == "NombreRol");
            foreach (var perfil in perfiles)
            {
                claims.Add(new Claim(ClaimTypes.Role, perfil.Value));
                claims.Add(new Claim("NombreRol", rolUsuarioClaim.Value));
            }

            await establecerAutenticacion(claims);
        }

        private async Task establecerAutenticacion(List<Claim> claims)
        {
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);
        }
    }
}
