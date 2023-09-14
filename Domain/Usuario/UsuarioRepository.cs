using Configuration;
using Configuration.GlobalHelpers;
using Dapper;
using Models;
using Models.ModelsCommons;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Usuario
{
    public class UsuarioRepository : IUsuarioRepository
    {
        readonly IGeneralConfiguration _config;
        IGlobalHelpers _helpers;
        public UsuarioRepository(IGeneralConfiguration config, IGlobalHelpers helpers)
        {
            _config = config;
            _helpers = helpers;
        }

        public async Task<Guid?> VerificarLogin(string user, string password) {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteScalarAsync<Guid?>("IniciarSesion", new {
                    @user = user,
                    @password = password
                }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;  
            }
        }

        public async Task<int> CambiarContraseña(string contraseña, Guid? IDPersona) {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("ChangePasswordUser", new { @password = contraseña, @idpersona = IDPersona },
                    commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
                
            }
        }

        public async Task VerificarActivo(Guid? id)
        {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                await con.ExecuteAsync("VerificarActivoUsuario",new { @idPersona = id }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
            }
        }

        public async Task<List<Models.Usuario>?> GetUsuarios() {

            _helpers.Policy(TypeRol.Dueño, TypeRol.Desarrollador);
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryAsync<Models.Usuario>("SelectAllUsuario",
                    commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                result = _config.getuserSistema().CodeRol == _config.GetRol(typerol: TypeRol.Dueño)
                    ? result.Where(x => x.codeRol != _config.GetRol(TypeRol.Desarrollador)) : result; 
                return result.Count() > 0 ? 
                    result.Where(x =>x.idUsuario != _config.getuserSistema()?.idUsuario).AsList() 
                    : null;
            }
        }

        public async Task<Models.Usuario?> InsertarUsuario(Models.Usuario? usuario)
        {
            _helpers.Policy(TypeRol.Desarrollador, TypeRol.Dueño);
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryFirstAsync<Models.Usuario?>("InsertUsuario",
                    new
                    {
                        @idPersona = usuario.IdPersona,
                        @usuario = usuario.usuario,
                        @contrasena = usuario.contrasena
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }
        }

        public async Task<Models.Usuario?> UpdateUsuario(Models.Usuario? usuario)
        {
            _helpers.Policy(TypeRol.Desarrollador, TypeRol.Dueño);
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryFirstAsync<Models.Usuario?>("UpdateUsuario",
                    new {
                        @idPersona = usuario.IdPersona,
                        @usuario = usuario.usuario,
                        @contrasena = usuario.contrasena,
                        @idusuario = usuario.idUsuario,
                        @isactivo = usuario.IsActivo
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }
        }

        public async Task<int> InactivarUsuario(Guid idusuario)
        {
            _helpers.Policy(TypeRol.Desarrollador, TypeRol.Dueño);
            using(var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("InactivarUsuario", new {
                    @iduser = idusuario
                }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }
        }

        public async Task<Models.Usuario?> EliminarUsuario(Guid idUsuario)
        {
            _helpers.Policy(TypeRol.Desarrollador, TypeRol.Dueño);
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryFirstAsync<Models.Usuario?>("DeleteUsuario",
                    new { @idusuario = idUsuario}, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }
        }
    }
}
