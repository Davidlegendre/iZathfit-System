using Models.ServiciodeModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ContratoModel
    {
        
        public Guid IdContrato { get; set; }
        public int IdPlan { get; set; }
        public Guid IdPersona { get; set; }
        public Guid IdUsuario { get; set; }
        public decimal ValorTotal { get; set; }
        public int Descuento { get; set; }
        public decimal ValorOriginal { get; set; }
        public DateTime FechaInicio { get; set; }  
        public DateTime FechaFinal { get; set; }
        public DateTime FechaFinalReal { get; set; }
        public DateTime Fecha_contrato { get; set; }
        public string NumeroContrato { get; set; }
        public int IdTipoPago { get; set; }
        public bool IsNotValid { get; set; }
        public bool IsDueñoOrDesarrolladorAndIsValid => IsDueñoOrDesarrollador && !IsNotValid;
        public bool IsDueñoOrDesarrolladorAndIsNotValid => IsDueñoOrDesarrollador && IsNotValid;
        public string? GetValidoString => IsNotValid ? "No Valido" : "Valido";
        public bool IsDueñoOrDesarrollador { get; set; }
        public string GetNombreContrato => "Contrato: " + NumeroContrato;
        public string GetValorTotal => "Total: " + ValorTotal;
        public string GetFechaInicio => "Empezo el: " + FechaInicio.ToLongDateString();
        public string GetFechaFinal => "Termina el: " + FechaFinal.ToLongDateString();
        public string GetFechaFinalReal => "Fecha Final Real: " + FechaFinalReal.ToLongDateString();
        public string GetTotalFaltante => "Falta: " + TotalFaltante.ToString("0.00") + " S/";

        public string? GetBackgroundIfIsVencido => (TotalFaltante != 0 && DateTime.Now.Date > FechaFinal) ? "#FFFF0000" : null;
        public string DescripcionIsNotValid { get; set; }
        public string PersonaNombres { get; set; }
        public string Identificacion { get; set; }
        public string TipoPago { get; set; }
        public decimal PrecioPlan { get; set; }
        public int PlanDuracion { get; set; }
        public decimal TotalFaltante { get; set; }

        public string GetMesesTiempoString => NotificadorServicesInModels.TransformMonthsToString(PlanDuracion);
    }
}
