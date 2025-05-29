using Abstracciones.DA;
using Abstracciones.Modelos.Roles;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class RolesDA : IRolesDA
    {
        private IRepositorioDapper _repositorioDapper;

        private SqlConnection _sqlConnection;

        public RolesDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<int> Agregar(RolesPost rol)
        {
            string query = @"AddRol";

            var id = await _sqlConnection.QueryAsync<RolesPost>(query, new
            {
                Id = rol.Id,
                Tipo = rol.Tipo
            });

            return rol.Id;
        }

        public async Task<int> Editar(RolesPost rol)
        {
            string query = @"EditRol";

            await _sqlConnection.QueryAsync<RolesPost>(query, new
            {
                Id = rol.Id,
                Tipo = rol.Tipo
            });

            return rol.Id;
        }

        public async Task<bool> Eliminar(int Id)
        {
            string query = @"DeleteRol";

            try
            {
                await _sqlConnection.QueryAsync(query, new { Id = Id });
            }
            catch (Exception ex) { return false; }

            return true;
        }

        public async Task<IEnumerable<RolesPost>> ObtenerTodos()
        {
            string sql = @"SELECT * FROM Roles";

            var consulta = await _sqlConnection.QueryAsync<RolesPost>(sql);

            if (!consulta.Any())
                return null;

            return consulta;
        }
    }
}
