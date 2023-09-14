using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TipoIdentificacion
{
    public interface ITipoIdentificacionRepository
    {
        public Task<List<TipoIdentificacionModel>?> GetTipoIdentificacion();
        public Task<TipoIdentificacionModel?> InsertTipoIdentificacion(TipoIdentificacionModel tipoidentificacion);
        public Task<TipoIdentificacionModel?> UpdateTipoIdentificacion(TipoIdentificacionModel tipoidentificacion);
        public Task<int> DeleteTipoIdentificacion(int IdTipoidentificacion);
    }
}
