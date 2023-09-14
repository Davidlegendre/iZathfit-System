using Configuration;
using Configuration.GlobalHelpers;
using Dapper;
using Models;
using System.Data.SqlClient;

namespace Domain.Ocupaciones;

public class OcupacionRepository : IOcupacionRepository
{
    IGeneralConfiguration _config;
    IGlobalHelpers _helper;
    public OcupacionRepository(IGeneralConfiguration config, IGlobalHelpers helpers)
    {
        _config = config;
        _helper = helpers;
    }

    public async Task<List<Ocupacion>?> GetOcupaciones() {
        using (var con = new SqlConnection(_config.GetConnection()))
        {
            var result = await con.QueryAsync<Ocupacion>("SelectAllOcupacion", 
                commandType: System.Data.CommandType.StoredProcedure);
            await con.CloseAsync();
            return result != null ? result.AsList() : null;
        }
    }

    public async Task<Ocupacion?> InsertOcupacion(Ocupacion ocupacion) {
       
        using(var con = new SqlConnection(_config.GetConnection()))
        {
            var result = await con.QueryFirstAsync<Ocupacion?>("AgregarOcupacion", new
            {
                @descripcion = ocupacion.descripcion
            }, commandType: System.Data.CommandType.StoredProcedure);
            await con.CloseAsync();
            return result;
        }
    }

    public async Task<Ocupacion?> UpdateOcupacion(Ocupacion ocupacion)
    {
        using (var con = new SqlConnection(_config.GetConnection()))
        {
            var result = await con.QueryFirstAsync<Ocupacion?>("UpdateOcupacion", new
            {
                @descripcion = ocupacion.descripcion,
                @idocupacion = ocupacion.IdOcupacion
            }, commandType: System.Data.CommandType.StoredProcedure);
            await con.CloseAsync();
            return result;
        }
    }

    public async Task<int> DeleteOcupacion(int idocupacion)
    {
        using (var con = new SqlConnection(_config.GetConnection()))
        {
            var result = await con.ExecuteAsync("DeleteOcupacion", new { @idocupacion = idocupacion },
                commandType: System.Data.CommandType.StoredProcedure);
            await con.CloseAsync();
            return result;
        }
    }

   
}
