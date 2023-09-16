﻿using iZathfit.Views.Pages.Negocio.Ventanas.ViewModels;
using Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Negocio.Ventanas
{
    /// <summary>
    /// Lógica de interacción para PromocionForm.xaml
    /// </summary>
    public partial class PromocionForm : UiWindow
    {
        PromocionModelo? _model;
        bool resultdiaglo = false;
        PromocionFormViewModel? _vm;
        public PromocionForm(PromocionModelo? modelo = null)
        {
            InitializeComponent();
            _model = modelo;
            _vm = DataContext as PromocionFormViewModel;
            this.Loaded += PromocionForm_Loaded;
            this.Closing += PromocionForm_Closing;
        }

        private void PromocionForm_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = resultdiaglo;
        }

        private async void PromocionForm_Loaded(object sender, RoutedEventArgs e)
        {
            tbtitle.Title = _model == null ? "Agregar" : "Modificar";
           
            if (_vm != null) if (!await _vm.GetData(this, _model)) Close();
            if (_model != null) { dpDuracion.DateSelect = _model.DuracionTiempo; txtpercent.Text = _model.DescuentoPercent.ToString(); }
            btnlimpiar.Visibility = _model == null ? Visibility.Visible : Visibility.Collapsed; 
        }

        private async void btnadd_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            _vm.SelectedDate = dpDuracion.DateSelect;
            _vm.PercentText = txtpercent.Text;
            var result = _model == null ? await _vm.Agregar(this) : await _vm.Modificar(this, _model.IdPromocion);
            if (result)
            {
                resultdiaglo = true;
                Close();
            }
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            resultdiaglo = false;
            Close();
        }

        private void btnlimpiar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tgactive_Checked(object sender, RoutedEventArgs e)
        {
            tgactive.Content = "Activada";
        }

        private void tgactive_Unchecked(object sender, RoutedEventArgs e)
        {
            tgactive.Content = "No esta Activada";
        }
    }
}
