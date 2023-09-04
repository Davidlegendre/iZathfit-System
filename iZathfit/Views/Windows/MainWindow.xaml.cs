using iZathfit.ViewModels.Windows;
using Wpf.Ui.Appearance;


namespace iZathfit.Views.Windows;

public partial class MainWindow : Window
{
    public MainWindowViewModel ViewModel { get; }
    readonly localDialogService localDialog;
    Thickness maxthk => new Thickness(7);
    Thickness minThk => new Thickness(0);

    public MainWindow(
        MainWindowViewModel viewModel,
        INavigationService navigationService,
        localDialogService localDialogService

    )
    {
        
        localDialog= localDialogService;
        
        
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
        Watcher.Watch(this, BackgroundType.Mica, true, true);
        this.Loaded += MainWindow_Loaded;
        this.StateChanged += (sender, obj) =>
        {
            changemargin();
        };

        this.Closing += MainWindow_Closing;
       
        //navigationService.SetNavigationControl(NavigationView);
        ////snackbarService.SetSnackbarPresenter(SnackbarPresenter);
        //NavigationView.SetServiceProvider(serviceProvider);
    }

    private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        if(localDialog.ShowDialog(new() { Title = "Saliendo", Message = "Desea Salir?", aceptarContent = "Si", cancelarContent = "No"}))
        {
            e.Cancel = true;
            salirYLogin(); 
        }
        else
            e.Cancel = true;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        //NavigationView.IsPaneOpen= false;
        changemargin();
        salirYLogin();
        
    }

    void salirYLogin() {
        this.Hide();
        var login = new Login().ShowDialog();
        switch(login)
        {
            case true: this.Show(); break;
            case false: System.Windows.MessageBox.Show("Login Incorrecto"); break;
        };
    }

    void changemargin() {
        content.Margin = WindowState == WindowState.Maximized
                ? maxthk : minThk;
    }
}
