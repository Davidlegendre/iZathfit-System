﻿using Configuration.GlobalHelpers;
using iZathfit.Helpers;
using Models;
using Services.Ocupacion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.ViewModels.Pages
{
    public partial class MantenimientoOcupacionVM : ObservableObject, IDisposable
    {
        IOcupacionService? _servicio;
        IExceptionHelperService? _helperx;
        localDialogService? _dialog;
        public MantenimientoOcupacionVM()
        {
            _servicio = App.GetService<IOcupacionService>();
            _helperx = App.GetService<IExceptionHelperService>();
            _dialog = App.GetService<localDialogService>();
        }

        [ObservableProperty]
        ObservableCollection<Models.Ocupacion>? _ocupaciones;

        public List<Models.Ocupacion>? _listaocupacion = new List<Ocupacion>();

        [ObservableProperty]
        int _columns = 4;

        public async Task<bool> CargarDatos() {
            if (_helperx == null || _dialog == null || _servicio == null) return false;
            var ocups = await _servicio.GetOcupaciones();
            _listaocupacion.Clear();
            if (ocups == null || ocups.Count() == 0)
            {
                _dialog.ShowDialog("No hay Ocupaciones");
                return false;   
            }
            _listaocupacion = ocups;
            //Ocupaciones = new ObservableCollection<Models.Ocupacion>(ocups);
            return true;            
        }

        public async Task<bool> Eliminar(Ocupacion ocupacion, UiWindow win) {
            if (_helperx == null || _dialog == null || _servicio == null) return false;
            var result = await _helperx.ExcepHandler(() => _servicio.DeleteOcupacion(ocupacion.IdOcupacion), win);
            if (result == 0)
            {
                _dialog?.ShowDialog("Ocupacion no puede ser Eliminada");               
                return false;
            }

            _dialog?.ShowDialog("Ocupacion eliminada");
            _listaocupacion.Remove(_listaocupacion.First(x => x.IdOcupacion == ocupacion.IdOcupacion));
            return true;
        }

        public void Dispose()
        {
            Ocupaciones = null;
            _listaocupacion = null;
        }
    }
}
