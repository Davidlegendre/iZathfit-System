using Domain.PlanDuracion;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PlanDuracion
{
    public class PlanDuracionService : IPlanDuracionService
    {
        IPlanDuracionRepository _repo;
        public PlanDuracionService(IPlanDuracionRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> DeletePlanDuracion(int idPlanduracion)
        {
            return await _repo.DeletePlanDuracion(idPlanduracion);
        }

        public async Task<List<PlanDuracionModel>?> GetPlanesDuracion()
        {
            return await _repo.GetPlanesDuracion();
        }

        public async Task<bool> InsertPlanDuracion(PlanDuracionModel planDuracion)
        {
            return await _repo.InsertPlanDuracion(planDuracion);
        }

        public async Task<bool> UpdatePlanDuracion(PlanDuracionModel planduracion)
        {
            return await _repo.UpdatePlanDuracion(planduracion);
        }
    }
}
