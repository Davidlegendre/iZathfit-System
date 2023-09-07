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
    public MainWindowViewModel ViewModel { get; }
    readonly localDialogService localDialog;
    GlobalService _global;
    Thickness maxthk => new Thickness(7);
    Thickness minThk => new Thickness(0);

    readonly LoginVM loginVM;
    readonly LoginPage loginpage;
    readonly HomePage _home;
    readonly IGeneralConfiguration _config;
    public MainWindow(
        MainWindowViewModel viewModel,
        localDialogService localDialogService,
        LoginVM loginVM,
        LoginPage loginpage,
        HomePage home,
        IGeneralConfiguration config,
        GlobalService global
    )
    {      

        InitializeComponent();
        
        Watcher.Watch(this, BackgroundType.Mica, false, true);
        Accent.Apply(System.Windows.Media.Color.FromArgb(220, 255, 0, 0), Theme.GetAppTheme());
        this.Loaded += MainWindow_Loaded;
        //this.StateChanged += (sender, obj) =>
        //{
        //    changemargin();
        //};

        this.loginVM = loginVM;
        localDialog = localDialogService;
        this.loginpage = loginpage;
        _config = config;
        _home = home;
        _global = global;
        ViewModel = viewModel;
        DataContext = this;

        this.Closing += MainWindow_Closing;
        loginVM.UsuarioLogeado += LoginVM_UsuarioLogeado;
        
       
        //navigationService.SetNavigationControl(NavigationView);
        ////snackbarService.SetSnackbarPresenter(SnackbarPresenter);
        //NavigationView.SetServiceProvider(serviceProvider);
    }

    private void LoginVM_UsuarioLogeado(object? sender, UsuarioSistema e)
    {
           
        NavigationView.Content = _home;
        _config.SetUserSistema(e);
        this.WindowState = WindowState.Maximized;
        this.ResizeMode = ResizeMode.CanResize;
        _global.InitTimerHour();
        
        //ViewModel.ShowButtons = true;
    }

    private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        if (NavigationView.Content.ToString() != typeof(LoginPage).FullName && localDialog.ShowDialog(new() { Title = "Saliendo", Message = "Desea Salir?", aceptarContent = "Si", cancelarContent = "No" }) == true)
        {
            e.Cancel = true;
            _config.SetUserSistema(null);
            salirYLogin();
            this.WindowState = WindowState.Normal;
            this.ResizeMode = ResizeMode.NoResize;
            _global.DisposeTimeHour();
            //ViewModel.ShowButtons = false;
        }
        else if (NavigationView.Content.ToString() != typeof(LoginPage).FullName)
        {
            _global.DisposeTimeHour();
            e.Cancel = true;
        }
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        //NavigationView.IsPaneOpen= false;
        changemargin();
    }

    void salirYLogin() {
        NavigationView.Content = loginpage;
    }

    void changemargin() {
        content.Margin = WindowState == WindowState.Maximized
                ? maxthk : minThk;
    }
}
