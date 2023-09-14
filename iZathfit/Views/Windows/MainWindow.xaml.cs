using Configuration;
using iZathfit.ViewModels.Windows;
using iZathfit.Views.Pages;
using Models;
using Models.DTOS;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Windows;

public partial class MainWindow : UiWindow
{
    MainWindowViewModel? ViewModel;
    localDialogService? localDialog;
    GlobalService? _global;
    LoginVM? loginVM;
    LoginPage? loginpage;
    IGeneralConfiguration? _config;
       
    public MainWindow()
    {      

        InitializeComponent();
        
        Watcher.Watch(this, BackgroundType.Mica, false, true);
        Accent.Apply(System.Windows.Media.Color.FromArgb(220, 255, 0, 0), Theme.GetAppTheme());
        
        this.loginVM = App.GetService<LoginPage>()?.DataContext as LoginVM;
        localDialog = App.GetService<localDialogService>();
        this.loginpage = App.GetService<LoginPage>();
        _config = App.GetService<IGeneralConfiguration>();
        _global = App.GetService<GlobalService>();
        ViewModel = this.DataContext as MainWindowViewModel;
        this.Loaded += MainWindow_Loaded;
        this.Closing += MainWindow_Closing;
        
    }

    private void LoginVM_UsuarioLogeado(object? sender, UsuarioSistema e)
    {
        
        NavigationView.Content = App.GetService<HomePage>();
        _config?.SetUserSistema(e);
        this.WindowState = WindowState.Maximized;
        this.ResizeMode = ResizeMode.CanResize;
        _global?.InitTimerHour();
        
        //ViewModel.ShowButtons = true;
    }

    private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        if (NavigationView.Content.ToString() != typeof(LoginPage).FullName 
            && localDialog?.ShowDialog(titulo: "Saliendo", mensaje: "Desea Salir?",
            aceptarbutton: "Si", cancelarButton: "No", owner: this, ShowCancelButton: true) == true)
        {
            e.Cancel = true;
            _config?.SetUserSistema(null);
            salirYLogin();
            this.WindowState = WindowState.Normal;
            this.ResizeMode = ResizeMode.NoResize;
            _global?.DisposeTimeHour();
            //ViewModel.ShowButtons = false;
        }
        else if (NavigationView.Content.ToString() != typeof(LoginPage).FullName)
        {
            _global?.DisposeTimeHour();
            e.Cancel = true;
        }
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        //NavigationView.IsPaneOpen= false;
        if (loginVM != null)
            loginVM.UsuarioLogeado += LoginVM_UsuarioLogeado;
    }

    void salirYLogin() {
        NavigationView.Content = loginpage;
    }
}
