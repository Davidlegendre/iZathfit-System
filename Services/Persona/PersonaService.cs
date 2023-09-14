using Domain.Persona;
using Models.DTOS;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Configuration;
using System.Linq;

namespace Services.Persona
{
    public class PersonaService : IPersonaService
    {
        IPersonaRepository _personarepo;
        IGeneralConfiguration _generalConfiguration;
        public PersonaService(IPersonaRepository personarepo, IGeneralConfiguration config)
        {
            _personarepo = personarepo;
            _generalConfiguration = config;
        }

        public async Task<Guid?> VerificarEmail(string? email)
        { 
            return await _personarepo.ConsultaPersonaByEmail(email);
        }

        public async Task<PersonaModel?> InsertarPersona(PersonaModel? persona)
        {
            return await _personarepo.InsertarPersona(persona);
        }
        public async Task<PersonaModel?> UpdatePersona(PersonaModel? persona)
        {
           return await _personarepo.UpdatePersona(persona);
        }

        public async Task<List<PersonaModel>?> SelectAllPersonasNormal()
        {
            
            var result = await _personarepo.SelectAllPersonas();
            return result?.Where(x=>x.IdPersona != _generalConfiguration.getuserSistema()?.IdPersona).ToList();
        }

        public async Task<int> DeletePersona(Guid? idpersona)
        {
            return await _personarepo.DeletePersona(idpersona);
        }

        public async Task<PersonaModel> GetPersona(Guid id)
        {
            return await _personarepo.GetPersona(id);  
        }

        public async Task<long> GetCountPersonas()
        {
            return await _personarepo.GetCountPersonas();
        }

        public async Task<List<PersonaModel>> SelectAllAll()
        {
            return await _personarepo.SelectAllAll();
        }
    }
}
