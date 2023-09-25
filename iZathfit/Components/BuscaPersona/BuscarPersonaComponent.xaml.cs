using Models;
using Services.Persona;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace iZathfit.Components.BuscaPersona
{
    /// <summary>
    /// Lógica de interacción para BuscarPersonaComponent.xaml
    /// </summary>
    public partial class BuscarPersonaComponent : UserControl, INotifyPropertyChanged
    {
        IPersonaService? _personaService;
        public BuscarPersonaComponent()
        {
            InitializeComponent();
            DataContext = this;
            _personaService = App.GetService<IPersonaService>();
        }
        PersonaModel? _personaselected;
        public PersonaModel? PersonaSelected
        {
            get => _personaselected;
            set { 
                if(value != _personaselected)
                {
                    _personaselected = value;
                    NombrePersona = _personaselected == null ? "Persona no Seleccionada" : _personaselected.GetCompleteName;
                    IdentificacionTipo = _personaselected == null ? "Busca una Persona": _personaselected.TipoIdentAbreviado;
                    Identificacion = _personaselected?.Identificacion;
                    if (_personaselected == null)
                    {
                        txtBuscarPersona.Clear();
                        ResultadoBusqueda = null;
                    }
                    change(nameof(NombrePersona));
                    change(nameof(IdentificacionTipo));
                    change(nameof(Identificacion));
                    if (selectedChanged != null)
                        selectedChanged.Invoke(this, PersonaSelected);
                }
            }
        }

        string? _NombrePersona = "Persona no Seleccionada";
        string? _identificacionTipo = "Busca una Persona";
        string? _Identificacion;
        ObservableCollection<PersonaModel>? _resultadoBusqueda;

        public ObservableCollection<PersonaModel>? ResultadoBusqueda {
            get => _resultadoBusqueda;
            set {
                if (value != _resultadoBusqueda)
                { 
                    _resultadoBusqueda= value;
                    change(nameof(ResultadoBusqueda));
                }
            }
        }

        public string? NombrePersona
        {
            get => _NombrePersona;
            set {
                if (value != _NombrePersona)
                { 
                    _NombrePersona = value;
                }
            }
        }

        public string? Identificacion {
            get => _Identificacion;
            set {
                if (value != _Identificacion)
                {
                    _Identificacion= value;
                }
            }
        }

        public string? IdentificacionTipo {
            get => _identificacionTipo;
            set {
                if (value != _identificacionTipo)
                { 
                    _identificacionTipo= value;
                }
            }
        }
            
        private void btnbuscarpersona_Click(object sender, RoutedEventArgs e)
        {
            flyoutpanel.Show();
        }

        void change(string property)
        { 
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public event EventHandler<PersonaModel?>? selectedChanged;

        public event PropertyChangedEventHandler? PropertyChanged;

        private async void txtBuscarPersona_keydown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {                
                if (_personaService != null && !string.IsNullOrWhiteSpace(txtBuscarPersona.Text))
                {
                    txtBuscarPersona.IsEnabled = false;
                    var result = await _personaService.SearchPersonaByNameLastNameIdenfity(txtBuscarPersona.Text);
                    ResultadoBusqueda = result != null ? new ObservableCollection<PersonaModel>(result) : null;
                    txtBuscarPersona.IsEnabled = true;
                }
            }
        }
    }
}
