using Abstracciones.DA;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DA.Repositorios
{
    public class RepositorioDapper : IRepositorioDapper
    {
        public SqlConnection _conexionBaseDatos { get; }

        private readonly IConfiguration _configuration;

        public RepositorioDapper(IConfiguration configuration)
        {
            _configuration = configuration;
            _conexionBaseDatos = new SqlConnection(_configuration.GetConnectionString("BDProyecto"));
        }
        public SqlConnection ObtenerRepositorioDapper()
        {
            return _conexionBaseDatos;
        }
    }
}