using Models.DTOS;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Rutina
{
    public interface IRutinaService
    {
        public Task<bool> InsertarRutina(RutinaModel rutina);
        public Task<List<TotalesByDateModel>?> GetTotalesByDate(DateTime fecha);
        public Task<bool> UpdateRutina(RutinaModel rutina);
        public Task<bool> DeleteRutina(Guid idrutina);
        public Task<List<RutinaModel>?> GetRutinasByFecha(DateTime fecha);
    }
}
