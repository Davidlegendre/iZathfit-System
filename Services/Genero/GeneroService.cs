using Domain.Genero;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Genero
{
    public class GeneroService : IGeneroService
    {
        readonly IGeneroRepository _generoRepository;
        public GeneroService(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        public async Task<List<GeneroModel>?> GetGeneros()
        {
            return await _generoRepository.GetGeneros();
        }
    }
}
