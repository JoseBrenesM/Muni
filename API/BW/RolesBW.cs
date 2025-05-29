using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Modelos.Roles;

namespace BW
{
    public class RolesBW : IRolesBW
    {
        private IRolesDA _rolesDA;

        public RolesBW(IRolesDA rolesDA)
        {
            _rolesDA = rolesDA;
        }

        public async Task<int> Agregar(RolesPost rol)
        {
            return await _rolesDA.Agregar(rol);
        }

        public async Task<int> Editar(RolesPost rol)
        {
            return await _rolesDA.Editar(rol);
        }

        public async Task<bool> Eliminar(int Id)
        {
            return await _rolesDA.Eliminar(Id);
        }

        public async Task<IEnumerable<RolesPost>> ObtenerTodos()
        {
            return await _rolesDA.ObtenerTodos();
        }
    }
}
