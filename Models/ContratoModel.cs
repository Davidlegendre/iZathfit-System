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
        public DateTime Fecha_contrato { get; set; }
        public string NumeroContrato { get; set; }
        public int IdTipoPago { get; set; }
        public bool IsNotValid { get; set; }
        public bool IsValid => !IsNotValid;
        public string? GetValidoString => IsNotValid ? "No Valido" : "Valido";
        public bool IsDueñoOrDesarrollador { get; set; }
        public string GetNombreContrato => "Contrato: " + NumeroContrato;
        public string GetValorTotal => "Total: " + ValorTotal;
        public string GetFechaInicio => "Empezo el: " + FechaInicio.ToLongDateString();
        public string DescripcionIsNotValid { get; set; }
        public string PersonaNombres { get; set; }
        public string Identificacion { get; set; }
        public string TipoPago { get; set; }
        public decimal PrecioPlan { get; set; }
    }
}
