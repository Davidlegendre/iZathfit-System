using Commons;
using Models.ModelsCommons;
using Models.ServiciodeModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Services;

public class GlobalService
{
    Thread? TimeHour;
    Thread? Promos;
    bool IsOnline = true;

    public void InitTimerHour()
    {
        var datatimehome = new HomeDataModel();
        if (TimeHour == null)
        {
            IsOnline = true;
            TimeHour = new Thread(() =>
            {
                while (IsOnline)
                {
                    if (TimeSystemEvent != null)
                    {
                        try
                        {
                            if (DateTime.Now.ToString("hh:mm") != datatimehome.Hora)
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    datatimehome.Fecha = DateTime.Now.ToLongDateString();
                                    datatimehome.Hora = DateTime.Now.ToString("hh:mm");
                                    datatimehome.Hora24 = DateTime.Now.Hour;
                                    datatimehome.TypeHora = DateTime.Now.Hour > 12 ? TypeHora.PM : TypeHora.AM;
                                    NotificadorServicesInModels.NotificarTiempoAModelos();
                                    TimeSystemEvent.Invoke(this, datatimehome);
                                });
                        }
                        catch
                        {
                        }
                        Thread.Sleep(1000);
                    }
                }
            });
            TimeHour.IsBackground = true;
            TimeHour.Start();
        }
    }

    public void DisposeTimeHour()
    {
        if (TimeHour != null || Promos != null)
        {
            IsOnline = false;
            TimeHour = null;
        }
    }

    public event EventHandler<HomeDataModel>? TimeSystemEvent;


}

