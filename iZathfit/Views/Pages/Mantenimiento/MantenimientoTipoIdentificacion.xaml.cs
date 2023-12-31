﻿using Configuration.GlobalHelpers;
using iZathfit.ViewModels.Pages;
using iZathfit.Views.Pages.Mantenimiento.WindowSecundarios;
using iZathfit.Views.Windows;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iZathfit.Views.Pages.Mantenimiento
{
    /// <summary>
    /// Lógica de interacción para MantenimientoTipoIdentificacion.xaml
    /// </summary>
    public partial class MantenimientoTipoIdentificacion : UserControl, IDisposable
    {
        MantenimientoTipoIdentificacionVM? _vm;
        localDialogService? _dialog;
        ObservableCollection<TipoIdentificacionModel>? _copy;
        IGlobalHelpers? _helpers;
        public MantenimientoTipoIdentificacion()
        {
            InitializeComponent();
            _vm = this.DataContext as MantenimientoTipoIdentificacionVM;
            _dialog = App.GetService<localDialogService>();
            _helpers = App.GetService<IGlobalHelpers>();
            this.Loaded += MantenimientoTipoIdentificacion_Loaded;
            this.SizeChanged += MantenimientoTipoIdentificacion_SizeChanged;
        }

        private void MantenimientoTipoIdentificacion_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_vm != null && _helpers != null)
            {
                _vm.Columns = _helpers.ColumnsFromWidthWindow(Convert.ToInt32(this.ActualWidth));
            }
        }

        private async void MantenimientoTipoIdentificacion_Loaded(object sender, RoutedEventArgs e)
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(this, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 500);
            if (_vm == null) return;
            await _vm.Cargardatos(App.GetService<MainWindow>());
            
        }

        private void btnagregar_Click(object sender, RoutedEventArgs e)
        {
            if (_copy != null) { _vm.TipoIdentificacionlist = _copy; _copy = null; };
            new MTipoIdentifiacionForm(_vm.TipoIdentificacionlist).ShowDialog();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_vm == null) return;
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                { if (_copy != null) _vm.TipoIdentificacionlist = _copy; _copy = null; }
                else
                {
                    if (_copy == null) _copy = _vm.TipoIdentificacionlist;

                    _vm.TipoIdentificacionlist = new ObservableCollection<TipoIdentificacionModel>(_copy.Where(x =>
                        x.descripcion.ToLower().Contains(txtBuscar.Text.ToLower())));
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as Models.TipoIdentificacionModel;
            if (context == null) return;
            if (_copy != null) { _vm.TipoIdentificacionlist = _copy; _copy = null; };
            new MTipoIdentifiacionForm(_vm.TipoIdentificacionlist,context).ShowDialog();
        }

        private async void btndelete_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as Models.TipoIdentificacionModel;
            if (context == null) return;
            if (_dialog?.ShowDialog("Desea eliminar el Tipo de indentificacion: " + context.descripcion + "?", "Eliminando", true) == true)
            {
                await _vm.Eliminar(context, App.GetService<MainWindow>());
            }
        }

        public void Dispose()
        {
            _copy = null;
            _vm?.Dispose();
            _dialog = null;
            _helpers = null;
        }
    }
}
