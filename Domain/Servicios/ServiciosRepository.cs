using Configuration;
using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicios
{
    public class ServiciosRepository : IServiciosRepositoryI
    {
        IGeneralConfiguration _config;
        public ServiciosRepository(IGeneralConfiguration config)
        {
            _config = config;
        }

        public async Task<List<Models.ServicioModel>?> GetServicios() {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryAsync<Models.ServicioModel>("SelectAllServicios"
                    , commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result.Count() == 0 ? null : result.AsList();
            }
        }

        public async Task<bool> InsertServicios(ServicioModel servicio)
        {
            var user = _config.getuserSistema();
            if (user == null) return false;
            using(var con=new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("InsertServicios",
                    new
                    {
                        @nombreservicio = servicio.NombreServicio,
                        @HorarioInicio = servicio.HorarioInicio,
                        @HorarioFin = servicio.HorarioFin,
                        @IsGrupal = servicio.IsGrupal,
                        @IdUsuario = user.idUsuario
                    
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }

        public async Task<bool> UpdateServicio(ServicioModel servicio)
        {
            var user = _config.getuserSistema();
            if (user == null) return false;
            using (var con = new SqlConnection(_config.GetConnection())) {
                var result = await con.ExecuteAsync("UpdateServicios",
                    new
                    {
                        @nombreservicio = servicio.NombreServicio,
                        @HorarioInicio = servicio.HorarioInicio,
                        @HorarioFin = servicio.HorarioFin,
                        @IsGrupal = servicio.IsGrupal,
                        @IdUsuario = user.idUsuario,
                        @idservicios = servicio.IdServicio
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }

        public async Task<bool> DeleteServicio(int idservicio)
        {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("DeleteServicio",
                    new { @idservicio = idservicio }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }

    }
}
