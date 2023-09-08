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
                await con.CloseAsync();
                return result;
            }
        }

        public async Task<Guid?> ConsultaPersonaByEmail(string? email)
        { 
            using(var con = new SqlConnection (_generalConfiguration.GetConnection()))
            {
                var result = await con.ExecuteScalarAsync<Guid?>("VerificarEmailPersona", new { @email = email }, 
                    commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }
        }
        public async Task<Guid?> InsertarPersona(PersonaModel? persona)
        {
            if (persona == null) throw new ArgumentNullException("Persona Es nula");
            using (var con = new SqlConnection(_generalConfiguration.GetConnection()))
            {
                var result = await con.ExecuteScalarAsync<Guid?>("AgregarPersona", 
                    new { @Nombres=persona.Nombres, @Apellidos = persona.Apellidos,
                        @idrol = persona.idRol,
                        @Fech_nac = persona.Fech_Nacimiento,
                        @Direccion = persona.Direccion,
                        @telefono = persona.Telefono,
                        @Email = persona.Email,
                        @idgenero = persona.idGenero,
                        @Identificacion = persona.Identificacion,
                        @idTipoIdent = persona.idtipoIdentificacion
                    }, 
                    commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }
        }
        public async Task<int> UpdatePersona(PersonaModel? persona)
        {
            if (persona == null) throw new ArgumentNullException("Persona Es nula");
            using (var con = new SqlConnection(_generalConfiguration.GetConnection()))
            {
                var result = await con.ExecuteAsync("UpdatePersonaData", 
                    new {
                        @Nombres = persona.Nombres,
                        @Apellidos = persona.Apellidos,
                        @Fech_nac = persona.Fech_Nacimiento,
                        @Direccion = persona.Direccion,
                        @idRol = persona.idRol,
                        @telefono = persona.Telefono,
                        @Email = persona.Email,
                        @idgenero = persona.idGenero,
                        @Identificacion = persona.Identificacion,
                        @idTipoIdent = persona.idtipoIdentificacion,
                        @id = persona.IdPersona
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }
        }

        public async Task<List<PersonaModel>?> SelectAllPersonasNormal() {
            using (var con = new SqlConnection(_generalConfiguration.GetConnection()))
            {
                var results = await con.QueryAsync<PersonaModel>("SelectAllPersonasNormal", 
                    commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return  results.Count() == 0 ? null : results.AsList();
            }
        }

        public async Task<List<PersonaDTO>?> SelectAllPersonsJoin() {
            using (var con = new SqlConnection(_generalConfiguration.GetConnection()))
            {
                var results = await con.QueryAsync<PersonaDTO>("SelectAllPersonsJoin", commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return  results.Count() == 0 ? null : results.AsList();
            }
        }

        public async Task<int> DeletePersona(Guid? idpersona)
        {
            if (idpersona == null) throw new ArgumentNullException("IdPersona esta nula");
            using (var con = new SqlConnection(_generalConfiguration.GetConnection()))
            {
                var result = await con.ExecuteAsync("DeletePersona",
                    new { @idPersona = idpersona }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }
        }
    }
}
