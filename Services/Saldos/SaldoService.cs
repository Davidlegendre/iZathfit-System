using Domain.Saldos;
using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Saldos
{
    public class SaldoService : ISaldoService
    {
        ISaldosRepository _repo;
        public SaldoService(ISaldosRepository repo)
        {
            _repo = repo;
        }

        public async Task<SaldosEstadisticasDTO?> GetEstadisticas(Guid idpersona)
        {
           return await _repo.GetEstadisticas(idpersona);   
        }
    }
}
