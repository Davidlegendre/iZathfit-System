using Domain.Contratos;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contratos
{
    public class ContratosService : IContratosService
    {
        IContratoRepository _repo;
        public ContratosService(IContratoRepository repo)
        {
                _repo= repo;
        }

        public async Task<List<ContratoModel>?> GetContratos()
        {
            return await _repo.GetContratos();
        }

        public async Task<bool> InsertContrato(ContratoModel contrato)
        {
            return await _repo.InsertContrato(contrato);
        }

        public async Task<List<ContratoModel>?> SearchContratoByPersona(Guid IdPersona)
        {
            return await _repo.SearchContratoByPersona(IdPersona);
        }

        public async Task<List<ContratoModel>?> SelectOneContratoPerDNIPerson(string identificacion)
        {
            return await _repo.SelectOneContratoPerDNIPerson(identificacion);
        }

        public async Task<bool> SetContratoNotValid(Guid idcontrato, bool idnotvalid, string description)
        {
            return await _repo.SetContratoNotValid(idcontrato, idnotvalid, description);    
        }

        public async Task<bool> UpdateContrato(Guid idcontrato, DateTime fechafinal, string numerocontrato)
        {
            return await _repo.UpdateContrato(idcontrato, fechafinal, numerocontrato);
        }
    }
}
