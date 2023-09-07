using Configuration;
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
        IGeneralConfiguration _config;
        public MenuUsuario()
        {
            InitializeComponent();
            _config = App.GetService<IGeneralConfiguration>();
        }

        private void prueba_click(object sender, RoutedEventArgs e)
        {
            _config.getuserSistema().Nombres = "Fulano de tal";
            App.GetService<HomePageVM>().DatoUsuario();
        }
    }
}
