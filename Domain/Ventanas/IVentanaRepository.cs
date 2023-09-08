using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ventanas
{
    public interface IVentanaRepository
    {
        public Task<List<VentanaModel>?> GetVentanas();
        public Task<VentanaModel?> GetOneVentana(int ID);
        public Task<int> InsertarVentana(VentanaModel? ventanaModel);
        public Task<int> EliminarVentana(int ID, Guid idusuario);
        public Task<int> ActualizarVentana(VentanaModel? ventanaModel);
    }
}
