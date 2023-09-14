using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PlanDuracionModel
    {
        public int IdPlanDuracion { get; set; }
        public string? descripcion { get; set; }
        public Guid IdUsuario { get; set; }
        public int MesesTiempo { get; set; }
        public string GetMesesTiempoString => MesesTiempo >= 12 ? "1 Año" : (MesesTiempo == 1) ? "1 Mes" : MesesTiempo + " Meses";
    }
}
