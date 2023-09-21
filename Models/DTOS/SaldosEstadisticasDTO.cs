using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS
{
    public class SaldosEstadisticasDTO
    {
        public int ContratosPagados { get; set; }
        public int ContratosAdeudados { get; set; }
        public decimal CantidadAdeudadaFaltante { get; set; }
        public decimal CantidadPagada { get; set; }
        public int ContratosVencidosNoPagos { get; set; }
    }
}
