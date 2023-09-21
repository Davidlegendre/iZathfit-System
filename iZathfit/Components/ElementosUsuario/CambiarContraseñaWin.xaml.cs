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

namespace iZathfit.Components.ElementosUsuario
{
    /// <summary>
    /// Lógica de interacción para CambiarContraseñaWin.xaml
    /// </summary>
    public partial class CambiarContraseñaWin : UiWindow
    {
        DatosPerfilViewModel? _vm;
       
        public CambiarContraseñaWin()
        {
            InitializeComponent();
            _vm = DataContext as DatosPerfilViewModel;
            this.Loaded += CambiarContraseñaWin_Loaded;
        }

        private void CambiarContraseñaWin_Loaded(object sender, RoutedEventArgs e)
        {
            txtContraseñaVieja.Focus();
        }

        private async void btnGuardar_click(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            {
                if (await _vm.ChangePassword(this) == true) Close();
            }
        }

        private void btnclose_click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
