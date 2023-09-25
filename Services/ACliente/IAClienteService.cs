using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ACliente
{
    public interface IAClienteService
    {
        public Task<bool> InsertACliente(PersonaModel persona, SaldoXPersonaModel pago,
           List<PadecimientoEnfermedades> padecimientos, ContratoModel contrato);
    }
}
