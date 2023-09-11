using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TipoIdentificacion
{
    public interface ITipoIdentificacionService
    {
        public Task<List<TipoIdentificacionModel>?> GetTipoIdentificaciones();
    }
}
