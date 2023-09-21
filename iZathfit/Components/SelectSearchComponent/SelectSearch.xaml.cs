using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.PortableExecutable;
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

namespace iZathfit.Components.SelectSearchComponent
{
    /// <summary>
    /// Lógica de interacción para SelectSearch.xaml
    /// </summary>
    public partial class SelectSearch : UserControl
    {
        SelectSearchViewModel? _vm;
        public SelectSearch()
        {
            InitializeComponent();
            _vm = DataContext as SelectSearchViewModel;
        }

        public void Limpiar() {
            txtbuscar.Clear();
            if (_vm != null)
            {
                _vm.SetNull(_vm.Personaselected);
            }
        }

        public PersonaModel? selectedPersona
        {
            get => _vm?.Personaselected; 
            set
            {
                if (_vm != null && value != null)
                {
                    _vm.BuscarFuncion(value.Identificacion);
                }
            }
        }


        public void SetCollectionToFind(ObservableCollection<PersonaModel> collection)
        {
            if (_vm == null) return;
            _vm.Personas = collection;
        }       

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (_vm != null)
                    _vm.BuscarFuncion(txtbuscar.Text);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
    }
}
