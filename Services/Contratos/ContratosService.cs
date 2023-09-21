﻿using Domain.Contratos;
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
        public async Task<bool> EliminarContrato(Guid idcontrato)
        {
            return await _repo.EliminarContrato(idcontrato);
        }

        public async Task<List<ContratoModel>?> GetContratos()
        {
            return await _repo.GetContratos();
        }

        public async Task<bool> InsertContrato(ContratoModel contrato)
        {
            return await _repo.InsertContrato(contrato);
        }

        public async Task<List<ContratoModel>?> SelectOneContratoPerDNIPerson(string identificacion)
        {
            return await _repo.SelectOneContratoPerDNIPerson(identificacion);
        }

        public async Task<bool> SetContratoNotValid(Guid idcontrato, bool idnotvalid, string description)
        {
            return await _repo.SetContratoNotValid(idcontrato, idnotvalid, description);    
        }
    }
}
