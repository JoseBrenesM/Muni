using Abstracciones.Modelos.Roles;

namespace Abstracciones.BW
{
    public interface IRolesxUsuarioBW
    {
        public Task<IEnumerable<RolesView>> ObtenerTodos(Guid IdPersona);
        public Task<Guid> Editar(RolesxUsuarioPost roles);
        public Task<Guid> Eliminar(Guid IdPersona);
    }
}