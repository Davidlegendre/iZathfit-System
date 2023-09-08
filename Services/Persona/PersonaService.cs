using Domain.Persona;
using Models.DTOS;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Persona
{
    public class PersonaService : IPersonaService
    {
        IPersonaRepository _personarepo;
        public PersonaService(IPersonaRepository personarepo)
        {
            _personarepo = personarepo;
        }

        public async Task<Guid?> VerificarEmail(string? email)
        { 
            return await _personarepo.ConsultaPersonaByEmail(email);
        }

        public async Task<Guid?> InsertarPersona(PersonaModel? persona)
        {
            return await _personarepo.InsertarPersona(persona);
        }
        public async Task<int> UpdatePersona(PersonaModel? persona)
        {
           return await _personarepo.UpdatePersona(persona);
        }

        public async Task<List<PersonaModel>?> SelectAllPersonasNormal()
        {
            return await _personarepo.SelectAllPersonasNormal();
        }

        public async Task<List<PersonaDTO>?> SelectAllPersonsJoin()
        {
           return await _personarepo.SelectAllPersonsJoin();
        }

        public async Task<int> DeletePersona(Guid? idpersona)
        {
            return await _personarepo.DeletePersona(idpersona);
        }
    }
}
