using Models;
using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Persona
{
    public interface IPersonaRepository
    {
        public Task<UsuarioSistema?> GetPersonaData(Guid? ID);
        public Task<Guid?> ConsultaPersonaByEmail(string? email);
        public Task<PersonaModel?> InsertarPersona(PersonaModel? persona);
        public Task<PersonaModel?> UpdatePersona(PersonaModel? persona);
        public Task<List<PersonaModel>?> SelectAllPersonas();
        public Task<int> DeletePersona(Guid? idpersona);
        public Task<PersonaModel> GetPersona(Guid id);
        public Task<long> GetCountPersonas();
        public Task<List<PersonaModel>> SelectAllAll();
    }
}
