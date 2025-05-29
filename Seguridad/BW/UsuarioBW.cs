using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Entidades;
using System.Security.Cryptography;
using System.Text;

namespace BW
{
    public class UsuarioBW : IUsuarioBW
    {
        private IUsuarioDA _usuarioDA;

        public UsuarioBW(IUsuarioDA usuarioDA)
        {
            _usuarioDA = usuarioDA;
        }

        public async Task<Guid> CrearUsuario(UsuarioPost usuario)
        {
            return await _usuarioDA.CrearUsuario(usuario);
        }

        public async Task<Usuario> ObtenerUsuario(Usuario usuario)
        {
            return await _usuarioDA.ObtenerUsuario(usuario);
        }
        public async Task<bool> ActualizarHashUsuario(Guid idPersona, string nuevoHash)
        {
            
                string hash = HashearPassword(nuevoHash);
                return await _usuarioDA.ActualizarHashUsuario(idPersona, hash);
           
        }
        private string HashearPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToUpperInvariant(); // o .ToLowerInvariant()
            }
        }

    }
}
