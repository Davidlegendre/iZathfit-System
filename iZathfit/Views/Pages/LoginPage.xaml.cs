using iZathfit.ViewModels.Windows;
using System.Windows.Controls;

namespace iZathfit.Views.Pages;

/// <summary>
/// Lógica de interacción para LoginPage.xaml
/// </summary>
public partial class LoginPage : UserControl
{
    readonly localDialogService localDialogService;
    LoginVM vm;
    public LoginPage(localDialogService localDialogService, LoginVM loginVM)
    {
        InitializeComponent();
        this.localDialogService = localDialogService;
        vm = loginVM;
        DataContext= vm;
        this.Loaded += LoginPage_Loaded;
    }

    private void LoginPage_Loaded(object sender, RoutedEventArgs e)
    {
        
       var ready = Wpf.Ui.Animations.Transitions.ApplyTransition(this, 
           Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 900);
        if (ready)
            vm.clean(txtuser, txtpass);
    }

    private void btnclean_Click(object sender, RoutedEventArgs e)
    {
        vm.clean(txtuser, txtpass);
    }

    private async void btnlogin_Click(object sender, RoutedEventArgs e)
    {
        await vm.verificarusuario(txtuser.Text, txtpass.Password);
    }

    private async void login_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if(e.Key == System.Windows.Input.Key.Enter)
            await vm.verificarusuario(txtuser.Text, txtpass.Password);
        else
        if (e.Key == System.Windows.Input.Key.Escape)
            vm.clean(txtuser, txtpass);
    }
}
