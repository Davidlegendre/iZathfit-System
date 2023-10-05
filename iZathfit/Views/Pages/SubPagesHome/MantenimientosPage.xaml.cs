using Configuration.GlobalHelpers;
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

namespace iZathfit.Views.Pages.SubPagesHome
{
    /// <summary>
    /// Lógica de interacción para MantenimientosPage.xaml
    /// </summary>
    public partial class MantenimientosPage : UserControl, IDisposable
    {
        IGlobalHelpers? _helpers;
        MantenimientosVM? _vm;
        public MantenimientosPage()
        {
            InitializeComponent();
            _helpers = App.GetService<IGlobalHelpers>();
            _vm = this.DataContext as MantenimientosVM;
            this.Loaded += MantenimientosPage_Loaded;
            this.SizeChanged += MantenimientosPage_SizeChanged;
        }

        private void MantenimientosPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(_vm != null && _helpers != null)
            {
                _vm.Columns = _helpers.ColumnsFromWidthWindow(Convert.ToInt32(this.ActualWidth));
            }
        }

        private void MantenimientosPage_Loaded(object sender, RoutedEventArgs e)
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(this, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 800);
        }

        private void ActionToCommand_Click(object sender, RoutedEventArgs e)
        {
            var button = (Wpf.Ui.Controls.CardAction)sender;
            var contexto = button.DataContext as MenuItemCardsModel;
            if (contexto != null && contexto.Comando != null)
            {
                Wpf.Ui.Animations.Transitions.ApplyTransition(contenidowin, Wpf.Ui.Animations.TransitionType.FadeIn, 200);
                contexto.Comando.Invoke();
            }
        }

        public void Dispose()
        {
            _vm?.Dispose();
            _vm = null;
        }
    }
}
