﻿using iZathfit.Helpers;
using Models;
using Services.Ocupacion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Mantenimiento.WindowSecundarios.ViewModels
{
    public partial class MOcupacionFormVM : ObservableObject, IDisposable
    {
        IOcupacionService? _servicio;
        IExceptionHelperService? _helpexec;
        localDialogService? _dialog;
        public MOcupacionFormVM()
        {
            _servicio = App.GetService<IOcupacionService>();    
            _dialog = App.GetService<localDialogService>();
            _helpexec = App.GetService<IExceptionHelperService>();
        }

        [ObservableProperty]
        string? _ocupaciontxt = "";

        [ObservableProperty]
        Visibility _limpiarbtnVisible = Visibility.Visible;

        public void CargarDatos(Ocupacion? ocupacion)
        {
            if (ocupacion != null)
            {
                Ocupaciontxt = ocupacion.descripcion;
                LimpiarbtnVisible = Visibility.Collapsed;
            }
        }

        public async Task<bool> AgregarOcupacion(UiWindow win, List<Ocupacion> lista, ObservableCollection<Ocupacion>? listacollection = null)
        {
            if (_helpexec == null || _servicio == null) return false;
            if (!validar())
                return false;

            var result = await _helpexec.ExcepHandler(()=> _servicio.InsertOcupacion(new Ocupacion() { descripcion= Ocupaciontxt }),
                win);

            if(result != null)
            {
                _dialog?.ShowDialog("Ocupacion Insertada Correctamente");
                lista.Add(result);
                if (listacollection != null)
                    listacollection.Add(result);
            }

            return result != null;
        }

        public async Task<bool> UpdateOcupacion(UiWindow win, List<Ocupacion> lista, int idocupacion)
        {
            if (!validar())
                return false;
            var result = await _helpexec.ExcepHandler(() => _servicio.UpdateOcupacion(new Ocupacion() { IdOcupacion = idocupacion, 
                descripcion = Ocupaciontxt }), win);
            if (result != null)
            {
                _dialog?.ShowDialog("Ocupacion Actualizada Correctamente");
                lista.Remove(lista.First(x => x.IdOcupacion == result.IdOcupacion));
                lista.Insert(0, result);
            }
            return result != null;
        }

        public void Limpiar()
        {
            Ocupaciontxt = "";
        }

        bool validar() {
            if (string.IsNullOrWhiteSpace(Ocupaciontxt))
            {
                _dialog?.ShowDialog("Ocupacion no tiene valor");
                return false;
            }
            return true;
        }

        public void Dispose()
        {
           
        }
    }
}
