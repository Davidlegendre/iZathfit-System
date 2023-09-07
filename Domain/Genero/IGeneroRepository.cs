using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Genero
{
    public interface IGeneroRepository
    {
        public Task<List<GeneroModel>?> GetGeneros();
    }
}
