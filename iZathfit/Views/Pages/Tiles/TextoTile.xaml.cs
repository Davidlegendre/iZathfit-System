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
    /// Lógica de interacción para TextoTile.xaml
    /// </summary>
    public partial class TextoTile : UserControl
    {
        PostsViewModel? _vm;
        public TextoTile()
        {
            InitializeComponent();
            _vm = DataContext as PostsViewModel;
            this.Loaded += TextoTile_Loaded;
        }

        private void TextoTile_Loaded(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            {
                _vm.CargarFonts();
            }
        }

        private void btnopen_Click(object sender, RoutedEventArgs e)
        {
            if(_vm != null)
            {
                _vm.VisiblePanelEdit = Visibility.Visible;
                _vm.ButtonOpenPanel = Visibility.Collapsed;
                Wpf.Ui.Animations.Transitions.ApplyTransition(cardPanelEdit, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 200);
            }
        }

        private void btnGoToStyles_Click(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            {
                _vm.VisibleEditFontStyle = Visibility.Visible;
                _vm.VisibleTextEdit = Visibility.Collapsed;
                Wpf.Ui.Animations.Transitions.ApplyTransition(panelEditFont, Wpf.Ui.Animations.TransitionType.SlideRight, 200);
            }
        }

        private void btnGoToTextEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            {
                _vm.VisibleEditFontStyle = Visibility.Collapsed;
                _vm.VisibleTextEdit = Visibility.Visible;
                Wpf.Ui.Animations.Transitions.ApplyTransition(panelTexto, Wpf.Ui.Animations.TransitionType.SlideLeft, 200);
            }
        }

        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            {
                _vm.VisiblePanelEdit = Visibility.Collapsed;
                _vm.VisibleTextEdit = Visibility.Visible;
                _vm.VisibleEditFontStyle = Visibility.Collapsed;
                _vm.ButtonOpenPanel = Visibility.Visible;
                Wpf.Ui.Animations.Transitions.ApplyTransition(cardPanelEdit, Wpf.Ui.Animations.TransitionType.SlideBottom, 200);
            }
        }
    }
}
