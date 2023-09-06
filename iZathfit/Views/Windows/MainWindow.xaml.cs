using iZathfit.ViewModels.Windows;
using iZathfit.Views.Pages;
using Models.Login;
using Wpf.Ui.Appearance;


namespace iZathfit.Views.Windows;

public partial class MainWindow : Window
{
    public MainWindowViewModel ViewModel { get; }
    readonly localDialogService localDialog;
    Thickness maxthk => new Thickness(7);
    Thickness minThk => new Thickness(0);

    readonly LoginVM loginVM;
    readonly LoginPage loginpage;
    readonly HomePage _home;
    public MainWindow(
        MainWindowViewModel viewModel,
        localDialogService localDialogService,
        LoginVM loginVM,
        LoginPage loginpage,
        HomePage home
    )
    {      

        InitializeComponent();
        Watcher.Watch(this, BackgroundType.Mica, true, true);
        this.Loaded += MainWindow_Loaded;
        this.StateChanged += (sender, obj) =>
        {
            changemargin();
        };

        this.loginVM = loginVM;
        localDialog = localDialogService;
        this.loginpage = loginpage;
        _home = home;

        ViewModel = viewModel;
        DataContext = this;

        this.Closing += MainWindow_Closing;
        loginVM.UsuarioLogeado += LoginVM_UsuarioLogeado;

       
        //navigationService.SetNavigationControl(NavigationView);
        ////snackbarService.SetSnackbarPresenter(SnackbarPresenter);
        //NavigationView.SetServiceProvider(serviceProvider);
    }

    private void LoginVM_UsuarioLogeado(object? sender, UserModel e)
    {
        localDialog.ShowDialog(new Models.ModelsCommons.DialogModel() { Title = "Login", Message = e.NombreCompleto, canShowCancelButton = false });
        NavigationView.Content = _home;
    }

    private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        if(NavigationView.Content.ToString() != typeof(LoginPage).FullName && localDialog.ShowDialog(new() { Title = "Saliendo", Message = "Desea Salir?", aceptarContent = "Si", cancelarContent = "No"}) == true)
        {
            e.Cancel = true;
            salirYLogin(); 
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
