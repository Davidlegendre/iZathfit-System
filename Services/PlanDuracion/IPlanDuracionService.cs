using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PlanDuracion
{
    public interface IPlanDuracionService
    {
        public Task<List<PlanDuracionModel>?> GetPlanesDuracion();
        public Task<bool> InsertPlanDuracion(PlanDuracionModel planDuracion);
        public Task<bool> UpdatePlanDuracion(PlanDuracionModel planduracion);
        public Task<bool> DeletePlanDuracion(int idPlanduracion);
    }
}
