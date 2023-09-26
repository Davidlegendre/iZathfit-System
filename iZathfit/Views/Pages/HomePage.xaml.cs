using iZathfit.Components;
using iZathfit.ModelsComponents;
using iZathfit.ViewModels.Pages;
using Services.Genero;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Styles.Controls;

namespace iZathfit.Views.Pages;

/// <summary>
/// Lógica de interacción para HomePage.xaml
/// </summary>
public partial class HomePage : UserControl
{
    HomePageVM? vm;
    localDialogService? _dialog;
   
    public HomePage()
    {
        
        InitializeComponent();
        this.Loaded += HomePage_Loaded;
        _dialog = App.GetService<localDialogService>();
        vm = this.DataContext as HomePageVM;
    }

    private void HomePage_Loaded(object sender, RoutedEventArgs e)
    {
        Wpf.Ui.Animations.Transitions.ApplyTransition(menu, Wpf.Ui.Animations.TransitionType.SlideLeft, 1000);
        Wpf.Ui.Animations.Transitions.ApplyTransition(btnmenuusuario, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 1000);
        if (vm != null)
            vm.CargarDatos();
    }

    private void btnmenuusuario_Click(object sender, RoutedEventArgs e)
    {
        if (!menuUsuario.IsOpen)
        {
            contentmenu.listamenu.SelectedIndex= -1;
            menuUsuario.Show();
        }
    }

    private void BtnItemMenu_Click(object sender, RoutedEventArgs e)
    {
        var button = (Wpf.Ui.Controls.Button)sender;
        var model = button.DataContext as MenuUserItemsModel;
        if (model != null && model.Comando != null)
        {
            model.Comando?.Invoke();
        }
    }

    private void protector_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (vm != null)
            vm.OpenMenuCommand.Execute(menu);
    }
}
