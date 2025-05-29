using Abstracciones.DA;
using Abstracciones.Entidades;
using Dapper;
using System.Data.SqlClient;

namespace DA
{
    public class UsuarioDA : IUsuarioDA
    {
        IRepositorioDapper _repositorioDapper;

        private SqlConnection _sqlConnection;

        public UsuarioDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<Guid> CrearUsuario(UsuarioPost usuario)
        {
            string query = @"Agregar_Usuario_MW";

            var consulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Hash = usuario.Hash,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Direccion = usuario.Direccion,
                Telefono = usuario.Telefono,
                Correo = usuario.Correo
            });

            return consulta;
        }

        public async Task<IEnumerable<Rol>> ObtenerPerfilesxUsuario(Usuario usuario)
        {
            string query = @"Obtener_RolesxUsuario";

            var consulta = await _sqlConnection.QueryAsync<Rol>(query, new
            {
                Correo = usuario.Corrreo
            });

            return consulta;
        }

        public async Task<Usuario> ObtenerUsuario(Usuario usuario)
        {
            string query = @"Obtener_Usuario_MW";

            var consulta = await _sqlConnection.QueryAsync<Usuario>(query, new
            {
                Correo = usuario.Corrreo
            });

            return consulta.FirstOrDefault();
        }

        public async Task<bool> VerificarPersonaActiva(Guid idPersona)
        {
            string query = @"SELECT Activo FROM Personas WHERE Id = @Id";

            var resultado = await _sqlConnection.QuerySingleOrDefaultAsync<bool?>(query, new { Id = idPersona });

            return resultado.HasValue && resultado.Value;
        }
        public async Task<bool> ActualizarHashUsuario(Guid idPersona, string nuevoHash)
        {
            string query = "ActualizarHashUsuario";

            var resultado = await _sqlConnection.QuerySingleAsync<string>(query, new
            {
                IdPersona = idPersona,
                NuevoHash = nuevoHash
            });

            return resultado == "OK";
        }



    }
}