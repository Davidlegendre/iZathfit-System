using Models;

namespace Domain.PadecimientosEnfermedades
{
    public interface IPadecimientosEnfermedadesRepository
    {
        public Task<List<PadecimientoEnfermedades>?> GetPadecimientoEnfermedades(Guid idPersona);
        public Task InsertarPadecimientosEnfermedades(List<PadecimientoEnfermedades> padecimientoEnfermedades);
        public Task UpdatePadecimientosEnfermedades(List<PadecimientoEnfermedades> padecimientoEnfermedades);
        public Task DeletePadecimientosEnfermedades(Guid idpersona);
        public Task<List<PadecimientoEnfermedades>?> GetAll();
    }
}
