using Abstracciones.DA;
using Abstracciones.Modelos.Roles;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class RolesxUsuarioDA : IRolesxUsuarioDA
    {
        private IRepositorioDapper _repositorioDapper;

        private SqlConnection _sqlConnection;

        public RolesxUsuarioDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<Guid> Editar(RolesxUsuarioPost roles)
        {
            string query = @"EditRolxUsuario";

            var id = await _sqlConnection.QueryAsync<RolesxUsuarioPost>(query, new
            {
                Id = roles.Id,
                IdPersona = roles.IdPersona
            });

            return roles.IdPersona;
        }

        public async Task<Guid> Eliminar(Guid IdPersona)
        {
            string query = @"DeleteRolxUsuario";

            await _sqlConnection.QueryAsync(query, new { IdPersona = IdPersona });

            return IdPersona;
        }

        public async Task<IEnumerable<RolesView>> ObtenerTodos(Guid IdPersona)
        {
            string query = @"RolesxUsuario";

            var consulta = await _sqlConnection.QueryAsync<RolesView>(query, new { IdPersona = IdPersona });

            if (!consulta.Any())
                return null;

            return consulta;
        }
    }
}
