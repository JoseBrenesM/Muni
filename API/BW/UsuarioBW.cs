using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW
{
    public class UsuarioBW : IUsuarioBW
    {
        private IUsuarioDA _UsuarioDA;
        public UsuarioBW(IUsuarioDA UsuarioDA)
        {
            _UsuarioDA = UsuarioDA;
        }
        public async Task<Guid> Agregar(Usuario usuario)
        {
            return await _UsuarioDA.Agregar(usuario);
        }

        public async Task<Guid> Editar(Usuario usuario)
        {
           return await _UsuarioDA.Editar(usuario);
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            return await _UsuarioDA.Eliminar(Id);
        }

        public Task<Usuario> Obtener(Guid Id)
        {
            return _UsuarioDA.Obtener(Id);
        }

        public Task<IEnumerable<Usuario>> Obtener()
        {
            return _UsuarioDA.Obtener();
        }
    }
}
