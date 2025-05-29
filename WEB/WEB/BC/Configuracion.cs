using Abstracciones.Modelos;
using Microsoft.Extensions.Configuration;


namespace BC
{
    public class Configuracion
    {
        private readonly IConfiguration _configuration;

        public Configuracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ObtenerEndPoint(string NombreEndPoint)
        {
            return _configuration.GetSection("APIEndPoints").Get<List<APIEndPoint>>()
                .Where(endPoint => endPoint.Nombre == NombreEndPoint)
                .First().Valor;
        }
    }
}
