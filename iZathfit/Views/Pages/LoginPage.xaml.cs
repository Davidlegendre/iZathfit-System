using iZathfit.ViewModels.Windows;
using System.Net.NetworkInformation;
using System.Windows.Controls;

namespace iZathfit.Views.Pages;

/// <summary>
/// Lógica de interacción para LoginPage.xaml
/// </summary>
public partial class LoginPage : UserControl, IDisposable
{
    localDialogService? localDialogService;
    
    LoginVM? vm;
    public LoginPage()
    {
        InitializeComponent();
        this.localDialogService = App.GetService<localDialogService>();
        vm = this.DataContext as LoginVM;
        this.Loaded += LoginPage_Loaded;
    }

    private void LoginPage_Loaded(object sender, RoutedEventArgs e)
    {
        if (vm != null)
        {
            var ready = Wpf.Ui.Animations.Transitions.ApplyTransition(this,
                Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 900);
            if (ready)
                vm.clean(txtuser, txtpass);
        }
    }

    private void btnclean_Click(object sender, RoutedEventArgs e)
    {
        if (vm != null)
            vm.clean(txtuser, txtpass);
    }

    private async void btnlogin_Click(object sender, RoutedEventArgs e)
    {
        if (vm != null)
            await vm.verificarusuario(txtuser.Text, txtpass.Password);
    }

    private async void login_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (vm != null)
            if (forgotpage.Visibility != Visibility.Visible)
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                    await vm.verificarusuario(txtuser.Text, txtpass.Password);
                else
                if (e.Key == System.Windows.Input.Key.Escape)
                    vm.clean(txtuser, txtpass);
            }
    }

    private void linkForgot_Click(object sender, RoutedEventArgs e)
    {
        if (!NetworkInterface.GetIsNetworkAvailable())
        {
            localDialogService?.ShowDialog(
                mensaje: "No podemos iniciar este proceso, ya que no tiene internet, conectese primero",
                titulo: "Ups No hay Internet");
            return;
        }
        forgotpage.Visibility = Visibility.Visible;
        loginpage.Visibility = Visibility.Collapsed;
        btnvolver.Visibility = Visibility.Visible;
        Wpf.Ui.Animations.Transitions.ApplyTransition(forgotpage, Wpf.Ui.Animations.TransitionType.SlideRight, 200);
    }

    private void btnvolver_Click(object sender, RoutedEventArgs e)
    {
        forgotpage.Visibility = Visibility.Collapsed;
        btnvolver.Visibility = Visibility.Collapsed;
        loginpage.Visibility = Visibility.Visible;
        limpiarforgot();
        Wpf.Ui.Animations.Transitions.ApplyTransition(loginpage, Wpf.Ui.Animations.TransitionType.SlideLeft, 200);
    }

    void limpiarforgot() {
        if (vm == null) return;
        forgotpage.txtemailuser.Clear();
        forgotpage.txtcodsended.Clear();
        forgotpage.txtnewpassword.Clear();
        vm.EnableCodeTXT = false;
        vm.EnableNewPasswordTxt = false;
        vm.EnableEmailtxt = true;
        vm.CodeEmail = "";
        vm.GuidPersonForgot = null;
    }

    public void Dispose()
    {

        localDialogService = null;
        vm?.Dispose();
    }
}
