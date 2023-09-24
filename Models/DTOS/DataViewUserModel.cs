using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS
{
    public class DataViewUserModel
    {
       public PersonaModel? Persona { get; set; }

        //contratos
        public int Contratoscontrat { get; set; }

        //saldos
        public SaldosEstadisticasDTO? SaldosEstadisticasDTO { get;set; }
        //saldosxpersona
        public SaldosXpersonaEstidisticas? SaldosXpersonaEstidisticas { get; set; }

       

        public string GetContratosContratados => "Contratos: " + Contratoscontrat;
        public string GetContratosPagados => "Contratos Pagados: " + SaldosEstadisticasDTO?.ContratosPagados;
        public string GetContratosAdeudados => "Contratos Adeudos: " + SaldosEstadisticasDTO?.ContratosAdeudados;
        public string GetCantidadFaltante => "Cantidad por Pagar: " + SaldosEstadisticasDTO?.CantidadAdeudadaFaltante.ToString("0.00") +" S/";
        public string GetContratosVencidosNoPagos => "Contratos Vencido sin Pagar: " + SaldosEstadisticasDTO?.ContratosVencidosNoPagos;
        public string GetCantidadPagada => "Cantidad Pagada: " + SaldosEstadisticasDTO?.CantidadPagada.ToString("0.00") + " S/";

        public string GetUltimaFechadePago => "Ultima Fecha de Pago: " + (SaldosXpersonaEstidisticas?.UltimaFechadePago.Date == DateTime.MinValue.Date? "" : SaldosXpersonaEstidisticas?.UltimaFechadePago.ToLongDateString());
        public string GetUltimoContratoPagado => "Ultimo Contrato Pagado: " + SaldosXpersonaEstidisticas?.UltimoContratoPagado;
        
    }
}
