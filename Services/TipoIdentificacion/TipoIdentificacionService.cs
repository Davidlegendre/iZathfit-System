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

        public async Task<List<TipoIdentificacionModel>?> GetTipoIdentificaciones() {
            return await _repository.GetTipoIdentificacion();
        }
    }
}
