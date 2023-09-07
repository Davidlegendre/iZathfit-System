using Configuration;
using Dapper;
using Models;
using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Persona
{
    public class PersonaRepository : IPersonaRepository
    {
        readonly IGeneralConfiguration _generalConfiguration;
        public PersonaRepository(IGeneralConfiguration generalConfiguration)
        {
            _generalConfiguration = generalConfiguration;
        }

        public async Task<UsuarioSistema?> GetPersonaData(Guid? ID)
        {
            using (var con = new SqlConnection(_generalConfiguration.GetConnection()))
            { 
                var result = await con.QueryFirstAsync<UsuarioSistema?>("PersonaDatos", 
                    new { @ID = ID }, 
                    commandType: System.Data.CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<Guid?> ConsultaPersonaByEmail(string? email)
        { 
            using(var con = new SqlConnection (_generalConfiguration.GetConnection()))
            {
                var result = await con.ExecuteScalarAsync<Guid?>("VerificarEmailPersona", new { @email = email }, 
                    commandType: System.Data.CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
