using Configuration;
using Configuration.GlobalHelpers;
using Dapper;
using Domain.Genero;
using Domain.Rol;
using Domain.TipoIdentificacion;
using Models;
using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Persona
{
    public class PersonaRepository : IPersonaRepository
    {
        readonly IGeneralConfiguration _generalConfiguration;
        IRolRepository _rolrepo;
        ITipoIdentificacionRepository _tipoIdentRepo;
        IGeneroRepository _generorepo;
        IGlobalHelpers _helper;
        public PersonaRepository(IGeneralConfiguration generalConfiguration, 
            IRolRepository rolRepository, 
            ITipoIdentificacionRepository tipoIdentificacionRepository,
            IGeneroRepository generoRepository, IGlobalHelpers globalHelpers)
        {
            _generalConfiguration = generalConfiguration;
            _rolrepo = rolRepository;
            _generorepo = generoRepository;
            _tipoIdentRepo = tipoIdentificacionRepository;
            _helper = globalHelpers;
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
                var result = await con.ExecuteScalarAsync<Guid?>("VerificarEmailPersona", 
                    new { @email = email }, 
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
                  new
                  {
                      @Nombres = persona.Nombres,
                      @Apellidos = persona.Apellidos,
                      @idrol = persona.IdRol,
                      @Fech_nac = persona.Fech_Nacimiento.Date,
                      @Direccion = persona.Direccion,
                      @telefono = persona.Telefono,
                      @Email = persona.Email,
                      @idgenero = persona.IdGenero,
                      @Identificacion = persona.Identificacion,
                      @idTipoIdent = persona.IdTipoIdentity,
                      @idocupacion = persona.IdOcupacion,
                      @numemergencia1 = persona.NumeroEmergencia1,
                      @numemergencia2 = persona.NumeroEmergencia2
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
                        @Fech_nac = persona.Fech_Nacimiento.Date,
                        @Direccion = persona.Direccion,
                        @idRol = persona.IdRol,
                        @telefono = persona.Telefono,
                        @Email = persona.Email,
                        @idgenero = persona.IdGenero,
                        @Identificacion = persona.Identificacion,
                        @idTipoIdent = persona.IdTipoIdentity,
                        @idocupacion = persona.IdOcupacion,
                        @numemergencia1 = persona.NumeroEmergencia1,
                        @numemergencia2 = persona.NumeroEmergencia2,
                        @id = persona.IdPersona
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }
        }

        public async Task<bool> UpdatePersonaSistema(PersonaModel persona)
        {
            using (var con = new SqlConnection(_generalConfiguration.GetConnection())) {
                var result = await con.ExecuteAsync("UpdatePersonaDatosPerfil",
                    new
                    {
                        @nombre = persona.Nombres,
                        @apellido = persona.Apellidos,
                        @fechnacimiento = persona.Fech_Nacimiento.Date,
                        @direccion = persona.Direccion,
                        @telefono = persona.Telefono,
                        @email = persona.Email,
                        @idpersona = persona.IdPersona,
                        @idgenero = persona.IdGenero
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }

        /*Dueño => solo ve administradores y clientes
         Desarrollador => Dueños, Administradores y clientes
        Administrador => clientes*/

        public async Task<List<PersonaModel>?> SelectAllPersonas() {
            _helper.Policy(TypeRol.Desarrollador, TypeRol.Administrador, TypeRol.Dueño);
            using (var con = new SqlConnection(_generalConfiguration.GetConnection()))
            {
                var results = await con.QueryAsync<PersonaModel>("SelectAllPersonas", 
                    commandType: System.Data.CommandType.StoredProcedure);

                await con.CloseAsync();
                return  results.Count() == 0 ? null : results.AsList();
            }
        }

        public async Task<List<PersonaModel>> SelectAllAll() {
            using (var con = new SqlConnection(_generalConfiguration.GetConnection()))
            {
                var result = await con.QueryAsync<PersonaModel>("SelectAllPersonas",
                    commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result.AsList();
            }
        }

        public async Task<PersonaModel> GetPersona(Guid id)
        {
            using (var con = new SqlConnection(_generalConfiguration.GetConnection())) {
                var result = await con.QueryFirstAsync<PersonaModel>("SelectOnePersona", new { @id = id }, 
                    commandType: System.Data.CommandType.StoredProcedure);

                await con.CloseAsync();
                return result;
            }
        }

        public async Task<long> GetCountPersonas() {
            using (var con = new SqlConnection(_generalConfiguration.GetConnection()))
            {
                var result = await con.ExecuteScalarAsync<long>("CountPersonas", commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
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

        public async Task<List<PersonaModel>?> SearchPersonaByNameLastNameIdenfity(string texto) { 
            using(var con = new SqlConnection(_generalConfiguration.GetConnection())) {
                var result = await con.QueryAsync<PersonaModel>("SearchPersonaByNameLastNameIdentify",
                    new {
                        @Texto= texto
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result.Count() == 0? null: result.AsList();
            }
        }
    }
}
