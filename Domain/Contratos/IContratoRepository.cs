using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contratos
{
    public interface IContratoRepository
    {
        public Task<List<ContratoModel>?> GetContratos();
        public Task<List<ContratoModel>?> SelectOneContratoPerDNIPerson(string identificacion);
        public Task<bool> InsertContrato(ContratoModel contrato);
        public Task<bool> SetContratoNotValid(Guid idcontrato, bool idnotvalid, string description);
        public Task<bool> UpdateContrato(ContratoModel contrato);
        public Task<List<ContratoModel>?> SearchContratoByPersona(Guid IdPersona);
    }
}
