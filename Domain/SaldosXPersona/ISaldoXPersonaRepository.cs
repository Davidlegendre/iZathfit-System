using Models;
using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SaldosXPersona
{
    public interface ISaldoXPersonaRepository
    {
        public Task<List<SaldoXPersonaModel>?> GetSaldoXPersonas();
        public Task<bool> InsertSaldoXPersona(SaldoXPersonaModel saldoxpersona);
        public Task<bool> DeleteSaldoXPersona(SaldoXPersonaModel saldoXPersona);
        public Task<SaldosXpersonaEstidisticas?> GetEstadisticas(Guid IdPersona);
        public Task<List<SaldoXPersonaModel>?> GetSaldosXPersonasbyPersona(Guid idpersona);
    }
}
