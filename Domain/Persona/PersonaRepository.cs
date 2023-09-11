using Configuration;
using Dapper;
using Domain.Genero;
using Domain.Rol;
using Domain.TipoIdentificacion;
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
        IRolRepository _rolrepo;
        ITipoIdentificacionRepository _tipoIdentRepo;
        IGeneroRepository _generorepo;
        public PersonaRepository(IGeneralConfiguration generalConfiguration, 
            IRolRepository rolRepository, 
            ITipoIdentificacionRepository tipoIdentificacionRepository,
            IGeneroRepository generoRepository)
        {
            _generalConfiguration = generalConfiguration;
            _rolrepo = rolRepository;
            _generorepo = generoRepository;
            _tipoIdentRepo = tipoIdentificacionRepository;
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
        public async Task<PersonaModel?> InsertarPersona(PersonaModel? persona)
        {
            if (persona == null) throw new ArgumentNullException("Persona Es nula");
            using (var con = new SqlConnection(_generalConfiguration.GetConnection()))
            {
                var result = await con.QueryFirstAsync<PersonaModel?>("AgregarPersona", 
                    new { @Nombres=persona.Nombres, @Apellidos = persona.Apellidos,
                        @idrol = persona.IdRol,
                        @Fech_nac = persona.Fech_Nacimiento,
                        @Direccion = persona.Direccion,
                        @telefono = persona.Telefono,
                        @Email = persona.Email,
                        @idgenero = persona.IdGenero,
                        @Identificacion = persona.Identificacion,
                        @idTipoIdent = persona.IdTipoIdentity
                    }, 
                    commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }
        }
        public async Task<PersonaModel?> UpdatePersona(PersonaModel? persona)
        {
            if (persona == null) throw new ArgumentNullException("Persona Es nula");
            using (var con = new SqlConnection(_generalConfiguration.GetConnection()))
            {
                var result = await con.QueryFirstAsync<PersonaModel?>("UpdatePersonaData", 
                    new {
                        @Nombres = persona.Nombres,
                        @Apellidos = persona.Apellidos,
                        @Fech_nac = persona.Fech_Nacimiento,
                        @Direccion = persona.Direccion,
                        @idRol = persona.IdRol,
                        @telefono = persona.Telefono,
                        @Email = persona.Email,
                        @idgenero = persona.IdGenero,
                        @Identificacion = persona.Identificacion,
                        @idTipoIdent = persona.IdTipoIdentity,
                        @id = persona.IdPersona
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }
        }

        public async Task<List<PersonaModel>?> SelectAllPersonas() {
            using (var con = new SqlConnection(_generalConfiguration.GetConnection()))
            {
                var results = await con.QueryAsync<PersonaModel>("SelectAllPersonas", 
                    commandType: System.Data.CommandType.StoredProcedure);

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
