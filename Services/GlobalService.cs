using Commons;
using Models.ModelsCommons;
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
            Promos = new Thread(() => {
                while (IsOnline)
                {
                    if (PromosEvent != null)
                    {
                        try
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                PromosEvent.Invoke(this, null);
                            });
                        }
                        catch
                        {
                        }
                        Thread.Sleep(20000);
                    }
                }
            });
            Promos.IsBackground = true;
            Promos.Start();
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
            Promos = null;
        }
    }

    public event EventHandler<object?>? PromosEvent;
    public event EventHandler<HomeDataModel>? TimeSystemEvent;


}

