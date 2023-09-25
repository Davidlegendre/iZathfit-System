using Configuration;
using Dapper;
using Models;
using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Rutina
{
    public class RutinaRepository : IRutinaRepository
    {
        IGeneralConfiguration _config;
        public RutinaRepository(IGeneralConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> InsertarRutina(RutinaModel rutina) {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("InsertRutina",
                    new { @pago = rutina.MontoPago, @idtipopago = rutina.IdTipoPago },
                    commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }

        public async Task<bool> UpdateRutina(RutinaModel rutina)
        { 
            using(var con = new SqlConnection(_config.GetConnection())) {
                var result = await con.ExecuteAsync("UpdateRutina",
                    new
                    {
                        @pago = rutina.MontoPago,
                        @idtipopago = rutina.IdTipoPago,
                        @idRutina = rutina.IdRutina
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }

        public async Task<bool> DeleteRutina(Guid idrutina)
        { 
            using(var con = new SqlConnection(_config.GetConnection())) {
                var result = await con.ExecuteAsync("DeleteRutina", new {
                    @idrutina = idrutina
                }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }

        public async Task<List<RutinaModel>?> GetRutinasByFecha(DateTime fecha)
        { 
            using(var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryAsync<RutinaModel>("SelectAllRutina",
                    new { 
                        @fecha = fecha
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result.Count() == 0 ? null : result.AsList();
            }
        }

        public async Task<List<TotalesByDateModel>?> GetTotalesByDate(DateTime fecha)
        {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryAsync<TotalesByDateModel>("SumRutinasByDate",
                    new { @date = fecha }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result.Count() == 0? null : result.AsList();
            }
        }
    }
}
