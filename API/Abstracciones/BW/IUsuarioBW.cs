using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.BW
{
    public interface IUsuarioBW
    {
        public Task<Usuario> Obtener(Guid Id);
        public Task<IEnumerable<Usuario>> Obtener();
        public Task<Guid> Agregar(Usuario usuario);
        public Task<Guid> Editar(Usuario usuario);
        public Task<Guid> Eliminar(Guid Id);
    }
}
