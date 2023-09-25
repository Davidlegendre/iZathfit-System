using iZathfit.Views.Pages.Negocio.Ventanas.ViewModels;
using Models;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Negocio.Ventanas
{
    /// <summary>
    /// Lógica de interacción para SaldoXPersonaForm.xaml
    /// </summary>
    public partial class SaldoXPersonaForm : UiWindow, IDisposable
    {
        SaldoXPersonaFormViewModel? _vm;
        bool resultdialog = false;
        public SaldoXPersonaForm()
        {
            InitializeComponent();
            _vm = DataContext as SaldoXPersonaFormViewModel;
            this.Loaded += SaldoXPersonaForm_Loaded;
            this.Closing += SaldoXPersonaForm_Closing;
        }

        private void SaldoXPersonaForm_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            Dispose();
            DialogResult = resultdialog;
        }

        private async void SaldoXPersonaForm_Loaded(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            {
                if (await _vm.Getdata(this) == false) Close();
                
            }
        }

        private async void btnadd_Click(object sender, RoutedEventArgs e)
        {
            if (_vm != null) { 
            //{
            //    _vm.Contratoselected = searchContrato.SelectedContrato;
            //    _vm.Personaselected = searchidentificacion.selectedPersona;
                if (await _vm.Guardar(this) == true)
                {
                    resultdialog = true;
                    Close();
                }
            }
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            resultdialog = false;
            Close();
        }

        private void btnlimpiar_Click(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            {
                _vm.Personaselected = null;
                _vm.Tipopagoselected = null;
                _vm.Contratoselected = null;
                _vm.Cantidadpago = "";
                Cbuscarpersona.PersonaSelected = null;
            }
        }

        private async void BuscarPersonaComponent_selectedChanged(object sender, PersonaModel e)
        {
            if (_vm != null)
            {
                _vm.Contratoselected = null;
                _vm.ContratolistByperson = null;
                _vm.Personaselected = e;
                if(e != null)
                    await _vm.GetContratosByPerson(e.IdPersona, this);
            }
        }

        public void Dispose()
        {
            _vm?.Dispose();
        }
    }
}
