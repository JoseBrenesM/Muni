using Abstracciones.Modelos.Persona;
using Abstracciones.Modelos.Roles;
using BC;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WEB.Pages.Roles
{
    [Authorize(Roles = "1")]
    public class EditarModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private Configuracion _configuracion;
        public Persona persona { get; set; } = default!;
        [BindProperty] public IList<RolesPost> roles { get; set; } = default!;
        [BindProperty] public IList<RolesxUsuarioPost> rolesxUsuario { get; set; } = default!;
        [BindProperty] public RolesxUsuarioPost rolesxUsuarioModel { get; set; } = default!;

        public EditarModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuracion = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            string endPoint = _configuracion.ObtenerEndPoint("EditarRolxUsuario");

            var cliente = _httpClientFactory.CreateClient("Cliente");

            var respuesta = await cliente.PutAsJsonAsync<RolesxUsuarioPost>(endPoint, rolesxUsuarioModel);

            if (respuesta.IsSuccessStatusCode)
            {
                await HttpContext.SignOutAsync();

                return RedirectToPage("../Index");
            }
            return Page();
        }

        public async Task<ActionResult> OnGet(Guid id)
        {
            await ObtenerPersona(id);

            await ObtenerRoles();

            string urlEndPoint = _configuracion.ObtenerEndPoint("ObtenerRolesxUsuario");

            var cliente = _httpClientFactory.CreateClient("Cliente");

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPoint, id));

            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true

                    };

                    rolesxUsuario = JsonSerializer.Deserialize<IList<RolesxUsuarioPost>>(resultado, options);
                    
                }
                catch (JsonException ex)
                {
                    return Page();
                }
            }
          

            return Page();
        }

        private async Task ObtenerRoles()
        {
            string urlEndPoint = _configuracion.ObtenerEndPoint("ObtenerRoles");

            var cliente = _httpClientFactory.CreateClient("Cliente");

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPoint));

            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    roles = JsonSerializer.Deserialize<IList<RolesPost>>(resultado, options);
                }
                catch (JsonException ex) { }
            }
        }

        private async Task ObtenerPersona(Guid id)
        {
            string urlEndPoint = _configuracion.ObtenerEndPoint("ObtenerPersona");

            var cliente = _httpClientFactory.CreateClient("Cliente");

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPoint, id));

            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    persona = JsonSerializer.Deserialize<Persona>(resultado, options);
                }
                catch (JsonException ex)
                {

                }
            }
        }
    }
}
