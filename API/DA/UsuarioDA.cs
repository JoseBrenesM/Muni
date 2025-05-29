using Abstracciones.DA;
using Abstracciones.Modelos;
using Dapper;
using Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class UsuarioDA : IUsuarioDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public UsuarioDA(IRepositorioDapper repositorioDapper)
        {

            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();

        }
        public async Task<Guid> Agregar(Usuario usuario)
        {
            string sql = @"AddUsuario";
            await _sqlConnection.QueryAsync<Abstracciones.Entidades.Usuario>(sql, new { Id = usuario.IdPersona, Hash = usuario.Hash });
            return usuario.IdPersona;
        }

        public async Task<Guid> Editar(Usuario usuario)
        {
            string sql = @"EditUsuario";
            await _sqlConnection.QueryAsync<Abstracciones.Entidades.Usuario>(sql, new { Id=usuario.IdPersona, Hash = usuario.Hash });
            return usuario.IdPersona;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            string sql = @"DeleteUsuario";
            await _sqlConnection.QueryAsync<Abstracciones.Entidades.Usuario>(sql, new { Id = Id });
            return Id;
        }

        public async Task<Usuario> Obtener(Guid Id)
        {
            string sql = @"GetUsuario";
            var resultadoConsulta = await _sqlConnection.QueryAsync<Abstracciones.Entidades.Usuario>(sql, new { Id = Id });
            if (!resultadoConsulta.Any())
                return null;
            return ConvertirP(resultadoConsulta.First());
        }

        public async Task<IEnumerable<Usuario>> Obtener()
        {
            string sql = @"GetUsuario";
            var resultadoConsulta = await _sqlConnection.QueryAsync<Abstracciones.Entidades.Usuario>(sql);
            if (!resultadoConsulta.Any())
                return null;
            return ConvertirPsAModelo(resultadoConsulta);
        }
        #region convertir
        private Abstracciones.Modelos.Usuario ConvertirP(Abstracciones.Entidades.Usuario usuario)
        {
            var resultadoConversion = Convertidor.Convertir<Abstracciones.Entidades.Usuario, Abstracciones.Modelos.Usuario>(usuario);
            return resultadoConversion;
        }
        private IEnumerable<Abstracciones.Modelos.Usuario> ConvertirPsAModelo(IEnumerable<Abstracciones.Entidades.Usuario> usuario)
        {
            var resultadoConversion = Convertidor.ConvertirLista<Abstracciones.Entidades.Usuario, Abstracciones.Modelos.Usuario>(usuario);
            return resultadoConversion;
        }
        #endregion
    }
}
