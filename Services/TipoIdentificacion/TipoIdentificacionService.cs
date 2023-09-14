using Domain.TipoIdentificacion;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TipoIdentificacion
{
    public class TipoIdentificacionService : ITipoIdentificacionService
    {
       ITipoIdentificacionRepository _repository;
        public TipoIdentificacionService(ITipoIdentificacionRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> DeleteTipoIdentificacion(int IdTipoidentificacion)
        {
            return await _repository.DeleteTipoIdentificacion(IdTipoidentificacion);
        }

        public async Task<List<TipoIdentificacionModel>?> GetTipoIdentificaciones() {
            return await _repository.GetTipoIdentificacion();
        }

        public async Task<TipoIdentificacionModel?> InsertTipoIdentificacion(TipoIdentificacionModel tipoidentificacion)
        {
            return await _repository.InsertTipoIdentificacion(tipoidentificacion);
        }

        public async Task<TipoIdentificacionModel?> UpdateTipoIdentificacion(TipoIdentificacionModel tipoidentificacion)
        {
            return await _repository.UpdateTipoIdentificacion(tipoidentificacion);
        }
    }
}
