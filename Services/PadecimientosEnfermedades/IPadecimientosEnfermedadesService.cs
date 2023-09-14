using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PadecimientosEnfermedades
{
    public interface IPadecimientosEnfermedadesService
    {
        public Task<List<PadecimientoEnfermedades>?> GetPadecimientoEnfermedades(Guid idPersona);
        public Task InsertarPadecimientosEnfermedades(List<PadecimientoEnfermedades> padecimientoEnfermedades);
        public Task UpdatePadecimientosEnfermedades(List<PadecimientoEnfermedades> padecimientoEnfermedades);
        public Task DeletePadecimientosEnfermedades(Guid idpersona);
        public Task<List<PadecimientoEnfermedades>?> GetAll();
    }
}
