using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Saldos
{
    public interface ISaldosRepository
    {
        public Task<SaldosEstadisticasDTO?> GetEstadisticas(Guid idpersona);
    }
}
