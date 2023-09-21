using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Plan
{
    public interface IPlanRepository
    {
        public Task<List<PlanModel>?> GetPlanes();
        public Task<bool> InsertPlan(PlanModel plan);

        public Task<bool> UpdatePlan(PlanModel plan);
        public Task<bool> DeletePlan(int idplan);
        public Task<List<PlanModel>?> GetPlanesWithoutServices();
        public Task<PlanModel?> GetPlanByID(int idplan);
    }
}
