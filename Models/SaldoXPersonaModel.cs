using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SaldoXPersonaModel
    {
        public Guid IdPersona { get; set; }
        public Guid IdContrato { get; set; }
        public Guid IdSaldo { get; set; }
        public int IdPositionRow { get; set; }
        public int IdTipoPago { get; set; }
        public bool IsNotValid { get; set; }
        public decimal TotalPagadoActual { get; set; }
        public string NombrePersona { get; set; }   
        public string Identificacion { get; set; }
        public string NumeroContrato { get; set; }
        public decimal totalContrato { get; set; }
        public decimal TotalFaltante { get; set; }
        public decimal TotalPagado { get; set; }
        public string TipoPago { get; set; }
        public DateTime FechaPago { get; set; }
        public string GetIsCompletePagado => TotalFaltante == 0 ? "Contrato Pagado" : "Aun Falta";

        public bool IsVisible { get; set; }

        public string GetIdentificacion => "Id: " + Identificacion;
        public string GetNumeroContrato => "Contrato: " + NumeroContrato;
        public string GetTotalContrato => "Total Contrato: " + totalContrato.ToString("0.00") + " S/";
        public string GetTotalFaltante => "Faltan: " + TotalFaltante.ToString("0.00") + " S/";
        public string GetTotalPagado => "Pagado: " + TotalPagadoActual.ToString("0.00") + " S/";
        public string GetTipoPago => "Tipo Pago: " + TipoPago;

        public string GetFechaPago => "Fecha de Pago: " + FechaPago.ToLongDateString();

    }
}
