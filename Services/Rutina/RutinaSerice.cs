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

        public async Task<bool> DeleteRutina(Guid idrutina)
        {
            return await _repo.DeleteRutina(idrutina);
        }

        public async Task<List<RutinaModel>?> GetRutinasByFecha(DateTime fecha)
        {
            return await _repo.GetRutinasByFecha(fecha);
        }

        public async Task<List<TotalesByDateModel>?> GetTotalesByDate(DateTime fecha)
        {
            return await _repo.GetTotalesByDate(fecha);
        }

        public async Task<bool> InsertarRutina(RutinaModel rutina)
        {
            return await _repo.InsertarRutina(rutina);
        }

        public async Task<bool> UpdateRutina(RutinaModel rutina)
        {
            return await _repo.UpdateRutina(rutina);
        }
    }
}
