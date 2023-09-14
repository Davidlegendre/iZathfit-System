using Domain.PadecimientosEnfermedades;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PadecimientosEnfermedades
{
    public class PadecimientosEnfermedadesService : IPadecimientosEnfermedadesService
    {
        IPadecimientosEnfermedadesRepository _repo;
        public PadecimientosEnfermedadesService(IPadecimientosEnfermedadesRepository repo)
        {
            _repo = repo;
        }

        public async Task DeletePadecimientosEnfermedades(Guid idpersona)
        {
            await _repo.DeletePadecimientosEnfermedades(idpersona);
        }

        public async Task<List<PadecimientoEnfermedades>?> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<List<PadecimientoEnfermedades>?> GetPadecimientoEnfermedades(Guid idPersona)
        {
           return await _repo.GetPadecimientoEnfermedades(idPersona);
        }

        public async Task InsertarPadecimientosEnfermedades(List<PadecimientoEnfermedades> padecimientoEnfermedades)
        {
            await _repo.InsertarPadecimientosEnfermedades(padecimientoEnfermedades);
        }

        public async Task UpdatePadecimientosEnfermedades(List<PadecimientoEnfermedades> padecimientoEnfermedades)
        {
            await _repo.UpdatePadecimientosEnfermedades(padecimientoEnfermedades);
        }
    }
}
