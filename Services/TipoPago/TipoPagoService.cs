using Domain.TipoPago;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TipoPago
{
    public class TipoPagoService : ITipoPagoService
    {
        ITipoPagoRepository _repo;
        public TipoPagoService(ITipoPagoRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> DeleteTipoPago(int idtipopago)
        {
            return await _repo.DeleteTipoPago(idtipopago);
        }

        public async Task<TipoPagoModel?> GetTipoPagoByID(int idtipopago)
        {
            return await _repo.GetTipoPagoByID(idtipopago);
        }

        public async Task<List<TipoPagoModel>?> GetTipoPagos()
        {
            return await _repo.GetTipoPagos();
        }

        public async Task<bool> InsertTipoPago(TipoPagoModel tipoapgo)
        {
            return await _repo.InsertTipoPago(tipoapgo);
        }

        public async Task<bool> UpdateTipoPago(TipoPagoModel tipoapgo)
        {
            return await _repo.UpdateTipoPago(tipoapgo);
        }
    }
}
