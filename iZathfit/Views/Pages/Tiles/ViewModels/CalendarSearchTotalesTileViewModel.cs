using Models.DTOS;
using Services.Rutina;
using Services.SaldoXPersona;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iZathfit.Views.Pages.Tiles.ViewModels
{
    public partial class CalendarSearchTotalesTileViewModel : ObservableObject
    {
        ISaldoXPersonaService? _saldoService;
        IRutinaService? _rutinaService;
        public CalendarSearchTotalesTileViewModel()
        {
            _saldoService = App.GetService<ISaldoXPersonaService>();
            _rutinaService = App.GetService<IRutinaService>();
        }

        [ObservableProperty]
        ObservableCollection<TotalesByDateModel> _totaleslist = new ObservableCollection<TotalesByDateModel>();

        [ObservableProperty]
        string? _TotalTotal = "";

        public async Task GetlistByDate(DateTime fecha) {
            if (_saldoService == null || _rutinaService == null) return;
            var rutinastotales = await _rutinaService.GetTotalesByDate(fecha);
            var pagos = await _saldoService.GetTotalesPagos(fecha.Date);
            Totaleslist.Clear();
            TotalTotal = "";
            if (pagos != null)
                Totaleslist = new ObservableCollection<TotalesByDateModel>(pagos);
            if (rutinastotales != null)
                rutinastotales.Where(x=>x.Total !=0).ToList().ForEach(x => Totaleslist.Insert(0,x));
            if (Totaleslist.Count() > 0)
                TotalTotal = "Suma Total: " + Totaleslist.Sum(x => x.Total).ToString("0.00") + " S/";            
        }
    }
}
