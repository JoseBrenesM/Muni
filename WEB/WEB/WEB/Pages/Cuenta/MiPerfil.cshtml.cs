using Abstracciones.Modelos;
using Abstracciones.Modelos.Persona;
using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WEB.Pages.Cuenta
{
    [Authorize(Roles = "1,2,3")]
    public class MiPerfilModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private Configuracion _configuracion;

        [BindProperty] public Persona persona { get; set; } = default!;
        [BindProperty]
        public IEnumerable<Horario> Horarios { get; set; } = new List<Horario>();


        public MiPerfilModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuracion = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            string endPoint = _configuracion.ObtenerEndPoint("EditarPersona");

            var cliente = _httpClientFactory.CreateClient("ClienteFerreteria");

            var respuesta = await cliente.PutAsJsonAsync<Persona>(endPoint, persona);

            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("./MiPerfil", new { id = persona.Id });
            }
            return Page();
        }

        public async Task<ActionResult> OnGet(Guid id)
        {
            string urlPersonaEndPoint = _configuracion.ObtenerEndPoint("ObtenerPersona");
            string urlHorariosEndPoint = _configuracion.ObtenerEndPoint("ObtenerHorariosxUsuario");

            var cliente = _httpClientFactory.CreateClient("Cliente");

            var solicitudPersona = new HttpRequestMessage(HttpMethod.Get, string.Format(urlPersonaEndPoint, id));
            var respuestaPersona = await cliente.SendAsync(solicitudPersona);

            if (respuestaPersona.IsSuccessStatusCode)
            {
                var resultadoPersona = await respuestaPersona.Content.ReadAsStringAsync();
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    persona = JsonSerializer.Deserialize<Persona>(resultadoPersona, options);
                }
                catch (JsonException)
                {
                    return Page();
                }
            }

            try
            {
                var solicitudHorarios = new HttpRequestMessage(HttpMethod.Get, string.Format(urlHorariosEndPoint, id));
                var respuestaHorarios = await cliente.SendAsync(solicitudHorarios);

                if (respuestaHorarios.IsSuccessStatusCode)
                {
                    var resultadoHorarios = await respuestaHorarios.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var horariosObtenidos = JsonSerializer.Deserialize<IEnumerable<Horario>>(resultadoHorarios, options);

                    Horarios = horariosObtenidos ?? new List<Horario>();
                }
                else
                {
                    Horarios = new List<Horario>();
                }
            }
            catch (Exception)
            {
                Horarios = new List<Horario>();
            }

            return Page();
        }

    }

}
