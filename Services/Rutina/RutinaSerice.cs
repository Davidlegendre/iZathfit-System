using Domain.Rutina;
using Models;
using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Rutina
{
    public class RutinaService : IRutinaService
    {
        IRutinaRepository _repo;
        public RutinaService(IRutinaRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<TotalesByDateModel>?> GetTotalesByDate(DateTime fecha)
        {
            return await _repo.GetTotalesByDate(fecha);
        }

        public async Task<bool> InsertarRutina(RutinaModel rutina)
        {
            return await _repo.InsertarRutina(rutina);
        }
    }
}
