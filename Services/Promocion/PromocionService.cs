using Domain.Promocion;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Promocion
{
    public class PromocionService : IPromocionService
    {
        IPromocionRepository _repo;
        public PromocionService(IPromocionRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> DeletePromocion(int idpromocion)
        {
            return await _repo.DeletePromocion(idpromocion);
        }

        public async Task<List<PromocionModelo>?> GetPromociones()
        {
            return await _repo.GetPromociones();
        }

        public async Task<List<PromocionModelo>?> GetPromocionesActive()
        {
            return await _repo.GetPromocionesActive();
        }

        public async Task<bool> InsertPromocion(PromocionModelo promocion)
        {
            return await _repo.InsertPromocion(promocion);
        }

        public async Task<bool> UpdatePromocion(PromocionModelo promocion)
        {
            return await _repo.UpdatePromocion(promocion);
        }
    }
}
