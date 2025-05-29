using Abstracciones.Modelos.Persona;
using BC;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WEB.Pages.Cuenta
{
    public class EliminarModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private Configuracion _configuracion;

        [BindProperty] public Persona persona { get; set; } = default!;

        public EliminarModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuracion = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> OnPost(Guid id)
        {
            if (!ModelState.IsValid)
                return Page();

            string endPoint = _configuracion.ObtenerEndPoint("EliminarPersona");

            //var cliente = new HttpClient();
            var cliente = _httpClientFactory.CreateClient("Cliente");

            var solicitud = new HttpRequestMessage(HttpMethod.Delete, string.Format(endPoint, id));

            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
            {
                await HttpContext.SignOutAsync();

                return RedirectToPage("../Index");
            }
            else
            {
                TempData["Atención"] = "Esta persona tiene pedidos pendientes. No puedes eliminarlo.";
                return Page();
            }
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
