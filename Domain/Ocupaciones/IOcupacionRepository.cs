using Models;

namespace Domain.Ocupaciones
{
    public interface IOcupacionRepository
    {
        public Task<List<Ocupacion>?> GetOcupaciones();
        public Task<Ocupacion?> InsertOcupacion(Ocupacion ocupacion);
        public Task<Ocupacion?> UpdateOcupacion(Ocupacion ocupacion);
        public Task<int> DeleteOcupacion(int idocupacion);
    }
}
