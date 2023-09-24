using Models;
using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Rutina
{
    public interface IRutinaRepository
    {
        public Task<bool> InsertarRutina(RutinaModel rutina);
        public Task<List<TotalesByDateModel>?> GetTotalesByDate(DateTime fecha);
    }
}
