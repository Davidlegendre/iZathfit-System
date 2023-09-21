using Configuration.GlobalHelpers;
using Dapper;
using iZathfit.ViewModels.Pages.Negocio;
using iZathfit.Views.Pages.Mantenimiento.WindowSecundarios;
using iZathfit.Views.Pages.Negocio.Ventanas;
using Models;
using Models.DTOS;
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

namespace iZathfit.Views.Pages.Negocio
{
    /// <summary>
    /// Lógica de interacción para Contratos.xaml
    /// </summary>
    public partial class Contratos : UserControl
    {
        ContratosViewModel? _vm;
        IGlobalHelpers? _helpers;
        ObservableCollection<ContratoModel>? _copy = new ObservableCollection<ContratoModel>();
        public Contratos()
        {
            InitializeComponent();
            _helpers = App.GetService<IGlobalHelpers>();
            _vm = DataContext as ContratosViewModel;
            this.SizeChanged += Contratos_SizeChanged;
            this.Loaded += Contratos_Loaded;
        }

        private async void Contratos_Loaded(object sender, RoutedEventArgs e)
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(this, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 400);
            if (_vm == null) return;
            await _vm.Getdata();
            _copy = new ObservableCollection<ContratoModel>(_vm._contratoslist);
            _vm.Contratos = paginator.GetPaginationCollection(
                new ObservableCollection<ContratoModel>(_copy.Where(x => x.IsNotValid == btnVerNovalidos.IsChecked)));
        }

        private void Contratos_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_vm != null && _helpers != null)
            {
                _vm.Columns = _helpers.ColumnsFromWidthWindow(Convert.ToInt32(this.ActualWidth));
            }
        }

        private void txtSearch_keydown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_vm == null || _copy == null) return;
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                { _vm.Contratos = paginator.GetPaginationCollection(
                    new ObservableCollection<ContratoModel>(_copy.Where(x => x.IsNotValid == btnVerNovalidos.IsChecked))); }
                else
                {
                    _vm.Contratos = paginator.BuscarWithPagination(_copy.Where(x =>
                        x.PersonaNombres.ToLower().Contains(txtSearch.Text.ToLower())
                        || x.Identificacion.Contains(txtSearch.Text)
                         || x.NumeroContrato.Contains(txtSearch.Text)
                          && x.IsNotValid == btnVerNovalidos.IsChecked));
                }
            }
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_copy == null || _vm == null) return;
            if (new ContratosForm().ShowDialog() == true)
            {
                await _vm.Getdata();
                _copy = new ObservableCollection<ContratoModel>(_vm._contratoslist);
                _vm.Contratos = paginator.GetPaginationCollection(
                    new ObservableCollection<ContratoModel>(_copy.Where(x => x.IsNotValid == btnVerNovalidos.IsChecked)));
            }
        }

        private void paginator_ChangePageEvent(object sender, EventArgs e)
        {
            if (_vm != null && _copy != null)
            {
                _vm.Contratos = paginator.GetPaginationCollection(
                    new ObservableCollection<ContratoModel>(_copy.Where(x => x.IsNotValid == btnVerNovalidos.IsChecked)));
            }
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as ContratoModel;
            if (_vm != null && _copy != null && context != null)
            {
                if (new InvalidarContratoForm(context).ShowDialog() == true)
                {
                    await _vm.Getdata();
                    _copy = new ObservableCollection<ContratoModel>(_vm._contratoslist);
                    _vm.Contratos = paginator.GetPaginationCollection(
                        new ObservableCollection<ContratoModel>(_copy.Where(x => x.IsNotValid == btnVerNovalidos.IsChecked)));
                }
            }
        }

        private void btnVer_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as ContratoModel;
            if (context != null && _vm != null)
            {
                _vm.Ver(context);
            }
        }

        private async void btndelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as ContratoModel;
            if (context != null && _vm != null && _copy != null)
            {
                if (await _vm.EliminarContrato(context.IdContrato) == true)
                {
                    await _vm.Getdata();
                    _copy = new ObservableCollection<ContratoModel>(_vm._contratoslist);
                    _vm.Contratos = paginator.GetPaginationCollection(
                        new ObservableCollection<ContratoModel>(_copy.Where(x => x.IsNotValid == btnVerNovalidos.IsChecked)));
                }
            }
        }

        private async void btnValidar_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as ContratoModel;
            if (context != null && _vm != null && _copy != null)
            {
                if (await _vm.ValidarContrato(context.IdContrato) == true)
                {
                    await _vm.Getdata();
                    _copy = new ObservableCollection<ContratoModel>(_vm._contratoslist);
                    _vm.Contratos = paginator.GetPaginationCollection(
                        new ObservableCollection<ContratoModel>(_copy.Where(x => x.IsNotValid == btnVerNovalidos.IsChecked)));
                }
            }
        }

        private void btnVerNovalidos_Click(object sender, RoutedEventArgs e)
        {
            if (_vm != null && _copy != null)
            {
                _vm.Contratos = paginator.GetPaginationCollection(
                    new ObservableCollection<ContratoModel>(_copy.Where(x => x.IsNotValid == btnVerNovalidos.IsChecked)));
            }
        }
    }
}
