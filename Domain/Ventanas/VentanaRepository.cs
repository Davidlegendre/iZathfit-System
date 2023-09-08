using Configuration;
using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ventanas
{
    public class VentanaRepository : IVentanaRepository
    {
        IGeneralConfiguration _generalConfiguration;
        public VentanaRepository(IGeneralConfiguration generalConfiguration)
        {
            _generalConfiguration = generalConfiguration;
        }

        public async Task<List<VentanaModel>?> GetVentanas() {
            using (var con = new SqlConnection(_generalConfiguration.GetConnection()))
            {
                var result = await con.QueryAsync<VentanaModel>("SelectAllVentana", commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result.Count() == 0 ? null : result.AsList();
            }
        }

        public async Task<VentanaModel?> GetOneVentana(int ID) {
            using (var con = new SqlConnection(_generalConfiguration.GetConnection()))
            {
                var result = await con.QueryFirstAsync<VentanaModel?>("SelectVentanaOne", new { @idVentana = ID },
                    commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }
        }
        public async Task<int> InsertarVentana(VentanaModel? ventanaModel)
        {
            if (ventanaModel == null) throw new ArgumentNullException("Ventana Model es nulo");
            using (var con = new SqlConnection(_generalConfiguration.GetConnection()))
            {
                var result = await con.ExecuteAsync("InsertarVentana", new
                {
                    @Titulo = ventanaModel.Titulo,
                    @Codigo = ventanaModel.Codigo
                }, commandType: CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }
        }

        public async Task<int> EliminarVentana(int ID, Guid idusuario)
        {
            using (var con = new SqlConnection(_generalConfiguration.GetConnection()))
            {
                var result = await con.ExecuteAsync("EliminarVentana", new {
                    @idVentana = ID,
                    @idUsuario = idusuario
                }, commandType: CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }
        }

        public async Task<int> ActualizarVentana(VentanaModel? ventanaModel)
        {
            if (ventanaModel == null) throw new ArgumentNullException("Ventana Model es nulo");
            using (var con = new SqlConnection(_generalConfiguration.GetConnection()))
            {
                var result = await con.ExecuteAsync("ActualizarVentana", new {
                    @Titulo = ventanaModel.Titulo,
                    @Codigo = ventanaModel.Codigo,
                    @idVentana = ventanaModel.idVentana
                });
                await con.CloseAsync();
                return result;
            }
        }
    }
}
