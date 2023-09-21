using Configuration;
using iZathfit.ModelsComponents;
using iZathfit.ViewModels.Pages;
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

namespace iZathfit.Components
{
    /// <summary>
    /// Lógica de interacción para MenuUsuario.xaml
    /// </summary>
    public partial class MenuUsuario : UserControl
    {
        IGeneralConfiguration? _config;
        HomePageVM? _vm;
        public MenuUsuario()
        {
            InitializeComponent();
           
            _config = App.GetService<IGeneralConfiguration>();
            this.Loaded += MenuUsuario_Loaded;
        }

        private void MenuUsuario_Loaded(object sender, RoutedEventArgs e)
        {
            _vm = DataContext as HomePageVM;
        }

        private void listamenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = listamenu.SelectedItem as MenuUserItemsModel;
            if (item != null)
            {
                item.Comando?.Invoke();
                listamenu.SelectedIndex = -1;
            }
        }
    }
}
