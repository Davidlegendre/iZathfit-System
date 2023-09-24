using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS
{
    public class ContratosModDataView
    {
        public string NombreContrato { get; set; }
        public decimal PrecioTotal { get; set; }
        public string Servicios { get; set; }
        public decimal UltimoPago { get; set; }  
        public DateTime FechaContrato { get; set; }
        public decimal PagoFaltante { get; set; }
        public DateTime FechaFinal { get; set; }
        
        public string NombrePlan { get; set; }

        public string ColorString => PagoFaltante == 0 ? "#FF556B2F" : 
            (PagoFaltante != 0 && DateTime.Now.Date > FechaFinal) ? "#FFFF0000" : "#FF9ACD32";


        public int idPlan { get; set; }
        public string GetPrecioTotal => "Valor Total: " + PrecioTotal.ToString("0.00") + " S/";
        public string GetUtimoPago => "Ultimo Pago: " + UltimoPago.ToString("0.00") + " S/";
        public string GetFechaContrato => "Contratado el: " + FechaContrato.ToLongDateString();
        public string GetPagoFaltante => "Pago Faltante: " + PagoFaltante.ToString("0.00") + " S/";
        public string GetFechaFinal => "Fecha Final: " + FechaFinal.ToLongDateString();
       
    }
}
