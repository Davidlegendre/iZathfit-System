using iZathfit.Views.Pages.Tiles.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iZathfit.Views.Pages.Tiles
{
    /// <summary>
    /// Lógica de interacción para CalendarSearchTotalesTile.xaml
    /// </summary>
    public partial class CalendarSearchTotalesTile : UserControl
    {
        CalendarSearchTotalesTileViewModel? _vm;
        public CalendarSearchTotalesTile()
        {
            InitializeComponent();
            _vm = DataContext as CalendarSearchTotalesTileViewModel;
            this.Loaded += CalendarSearchTotalesTile_Loaded;
        }

        private void CalendarSearchTotalesTile_Loaded(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            {
                dtPicker.DateSelect = DateTime.Now;
            }
        }

        private async void btnrefresh_Click(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            { 
               await _vm.GetlistByDate(dtPicker.DateSelect);
            }
        }
    }
}
