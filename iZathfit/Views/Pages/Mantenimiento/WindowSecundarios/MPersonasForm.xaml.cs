using iZathfit.ViewModels.Pages;
using iZathfit.ViewModels.Windows;
using iZathfit.Views.Pages.Mantenimiento.WindowSecundarios.ViewModels;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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

namespace iZathfit.Views.Pages.Mantenimiento.WindowSecundarios
{
    /// <summary>
    /// Lógica de interacción para MPersonasForm.xaml
    /// </summary>
    public partial class MPersonasForm : UiWindow
    {
        MPersonasFormVM? _vm;
        PersonaModel? persona;
        bool resultdialog = false;
        ObservableCollection<PersonaModel> _personas;
        public MPersonasForm(ObservableCollection<PersonaModel> personaslista, PersonaModel? persona = null)
        {
            InitializeComponent();
            _vm = this.DataContext as MPersonasFormVM;
            this.persona = persona;
            _personas = personaslista;
            this.Loaded += MPersonasForm_Loaded;
            this.Closing += MPersonasForm_Closing;
        }

        private void MPersonasForm_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = resultdialog;
        }

        private async void MPersonasForm_Loaded(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
                if (await _vm.CargarDatos(this,persona) == false)
                    this.Close();
            TBTitulo.Title = persona == null ? "Agregar Persona" : "Modificar Persona";
            txtidentificacion.Focus();
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            var result = persona == null? await _vm!.GuardarPersona(this, _personas):
                await _vm!.ActualizarPersona(this, persona.IdPersona, _personas);
            if(result)
            {
                resultdialog = true;
                this.Close();
            }    
        }

        private void btnlimpiar_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            _vm.Limpiar();
            txtidentificacion.Focus();
        }

        private void btnAddOcupacion_click(object sender, RoutedEventArgs e)
        {
            var win = 
            new MOcupacionesForm(_vm.OcupacionList);
            win.Owner = this;
            win.ShowDialog();
            
        }
    }
}
