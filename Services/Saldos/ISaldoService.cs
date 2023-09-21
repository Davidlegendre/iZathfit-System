using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Saldos
{
    public interface ISaldoService
    {
        public Task<SaldosEstadisticasDTO?> GetEstadisticas(Guid idpersona);
    }
}
