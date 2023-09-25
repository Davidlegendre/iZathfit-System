using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RutinaModel
    {
        public Guid IdRutina { get; set; }
        public decimal MontoPago { get; set; }
        public int IdTipoPago { get; set; }
        public DateTime FechaPagoRutina { get; set; }
        public string TipoPago { get; set; }

        public string GetFechaRutina => "Rutina del: " + FechaPagoRutina.ToLongDateString();

        public string GetMonto => "Monto: " + MontoPago.ToString("0.00") + " S/";
       
    }
}
