using Abstracciones.Modelos.Roles;

namespace Abstracciones.BW
{
    public interface IRolesBW
    {
        public Task<IEnumerable<RolesPost>> ObtenerTodos();
        public Task<int> Agregar(RolesPost rol);
        public Task<int> Editar(RolesPost rol);
        public Task<bool> Eliminar(int Id);
    }
}
