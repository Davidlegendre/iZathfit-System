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
    bool IsOnline = true;

    public void InitTimerHour()
    {
        if (TimeHour == null)
        {
            IsOnline = true;
            TimeHour = new Thread(() =>
            {
                while (IsOnline)
                {
                    if (TimeSystemEvent != null)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            TimeSystemEvent.Invoke(this, new HomeDataModel()
                            {
                                Fecha = DateTime.Now.ToLongDateString(),
                                Hora = DateTime.Now.ToString("hh:mm"),
                                Hora24 = DateTime.Now.Hour,
                                TypeHora = DateTime.Now.Hour > 12 ? TypeHora.PM : TypeHora.AM
                            });
                        });
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
        if (TimeHour != null)
        {
            IsOnline = false;
            TimeHour = null;
        }
    }

    public event EventHandler<HomeDataModel>? TimeSystemEvent;


}

