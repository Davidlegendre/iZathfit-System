using Models;
using Models.DTOS;

namespace Domain.Rutina
{
    public interface IRutinaRepository
    {
        public Task<bool> InsertarRutina(RutinaModel rutina);
        public Task<List<TotalesByDateModel>?> GetTotalesByDate(DateTime fecha);
        public Task<bool> UpdateRutina(RutinaModel rutina);
        public Task<bool> DeleteRutina(Guid idrutina);
        public Task<List<RutinaModel>?> GetRutinasByFecha(DateTime fecha);
    }
}
