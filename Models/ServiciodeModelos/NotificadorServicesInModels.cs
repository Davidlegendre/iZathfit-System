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

        public static event EventHandler<PromocionModelo>? PromoEliminarEvent;
        public static event EventHandler<ObservableCollection<PromocionModelo>>? PromosEvent;
        public static event EventHandler? TimeEvent;
    }
}
