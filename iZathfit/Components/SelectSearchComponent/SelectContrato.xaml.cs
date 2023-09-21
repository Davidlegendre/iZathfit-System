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

namespace iZathfit.Components.SelectSearchComponent
{
    /// <summary>
    /// Lógica de interacción para SelectContrato.xaml
    /// </summary>
    public partial class SelectContrato : UserControl
    {
        SelectSearchViewModel? _vm;
        public SelectContrato()
        {
            InitializeComponent();
            _vm = DataContext as SelectSearchViewModel;
        }
        public ContratoModel? SelectedContrato { 
            get => _vm?.Contratoselected;
            set
            {
                if (_vm != null && value != null)
                {
                    _vm.BuscarContrato(value.NumeroContrato);
                }
            }
        }
        public void SetCollectionToFind(ObservableCollection<ContratoModel> collection)
        {
            if (_vm == null) return;
            _vm.Contratos = collection;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_vm != null)
                    _vm.BuscarContrato(txtbuscar.Text);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        public void Limpiar()
        {
            txtbuscar.Clear();
            if (_vm != null)
            {
                _vm.SetNull(_vm.Contratoselected);
            }
        }
    }
}
