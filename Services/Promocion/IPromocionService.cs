using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Promocion
{
    public interface IPromocionService
    {
        public Task<List<PromocionModelo>?> GetPromociones();
        public Task<List<PromocionModelo>?> GetPromocionesActive();
        public Task<bool> InsertPromocion(PromocionModelo promocion);
        public Task<bool> UpdatePromocion(PromocionModelo promocion);
        public Task<bool> DeletePromocion(int idpromocion);
    }
}
