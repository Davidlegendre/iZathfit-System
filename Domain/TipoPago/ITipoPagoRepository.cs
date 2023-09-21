using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TipoPago
{
    public interface ITipoPagoRepository
    {
        public Task<List<TipoPagoModel>?> GetTipoPagos();
        public Task<TipoPagoModel?> GetTipoPagoByID(int idtipopago);
        public Task<bool> InsertTipoPago(TipoPagoModel tipoapgo);
        public Task<bool> UpdateTipoPago(TipoPagoModel tipoapgo);
        public Task<bool> DeleteTipoPago(int idtipopago);
    }
}
