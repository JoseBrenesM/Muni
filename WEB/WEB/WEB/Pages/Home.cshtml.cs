using System.Text.Json;
using Abstracciones.Modelos;
using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB.Pages
{
    [Authorize(Roles = "1,2,3")]
    public class HomeModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Configuracion _configuracion;

        public Horario? HorarioActual { get; set; }

        public HomeModel(Configuracion configuracion, IHttpClientFactory httpClientFactory)
        {
            _configuracion = configuracion;
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var idUsuario = Abstracciones.Modelos.Autenticacion.ClaimsPrincipalExtensions.GetIdUsuario(User);
            var endPoint = _configuracion.ObtenerEndPoint("ObtenerEstado");
            var cliente = _httpClientFactory.CreateClient("Cliente");

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endPoint, idUsuario));
            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                HorarioActual = JsonSerializer.Deserialize<Horario>(resultado, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        public string ObtenerNombreEstado(int? idEstado)
        {
            return idEstado switch
            {
                1 => "Jornada Iniciada",
                2 => "En Almuerzo",
                3 => "Regreso del Almuerzo",
                4 => "Jornada Finalizada",
                _ => "Sin estado"
            };
        }
    }
}
