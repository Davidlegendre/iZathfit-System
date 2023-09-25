using Configuration.GlobalHelpers;
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

namespace iZathfit.Views.Pages.SubPagesHome
{
    /// <summary>
    /// Lógica de interacción para ClienteDataView.xaml
    /// </summary>
    public partial class ClienteDataView : UserControl
    {
        IGlobalHelpers _globalhelpers;
        SubHomeViewmodel? _vm;
        public ClienteDataView(IGlobalHelpers globalHelpers)
        {
            InitializeComponent();
            _globalhelpers = globalHelpers;           
            this.Loaded += ClienteDataView_Loaded;
            
        }

        private void ClienteDataView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CambiarPaneles();
        }

        private void ClienteDataView_Loaded(object sender, RoutedEventArgs e)
        {
            _vm = DataContext as SubHomeViewmodel;
            _vm.ContratosObser = paginator.GetPaginationCollection(new ObservableCollection<ContratosModDataView>(_vm._contratosList));
            rbpagados.IsChecked = true;
            CambiarPaneles();
            listcontracts.SizeChanged += Listcontracts_SizeChanged;
            this.SizeChanged += ClienteDataView_SizeChanged;
        }

        private void Listcontracts_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_vm != null)
            {
               _vm.Columns = _globalhelpers.ColumnsFromWidthWindow(Convert.ToInt32(listcontracts.ActualWidth));
                
            }
        }

        void CambiarPaneles() {
            if (_vm != null)
            {
                if (this.ActualWidth > 740)
                {
                    _vm.Visibletextosbuttons = Visibility.Visible;
                    _vm.AligmentButtons = HorizontalAlignment.Left;
                }
                else if (this.ActualWidth <= 740)
                {
                    _vm.Visibletextosbuttons = Visibility.Collapsed;
                    _vm.AligmentButtons = HorizontalAlignment.Stretch;
                }
                if (this.ActualWidth > 680)
                {
                    _vm.DockPanels = Dock.Left;
                    _vm.Lastchild = true;
                }
                else if (this.ActualWidth <= 680)
                {
                    _vm.Lastchild = false;
                    _vm.DockPanels = Dock.Top;
                }
            }
        }
        

        private void filtros1radios_checked(object sender, RoutedEventArgs e)
        {
            var radio = sender as RadioButton;
            if (radio != null && _vm != null)
            {
                _vm.ContratosObser = paginator.GetPaginationCollection(_vm.Ordenar(Convert.ToInt32(radio.Tag)));
                radio.Background = radio.BorderBrush;
            }
        }

        private void filtros1radios_unchecked(object sender, RoutedEventArgs e)
        {
            var radio = sender as RadioButton;
            if (radio != null)
                radio.Background = Brushes.Transparent;
        }

        private void paginator_ChangePageEvent(object sender, EventArgs e)
        {
            if (_vm != null)
                _vm.ContratosObser = paginator.GetPaginationCollection(new ObservableCollection<ContratosModDataView>(_vm._contratosList));
        }

    }
}
