using Domain.Ventanas;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Ventana
{
    public class VentanaService : IVentanaService
    {
        IVentanaRepository _repo;
        public VentanaService(IVentanaRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<VentanaModel>?> GetVentanas() { 
            return await _repo.GetVentanas();
        }
    }
}
