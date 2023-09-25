using Models;
using Models.DTOS;
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
    /// Lógica de interacción para DatosPerfinWindow.xaml
    /// </summary>
    public partial class DatosPerfinWindow : UiWindow, IDisposable
    {
        UsuarioSistema? _persona;
        public DatosPerfinWindow(UsuarioSistema? persona)
        {
            InitializeComponent();
            _persona = persona;
            this.Loaded += DatosPerfinWindow_Loaded;
            this.Closing += DatosPerfinWindow_Closing;
        }

        private void DatosPerfinWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            Dispose();
        }

        private void DatosPerfinWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
            datosperfil.PersonaModel = _persona;
        }

        Action _changeuseraction;

        public Action ChangeUserAction { get => _changeuseraction;
            set {
                _changeuseraction = value;
                datosperfil.ChangeUserAction = ChangeUserAction;
                datosperfil.Win = this;
            }
        }

        private void btnClose_click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void Dispose()
        {
            _persona = null;
            datosperfil.Dispose();
            datosperfil = null;
        }
    }
}
