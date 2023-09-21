using Domain.Plan;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Plan
{
    public class PlanService : IPlanService
    {
        IPlanRepository _repo;
        public PlanService(IPlanRepository repo)
        {
            _repo = repo;
        }

        public  async Task<bool> DeletePlan(int idplan)
        {
            return await _repo.DeletePlan(idplan);
        }

        public async Task<PlanModel?> GetPlanByID(int idplan)
        {
           return await _repo.GetPlanByID(idplan);
        }

        public async Task<List<PlanModel>?> GetPlanes()
        {
            return await _repo.GetPlanes();
        }

        public async Task<List<PlanModel>?> GetPlanesWithoutServices()
        {
            return await _repo.GetPlanesWithoutServices();
        }

        public async Task<bool> InsertPlan(PlanModel plan)
        {
           return await _repo.InsertPlan(plan);
        }
        public async Task<bool> UpdatePlan(PlanModel plan)
        {
            return await _repo.UpdatePlan(plan);
        }
    }
}
