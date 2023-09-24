using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ServiciodeModelos
{
    public class NotificadorServicesInModels
    {
        public static void NotificarTiempoAModelos() { 
            if(TimeEvent != null)
                TimeEvent.Invoke(null, new EventArgs());
        }

        public static void NotificarEliminaciondePromo(PromocionModelo promo) {
            if (PromoEliminarEvent != null)
                PromoEliminarEvent.Invoke(null, promo);
        }

        public static void NotificarPromos(ObservableCollection<PromocionModelo> promos) { 
            if(PromosEvent != null)
                PromosEvent.Invoke(null, promos);
        }

       public static string TransformMonthsToString(int Months)
        {
            //10 Años y 2 Meses
            if (Months < 12) return Months + ((Months == 1) ? " Mes" : " Meses");
            if (Months == 12) return "1 Año";
            var FechaFutura = DateTime.Now.AddMonths(Months).Date;
            var FechaActual = DateTime.Now.Date;
            var years = FechaFutura.Year - FechaActual.Year;
            years = FechaFutura.Month <= FechaActual.Month ? years - 1 : years;
            var meses = Months % 12;
            return years + ((years == 1) ? " Año" : " Años") + (meses > 0 ? (meses == 1 ? " " + meses + " Mes" : " " + meses + " Meses") : "");

        }

        public static event EventHandler<PromocionModelo>? PromoEliminarEvent;
        public static event EventHandler<ObservableCollection<PromocionModelo>>? PromosEvent;
        public static event EventHandler? TimeEvent;
    }
}
