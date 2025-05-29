using Abstracciones.Modelos.Roles;

namespace Abstracciones.DA
{
    public interface IRolesxUsuarioDA
    {
        public Task<IEnumerable<RolesView>> ObtenerTodos(Guid IdPersona);
        public Task<Guid> Editar(RolesxUsuarioPost roles);
        public Task<Guid> Eliminar(Guid IdPersona);
    }
}