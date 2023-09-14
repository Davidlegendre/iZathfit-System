using Configuration;
using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PadecimientosEnfermedades
{
    public class PadecimientosEnfermedadesRepository : IPadecimientosEnfermedadesRepository
    {
        IGeneralConfiguration _config;
        public PadecimientosEnfermedadesRepository(IGeneralConfiguration config)
        {
            _config = config;
        }

        public async Task<List<PadecimientoEnfermedades>?> GetAll() {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryAsync<PadecimientoEnfermedades>("SelectAllPadecimiento",
                    commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result.Count() == 0? null: result.AsList();
            }
        }

        public async Task<List<PadecimientoEnfermedades>?> GetPadecimientoEnfermedades(Guid idPersona) {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryAsync<PadecimientoEnfermedades>("SelectAllPadecimientoPersona",
                    new { @idpersona = idPersona }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result.Count() == 0? null : result.AsList();
            }
        }

        public async Task InsertarPadecimientosEnfermedades(List<PadecimientoEnfermedades> padecimientoEnfermedades)
        {
            using(var con = new SqlConnection(_config.GetConnection())) {

                foreach (var x in padecimientoEnfermedades)
                {
                    await con.ExecuteAsync("InsertPadecimientos", new
                    {
                        @idpersona = x.IdPersona,
                        @padecimiento = x.Padecimiento
                    }, commandType: System.Data.CommandType.StoredProcedure);
                }

                await con.CloseAsync();
                
            }
        }

        public async Task UpdatePadecimientosEnfermedades(List<PadecimientoEnfermedades> padecimientoEnfermedades)
        {
            await DeletePadecimientosEnfermedades(padecimientoEnfermedades[0].IdPersona);
            await InsertarPadecimientosEnfermedades(padecimientoEnfermedades);
        }

        public async Task DeletePadecimientosEnfermedades(Guid idpersona)
        {
            using(var con = new SqlConnection(_config.GetConnection()))
            {
                await con.ExecuteAsync("DeletePadecimientos", new
                {
                    @idPersona = idpersona
                }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
            }
        }
    }
}
