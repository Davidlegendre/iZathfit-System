using Domain.SaldosXPersona;
using Models;
using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SaldoXPersona
{
    public class SaldoXPersonaService : ISaldoXPersonaService
    {
        ISaldoXPersonaRepository _repo;
        public SaldoXPersonaService(ISaldoXPersonaRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> DeleteSaldoXPersona(SaldoXPersonaModel saldoXPersona)
        {
            return await _repo.DeleteSaldoXPersona(saldoXPersona);
        }

        public async Task<SaldosXpersonaEstidisticas?> GetEstadisticas(Guid IdPersona)
        {
            return await _repo.GetEstadisticas(IdPersona);
        }

        public async Task<List<SaldoXPersonaModel>?> GetSaldosXPersonasbyPersona(Guid idpersona)
        {
            return await _repo.GetSaldosXPersonasbyPersona(idpersona);
        }

        public async Task<List<SaldoXPersonaModel>?> GetSaldoXPersonas()
        {
            return await _repo.GetSaldoXPersonas();
        }

        public async Task<bool> InsertSaldoXPersona(SaldoXPersonaModel saldoxpersona)
        {
            return await _repo.InsertSaldoXPersona(saldoxpersona);
        }
    }
}
