﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ACliente
{
    public interface IAClienteRepository
    {
        public Task<bool> InsertACliente(PersonaModel persona, SaldoXPersonaModel pago,
           List<PadecimientoEnfermedades> padecimientos, ContratoModel contrato);
    }
}
