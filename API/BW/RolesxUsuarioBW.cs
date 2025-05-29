using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Modelos.Roles;

namespace BW
{
    public class RolesxUsuarioBW : IRolesxUsuarioBW
    {
        private IRolesxUsuarioDA _rolesxUsuarioDA;

        public RolesxUsuarioBW(IRolesxUsuarioDA rolesxUsuarioDA)
        {
            _rolesxUsuarioDA = rolesxUsuarioDA;
        }

        public async Task<Guid> Editar(RolesxUsuarioPost roles)
        {
            return await _rolesxUsuarioDA.Editar(roles);
        }

        public async Task<Guid> Eliminar(Guid IdPersona)
        {
            return await _rolesxUsuarioDA.Eliminar(IdPersona);
        }

        public async Task<IEnumerable<RolesView>> ObtenerTodos(Guid IdPersona)
        {
            return await _rolesxUsuarioDA.ObtenerTodos(IdPersona);
        }
    }
}
