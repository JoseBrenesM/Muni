using Abstracciones.Modelos.Persona;
using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WEB.Pages.Personas
{
    [Authorize(Roles = "1")]
    public class EditarPerfilModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private Configuracion _configuracion;

        [BindProperty] public Persona persona { get; set; } = default!;

        public EditarPerfilModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuracion = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            string endPoint = _configuracion.ObtenerEndPoint("EditarPersona");

            //var cliente = new HttpClient();
            var cliente = _httpClientFactory.CreateClient("ClienteFerreteria");

            var respuesta = await cliente.PutAsJsonAsync<Persona>(endPoint, persona);

            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }

        public async Task<ActionResult> OnGet(Guid id)
        {
            string urlEndPoint = _configuracion.ObtenerEndPoint("ObtenerPersona");

            //var cliente = new HttpClient();
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
                    return Page();
                }
            }
            return Page();
        }
    }
}
