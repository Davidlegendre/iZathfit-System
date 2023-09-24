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

namespace Domain.SaldosXPersona
{
    public class SaldoXPersonaRepository : ISaldoXPersonaRepository
    {
        IGeneralConfiguration _config;
        public SaldoXPersonaRepository(IGeneralConfiguration config)
        {
            _config = config;
        }

        public async Task<List<SaldoXPersonaModel>?> GetSaldoXPersonas() {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryAsync<SaldoXPersonaModel>("SelectAllSaldoXPersona",
                    commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result.Count() == 0 ? null : result.AsList();
            }
        }

        public async Task<List<SaldoXPersonaModel>?> GetSaldosXPersonasbyPersona(Guid idpersona) { 
        using(var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryAsync<SaldoXPersonaModel>("SelectAllSaldosXPersonaByPersona",
                    new { @Idpersona = idpersona }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result.Count() == 0? null : result.AsList();
            }
        }

        public async Task<bool> InsertSaldoXPersona(SaldoXPersonaModel saldoxpersona) {
            using(var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("insertSaldoXPersona", new
                {
                    @IdPersona = saldoxpersona.IdPersona,
                    @IdContrato = saldoxpersona.IdContrato,
                    @TotalPagadoActual = saldoxpersona.TotalPagadoActual,
                    @idtipopago = saldoxpersona.IdTipoPago
                }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }

        public async Task<bool> DeleteSaldoXPersona(SaldoXPersonaModel saldoXPersona) {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("deleteSaldoXPersona",
                    new
                    {
                        @IdPositionRow = saldoXPersona.IdPositionRow,
                        @idContrato = saldoXPersona.IdContrato,
                        @IdSaldo = saldoXPersona.IdSaldo
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }

        public async Task<SaldosXpersonaEstidisticas?> GetEstadisticas(Guid IdPersona) { 
            using(var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryFirstAsync<SaldosXpersonaEstidisticas?>("EstadisticasSaldosXPersona",
                    new { @idpersona = IdPersona }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }
        }

        public async Task<List<TotalesByDateModel>?> GetTotalesPagos(DateTime fecha) {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryAsync<TotalesByDateModel>("SumPagosGroupPersonaByDate",
                    new { @date = fecha }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result.Count() == 0? null : result.AsList();
            }
        } 
    }
}
