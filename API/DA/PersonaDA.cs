using Abstracciones.DA;
using Abstracciones.Modelos.Persona;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class PersonaDA : IPersonaDA
    {
        private IRepositorioDapper _repositorioDapper;

        private SqlConnection _sqlConnection;

        public PersonaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<bool> Activar(Guid Id)
        {
            string sql = @"ActivarPersona";

            try
            {
                await _sqlConnection.QueryAsync(sql, new { Id = Id });
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Agregar(PersonaPost persona)
        {
            string query = @"AddPersona";
            try
            {
                await _sqlConnection.QueryAsync<PersonaPost>(query, new
                {
                    Nombre = persona.Nombre,
                    Apellido = persona.Apellido,
                    Direccion = persona.Direccion,
                    Telefono = persona.Telefono,
                    Correo = persona.Correo
                });
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Editar(Persona persona)
        {
            string query = @"EditPersona";
            try
            {
                await _sqlConnection.QueryAsync<PersonaPost>(query, new
                {
                    Id = persona.Id,
                    Nombre = persona.Nombre,
                    Apellido = persona.Apellido,
                    Direccion = persona.Direccion,
                    Telefono = persona.Telefono,
                    Correo = persona.Correo
                });
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Eliminar(Guid Id)
        {
            string sql = @"DeletePersona";

            try
            {
                await _sqlConnection.QueryAsync(sql, new { Id = Id });
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<Persona> Obtener(Guid Id)
        {
            string sql = @"GetPersona";

            var consulta = await _sqlConnection.QueryAsync<Persona>(sql, new { Id = Id });

            if (!consulta.Any())
                return null;

            return consulta.First();
        }

        public async Task<IEnumerable<Persona>> ObtenerTodos()
        {
            string sql = @"GetPersonas";

            var consulta = await _sqlConnection.QueryAsync<Persona>(sql);

            if (!consulta.Any())
                return null;

            return consulta;
        }
    }
}