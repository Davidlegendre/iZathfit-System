using Configuration;
using Dapper;
using Models;
using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Plan
{
    public class PlanRepository : IPlanRepository
    {
        IGeneralConfiguration _config;
        public PlanRepository(IGeneralConfiguration config)
        {
            _config = config;
        }

        public async Task<List<PlanModel>?> GetPlanes()
        {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryAsync<PlanModel>("SelectAllPlanes",
                    commandType: System.Data.CommandType.StoredProcedure);

                foreach (var plan in result)
                {
                    var servicios = await con.QueryAsync<ServicioModel>("SelectOneServicioXPlanes",
                        new { @idplan = plan.IdPlanes }, commandType: System.Data.CommandType.StoredProcedure);

                    plan.Servicios = servicios.AsList();

                }

                await con.CloseAsync();
                return result.Count() == 0 ? null : result.AsList();
            }
        }

        public async Task<bool> InsertPlan(PlanModel plan)
        {
            if (plan.Servicios == null) return false;
            var user = _config.getuserSistema();
            if (user == null) return false;
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var dt = new DataTable();
                dt.Columns.Add("IdServicio", typeof(int));
                plan.Servicios.Select(x => x.IdServicio).AsList().ForEach(x => dt.Rows.Add(x));

                var result = await con.ExecuteAsync("InsertarPlanes",
                    new
                    {
                        @ServiciosType =  dt.AsTableValuedParameter("dbo.ServiciosType"),
                        @idplanduracion = plan.IdPlanDuracion,
                        @idusuario = user.idUsuario,
                        @precio = plan.Precio,
                        @descripcion = plan.descripcion
                    }, commandType: System.Data.CommandType.StoredProcedure);

                await con.CloseAsync();
                return result != 0;
            }
        }

        public async Task<bool> UpdatePlan(PlanModel plan)
        {
            if (plan.Servicios == null) return false;
            var user = _config.getuserSistema();
            if (user == null) return false;
            using (var con = new SqlConnection(_config.GetConnection()))
            {

                var dt = new DataTable();
                dt.Columns.Add("IdServicio", typeof(int));
                plan.Servicios.Select(x => x.IdServicio).AsList().ForEach(x => dt.Rows.Add(x));

                var result = await con.ExecuteAsync("UpdatePlanes",
                    new
                    {
                        @ServiciosType = dt.AsTableValuedParameter("dbo.ServiciosType"),
                        @idplanduracion = plan.IdPlanDuracion,
                        @idusuario = user.idUsuario,
                        @precio = plan.Precio,
                        plan.descripcion,
                        @idplan = plan.IdPlanes
                    }, commandType: System.Data.CommandType.StoredProcedure);

                await con.CloseAsync();
                return result != 0;
            }
        }

        public async Task<bool> DeletePlan(int idplan)
        {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("DeletePlan",
                    new { idplan }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }
    }
}
