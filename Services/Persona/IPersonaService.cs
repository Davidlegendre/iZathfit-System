using Models;
using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Persona
{
    public interface IPersonaService
    {
        public  Task<Guid?> VerificarEmail(string? email);
        public Task<Guid?> InsertarPersona(PersonaModel? persona);
        public Task<int> UpdatePersona(PersonaModel? persona);
        public Task<List<PersonaModel>?> SelectAllPersonasNormal();
        public Task<List<PersonaDTO>?> SelectAllPersonsJoin();
        public Task<int> DeletePersona(Guid? idpersona);
    }
}
