using Abstracciones.Modelos;
using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WEB.Pages.Horarios
{
    [Authorize(Roles = "1, 3")]
    public class IndexModel : PageModel
    {
        private readonly Configuracion _configuracion;
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(Configuracion configuracion, IHttpClientFactory httpClientFactory)
        {
            _configuracion = configuracion;
            _httpClientFactory = httpClientFactory;
        }

        public List<HorarioDetalle> Horarios { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            string endpoint = _configuracion.ObtenerEndPoint("ObtenerHorarios");
            var cliente = _httpClientFactory.CreateClient("Cliente");

            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();

                Horarios = JsonSerializer.Deserialize<List<HorarioDetalle>>(contenido, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<HorarioDetalle>();
            }

            return Page();
        }
    }
}
