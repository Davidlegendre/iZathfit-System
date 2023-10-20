using iZathfit.Helpers;
using Models;
using Services.TipoIdentificacion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Mantenimiento.WindowSecundarios.ViewModels
{
    public partial class MTIpoIdentificacionVM : ObservableObject, IDisposable
    {
        IExceptionHelperService? _helpexcep;
        localDialogService? _dialog;
        ITipoIdentificacionService? _servicio;
        public MTIpoIdentificacionVM()
        {
            _helpexcep = App.GetService<IExceptionHelperService>();
            _dialog = App.GetService<localDialogService>();
            _servicio = App.GetService<ITipoIdentificacionService>();
        }

        [ObservableProperty]
        string? _tipoIdenttxt = "";

        [ObservableProperty]
        string? _abreviaturatxt = "";

        [ObservableProperty]
        Visibility _visibleLimpiar = Visibility.Visible;

        public void Cargar(TipoIdentificacionModel? tipo) { 
            if (tipo != null)
            {
                VisibleLimpiar = Visibility.Collapsed;
                TipoIdenttxt = tipo.descripcion;
                Abreviaturatxt = tipo.abreviado;

            }

        }

        public async Task<bool> Guardar(ObservableCollection<TipoIdentificacionModel> lista, UiWindow win) {
            
            if (_helpexcep == null || _dialog == null || _servicio == null) return false;
            if (!validar()) return false;
            var result = await _helpexcep.ExcepHandler(() => _servicio.InsertTipoIdentificacion(new TipoIdentificacionModel() { 
                descripcion = TipoIdenttxt, abreviado = Abreviaturatxt
            }), win);

            if (result == null)
            {
                _dialog.ShowDialog("Tipo de identificacion no se puede agregar");
                return false;
            }
            _dialog.ShowDialog("Tipo de identificacion agregada correctamente");
            lista.Add(result);
            return result != null;
        }


        public async Task<bool> Update(ObservableCollection<TipoIdentificacionModel> lista, UiWindow win, int idtipo)
        {
            if (_helpexcep == null || _dialog == null || _servicio == null) return false;
            if (!validar()) return false;
            var result = await _helpexcep.ExcepHandler(() => _servicio.UpdateTipoIdentificacion(new TipoIdentificacionModel()
            {
                IdTipoIdentity = idtipo,
                abreviado= Abreviaturatxt,
                descripcion = TipoIdenttxt
            }), win);
            if (result == null)
            {
                _dialog.ShowDialog("Tipo de identificacion no se pudo actualizar");
                return false;
            }
            lista.Remove(lista.First(x => x.IdTipoIdentity == idtipo));
            lista.Insert(0, result);
            _dialog.ShowDialog("Tipo de identificacion actualizada");
            return result != null;

        }

        public void limpiar()
        {
            Abreviaturatxt = "";
            TipoIdenttxt = "";
        }


        bool validar()
        {
            if (string.IsNullOrWhiteSpace(TipoIdenttxt))
            {
                _dialog?.ShowDialog("Tipo Identificaion no tiene valor");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Abreviaturatxt))
            {
                _dialog?.ShowDialog("Abreviatura no tiene valor");
                return false;
            }
            return true;
        }

        public void Dispose()
        {
          
        }
    }
}
