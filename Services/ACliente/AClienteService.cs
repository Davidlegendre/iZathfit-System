using Domain.ACliente;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ACliente
{
    public class AClienteService : IAClienteService
    {
        IAClienteRepository _repo;
        public AClienteService(IAClienteRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> InsertACliente(PersonaModel persona, SaldoXPersonaModel pago, List<PadecimientoEnfermedades> padecimientos, ContratoModel contrato)
        {
            return await _repo.InsertACliente(persona, pago, padecimientos, contrato);
        }
    }
}
