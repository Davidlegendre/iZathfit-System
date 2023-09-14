using Configuration;
using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PlanDuracion
{
    public class PlanDuracionRepository : IPlanDuracionRepository
    {
        IGeneralConfiguration _config;
        public PlanDuracionRepository(IGeneralConfiguration config)
        {
            _config = config;
        }

        public async Task<List<PlanDuracionModel>?> GetPlanesDuracion() {
            using (var con = new SqlConnection(_config.GetConnection())) {
                var result = await con.QueryAsync<PlanDuracionModel>("SelectAllPlanDuracion",
                    commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result.Count() == 0 ? null : result.AsList();
            }
        }

        public async Task<bool> InsertPlanDuracion(PlanDuracionModel planDuracion)
        {
            var user = _config.getuserSistema();
            if (user == null) return false;
            using(var con = new SqlConnection(_config.GetConnection())) {
                var result = await con.ExecuteAsync("InsertPlanDuracion",
                    new
                    {
                        @descripcion = planDuracion.descripcion,
                        @IdUsuario = user.idUsuario,
                        @MesesTiempo = planDuracion.MesesTiempo
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }

        public async Task<bool> UpdatePlanDuracion(PlanDuracionModel planduracion)
        {
            var user = _config.getuserSistema();
            if (user == null) return false;
            using(var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("UpdatePlanDuracion",
                    new
                    {
                        @descripcion = planduracion.descripcion,
                        @IdUsuario = user.idUsuario,
                        @MesesTiempo = planduracion.MesesTiempo,
                        @idPlanduracion = planduracion.IdPlanDuracion
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }

        public async Task<bool> DeletePlanDuracion(int idPlanduracion)
        {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("DeletePlanDuracion",
                    new { @idplanduracion = idPlanduracion },
                    commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }
    }
}
