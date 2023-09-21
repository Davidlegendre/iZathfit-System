using Configuration;
using Configuration.GlobalHelpers;
using Dapper;
using Models;
using Models.ServiciodeModelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Promocion
{
    public class PromocionRepository : IPromocionRepository
    {
        IGeneralConfiguration _config;
        IGlobalHelpers _helpers;
        public PromocionRepository(IGeneralConfiguration config, IGlobalHelpers helpers)
        {
            _config = config;
            _helpers = helpers;
        }

        public async Task<List<PromocionModelo>?> GetPromociones() {
            using (var con = new SqlConnection(_config.GetConnection())) {
                var result = await con.QueryAsync<PromocionModelo>("SelectAllPromociones",
                    commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result.Count() == 0 ? null : result.AsList();
            }
        }

        public async Task<List<PromocionModelo>?> GetPromocionesActive() { 
            using(var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryAsync<PromocionModelo>("SelectActiveAllPromociones",
                    commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result.Count() == 0? null : result.AsList();
            }
        }
        public async Task<bool> InsertPromocion(PromocionModelo promocion) {
            var user = _config.getuserSistema();
            if (user == null) return false;
            _helpers.Policy(TypeRol.Desarrollador, TypeRol.Dueño);
            using(var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("InsertarPromocion", new {
                    @IdPlan = promocion.IdPlan,
                    @IdUsuario = user.idUsuario,
                    @DescuentoPercent = promocion.DescuentoPercent,
                    @DuracionTiempo = promocion.DuracionTiempo,
                    @descripcion = promocion.descripcion
                }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }

        public async Task<bool> UpdatePromocion(PromocionModelo promocion)
        {
            var user = _config.getuserSistema();
            if (user == null) return false;
            _helpers.Policy(TypeRol.Dueño, TypeRol.Desarrollador);
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("UpdatePromocion",
                    new
                    {
                        @IdPlan = promocion.IdPlan,
                        @IdUsuario = user.idUsuario,
                        @DescuentoPercent = promocion.DescuentoPercent,
                        @DuracionTiempo = promocion.DuracionTiempo,
                        @descripcion = promocion.descripcion,
                        @idPromocion = promocion.IdPromocion
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }

        public async Task<bool> DeletePromocion(int idpromocion)
        {
            _helpers.Policy(TypeRol.Dueño, TypeRol.Desarrollador);
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("DeletePromocion",
                    new { @idPromocion = idpromocion }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }
    }
}
