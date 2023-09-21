using Configuration;
using Configuration.GlobalHelpers;
using Dapper;
using Models;
using System.Data.SqlClient;

namespace Domain.TipoPago
{
    public class TipoPagoRepository : ITipoPagoRepository
    {
        IGeneralConfiguration _config;
        IGlobalHelpers _helpers;
        public TipoPagoRepository(IGeneralConfiguration config, IGlobalHelpers helpers)
        {
            _config = config;
            _helpers = helpers;

        }

        public async Task<List<TipoPagoModel>?> GetTipoPagos() {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryAsync<TipoPagoModel>("SelectAllTipoPago", commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result.Count() > 0? result.AsList() : null;
            }
        }

        public async Task<TipoPagoModel?> GetTipoPagoByID(int idtipopago) { 
            using(var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryFirstAsync<TipoPagoModel?>("SelectOneTIpoPago",
                    new { @idtipopago = idtipopago }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }

        }

        public async Task<bool> InsertTipoPago(TipoPagoModel tipoapgo) {
            _helpers.Policy(TypeRol.Desarrollador, TypeRol.Dueño);
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("InsertTipoPago",
                    new
                    {
                        @descripcion = tipoapgo.descripcion
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }

        public async Task<bool> UpdateTipoPago(TipoPagoModel tipoapgo) {
            _helpers.Policy(TypeRol.Desarrollador, TypeRol.Dueño);
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("UpdateTipoPago",
                    new
                    {
                        @descripcion = tipoapgo.descripcion,
                        @idtipopago = tipoapgo.IdtipoPago
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }

        public async Task<bool> DeleteTipoPago(int idtipopago)
        {
            _helpers.Policy(TypeRol.Desarrollador, TypeRol.Dueño);
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("DeleteTipoPago",
                    new
                    {
                        @idtipopago = idtipopago
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }

        }


    }
}
