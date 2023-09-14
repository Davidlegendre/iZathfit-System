using Configuration;
using Configuration.GlobalHelpers;
using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TipoIdentificacion
{
    public class TipoIdentificacionRepository : ITipoIdentificacionRepository
    {
        IGeneralConfiguration _config;
        IGlobalHelpers _helper;
        public TipoIdentificacionRepository(IGeneralConfiguration config, IGlobalHelpers helper)
        {
            _config = config;
            _helper = helper;
        }

        public async Task<List<TipoIdentificacionModel>?> GetTipoIdentificacion()
        {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryAsync<TipoIdentificacionModel>("SelectAllTipoIdentificacion",
                    commandType: System.Data.CommandType.StoredProcedure);
                return result.Count() == 0 ? null : result.AsList();
            }
        }

        public async Task<TipoIdentificacionModel?> InsertTipoIdentificacion(TipoIdentificacionModel tipoidentificacion)
        {
            _helper.Policy(TypeRol.Desarrollador, TypeRol.Dueño);
            using(var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryFirstAsync<TipoIdentificacionModel?>("InsertTipoIdentificacion",
                    new { @abreviado = tipoidentificacion.abreviado, @descripcion = tipoidentificacion.descripcion },
                    commandType: System.Data.CommandType.StoredProcedure);

                await con.CloseAsync();
                return result;
            }
        }

        public async Task<TipoIdentificacionModel?> UpdateTipoIdentificacion(TipoIdentificacionModel tipoidentificacion)
        {
            _helper.Policy(TypeRol.Desarrollador, TypeRol.Dueño);
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryFirstAsync<TipoIdentificacionModel?>("UpdateTipoIdentificacion",
                    new
                    {
                        @abreviado = tipoidentificacion.abreviado,
                        @descripcion = tipoidentificacion.descripcion,
                        @idtipoidentificacion = tipoidentificacion.IdTipoIdentity
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }
        }

        public async Task<int> DeleteTipoIdentificacion(int IdTipoidentificacion)
        {
            _helper.Policy(TypeRol.Desarrollador, TypeRol.Dueño);
            using (var con = new SqlConnection(_config.GetConnection()))
            { 
                var result = await con.ExecuteAsync("DeleteTIpoIdentificacion", new { @idtipoidentificacion = IdTipoidentificacion },
                    commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }
        }
    }
}
