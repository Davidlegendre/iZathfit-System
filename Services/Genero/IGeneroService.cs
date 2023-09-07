using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Genero
{
    public interface IGeneroService
    {
        public Task<List<GeneroModel>?> GetGeneros();
    }
}
